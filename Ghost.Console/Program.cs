﻿#region Using

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Delegates.Convert;
using Delegates.GetDirectory;
using Delegates.GetFilename;
using Delegates.GetPath;
using Delegates.Format.Numbers;
using Delegates.Format.Uri;
using Delegates.Format.Text;
using Delegates.Format.Status;
using Delegates.Confirm;
using Delegates.GetQueryParameters;
using Delegates.Recycle;
using Delegates.Correct;
using Delegates.Constrain;
using Delegates.Itemize;
using Delegates.Replace;
using Delegates.EnumerateIds;
using Delegates.Download;
using Delegates.Hash;

using Controllers.Stream;
using Controllers.Storage;
using Controllers.File;
using Controllers.Directory;
using Controllers.Uri;
using Controllers.Network;
using Controllers.Language;
using Controllers.Serialization;
using Controllers.Extraction;
using Controllers.Collection;
using Controllers.Console;
using Controllers.Cookies;
using Controllers.Validation;
using Controllers.ValidationResult;
using Controllers.Stash;
using Controllers.Index;
using Controllers.Data;
using Controllers.SerializedStorage;
using Controllers.Presentation;
using Controllers.Routing;
using Controllers.Status;
using Controllers.Hash;
using Controllers.Template;
using Controllers.ViewModel;
using Controllers.ViewUpdates;
using Controllers.ActivityContext;
using Controllers.InputOutput;
using Controllers.Records;

using Interfaces.Activity;
using Interfaces.Extraction;
using Interfaces.ActivityDefinitions;
using Interfaces.ContextDefinitions;
using Interfaces.Status;

using GOG.Models;

using GOG.Delegates.GetPageResults;
using GOG.Delegates.FillGaps;
using GOG.Delegates.GetDownloadSources;
using GOG.Delegates.GetUpdateIdentity;
using GOG.Delegates.DownloadProductFile;
using GOG.Delegates.GetImageUri;
using GOG.Delegates.UpdateScreenshots;
using GOG.Delegates.GetDeserialized;
using GOG.Delegates.Itemize;
using GOG.Delegates.GetUpdateUri;
using GOG.Delegates.Format;
using GOG.Delegates.RequestPage;
using GOG.Delegates.Confirm;

using GOG.Controllers.Authorization;

using GOG.Activities.Help;
using GOG.Activities.CorrectSettings;
using GOG.Activities.Authorize;
using GOG.Activities.UpdateData;
using GOG.Activities.UpdateDownloads;
using GOG.Activities.DownloadProductFiles;
using GOG.Activities.Repair;
using GOG.Activities.Cleanup;
using GOG.Activities.Validate;
using GOG.Activities.Report;
using GOG.Activities.List;

using Models.ProductRoutes;
using Models.ProductScreenshots;
using Models.ProductDownloads;
using Models.ValidationResult;
using Models.Status;
using Models.QueryParameters;
using Models.Directories;
using Models.Filenames;
using Models.ActivityContext;
using Models.Settings;
using Models.Template;
using Models.ProductRecords;

#endregion

