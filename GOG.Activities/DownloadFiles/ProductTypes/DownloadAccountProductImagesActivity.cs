﻿using Interfaces.Controllers.Data;
using Interfaces.Controllers.Logs;

using Models.ProductTypes;

using GOG.Interfaces.Delegates.DownloadProductFile;

using Attributes;

using GOG.Models;

namespace GOG.Activities.DownloadProductFiles.ProductTypes
{
    public class DownloadAccountProductImagesActivity : DownloadFilesActivity<AccountProductImage>
    {
		[Dependencies(
			"Controllers.Data.ProductTypes.ProductDownloadsDataController,Controllers",
			"GOG.Delegates.DownloadProductFile.DownloadProductImageAsyncDelegate,GOG.Delegates",
			"Controllers.Logs.ActionLogController,Controllers")]        
        public DownloadAccountProductImagesActivity(
            IDataController<ProductDownloads> productDownloadsDataController,
            IDownloadProductFileAsyncDelegate downloadProductFileAsyncDelegate,
            IActionLogController actionLogController) :
            base(
                productDownloadsDataController,
                downloadProductFileAsyncDelegate,
                actionLogController)
        {
            // ...
        }
    }
}
