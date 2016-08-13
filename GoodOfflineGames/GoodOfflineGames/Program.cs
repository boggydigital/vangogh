﻿using System;
using System.Collections.Generic;

using Controllers.Stream;
using Controllers.Storage;
using Controllers.File;
using Controllers.Directory;
using Controllers.Network;
using Controllers.Language;
using Controllers.Serialization;
using Controllers.Extraction;
using Controllers.Console;
using Controllers.Reporting;
using Controllers.Settings;
using Controllers.RequestPage;

using Interfaces.TaskActivity;

using GOG.TaskActivities.Authorization;
using GOG.TaskActivities.Updates;

namespace GoodOfflineGames
{
    class Program
    {
        static void Main(string[] args)
        {
            var streamController = new StreamController();
            var fileController = new FileController();
            var directoryController = new DirectoryController();
            var storageController = new StorageController(
                streamController,
                fileController);

            var consoleController = new ConsoleController();
            var taskReportingController = new TaskReportingController(consoleController);

            var uriController = new UriController();
            var networkController = new NetworkController(uriController);
            var requestPageController = new RequestPageController(
                networkController);

            var languageController = new LanguageController();

            var serializationController = new JSONStringController();

            var extractionController = new ExtractionController();

            var productStorageController = new ProductStorageController(
                storageController,
                serializationController);

            // Load settings that (might) have authorization information, and request to run or not specific task activities

            var settingsController = new SettingsController(
                storageController,
                serializationController,
                languageController,
                consoleController);

            taskReportingController.AddTask("Load settings");
            //var settings = settingsController.Load().Result;
            taskReportingController.CompleteTask();

            // Create and add all task activity controllersa
            // Task activities are encapsulated set of activity - so no data can be passed around!
            // Individual task activity would need to load data it needs from the disk / network

            //var authorizationController = new AuthorizationController(
            //    uriController,
            //    networkController,
            //    extractionController,
            //    consoleController,
            //    settings.Authenticate,
            //    taskReportingController);

            var productsUpdateController = new ProductsUpdateController(
                requestPageController,
                serializationController,
                productStorageController,
                taskReportingController);

            var accountProductsUpdateController = new AccountProductsUpdateController(
                requestPageController,
                serializationController,
                productStorageController,
                taskReportingController);

            var newUpdatedAccountProductsController = new NewUpdatedAccountProductsController(
                productStorageController,
                taskReportingController);

            // Iterate and process all tasks

            var taskActivityControllers = new List<ITaskActivityController>();

            //taskActivityControllers.Add(authorizationController);
            //taskActivityControllers.Add(productsUpdateController);
            //taskActivityControllers.Add(accountProductsUpdateController);
            taskActivityControllers.Add(newUpdatedAccountProductsController);

            foreach (var taskActivityController in taskActivityControllers)
                try
                {
                    taskActivityController.ProcessTask().Wait();
                }
                catch (AggregateException ex)
                {
                    List<string> errorMessages = new List<string>();

                    foreach (var innerException in ex.InnerExceptions)
                        errorMessages.Add(innerException.Message);

                    taskReportingController.ReportFailure(string.Join(", ", errorMessages));
                    break;
                }

            consoleController.WriteLine("Press ENTER to continue...");
            consoleController.ReadLine();
        }
    }
}
