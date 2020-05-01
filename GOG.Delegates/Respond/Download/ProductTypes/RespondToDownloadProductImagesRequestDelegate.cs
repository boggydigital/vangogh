﻿using Interfaces.Delegates.Data;
using Interfaces.Delegates.Itemize;
using Interfaces.Delegates.Activities;
using Models.ProductTypes;
using GOG.Interfaces.Delegates.DownloadProductFile;
using Attributes;
using Delegates.Activities;
using Delegates.Data.Models.ProductTypes;
using Delegates.Itemize.ProductTypes;

namespace GOG.Delegates.Respond.Download.ProductTypes
{
    [RespondsToRequests(Method = "download", Collection = "productimages")]
    public class RespondToDownloadProductImagesRequestDelegate : RespondToDownloadRequestDelegate<ProductImage>
    {
        [Dependencies(
            typeof(ItemizeAllProductDownloadsAsyncDelegate),
            typeof(UpdateProductDownloadsAsyncDelegate),
            typeof(DeleteProductDownloadsAsyncDelegate),
            typeof(GOG.Delegates.DownloadProductFile.DownloadProductImageAsyncDelegate),
            typeof(StartDelegate),
            typeof(SetProgressDelegate),
            typeof(CompleteDelegate))]
        public RespondToDownloadProductImagesRequestDelegate(
            IItemizeAllAsyncDelegate<ProductDownloads> itemizeAllProductDownloadsAsyncDelegate,
            IUpdateAsyncDelegate<ProductDownloads> updateProductDownloadsAsyncDelegate,
            IDeleteAsyncDelegate<ProductDownloads> deleteProductDownloadsAsyncDelegate,
            IDownloadProductFileAsyncDelegate downloadProductFileAsyncDelegate,
            IStartDelegate startDelegate,
            ISetProgressDelegate setProgressDelegate,
            ICompleteDelegate completeDelegate) :
            base(
                itemizeAllProductDownloadsAsyncDelegate,
                updateProductDownloadsAsyncDelegate,
                deleteProductDownloadsAsyncDelegate,
                downloadProductFileAsyncDelegate,
                startDelegate,
                setProgressDelegate,
                completeDelegate)
        {
            // ...
        }
    }
}