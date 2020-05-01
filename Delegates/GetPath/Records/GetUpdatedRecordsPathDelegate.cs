using Interfaces.Delegates.GetDirectory;
using Interfaces.Delegates.GetFilename;
using Attributes;
using Delegates.GetDirectory.ProductTypes;
using Delegates.GetFilename.ProductTypes;

namespace Delegates.GetPath.Records
{
    public class GetUpdatedRecordsPathDelegate : GetPathDelegate
    {
        [Dependencies(
            typeof(Delegates.GetDirectory.ProductTypes.GetRecordsDirectoryDelegate),
            typeof(Delegates.GetFilename.ProductTypes.GetUpdatedFilenameDelegate))]
        public GetUpdatedRecordsPathDelegate(
            IGetDirectoryDelegate getRecordsDirectoryDelegate,
            IGetFilenameDelegate getUpdatedFilenameDelegate) :
            base(
                getRecordsDirectoryDelegate,
                getUpdatedFilenameDelegate)
        {
            // ...
        }
    }
}