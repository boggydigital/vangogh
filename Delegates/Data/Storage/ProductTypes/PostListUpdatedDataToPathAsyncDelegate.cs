using System.Collections.Generic;
using Attributes;
using Interfaces.Delegates.Data;
using Interfaces.Delegates.GetPath;

namespace Delegates.Data.Storage.ProductTypes
{
    public class PostListUpdatedDataToPathAsyncDelegate : PostJSONDataToPathAsyncDelegate<List<long>>
    {
        [Dependencies(
            typeof(Delegates.Data.Storage.ProductTypes.PostListUpdatedDataAsyncDelegate),
            typeof(Delegates.GetPath.ProductTypes.GetUpdatedPathDelegate))]
        public PostListUpdatedDataToPathAsyncDelegate(
            IPostDataAsyncDelegate<List<long>> postListUpdatedDataAsyncDelegate,
            IGetPathDelegate getUpdatedPathDelegate) :
            base(
                postListUpdatedDataAsyncDelegate,
                getUpdatedPathDelegate)
        {
            // ...
        }
    }
}