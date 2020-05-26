﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Attributes;
using GOG.Delegates.Itemizations.Throttling;
using SecretSauce.Delegates.Collections.Interfaces;
using SecretSauce.Delegates.Collections.System;
using SecretSauce.Delegates.Itemizations.Interfaces;
using SecretSauce.Delegates.Throttling;
using SecretSauce.Delegates.Throttling.Interfaces;

namespace GOG.Delegates.Throttling.Network
{
    public class ThrottleGOGRequestRateAsyncDelegate : IThrottleAsyncDelegate<string>
    {
        private const int requestIntervalSeconds = 30;
        private const int passthroughCount = 100; // don't throttle first N requests
        private readonly IFindDelegate<string> findStringDelegate;
        private readonly IItemizeAllDelegate<string> itemizeRateContraindesUris;
        private readonly Dictionary<string, DateTime> lastRequestToUriPrefix;
        private readonly IThrottleAsyncDelegate<int> throttleExecutionAsyncDelegate;
        private int rateLimitRequestsCount;

        [Dependencies(
            typeof(ThrottleAsyncDelegate),
            typeof(FindStringDelegate),
            typeof(ItemizeAllRateConstrainedUrisDelegate))]
        public ThrottleGOGRequestRateAsyncDelegate(
            IThrottleAsyncDelegate<int> throttleExecutionAsyncDelegate,
            IFindDelegate<string> findStringDelegate,
            IItemizeAllDelegate<string> itemizeRateContraindesUris)
        {
            this.throttleExecutionAsyncDelegate = throttleExecutionAsyncDelegate;
            this.findStringDelegate = findStringDelegate;
            lastRequestToUriPrefix = new Dictionary<string, DateTime>();
            rateLimitRequestsCount = 0;

            this.itemizeRateContraindesUris = itemizeRateContraindesUris;

            if (this.itemizeRateContraindesUris != null)
                foreach (var uri in this.itemizeRateContraindesUris.ItemizeAll())
                    lastRequestToUriPrefix.Add(
                        uri,
                        DateTime.UtcNow - TimeSpan.FromSeconds(requestIntervalSeconds));
        }

        public async Task ThrottleAsync(string uri)
        {
            var prefix = findStringDelegate.Find(itemizeRateContraindesUris.ItemizeAll(), uri.StartsWith);
            if (string.IsNullOrEmpty(prefix)) return;

            // don't limit rate for the first N requests, even if they match rate limit prefix
            if (++rateLimitRequestsCount <= passthroughCount) return;

            var now = DateTime.UtcNow;
            var elapsed = (int) (now - lastRequestToUriPrefix[prefix]).TotalSeconds;
            if (elapsed < requestIntervalSeconds)
                await throttleExecutionAsyncDelegate.ThrottleAsync(requestIntervalSeconds - elapsed);

            lastRequestToUriPrefix[prefix] = now;
        }
    }
}