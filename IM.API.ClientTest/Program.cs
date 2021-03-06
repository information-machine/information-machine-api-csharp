﻿using InformationMachineAPI.PCL;
using InformationMachineAPI.PCL.Controllers;
using InformationMachineAPI.PCL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace IM.API.ClientTest
{
    public enum AlternativeTypes
    {
        ReduceSodium = 1,
        LessSolidFatsAndTransFats = 2,
        ReduceCalories = 3,
        ReduceCholesterol = 4,
        IncreaseFiber = 5,
        ReduceFats = 6,
        General = 7,
        ReduceSugar = 8
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                string[] config = File.ReadAllLines("clienttest.txt");
                var clientId = config[0];
                var clientSecret = config[1];
                var supermarketName = config[2];
                var username = config[3];
                var password = config[4];
                InformationMachineAPIClient client = new InformationMachineAPIClient(clientId, clientSecret);

                var superMarketId = LookupControllerTest(client.Lookup, supermarketName);

                ProductsControllerTest(client.Products);

                TestUserPurchase(client, superMarketId, username, password);

                Console.WriteLine("All tests passed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void TestUserPurchase(InformationMachineAPIClient client, long superMarketId, string username, string password)
        {
            string email = "testuser@iamdata.co";
            string userId = "testuserId1234";

            UserManagementController usersController = client.UserManagement;
            UserStoresController storesController = client.UserStores;
            UserPurchasesController purchasesController = client.UserPurchases;
            UserScansController userScansController = client.UserScans;
            
            UsersControllerTest(email, usersController, userId);

            string encodedImage = File.ReadAllText("encoded_logo.txt");
            string receiptId = "fe6ba83b-d45c-457a-afd5-35bdb3cdffff";
            UploadReceiptRequest receiptRequest = new UploadReceiptRequest()
            {
                Image = encodedImage,
                ReceiptId = receiptId
            };

            userScansController.UserScansUploadReceipt(receiptRequest, userId);

            ConnectUserStoreRequest storeConnect = new ConnectUserStoreRequest()
            {
                StoreId = superMarketId,
                Username = username,
                Password = password
            };

            UserStore userStore = storesController.UserStoresConnectStore(storeConnect, userId).Result;

            bool storeConnectionValid = CheckStoreValidity(storesController, userId, userStore.Id);
            if (!storeConnectionValid)
            {
                storesController.UserStoresDeleteSingleStore(userId, userStore.Id);
                throw new APITestException("Error: could not connect to store");
            }

            if (!WaitForScrapeToFinish(storesController, userId, userStore.Id))
            {
                throw new APITestException("Error: scrape is not finished");
            }

            List<UserStore> stores = storesController.UserStoresGetAllUserStores(userId).Result;
            if (stores.Count == 0 || stores[0].Id <= 0)
            {
                throw new APITestException("Error: could not get all stores");
            }

            PurchaseData purchaseHistory = purchasesController.UserPurchasesGetPurchaseHistoryUnified(userId).Result;
            if (purchaseHistory.PurchasedItems.Count == 0)
            {
                throw new APITestException("Error: get purchase history");
            }

            UploadBarcodeRequest barcodeRequest = new UploadBarcodeRequest()
            {
                BarCode = "021130126026",
                BarCodeType = "UPC-A"
            };

            UploadBarcodeResponse barcodeResponse = userScansController.UserScansUploadBarcode(barcodeRequest, userId).Result;
            if (barcodeResponse.BarCodeType != "UPC-A" || barcodeResponse.BarCode != "021130126026")
            {
                throw new APITestException("Error: upload barcode");
            }

            storesController.UserStoresDeleteSingleStore(userId, userStore.Id);

            usersController.UserManagementDeleteUser(userId);
        }

        private static bool WaitForScrapeToFinish(UserStoresController storesController, string userIdentifier, long storeId)
        {
            // try to see if the users credentials are valid
            for (int i = 0; i < 120; i++)
            {
                var connectedStore = storesController.UserStoresGetSingleStore(userIdentifier, storeId);

                if (connectedStore != null &&
                    (connectedStore.Result.ScrapeStatus == "Done" || connectedStore.Result.ScrapeStatus == "DoneWithWarning"))
                {
                    return true;
                }

                Thread.Sleep(3000);
            }

            return false;
        }

        private static bool CheckStoreValidity(UserStoresController storesController, string userIdentifier, long storeId)
        {
            // try to see if the users credentials are valid
            for (int i = 0; i < 15; i++)
            {
                var connectedStore = storesController.UserStoresGetSingleStore(userIdentifier, storeId);

                if (connectedStore != null &&
                    (connectedStore.Result.ScrapeStatus == "Done" || connectedStore.Result.ScrapeStatus == "DoneWithWarning" || connectedStore.Result.ScrapeStatus == "Scraping"))
                {
                    if (connectedStore.Result.CredentialsStatus == "Verified")
                    {
                        return true;
                    }

                    return false;
                }

                Thread.Sleep(3000);
            }

            return false;
        }

        private static void UsersControllerTest(string email, UserManagementController usersController, string userId)
        {
            RegisterUserRequest registerUserRequest = new RegisterUserRequest()
            {
                Email = email,
                UserId = userId,
                Zip = "21000"
            };

            CreateUserWrapper user = usersController.UserManagementCreateUser(registerUserRequest);

            if (user.Result.Email != email || user.Result.UserId != userId)
            {
                throw new APITestException("Error: create user");
            }

            GetAllUsersWrapper allUsers = usersController.UserManagementGetAllUsers();

            if (allUsers.Result.Count == 0)
            {
                throw new APITestException("Error: get all users");
            }

            GetSingleUserWrapper userResponse = usersController.UserManagementGetSingleUser(userId);
            if (userResponse.Result.Email != email)
            {
                throw new APITestException("Error: get single user");
            }
        }

        private static long LookupControllerTest(LookupController lookupController, string supermarketName)
        {
            long superMarketId = 0;

            var categories = lookupController.LookupGetCategories().Result;
            if (categories.Count == 0 || categories[0].Id != 1)
            {
                throw new APITestException("Error: categories");
            }

            var nutrients = lookupController.LookupGetNutrients().Result;
            if (nutrients.Count == 0 || nutrients[0].Id != 7)
            {
                throw new APITestException("Error: nutrients");
            }

            var alternatives = lookupController.LookupGetProductAlternativeTypes().Result;
            if (alternatives.Count == 0 || alternatives[0].Id != 1 || alternatives[0].Name != "Reduce Sodium")
            {
                throw new APITestException("Error: alternatives");
            }

            var tags = lookupController.LookupGetTags().Result;
            if (tags.Count == 0 || tags[0].Id != 29 || tags[0].Name != "Low Fat")
            {
                throw new APITestException("Error: tags");
            }

            var unitsOfMeasurement = lookupController.LookupGetUOMs().Result;
            if (unitsOfMeasurement.Count == 0 || unitsOfMeasurement[0].Id != 1 || unitsOfMeasurement[0].Name != "g")
            {
                throw new APITestException("Error: units of measurements");
            }

            var stores = lookupController.LookupGetStores().Result;
            if (stores.Count == 0 || stores[0].Id != 1 || stores[0].Name != "FreshDirect")
            {
                throw new APITestException("Error: stores");
            }

            for (int i = 0; i < stores.Count; i++)
            {
                if (stores[i].Name == supermarketName)
                {
                    superMarketId = stores[i].Id;
                    break;
                }
            }

            return superMarketId;
        }

        private static void ProductsControllerTest(ProductsController productsController)
        {
            List<ProductData> kaleProducts = productsController.ProductsSearchProducts("Kale", null, 1, 25, null, true).Result;
            if (kaleProducts.Count == 0 || kaleProducts[0].Name == null)
            {
                throw new APITestException("Error: get products");
            }

            if (string.IsNullOrEmpty(kaleProducts[0].Nutrients[0].Name))
            {
                throw new APITestException("Error: get detail product info");
            }

            var secondPageKaleProducts = productsController.ProductsSearchProducts("Kale", null, 2, 25, null, true).Result;
            if (secondPageKaleProducts.Count == 0 || secondPageKaleProducts[0].Name == null || secondPageKaleProducts[0].Id == kaleProducts[0].Id)
            {
                throw new APITestException("Error: get 2nd page products");
            }

            List<ProductData> upcProduct = productsController.ProductsSearchProducts(null, "014100044208", 1, 25, null, true).Result;
            if (upcProduct.Count == 0 || upcProduct[0].Name != "Pepperidge Farm Classic Bbq Cracker Chips, 6 Oz")
            {
                throw new APITestException("Error: get upc products");
            }

            List<ProductData> eanProduct = productsController.ProductsSearchProducts(null, "096619872404", null, null, null, true).Result;
            if (eanProduct.Count == 0 || eanProduct[0].Name != "Beckett Basketball Monthly Houston Rocket English")
            {
                throw new APITestException("Error: get ean products");
            }

            ProductData productFull = productsController.ProductsGetProduct(2224617, true).Result;
            if (productFull == null || productFull.Name != "Peanut Butter Chocolate Chunk Smart Cookies")
            {
                throw new APITestException("Error: get full product");
            }
        }
    }
}