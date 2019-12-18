using System.Collections.Generic;

using Interfaces.Delegates.GetPath;
using Interfaces.Controllers.SerializedStorage;
using Interfaces.Controllers.Logs;

using Attributes;

using Controllers.Stash;

using Models.Dependencies;
using GOG.Models;

namespace GOG.Controllers.Stash.ProductTypes
{
    public class ProductsStashController : StashController<List<Product>>
    {
        [Dependencies(
            "Delegates.GetPath.ProductTypes.GetProductsPathDelegate,Delegates",
            Dependencies.DefaultSerializedStorageController,
            "Controllers.Logs.ResponseLogController,Controllers")]
        public ProductsStashController(
            IGetPathDelegate getProductsPathDelegate,
            ISerializedStorageController serializedStorageController,
            IActionLogController actionLogController) :
            base(
                getProductsPathDelegate,
                serializedStorageController,
                actionLogController)
        {
            // ...
        }
    }
}