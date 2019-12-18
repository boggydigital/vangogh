using System.Collections.Generic;

using Interfaces.Controllers.Stash;
using Interfaces.Controllers.Collection;
using Interfaces.Controllers.Logs;

using Interfaces.Delegates.Convert;

using Attributes;

using Models.ProductTypes;

namespace Controllers.Data.Records
{
    public class ProductsRecordsDataController : DataController<ProductRecords>
    {
        [Dependencies(
            "Controllers.Stash.Records.ProductsRecordsStashController,Controllers",
            "Delegates.Convert.Records.ConvertProductRecordsToIndexDelegate,Delegates",
            "Controllers.Collection.CollectionController,Controllers",
            "Controllers.Logs.ResponseLogController,Controllers")]
        public ProductsRecordsDataController(
            IStashController<List<ProductRecords>> productsRecordsStashController,
            IConvertDelegate<ProductRecords, long> convertProductRecordsToIndexDelegate,
            ICollectionController collectionController,
            IActionLogController actionLogController) :
            base(
                productsRecordsStashController,
                convertProductRecordsToIndexDelegate,
                null,
                collectionController,
                actionLogController)
        {
            // ...
        }
    }
}