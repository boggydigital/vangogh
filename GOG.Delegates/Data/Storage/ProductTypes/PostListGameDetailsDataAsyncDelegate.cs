using System.Collections.Generic;
using Attributes;
using Delegates.Data.Storage;
using Interfaces.Delegates.Convert;
using Interfaces.Delegates.Data;
using GOG.Models;
using Delegates.Data.Storage;

namespace GOG.Delegates.Data.Storage.ProductTypes
{
    public class PostListGameDetailsDataAsyncDelegate : PostJSONDataAsyncDelegate<List<GameDetails>>
    {
        [Dependencies(
            typeof(PostStringDataAsyncDelegate),
            typeof(GOG.Delegates.Convert.JSON.ProductTypes.ConvertListGameDetailsToJSONDelegate))]
        public PostListGameDetailsDataAsyncDelegate(
            IPostDataAsyncDelegate<string> postStringDataAsyncDelegate,
            IConvertDelegate<List<GameDetails>, string> convertListGameDetailsToJSONDelegate) :
            base(
                postStringDataAsyncDelegate,
                convertListGameDetailsToJSONDelegate)
        {
            // ...
        }
    }
}