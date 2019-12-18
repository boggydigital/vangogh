﻿using Interfaces.Delegates.Convert;
using Interfaces.Delegates.GetValue;
using Interfaces.Delegates.Itemize;

using Interfaces.Controllers.Data;
using Interfaces.Controllers.Logs;

using Attributes;

using GOG.Interfaces.Delegates.GetDeserialized;
using GOG.Models;

namespace GOG.Activities.Update.ProductTypes
{
    public class UpdateGameProductDataByProductsActivity :
        UpdateDetailProductsByMasterProductsActivity<GameProductData, Product>
    {
        [Dependencies(
            "Delegates.GetValue.Uri.ProductTypes.GetGameProductDataUpdateUriDelegate,Delegates",
            "GOG.Delegates.Convert.UpdateIdentity.ConvertProductToGameProductDataUpdateIdentityDelegate,GOG.Delegates",
            "GOG.Controllers.Data.ProductTypes.GameProductDataDataController,GOG.Controllers",
            "GOG.Delegates.Itemize.MasterDetail.ItemizeAllProductsGameProductDataGapsAsyncDelegatepsDelegate,GOG.Delegates",
            "GOG.Delegates.GetDeserialized.ProductTypes.GetDeserializedGameProductDataAsyncDelegate,GOG.Delegates",
            "Controllers.Logs.ResponseLogController,Controllers")]
        public UpdateGameProductDataByProductsActivity(
            IGetValueDelegate<string> getGameProductDataUpdateUriDelegate,
            IConvertDelegate<Product, string> convertProductToGameProductDataUpdateIdentityDelegate,
            IDataController<GameProductData> gameProductDataDataController,
            IItemizeAllAsyncDelegate<Product> itemizeAllProductsGameProductDataGapsAsyncDelegate,
            IGetDeserializedAsyncDelegate<GameProductData> getDeserializedGameProductDataAsyncDelegate,
            IResponseLogController responseLogController):
            base(
                getGameProductDataUpdateUriDelegate,
                convertProductToGameProductDataUpdateIdentityDelegate,
                gameProductDataDataController,
                itemizeAllProductsGameProductDataGapsAsyncDelegate,
                getDeserializedGameProductDataAsyncDelegate,
                responseLogController,
                null)
                {
                    // ...
                }
    }
}
