﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Delegates.GetDirectory;
using Interfaces.Delegates.Itemize;
using Attributes;
using GOG.Models;
using Delegates.GetDirectory.ProductTypes;

namespace GOG.Delegates.Itemize
{
    public class ItemizeGameDetailsDirectoriesAsyncDelegate : IItemizeAsyncDelegate<GameDetails, string>
    {
        private readonly IItemizeAsyncDelegate<GameDetails, string> itemizeGameDetailsManualUrlsAsyncDelegate;
        private readonly IGetDirectoryDelegate getDirectoryDelegate;

        [Dependencies(
            typeof(GOG.Delegates.Itemize.ItemizeGameDetailsManualUrlsAsyncDelegate),
            typeof(GetProductFilesDirectoryDelegate))]
        public ItemizeGameDetailsDirectoriesAsyncDelegate(
            IItemizeAsyncDelegate<GameDetails, string> itemizeGameDetailsManualUrlsAsyncDelegate,
            IGetDirectoryDelegate getDirectoryDelegate)
        {
            this.itemizeGameDetailsManualUrlsAsyncDelegate = itemizeGameDetailsManualUrlsAsyncDelegate;
            this.getDirectoryDelegate = getDirectoryDelegate;
        }

        public async Task<IEnumerable<string>> ItemizeAsync(GameDetails gameDetails)
        {
            var gameDetailsDirectories = new List<string>();

            foreach (var manualUrl in await itemizeGameDetailsManualUrlsAsyncDelegate.ItemizeAsync(gameDetails))
            {
                var directory = getDirectoryDelegate.GetDirectory(manualUrl);

                if (!gameDetailsDirectories.Contains(directory))
                    gameDetailsDirectories.Add(directory);
            }

            return gameDetailsDirectories;
        }
    }
}