using System.Collections.Generic;
using Attributes;
using Interfaces.Delegates.Convert;
using Interfaces.Delegates.Data;
using Models.ProductTypes;

namespace Delegates.Data.Storage.Records
{
    public class GetListProductRecordsDataAsyncDelegate : GetJSONDataAsyncDelegate<List<ProductRecords>>
    {
        [Dependencies(
            typeof(Delegates.Data.Storage.GetStringDataAsyncDelegate),
            typeof(Delegates.Convert.JSON.Records.ConvertJSONToListProductRecordsDelegate))]
        public GetListProductRecordsDataAsyncDelegate(
            IGetDataAsyncDelegate<string, string> getStringDataAsyncDelegate,
            IConvertDelegate<string, List<ProductRecords>> convertJSONToListProductRecordsDelegate) :
            base(
                getStringDataAsyncDelegate,
                convertJSONToListProductRecordsDelegate)
        {
            // ...
        }
    }
}