namespace Ghost.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region Delegates.GetDirectory

            var getEmptyDirectoryDelegate = new GetRelativeDirectoryDelegate(string.Empty);

            var getTemplatesDirectoryDelegate = new GetRelativeDirectoryDelegate(Directories.Templates);

            var getDataDirectoryDelegate = new GetRelativeDirectoryDelegate(Directories.Data, getEmptyDirectoryDelegate);

            var getAccountProductsDirectoryDelegate = new GetRelativeDirectoryDelegate(DataDirectories.AccountProducts, getDataDirectoryDelegate);
            var getApiProductsDirectoryDelegate = new GetRelativeDirectoryDelegate(DataDirectories.ApiProducts, getDataDirectoryDelegate);
            var getGameDetailsDirectoryDelegate = new GetRelativeDirectoryDelegate(DataDirectories.GameDetails, getDataDirectoryDelegate);
            var getGameProductDataDirectoryDelegate = new GetRelativeDirectoryDelegate(DataDirectories.GameProductData, getDataDirectoryDelegate);
            var getProductsDirectoryDelegate = new GetRelativeDirectoryDelegate(DataDirectories.Products, getDataDirectoryDelegate);
            var getProductDownloadsDirectoryDelegate = new GetRelativeDirectoryDelegate(DataDirectories.ProductDownloads, getDataDirectoryDelegate);
            var getProductRoutesDirectoryDelegate = new GetRelativeDirectoryDelegate(DataDirectories.ProductRoutes, getDataDirectoryDelegate);
            var getProductScreenshotsDirectoryDelegate = new GetRelativeDirectoryDelegate(DataDirectories.ProductScreenshots, getDataDirectoryDelegate);
            var getValidationResultsDirectoryDelegate = new GetRelativeDirectoryDelegate(DataDirectories.ValidationResults, getDataDirectoryDelegate);

            var getRecycleBinDirectoryDelegate = new GetRelativeDirectoryDelegate(Directories.RecycleBin, getDataDirectoryDelegate);
            var getImagesDirectoryDelegate = new GetRelativeDirectoryDelegate(Directories.Images, getDataDirectoryDelegate);
            var getReportDirectoryDelegate = new GetRelativeDirectoryDelegate(Directories.Reports, getDataDirectoryDelegate);
            var getValidationDirectoryDelegate = new GetRelativeDirectoryDelegate(Directories.Md5, getDataDirectoryDelegate);
            var getProductFilesBaseDirectoryDelegate = new GetRelativeDirectoryDelegate(Directories.ProductFiles, getDataDirectoryDelegate);
            var getScreenshotsDirectoryDelegate = new GetRelativeDirectoryDelegate(Directories.Screenshots, getDataDirectoryDelegate);

            var getRecordsDirectoryDelegate = new GetRelativeDirectoryDelegate(Directories.Records, getDataDirectoryDelegate);

            var getProductRecordsDirectoryDelegate = new GetRelativeDirectoryDelegate(DataDirectories.Products, getRecordsDirectoryDelegate);
            var getAccountProductRecordsDirectoryDelegate = new GetRelativeDirectoryDelegate(DataDirectories.AccountProducts, getRecordsDirectoryDelegate);
            var getGameDetailsRecordsDirectoryDelegate = new GetRelativeDirectoryDelegate(DataDirectories.GameDetails, getRecordsDirectoryDelegate);

            var getProductFilesDirectoryDelegate = new GetUriDirectoryDelegate(getProductFilesBaseDirectoryDelegate);

            #endregion

            #region Delegates.GetFilename

            var getJsonFilenameDelegate = new GetJsonFilenameDelegate();
            var getStoredHashesFilenameDelegate = new GetFixedFilenameDelegate(Filenames.Hashes, getJsonFilenameDelegate);

            var getAppTemplateFilenameDelegate = new GetFixedFilenameDelegate(Filenames.AppTemplates, getJsonFilenameDelegate);
            var gerReportTemplateFilenameDelegate = new GetFixedFilenameDelegate(Filenames.ReportTemplates, getJsonFilenameDelegate);
            var getCookiesFilenameDelegate = new GetFixedFilenameDelegate(Filenames.Cookies, getJsonFilenameDelegate);
            var getSettingsFilenameDelegate = new GetFixedFilenameDelegate(Filenames.Settings, getJsonFilenameDelegate);

            var getIndexFilenameDelegate = new GetFixedFilenameDelegate(Filenames.Index, getJsonFilenameDelegate);

            var getWishlistedFilenameDelegate = new GetFixedFilenameDelegate(Filenames.Wishlisted, getJsonFilenameDelegate);
            var getUpdatedFilenameDelegate = new GetFixedFilenameDelegate(Filenames.Updated, getJsonFilenameDelegate);

            var getUriFilenameDelegate = new GetUriFilenameDelegate();
            var getReportFilenameDelegate = new GetReportFilenameDelegate();
            var getValidationFilenameDelegate = new GetValidationFilenameDelegate();

            #endregion

            #region Delegates.GetPath

            var getStoredHashesPathDelegate = new GetPathDelegate(
                getEmptyDirectoryDelegate,
                getStoredHashesFilenameDelegate);

            var getAppTemplatePathDelegate = new GetPathDelegate(
                getTemplatesDirectoryDelegate,
                getAppTemplateFilenameDelegate);

            var getReportTemplatePathDelegate = new GetPathDelegate(
                getTemplatesDirectoryDelegate,
                gerReportTemplateFilenameDelegate);

            var getCookiePathDelegate = new GetPathDelegate(
                getEmptyDirectoryDelegate,
                getCookiesFilenameDelegate);

            var getSettingsPathDelegate = new GetPathDelegate(
                getEmptyDirectoryDelegate,
                getSettingsFilenameDelegate);

            var getProductsIndexPathDelegate = new GetPathDelegate(
                getProductsDirectoryDelegate,
                getIndexFilenameDelegate);

            var getAccountProductsIndexPathDelegate = new GetPathDelegate(
                getAccountProductsDirectoryDelegate,
                getIndexFilenameDelegate);

            var getGameDetailsIndexPathDelegate = new GetPathDelegate(
                getGameDetailsDirectoryDelegate,
                getIndexFilenameDelegate);

            var getGameProductDataIndexPathDelegate = new GetPathDelegate(
                getGameProductDataDirectoryDelegate,
                getIndexFilenameDelegate);

            var getApiProductsIndexPathDelegate = new GetPathDelegate(
                getApiProductsDirectoryDelegate,
                getIndexFilenameDelegate);

            var getProductScreenshotsIndexPathDelegate = new GetPathDelegate(
                getProductScreenshotsDirectoryDelegate,
                getIndexFilenameDelegate);

            var getProductDownloadsIndexPathDelegate = new GetPathDelegate(
                getProductDownloadsDirectoryDelegate,
                getIndexFilenameDelegate);

            var getProductRoutesIndexPathDelegate = new GetPathDelegate(
                getProductRoutesDirectoryDelegate,
                getIndexFilenameDelegate);

            var getValidationResultsIndexPathDelegate = new GetPathDelegate(
                getValidationResultsDirectoryDelegate,
                getIndexFilenameDelegate);

            var getWishlistedPathDelegate = new GetPathDelegate(
                getDataDirectoryDelegate,
                getWishlistedFilenameDelegate);

            var getUpdatedPathDelegate = new GetPathDelegate(
                getDataDirectoryDelegate,
                getUpdatedFilenameDelegate);

            var getProductsPathDelegate = new GetPathDelegate(
                getProductsDirectoryDelegate,
                getJsonFilenameDelegate);

            var getAccountProductsPathDelegate = new GetPathDelegate(
                getAccountProductsDirectoryDelegate,
                getJsonFilenameDelegate);

            var getGameDetailsPathDelegate = new GetPathDelegate(
                getGameDetailsDirectoryDelegate,
                getJsonFilenameDelegate);

            var getGameProductDataPathDelegate = new GetPathDelegate(
                getGameProductDataDirectoryDelegate,
                getJsonFilenameDelegate);

            var getApiProductPathDelegate = new GetPathDelegate(
                getApiProductsDirectoryDelegate,
                getJsonFilenameDelegate);

            var getScreenshotsPathDelegate = new GetPathDelegate(
                getProductScreenshotsDirectoryDelegate,
                getJsonFilenameDelegate);

            var getProductDownloadsPathDelegate = new GetPathDelegate(
                getProductDownloadsDirectoryDelegate,
                getJsonFilenameDelegate);

            var getProductRoutesPathDelegate = new GetPathDelegate(
                getProductRoutesDirectoryDelegate,
                getJsonFilenameDelegate);

            var getValidationResultsPathDelegate = new GetPathDelegate(
                getValidationResultsDirectoryDelegate,
                getJsonFilenameDelegate);

            var getGameDetailsFilesPathDelegate = new GetPathDelegate(
                getProductFilesDirectoryDelegate,
                getUriFilenameDelegate);

            var getValidationPathDelegate = new GetPathDelegate(
                getValidationDirectoryDelegate,
                getValidationFilenameDelegate);

            var getProductRecordsIndexPathDelegate = new GetPathDelegate(
                getProductRecordsDirectoryDelegate,
                getIndexFilenameDelegate);

            var getProductRecordsPathDelegate = new GetPathDelegate(
                getProductRecordsDirectoryDelegate,
                getJsonFilenameDelegate);

            var getAccountProductRecordsIndexPathDelegate = new GetPathDelegate(
                getAccountProductRecordsDirectoryDelegate,
                getIndexFilenameDelegate);

            var getAccountProductRecordsPathDelegate = new GetPathDelegate(
                getAccountProductRecordsDirectoryDelegate,
                getJsonFilenameDelegate);

            var getGameDetailsRecordsIndexPathDelegate = new GetPathDelegate(
                getGameDetailsRecordsDirectoryDelegate,
                getIndexFilenameDelegate);

            var getGameDetailsRecordsPathDelegate = new GetPathDelegate(
                getGameDetailsRecordsDirectoryDelegate,
                getJsonFilenameDelegate);

            #endregion

            var statusController = new StatusController();

            var streamController = new StreamController();
            var fileController = new FileController();
            var directoryController = new DirectoryController();

            var storageController = new StorageController(
                streamController,
                fileController);
            var transactionalStorageController = new TransactionalStorageController(
                storageController,
                fileController);
            var serializationController = new JSONStringController();

            var convertBytesToStringDelegate = new ConvertBytesToStringDelegate();
            var getBytesMd5HashAsyncDelegate = new GetBytesMd5HashAsyncDelegate(convertBytesToStringDelegate);
            var convertStringToBytesDelegate = new ConvertStringToBytesDelegate();
            var getStringMd5HashAsyncDelegate = new GetStringMd5HashAsyncDelegate(
                convertStringToBytesDelegate, 
                getBytesMd5HashAsyncDelegate);

            #region Controllers.Stash

            var storedHashesStashController = new StashController<Dictionary<string, string>>(
                getStoredHashesPathDelegate,
                serializationController,
                storageController,
                statusController);

            var appTemplateStashController = new StashController<List<Template>>(
                getAppTemplatePathDelegate,
                serializationController,
                storageController,
                statusController);

            var reportTemplateStashController = new StashController<List<Template>>(
                getReportTemplatePathDelegate,
                serializationController,
                storageController,
                statusController);

            var cookieStashController = new StashController<Dictionary<string, string>>(
                getCookiePathDelegate,
                serializationController,
                storageController,
                statusController);

            var settingsStashController = new StashController<Settings>(
                getSettingsPathDelegate,
                serializationController,
                storageController,
                statusController);

            var productsIndexStashController = new StashController<List<long>>(
                getProductsIndexPathDelegate,
                serializationController,
                storageController,
                statusController);

            var productRecordsIndexStashController = new StashController<List<long>>(
                getProductRecordsIndexPathDelegate,
                serializationController,
                storageController,
                statusController);

            var accountProductsIndexStashController = new StashController<List<long>>(
                getAccountProductsIndexPathDelegate,
                serializationController,
                storageController,
                statusController);

            var accountProductRecordsIndexStashController = new StashController<List<long>>(
                getAccountProductRecordsIndexPathDelegate,
                serializationController,
                storageController,
                statusController);

            var gameDetailsIndexStashController = new StashController<List<long>>(
                getGameDetailsIndexPathDelegate,
                serializationController,
                storageController,
                statusController);

            var gameProductDataIndexStashController = new StashController<List<long>>(
                getGameProductDataIndexPathDelegate,
                serializationController,
                storageController,
                statusController);

            var apiProductsIndexStashController = new StashController<List<long>>(
                getApiProductsIndexPathDelegate,
                serializationController,
                storageController,
                statusController);

            var productScreenshotsIndexStashController = new StashController<List<long>>(
                getProductScreenshotsIndexPathDelegate,
                serializationController,
                storageController,
                statusController);

            var productDownloadsIndexStashController = new StashController<List<long>>(
                getProductDownloadsIndexPathDelegate,
                serializationController,
                storageController,
                statusController);

            var productRoutesIndexStashController = new StashController<List<long>>(
                getProductRoutesIndexPathDelegate,
                serializationController,
                storageController,
                statusController);

            var validationResultsIndexStashController = new StashController<List<long>>(
                getValidationResultsIndexPathDelegate,
                serializationController,
                storageController,
                statusController);

            var wishlistedStashController = new StashController<List<long>>(
                getWishlistedPathDelegate,
                serializationController,
                storageController,
                statusController);

            var updatedStashController = new StashController<List<long>>(
                getUpdatedPathDelegate,
                serializationController,
                storageController,
                statusController);

            #endregion

            var precomputedHashController = new StoredHashController(storedHashesStashController);

            var consoleController = new ConsoleController();
            var formatTextToFitConsoleWindowDelegate = new FormatTextToFitConsoleWindowDelegate(consoleController);

            var consoleInputOutputController = new ConsoleInputOutputController(
                formatTextToFitConsoleWindowDelegate,
                consoleController);

            var formatBytesDelegate = new FormatBytesDelegate();
            var formatSecondsDelegate = new FormatSecondsDelegate();

            var serializedTransactionalStorageController = new SerializedStorageController(
                precomputedHashController,
                transactionalStorageController,
                getStringMd5HashAsyncDelegate,
                serializationController,
                statusController);

            var collectionController = new CollectionController();

            var itemizeStatusChildrenDelegate = new ItemizeStatusChildrenDelegate();

            var statusTreeToEnumerableController = new ConvertTreeToEnumerableDelegate<IStatus>(itemizeStatusChildrenDelegate);

            var applicationStatus = new Status() { Title = "This ghost is a kind one." };

            var appTemplateController = new TemplateController(
                "status",
                appTemplateStashController,
                collectionController);


            var reportTemplateController = new TemplateController(
                "status",
                reportTemplateStashController,
                collectionController);

            var formatRemainingTimeAtSpeedDelegate = new FormatRemainingTimeAtSpeedDelegate();

            var statusAppViewModelDelegate = new StatusAppViewModelDelegate(
                formatRemainingTimeAtSpeedDelegate,
                formatBytesDelegate,
                formatSecondsDelegate);

            var statusReportViewModelDelegate = new StatusReportViewModelDelegate(
                formatBytesDelegate,
                formatSecondsDelegate);

            var getStatusViewUpdateDelegate = new GetStatusViewUpdateDelegate(
                applicationStatus,
                appTemplateController,
                statusAppViewModelDelegate,
                statusTreeToEnumerableController);

            var consoleNotifyStatusViewUpdateController = new NotifyStatusViewUpdateController(
                getStatusViewUpdateDelegate,
                consoleInputOutputController);

            // TODO: Implement a better way
            // add notification handler to drive console view updates
            statusController.NotifyStatusChangedAsync += consoleNotifyStatusViewUpdateController.NotifyViewUpdateOutputOnRefreshAsync;

            var constrainExecutionAsyncDelegate = new ConstrainExecutionAsyncDelegate(
                statusController,
                formatSecondsDelegate);

            var constrainRequestRateAsyncDelegate = new ConstrainRequestRateAsyncDelegate(
                constrainExecutionAsyncDelegate,
                collectionController,
                statusController,
                new string[] {
                    Models.Uris.Uris.Paths.Account.GameDetails, // gameDetails requests
                    Models.Uris.Uris.Paths.ProductFiles.ManualUrlDownlink, // manualUrls from gameDetails requests
                    Models.Uris.Uris.Paths.ProductFiles.ManualUrlCDNSecure, // resolved manualUrls and validation files requests
                    Models.Uris.Uris.Roots.Api // API entries
                });

            var uriController = new UriController();

            var cookieSerializationController = new CookieSerializationController();

            var cookiesController = new CookiesController(
                cookieStashController,
                cookieSerializationController,
                statusController);

            var networkController = new NetworkController(
                cookiesController,
                uriController,
                constrainRequestRateAsyncDelegate);

            var downloadFromResponseAsyncDelegate = new DownloadFromResponseAsyncDelegate(
                networkController,
                streamController,
                fileController,
                statusController);

            var downloadFromUriAsyncDelegate = new DownloadFromUriAsyncDelegate(
                networkController,
                downloadFromResponseAsyncDelegate,
                statusController);

            var requestPageAsyncDelegate = new RequestPageAsyncDelegate(
                networkController);

            var languageController = new LanguageController();

            var loginIdExtractionController = new LoginIdExtractionController();
            var loginUsernameExtractionController = new LoginUsernameExtractionController();
            var loginUnderscoreTokenExtractionController = new LoginTokenExtractionController();
            var secondStepAuthenticationUnderscoreTokenExtractionController = new SecondStepAuthenticationTokenExtractionController();

            var gogDataExtractionController = new GOGDataExtractionController();
            var screenshotExtractionController = new ScreenshotExtractionController();

            var formatImagesUriDelegate = new FormatImagesUriDelegate();
            var formatScreenshotsUriDelegate = new FormatScreenshotsUriDelegate();

            var recycleDelegate = new RecycleDelegate(
                getRecycleBinDirectoryDelegate,
                fileController,
                directoryController);

            #region Delegates.Convert

            var convertProductToIndexDelegate = new ConvertProductCoreToIndexDelegate<Product>();
            var convertAccountProductToIndexDelegate = new ConvertProductCoreToIndexDelegate<AccountProduct>();
            var convertGameDetailsToIndexDelegate = new ConvertProductCoreToIndexDelegate<GameDetails>();
            var convertGameProductDataToIndexDelegate = new ConvertProductCoreToIndexDelegate<GameProductData>();
            var convertApiProductToIndexDelegate = new ConvertProductCoreToIndexDelegate<ApiProduct>();
            var convertProductScreenshotsToIndexDelegate = new ConvertProductCoreToIndexDelegate<ProductScreenshots>();
            var convertProductDownloadsToIndexDelegate = new ConvertProductCoreToIndexDelegate<ProductDownloads>();
            var convertProductRoutesToIndexDelegate = new ConvertProductCoreToIndexDelegate<ProductRoutes>();
            var convertValidationResultToIndexDelegate = new ConvertProductCoreToIndexDelegate<ValidationResult>();
            var convertProductRecordsToIndexDelegate = new ConvertProductCoreToIndexDelegate<ProductRecords>();

            #endregion

            #region Data Controllers

            // Data controllers for products, game details, game product data, etc.

            var correctSettingsCollectionsAsyncDelegate = new CorrectSettingsCollectionsAsyncDelegate(statusController);
            var correctSettingsDownloadsLanguagesAsyncDelegate = new CorrectSettingsDownloadsLanguagesAsyncDelegate(languageController);
            var correctSettingsDownloadsOperatingSystemsAsyncDelegate = new CorrectSettingsDownloadsOperatingSystemsAsyncDelegate();
            var correctSettingsDirectoriesAsyncDelegate = new CorrectSettingsDirectoriesAsyncDelegate();

            var correctSettingsActivity = new CorrectSettingsActivity(
                settingsStashController,
                statusController,
                correctSettingsCollectionsAsyncDelegate,
                correctSettingsDownloadsLanguagesAsyncDelegate,
                correctSettingsDownloadsOperatingSystemsAsyncDelegate,
                correctSettingsDirectoriesAsyncDelegate);

            var productRecordsIndexController = new IndexController<long>(
                productRecordsIndexStashController,
                collectionController,
                null, // records index controller doesn't need records tracking
                statusController);

            var productRecordsDataController = new DataController<ProductRecords>(
                productRecordsIndexController,
                serializedTransactionalStorageController,
                convertProductRecordsToIndexDelegate,
                collectionController,
                getProductRecordsPathDelegate,
                recycleDelegate,
                null, // records data controller doesn't need records tracking
                statusController);

            var productsRecordsController = new RecordsController(
                productRecordsDataController,
                statusController);

            var productsIndexController = new IndexController<long>(
                productsIndexStashController,
                collectionController,
                productsRecordsController,
                statusController);

            var accountProductRecordsIndexController = new IndexController<long>(
                accountProductRecordsIndexStashController,
                collectionController,
                null, // records index controller doesn't need records tracking
                statusController);

            var accountProductRecordsDataController = new DataController<ProductRecords>(
                accountProductRecordsIndexController,
                serializedTransactionalStorageController,
                convertProductRecordsToIndexDelegate,
                collectionController,
                getAccountProductRecordsPathDelegate,
                recycleDelegate,
                null, // records data controller doesn't need records tracking
                statusController);

            var accountProductsRecordsController = new RecordsController(
                accountProductRecordsDataController,
                statusController);

            var accountProductsIndexController = new IndexController<long>(
                accountProductsIndexStashController,
                collectionController,
                accountProductsRecordsController,
                statusController);
            
            var gameDetailsIndexController = new IndexController<long>(
                gameDetailsIndexStashController,
                collectionController,
                null,
                statusController);

            var gameProductDataIndexController = new IndexController<long>(
                gameProductDataIndexStashController,
                collectionController,
                null,
                statusController);

            var apiProductsIndexController = new IndexController<long>(
                apiProductsIndexStashController,
                collectionController,
                null,
                statusController);

            var productScreenshotsIndexController = new IndexController<long>(
                productScreenshotsIndexStashController,
                collectionController,
                null,
                statusController);

            var productDownloadsIndexController = new IndexController<long>(
                productDownloadsIndexStashController,
                collectionController,
                null,
                statusController);

            var productRoutesIndexController = new IndexController<long>(
                productRoutesIndexStashController,
                collectionController,
                null,
                statusController);

            var validationResultsIndexController = new IndexController<long>(
                validationResultsIndexStashController,
                collectionController,
                null,
                statusController);

            var wishlistedController = new IndexController<long>(
                wishlistedStashController,
                collectionController,
                null,
                statusController);

            var updatedController = new IndexController<long>(
                updatedStashController,
                collectionController,
                null,
                statusController);

            // data controllers

            var productsDataController = new DataController<Product>(
                productsIndexController,
                serializedTransactionalStorageController,
                convertProductToIndexDelegate,
                collectionController,
                getProductsPathDelegate,
                recycleDelegate,
                productsRecordsController,
                statusController);

            var accountProductsDataController = new DataController<AccountProduct>(
                accountProductsIndexController,
                serializedTransactionalStorageController,
                convertAccountProductToIndexDelegate,
                collectionController,
                getAccountProductsPathDelegate,
                recycleDelegate,
                accountProductsRecordsController,
                statusController);

            var gameDetailsDataController = new DataController<GameDetails>(
                gameDetailsIndexController,
                serializedTransactionalStorageController,
                convertGameDetailsToIndexDelegate,
                collectionController,
                getGameDetailsPathDelegate,
                recycleDelegate,
                null,
                statusController);

            var gameProductDataController = new DataController<GameProductData>(
                gameProductDataIndexController,
                serializedTransactionalStorageController,
                convertGameProductDataToIndexDelegate,
                collectionController,
                getGameProductDataPathDelegate,
                recycleDelegate,
                null,
                statusController);

            var apiProductsDataController = new DataController<ApiProduct>(
                apiProductsIndexController,
                serializedTransactionalStorageController,
                convertApiProductToIndexDelegate,
                collectionController,
                getApiProductPathDelegate,
                recycleDelegate,
                null,
                statusController);

            var screenshotsDataController = new DataController<ProductScreenshots>(
                productScreenshotsIndexController,
                serializedTransactionalStorageController,
                convertProductScreenshotsToIndexDelegate,
                collectionController,
                getScreenshotsPathDelegate,
                recycleDelegate,
                null,
                statusController);

            var productDownloadsDataController = new DataController<ProductDownloads>(
                productDownloadsIndexController,
                serializedTransactionalStorageController,
                convertProductDownloadsToIndexDelegate,
                collectionController,
                getProductDownloadsPathDelegate,
                recycleDelegate,
                null,
                statusController);

            var productRoutesDataController = new DataController<ProductRoutes>(
                productRoutesIndexController,
                serializedTransactionalStorageController,
                convertProductRoutesToIndexDelegate,
                collectionController,
                getProductRoutesPathDelegate,
                recycleDelegate,
                null,
                statusController);

            var validationResultsDataController = new DataController<ValidationResult>(
                validationResultsIndexController,
                serializedTransactionalStorageController,
                convertValidationResultToIndexDelegate,
                collectionController,
                getValidationResultsPathDelegate,
                recycleDelegate,
                null,
                statusController);

            #endregion

            var aliasController = new AliasController(ActivityContext.Aliases);
            var whitelistController = new WhitelistController(ActivityContext.Whitelist);
            var prerequisitesController = new PrerequisiteController(ActivityContext.Prerequisites);
            var supplementaryController = new SupplementaryController(ActivityContext.Supplementary);

            var activityContextController = new ActivityContextController(
                aliasController,
                whitelistController,
                prerequisitesController,
                supplementaryController);

            #region Activity Controllers

            #region Authorize

            var correctUsernamePasswordAsyncDelegate = new CorrectUsernamePasswordAsyncDelegate(
                consoleInputOutputController);
            var correctSecurityCodeAsyncDelegate = new CorrectSecurityCodeAsyncDelegate(
                consoleInputOutputController);

            var authorizationExtractionControllers = new Dictionary<string, IStringExtractionController>()
            {
                { QueryParameters.LoginId,
                    loginIdExtractionController },
                { QueryParameters.LoginUsername,
                    loginUsernameExtractionController },
                { QueryParameters.LoginUnderscoreToken,
                    loginUnderscoreTokenExtractionController },
                { QueryParameters.SecondStepAuthenticationUnderscoreToken,
                    secondStepAuthenticationUnderscoreTokenExtractionController }
            };

            var authorizationController = new AuthorizationController(
                correctUsernamePasswordAsyncDelegate,
                correctSecurityCodeAsyncDelegate,
                uriController,
                networkController,
                serializationController,
                authorizationExtractionControllers,
                statusController);

            var authorizeActivity = new AuthorizeActivity(
                settingsStashController,
                authorizationController,
                statusController);

            #endregion

            #region Update.PageResults

            var getProductUpdateUriByContextDelegate = new GetProductUpdateUriByContextDelegate();
            var getQueryParametersForProductContextDelegate = new GetQueryParametersForProductContextDelegate();

            var getProductsPageResultsAsyncDelegate = new GetPageResultsAsyncDelegate<ProductsPageResult>(
                Context.Products,
                getProductUpdateUriByContextDelegate,
                getQueryParametersForProductContextDelegate,
                requestPageAsyncDelegate,
                getStringMd5HashAsyncDelegate,
                precomputedHashController,
                serializationController,
                statusController);

            var itemizeProductsPageResultProductsDelegate = new ItemizeProductsPageResultProductsDelegate();

            var productsUpdateActivity = new PageResultUpdateActivity<ProductsPageResult, Product>(
                    (Activity.UpdateData, Context.Products),
                    activityContextController,
                    getProductsPageResultsAsyncDelegate,
                    itemizeProductsPageResultProductsDelegate,
                    //requestPageAsyncDelegate,
                    productsDataController,
                    statusController);

            var getAccountProductsPageResultsAsyncDelegate = new GetPageResultsAsyncDelegate<AccountProductsPageResult>(
                Context.AccountProducts,
                getProductUpdateUriByContextDelegate,
                getQueryParametersForProductContextDelegate,
                requestPageAsyncDelegate,
                getStringMd5HashAsyncDelegate,
                precomputedHashController,
                serializationController,
                statusController);

            var itemizeAccountProductsPageResultProductsDelegate = new ItemizeAccountProductsPageResultProductsDelegate();

            var accountProductsUpdateActivity = new PageResultUpdateActivity<AccountProductsPageResult, AccountProduct>(
                    (Activity.UpdateData, Context.AccountProducts),
                    activityContextController,
                    getAccountProductsPageResultsAsyncDelegate,
                    itemizeAccountProductsPageResultProductsDelegate,
                    accountProductsDataController,
                    statusController);

            var confirmAccountProductUpdatedDelegate = new ConfirmAccountProductUpdatedDelegate();

            var updatedUpdateActivity = new UpdatedUpdateActivity(
                activityContextController,
                accountProductsDataController,
                confirmAccountProductUpdatedDelegate,
                updatedController,
                statusController);

            #endregion

            #region Update.Wishlisted

            var getDeserializedPageResultAsyncDelegate = new GetDeserializedGOGDataAsyncDelegate<ProductsPageResult>(networkController,
                gogDataExtractionController,
                serializationController);

            var wishlistedUpdateActivity = new WishlistedUpdateActivity(
                getDeserializedPageResultAsyncDelegate,
                wishlistedController,
                statusController);

            #endregion

            #region Update.Products

            // dependencies for update controllers

            var getDeserializedGOGDataAsyncDelegate = new GetDeserializedGOGDataAsyncDelegate<GOGData>(networkController,
                gogDataExtractionController,
                serializationController);

            var getDeserializedGameProductDataAsyncDelegate = new GetDeserializedGameProductDataAsyncDelegate(
                getDeserializedGOGDataAsyncDelegate);

            var getProductUpdateIdentityDelegate = new GetProductUpdateIdentityDelegate();
            var getGameProductDataUpdateIdentityDelegate = new GetGameProductDataUpdateIdentityDelegate();
            var getAccountProductUpdateIdentityDelegate = new GetAccountProductUpdateIdentityDelegate();

            var fillGameDetailsGapsDelegate = new FillGameDetailsGapsDelegate();

            var itemizeAllUserRequestedIdsAsyncDelegate = new ItemizeAllUserRequestedIdsAsyncDelegate(args);

            // product update controllers

            var itemizeAllGameProductDataGapsAsyncDelegatepsDelegate = new ItemizeAllMasterDetailsGapsAsyncDelegate<Product, GameProductData>(
                productsDataController,
                gameProductDataController);

            var itemizeAllUserRequestedIdsOrDefaultAsyncDelegate = new ItemizeAllUserRequestedIdsOrDefaultAsyncDelegate(
                itemizeAllUserRequestedIdsAsyncDelegate,
                itemizeAllGameProductDataGapsAsyncDelegatepsDelegate,
                updatedController);

            var gameProductDataUpdateActivity = new MasterDetailProductUpdateActivity<Product, GameProductData>(
                Context.GameProductData,
                getProductUpdateUriByContextDelegate,
                itemizeAllUserRequestedIdsOrDefaultAsyncDelegate,
                productsDataController,
                gameProductDataController,
                updatedController,
                getDeserializedGameProductDataAsyncDelegate,
                getGameProductDataUpdateIdentityDelegate,
                statusController);

            var getApiProductDelegate = new GetDeserializedGOGModelAsyncDelegate<ApiProduct>(
                networkController,
                serializationController);

            var itemizeAllApiProductsGapsAsyncDelegate = new ItemizeAllMasterDetailsGapsAsyncDelegate<Product, ApiProduct>(
                productsDataController,
                apiProductsDataController);

            var itemizeAllUserRequestedOrApiProductGapsAndUpdatedDelegate = new ItemizeAllUserRequestedIdsOrDefaultAsyncDelegate(
                itemizeAllUserRequestedIdsAsyncDelegate,
                itemizeAllApiProductsGapsAsyncDelegate,
                updatedController);

            var apiProductUpdateActivity = new MasterDetailProductUpdateActivity<Product, ApiProduct>(
                Context.ApiProducts,
                getProductUpdateUriByContextDelegate,
                itemizeAllUserRequestedOrApiProductGapsAndUpdatedDelegate,
                productsDataController,
                apiProductsDataController,
                updatedController,
                getApiProductDelegate,
                getProductUpdateIdentityDelegate,
                statusController);

            var getDeserializedGameDetailsDelegate = new GetDeserializedGOGModelAsyncDelegate<GameDetails>(
                networkController,
                serializationController);

            var confirmStringContainsLanguageDownloadsDelegate = new ConfirmStringMatchesAllDelegate(
                collectionController,
                Models.Separators.Separators.GameDetailsDownloadsStart,
                Models.Separators.Separators.GameDetailsDownloadsEnd);

            var replaceMultipleStringsDelegate = new ReplaceMultipleStringsDelegate();

            var itemizeDownloadLanguagesDelegate = new ItemizeDownloadLanguagesDelegate(
                languageController,
                replaceMultipleStringsDelegate);

            var itemizeGameDetailsDownloadsDelegate = new ItemizeGameDetailsDownloadsDelegate();

            var formatDownloadLanguagesDelegate = new FormatDownloadLanguageDelegate(
                replaceMultipleStringsDelegate);

            var convertOperatingSystemsDownloads2DArrayToArrayDelegate = new Convert2DArrayToArrayDelegate<OperatingSystemsDownloads>();

            var getDeserializedGameDetailsAsyncDelegate = new GetDeserializedGameDetailsAsyncDelegate(
                networkController,
                serializationController,
                languageController,
                formatDownloadLanguagesDelegate,
                confirmStringContainsLanguageDownloadsDelegate,
                itemizeDownloadLanguagesDelegate,
                itemizeGameDetailsDownloadsDelegate,
                replaceMultipleStringsDelegate,
                convertOperatingSystemsDownloads2DArrayToArrayDelegate,
                collectionController);

            var itemizeAllGameDetailsGapsAsyncDelegate = new ItemizeAllMasterDetailsGapsAsyncDelegate<AccountProduct, GameDetails>(
                accountProductsDataController,
                gameDetailsDataController);

            var itemizeAllUserRequestedOrDefaultAsyncDelegate = new ItemizeAllUserRequestedIdsOrDefaultAsyncDelegate(
                itemizeAllUserRequestedIdsAsyncDelegate,
                itemizeAllGameDetailsGapsAsyncDelegate,
                updatedController);

            var gameDetailsUpdateActivity = new MasterDetailProductUpdateActivity<AccountProduct, GameDetails>(
                Context.GameDetails,
                getProductUpdateUriByContextDelegate,
                itemizeAllUserRequestedOrDefaultAsyncDelegate,
                accountProductsDataController,
                gameDetailsDataController,
                updatedController,
                getDeserializedGameDetailsAsyncDelegate,
                getAccountProductUpdateIdentityDelegate,
                statusController,
                fillGameDetailsGapsDelegate);

            #endregion

            #region Update.Screenshots

            var updateScreenshotsAsyncDelegate = new UpdateScreenshotsAsyncDelegate(
                getProductUpdateUriByContextDelegate,
                screenshotsDataController,
                networkController,
                screenshotExtractionController,
                statusController);

            var updateScreenshotsActivity = new UpdateScreenshotsActivity(
                productsDataController,
                productScreenshotsIndexController,
                updateScreenshotsAsyncDelegate,
                statusController);

            #endregion

            // dependencies for download controllers

            var getProductImageUriDelegate = new GetProductImageUriDelegate();
            var getAccountProductImageUriDelegate = new GetAccountProductImageUriDelegate();

            var itemizeAllUserRequestedIdsOrUpdatedAsyncDelegate = new ItemizeAllUserRequestedIdsOrDefaultAsyncDelegate(
                itemizeAllUserRequestedIdsAsyncDelegate,
                updatedController);

            var getProductsImagesDownloadSourcesAsyncDelegate = new GetProductCoreImagesDownloadSourcesAsyncDelegate<Product>(
                itemizeAllUserRequestedIdsOrUpdatedAsyncDelegate,
                productsDataController,
                formatImagesUriDelegate,
                getProductImageUriDelegate,
                statusController);

            var getAccountProductsImagesDownloadSourcesAsyncDelegate = new GetProductCoreImagesDownloadSourcesAsyncDelegate<AccountProduct>(
                itemizeAllUserRequestedIdsOrUpdatedAsyncDelegate,
                accountProductsDataController,
                formatImagesUriDelegate,
                getAccountProductImageUriDelegate,
                statusController);

            var getScreenshotsDownloadSourcesAsyncDelegate = new GetScreenshotsDownloadSourcesAsyncDelegate(
                screenshotsDataController,
                formatScreenshotsUriDelegate,
                getScreenshotsDirectoryDelegate,
                fileController,
                statusController);

            var routingController = new RoutingController(
                productRoutesDataController,
                statusController);

            var itemizeGameDetailsManualUrlsAsyncDelegate = new ItemizeGameDetailsManualUrlsAsyncDelegate(
                settingsStashController,
                gameDetailsDataController);

            var itemizeGameDetailsDirectoriesAsyncDelegate = new ItemizeGameDetailsDirectoriesAsyncDelegate(
                itemizeGameDetailsManualUrlsAsyncDelegate,
                getProductFilesDirectoryDelegate);

            var itemizeGameDetailsFilesAsyncDelegate = new ItemizeGameDetailsFilesAsyncDelegate(
                itemizeGameDetailsManualUrlsAsyncDelegate,
                routingController,
                getGameDetailsFilesPathDelegate,
                statusController);

            // product files are driven through gameDetails manual urls
            // so this sources enumerates all manual urls for all updated game details
            var getManualUrlDownloadSourcesAsyncDelegate = new GetManualUrlDownloadSourcesAsyncDelegate(
                updatedController,
                gameDetailsDataController,
                itemizeGameDetailsManualUrlsAsyncDelegate,
                statusController);

            // schedule download controllers

            var updateProductsImagesDownloadsActivity = new UpdateDownloadsActivity(
                Context.ProductsImages,
                getProductsImagesDownloadSourcesAsyncDelegate,
                getImagesDirectoryDelegate,
                fileController,
                productDownloadsDataController,
                accountProductsDataController,
                productsDataController,
                statusController);

            var updateAccountProductsImagesDownloadsActivity = new UpdateDownloadsActivity(
                Context.AccountProductsImages,
                getAccountProductsImagesDownloadSourcesAsyncDelegate,
                getImagesDirectoryDelegate,
                fileController,
                productDownloadsDataController,
                accountProductsDataController,
                productsDataController,
                statusController);

            var updateScreenshotsDownloadsActivity = new UpdateDownloadsActivity(
                Context.Screenshots,
                getScreenshotsDownloadSourcesAsyncDelegate,
                getScreenshotsDirectoryDelegate,
                fileController,
                productDownloadsDataController,
                accountProductsDataController,
                productsDataController,
                statusController);

            var updateProductFilesDownloadsActivity = new UpdateDownloadsActivity(
                Context.ProductsFiles,
                getManualUrlDownloadSourcesAsyncDelegate,
                getProductFilesDirectoryDelegate,
                fileController,
                productDownloadsDataController,
                accountProductsDataController,
                productsDataController,
                statusController);

            // downloads processing

            var formatUriRemoveSessionDelegate = new FormatUriRemoveSessionDelegate();

            var confirmValidationExpectedDelegate = new ConfirmValidationExpectedDelegate();

            var formatValidationUriDelegate = new FormatValidationUriDelegate(
                getValidationFilenameDelegate,
                formatUriRemoveSessionDelegate);

            var formatValidationFileDelegate = new FormatValidationFileDelegate(
                getValidationPathDelegate);

            var downloadValidationFileAsyncDelegate = new DownloadValidationFileAsyncDelegate(
                formatUriRemoveSessionDelegate,
                confirmValidationExpectedDelegate,
                formatValidationFileDelegate,
                getValidationDirectoryDelegate,
                formatValidationUriDelegate,
                fileController,
                downloadFromUriAsyncDelegate,
                statusController);

            var downloadManualUrlFileAsyncDelegate = new DownloadManualUrlFileAsyncDelegate(
                networkController,
                formatUriRemoveSessionDelegate,
                routingController,
                downloadFromResponseAsyncDelegate,
                downloadValidationFileAsyncDelegate,
                statusController);

            var downloadProductImageAsyncDelegate = new DownloadProductImageAsyncDelegate(downloadFromUriAsyncDelegate);

            var productsImagesDownloadActivity = new DownloadFilesActivity(
                Context.ProductsImages,
                productDownloadsDataController,
                downloadProductImageAsyncDelegate,
                statusController);

            var accountProductsImagesDownloadActivity = new DownloadFilesActivity(
                Context.AccountProductsImages,
                productDownloadsDataController,
                downloadProductImageAsyncDelegate,
                statusController);

            var screenshotsDownloadActivity = new DownloadFilesActivity(
                Context.Screenshots,
                productDownloadsDataController,
                downloadProductImageAsyncDelegate,
                statusController);

            var productFilesDownloadActivity = new DownloadFilesActivity(
                Context.ProductsFiles,
                productDownloadsDataController,
                downloadManualUrlFileAsyncDelegate,
                statusController);

            // validation controllers

            var validationResultController = new ValidationResultController();

            var fileMd5Controller = new GetFileMd5HashAsyncDelegate(
                storageController,
                getStringMd5HashAsyncDelegate);

            var dataFileValidateDelegate = new DataFileValidateDelegate(
                fileMd5Controller,
                statusController);

            var productFileValidationController = new FileValidationController(
                confirmValidationExpectedDelegate,
                fileController,
                streamController,
                getBytesMd5HashAsyncDelegate,
                validationResultController,
                statusController);

            var validateProductFilesActivity = new ValidateProductFilesActivity(
                getProductFilesDirectoryDelegate,
                getUriFilenameDelegate,
                formatValidationFileDelegate,
                productFileValidationController,
                validationResultsDataController,
                gameDetailsDataController,
                itemizeGameDetailsManualUrlsAsyncDelegate,
                itemizeAllUserRequestedIdsOrUpdatedAsyncDelegate,
                routingController,
                statusController);

            var validateDataActivity = new ValidateDataActivity(
                precomputedHashController,
                fileController,
                dataFileValidateDelegate,
                statusController);

            #region Repair

            var repairActivity = new RepairActivity(
                validationResultsDataController,
                validationResultController,
                statusController);

            #endregion

            #region Cleanup

            var itemizeAllGameDetailsDirectoriesAsyncDelegate = new ItemizeAllGameDetailsDirectoriesAsyncDelegate(
                gameDetailsDataController,
                itemizeGameDetailsDirectoriesAsyncDelegate,
                statusController);

            var itemizeAllProductFilesDirectoriesAsyncDelegate = new ItemizeAllProductFilesDirectoriesAsyncDelegate(
                getProductFilesBaseDirectoryDelegate,
                directoryController,
                statusController);

            var itemizeDirectoryFilesDelegate = new ItemizeDirectoryFilesDelegate(directoryController);

            var directoryCleanupActivity = new CleanupActivity(
                Context.Directories,
                itemizeAllGameDetailsDirectoriesAsyncDelegate, // expected items (directories for gameDetails)
                itemizeAllProductFilesDirectoriesAsyncDelegate, // actual items (directories in productFiles)
                itemizeDirectoryFilesDelegate, // detailed items (files in directory)
                formatValidationFileDelegate, // supplementary items (validation files)
                recycleDelegate,
                directoryController,
                statusController);

            var itemizeAllUpdatedGameDetailsManualUrlFilesAsyncDelegate = 
                new ItemizeAllUpdatedGameDetailsManualUrlFilesAsyncDelegate(
                    updatedController,
                    gameDetailsDataController,
                    itemizeGameDetailsFilesAsyncDelegate,
                    statusController);

            var itemizeAllUpdatedProductFilesAsyncDelegate = 
                new ItemizeAllUpdatedProductFilesAsyncDelegate(
                    updatedController,
                    gameDetailsDataController,
                    itemizeGameDetailsDirectoriesAsyncDelegate,
                    directoryController,
                    statusController);

            var itemizePassthroughDelegate = new ItemizePassthroughDelegate();

            var fileCleanupActivity = new CleanupActivity(
                Context.Files,
                itemizeAllUpdatedGameDetailsManualUrlFilesAsyncDelegate, // expected items (files for updated gameDetails)
                itemizeAllUpdatedProductFilesAsyncDelegate, // actual items (updated product files)
                itemizePassthroughDelegate, // detailed items (passthrough)
                formatValidationFileDelegate, // supplementary items (validation files)
                recycleDelegate,
                directoryController,
                statusController);

            var cleanupUpdatedActivity = new CleanupUpdatedActivity(
                updatedController,
                statusController);

            #endregion

            #region Help

            var helpActivity = new HelpActivity(
                activityContextController,
                statusController);

            #endregion

            #region Report Task Status 

            var reportFilePresentationController = new FilePresentationController(
                getReportDirectoryDelegate,
                getReportFilenameDelegate,
                streamController);

            var fileNotifyStatusViewUpdateController = new NotifyStatusViewUpdateController(
                getStatusViewUpdateDelegate,
                reportFilePresentationController);

            var reportActivity = new ReportActivity(
                fileNotifyStatusViewUpdateController,
                statusController);

            #endregion

            #region List

            var listUpdatedActivity = new ListUpdatedActivity(
                updatedController,
                accountProductsDataController,
                statusController);

            #endregion

            #endregion

            #region Activity Context To Activity Controllers Mapping

            var activityContextToActivityControllerMap = new Dictionary<(Activity, Context), IActivity>()
            {
                { (Activity.Correct, Context.Settings), correctSettingsActivity },
                { (Activity.Authorize, Context.None), authorizeActivity },
                { (Activity.UpdateData, Context.Products), productsUpdateActivity },
                { (Activity.UpdateData, Context.AccountProducts), accountProductsUpdateActivity },
                { (Activity.UpdateData, Context.Updated), updatedUpdateActivity },
                { (Activity.UpdateData, Context.Wishlist), wishlistedUpdateActivity },
                { (Activity.UpdateData, Context.GameProductData), gameProductDataUpdateActivity },
                { (Activity.UpdateData, Context.ApiProducts), apiProductUpdateActivity },
                { (Activity.UpdateData, Context.GameDetails), gameDetailsUpdateActivity },
                { (Activity.UpdateData, Context.Screenshots), updateScreenshotsActivity },
                { (Activity.UpdateDownloads, Context.ProductsImages), updateProductsImagesDownloadsActivity },
                { (Activity.UpdateDownloads, Context.AccountProductsImages), updateAccountProductsImagesDownloadsActivity },
                { (Activity.UpdateDownloads, Context.Screenshots), updateScreenshotsDownloadsActivity },
                { (Activity.UpdateDownloads, Context.ProductsFiles), updateProductFilesDownloadsActivity },
                { (Activity.Download, Context.ProductsImages), productsImagesDownloadActivity },
                { (Activity.Download, Context.AccountProductsImages), accountProductsImagesDownloadActivity },
                { (Activity.Download, Context.Screenshots), screenshotsDownloadActivity },
                { (Activity.Download, Context.ProductsFiles), productFilesDownloadActivity },
                { (Activity.Validate, Context.ProductsFiles), validateProductFilesActivity },
                { (Activity.Validate, Context.Data), validateDataActivity },
                { (Activity.Repair, Context.ProductsFiles), repairActivity },
                { (Activity.Cleanup, Context.Directories), directoryCleanupActivity },
                { (Activity.Cleanup, Context.Files), fileCleanupActivity },
                { (Activity.Cleanup, Context.Updated), cleanupUpdatedActivity },
                { (Activity.Report, Context.None), reportActivity },
                { (Activity.List, Context.Updated), listUpdatedActivity },
                { (Activity.Help, Context.None), helpActivity }
            };



            var activityContextQueue = activityContextController.GetQueue(args);
            var commandLineParameters = activityContextController.GetParameters(args).ToArray();

            #endregion

            #region Core Activities Loop

            foreach (var activityContext in activityContextQueue)
            {
                if (!activityContextToActivityControllerMap.ContainsKey(activityContext))
                {
                    await statusController.WarnAsync(
                        applicationStatus,
                        activityContextController.ToString(activityContext) + " is not mapped to an Activity.");
                    continue;
                }

                var activity = activityContextToActivityControllerMap[activityContext];
                try
                {
                    await activity.ProcessActivityAsync(applicationStatus);
                }
                catch (AggregateException ex)
                {
                    var itemizeInnerExceptionsDelegate = new ItemizeInnerExceptionsDelegate();
                    var convertTreeToEnumerableDelegate = new ConvertTreeToEnumerableDelegate<Exception>(itemizeInnerExceptionsDelegate);

                    List<string> errorMessages = new List<string>();
                    foreach (var innerException in convertTreeToEnumerableDelegate.Convert(ex))
                        errorMessages.Add(innerException.Message);

                    var combinedErrorMessages = string.Join(Models.Separators.Separators.Common.Comma, errorMessages);

                    await statusController.FailAsync(applicationStatus, combinedErrorMessages);

                    var failureDumpUri = "failureDump.json";
                    await serializedTransactionalStorageController.SerializePushAsync(failureDumpUri, applicationStatus, applicationStatus);

                    await consoleInputOutputController.OutputOnRefreshAsync(
                        "GoodOfflineGames.exe has encountered fatal error(s): " +
                        combinedErrorMessages +
                        $".\nPlease refer to {failureDumpUri} for further details.\n" +
                        "Press ENTER to close the window...");

                    consoleController.ReadLine();

                    return;
                }
            }

            #endregion

            // TODO: Present session results

            if (applicationStatus.SummaryResults != null)
            {
                foreach (var line in applicationStatus.SummaryResults)
                    await consoleInputOutputController.OutputOnRefreshAsync(string.Join(" ", applicationStatus.SummaryResults));
            }
            else
            {
                await consoleInputOutputController.OutputContinuousAsync(applicationStatus, string.Empty, "All tasks are complete. Press ENTER to exit...");
            }

            consoleController.ReadLine();
        }
    }
}
