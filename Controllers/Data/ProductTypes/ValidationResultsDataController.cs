using System.Collections.Generic;

using Interfaces.Controllers.Stash;
using Interfaces.Controllers.Records;
using Interfaces.Controllers.Collection;
using Interfaces.Controllers.Logs;

using Interfaces.Delegates.Convert;

using Attributes;

using Models.ProductTypes;

namespace Controllers.Data.ProductTypes
{
    public class ValidationResultsDataController : DataController<ValidationResults>
    {
        [Dependencies(
            "Controllers.Stash.ProductTypes.ValidationResultsStashController,Controllers",
            "Delegates.Convert.ProductTypes.ConvertValidationResultsToIndexDelegate,Delegates",
            "Controllers.Records.ProductTypes.ValidationResultsRecordsIndexController,Controllers",
            "Controllers.Collection.CollectionController,Controllers",
            "Controllers.Logs.ResponseLogController,Controllers")]
        public ValidationResultsDataController(
            IStashController<List<ValidationResults>> validationResultsStashController,
            IConvertDelegate<ValidationResults, long> convertValidationResultsToIndexDelegate,
            IRecordsController<long> validationResultsRecordsIndexController,
            ICollectionController collectionController,
            IActionLogController actionLogController) :
            base(
                validationResultsStashController,
                convertValidationResultsToIndexDelegate,
                validationResultsRecordsIndexController,
                collectionController,
                actionLogController)
        {
            // ...
        }
    }
}