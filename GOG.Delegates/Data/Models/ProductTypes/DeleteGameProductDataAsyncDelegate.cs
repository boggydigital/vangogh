using System.Collections.Generic;
using Delegates.Data.Models;
using GOG.Models;
using Interfaces.Delegates.Confirm;
using Interfaces.Delegates.Convert;
using Interfaces.Delegates.Data;
using Attributes;

namespace GOG.Delegates.Data.Models.ProductTypes
{
    public class DeleteGameProductDataAsyncDelegate: DeleteAsyncDelegate<GameProductData>
    {
        [Dependencies(
            typeof(GOG.Delegates.Data.Storage.ProductTypes.GetListGameProductDataDataFromPathAsyncDelegate),
            typeof(GOG.Delegates.Convert.ProductTypes.ConvertGameProductDataToIndexDelegate),
            typeof(GOG.Delegates.Confirm.ProductTypes.ConfirmGameProductDataContainIdAsyncDelegate))]
        public DeleteGameProductDataAsyncDelegate(
            IGetDataAsyncDelegate<List<GameProductData>, string> getDataCollectionAsyncDelegate, 
            IConvertDelegate<GameProductData, long> convertProductToIndexDelegate, 
            IConfirmAsyncDelegate<long> confirmContainsAsyncDelegate) : 
            base(
                getDataCollectionAsyncDelegate, 
                convertProductToIndexDelegate, 
                confirmContainsAsyncDelegate)
        {
            // ...
        }
    }
}