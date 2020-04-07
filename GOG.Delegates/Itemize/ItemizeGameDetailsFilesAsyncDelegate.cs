﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

using Interfaces.Delegates.GetDirectory;
using Interfaces.Delegates.GetFilename;
using Interfaces.Delegates.GetPath;
using Interfaces.Delegates.Itemize;


using Interfaces.Delegates.Activities;

using Interfaces.Routing;


using Attributes;

using GOG.Models;
using System;

namespace GOG.Delegates.Itemize
{
    public class ItemizeGameDetailsFilesAsyncDelegate : IItemizeAsyncDelegate<GameDetails, string>
    {
        readonly IItemizeAsyncDelegate<GameDetails, string> itemizeGameDetailsManualUrlsDelegate;
        readonly IGetPathDelegate getPathDelegate;
        readonly IRoutingController routingController;
        private readonly IStartDelegate startDelegate;
        private readonly ICompleteDelegate completeDelegate;

		[Dependencies(
			"GOG.Delegates.Itemize.ItemizeGameDetailsManualUrlsAsyncDelegate,GOG.Delegates",
			"Controllers.Routing.RoutingController,Controllers",
			"Delegates.GetPath.Json.GetGameDetailsFilesPathDelegate,Delegates",
            "Delegates.Activities.StartDelegate,Delegates",
            "Delegates.Activities.CompleteDelegate,Delegates")]
        public ItemizeGameDetailsFilesAsyncDelegate(
            IItemizeAsyncDelegate<GameDetails, string> itemizeGameDetailsManualUrlsDelegate,
            IRoutingController routingController,
            IGetPathDelegate getPathDelegate,
            IStartDelegate startDelegate,
            ICompleteDelegate completeDelegate)
        {
            this.itemizeGameDetailsManualUrlsDelegate = itemizeGameDetailsManualUrlsDelegate;
            this.getPathDelegate = getPathDelegate;
            this.routingController = routingController;
            this.startDelegate = startDelegate;
            this.completeDelegate = completeDelegate;
        }

        public async Task<IEnumerable<string>> ItemizeAsync(GameDetails gameDetails)
        {
            startDelegate.Start("Enumerate game details files");

            var gameDetailsFiles = new List<string>();

            var gameDetailsManualUrls = await itemizeGameDetailsManualUrlsDelegate.ItemizeAsync(gameDetails);
            var gameDetailsManualUrlsCount = gameDetailsManualUrls.Count();
            var gameDetailsResolvedUris = await routingController.TraceRoutesAsync(
                gameDetails.Id, 
                gameDetailsManualUrls);

            // that means that routes information is incomplete and 
            // it's not possible to map manualUrls to resolvedUrls
            if (gameDetailsManualUrlsCount != gameDetailsResolvedUris.Count)
            {
                completeDelegate.Complete();
                throw new ArgumentException($"Product {gameDetails.Id} resolvedUris count doesn't match manualUrls count");
            }

            for (var ii = 0; ii < gameDetailsResolvedUris.Count; ii++)
            {
                // since we don't download all languages and operating systems 
                // we don't have routes for each and every gameDetails uri
                // however the ones we have should represent expected files for that product
                if (string.IsNullOrEmpty(gameDetailsResolvedUris[ii]))
                    continue;

                //var localFileUri = Path.Combine(
                //    getDirectoryDelegate.GetDirectory(gameDetailsManualUrls.ElementAt(ii)),
                //getFilenameDelegate.GetFilename(gameDetailsResolvedUris[ii]));

                var localFilePath = getPathDelegate.GetPath(
                    gameDetailsManualUrls.ElementAt(ii),
                    gameDetailsResolvedUris[ii]);

                gameDetailsFiles.Add(localFilePath);
            }

            completeDelegate.Complete();

            if (gameDetailsManualUrlsCount != gameDetailsFiles.Count)
                throw new ArgumentException($"Product {gameDetails.Id} files count doesn't match manualUrls count");

            return gameDetailsFiles;
        }
    }
}
