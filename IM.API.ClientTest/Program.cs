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

                var superMarketId = LookupControllerTest(new LookupController(clientId, clientSecret), supermarketName);
                ProductsController productsController = new ProductsController(clientId, clientSecret);

                ProductsControllerTest(productsController);

                TestUserPurchase(productsController, clientId, clientSecret, superMarketId, username, password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Environment.Exit(1);
            }

            Console.WriteLine("All tests passed");
        }

        private static void TestUserPurchase(ProductsController productsController, string clientId, string clientSecret, int superMarketId, string username, string password)
        {
            string email = "testuser@iamdata.co";
            string userId = "testuserId1234";

            UsersController usersController = new UsersController(clientId, clientSecret);
            StoresController storesController = new StoresController(clientId, clientSecret);
            PurchasesController purchasesController = new PurchasesController(clientId, clientSecret);
            BarcodeController barcodeController = new BarcodeController(clientId, clientSecret);
            ReceiptController receiptController = new ReceiptController(clientId, clientSecret);

            UsersControllerTest(email, usersController, userId);

            string encodedImage = File.ReadAllText("encoded_logo.txt");
            string receiptId = "fe6ba83b-d45c-457a-afd5-35bdb3cdffff";
            UploadReceiptRequest receiptRequest = new UploadReceiptRequest()
            {
                Image = encodedImage,
                ReceiptId = receiptId
            };

            receiptController.ReceiptUploadReceipt(receiptRequest, userId);

            ConnectUserStoreRequest storeConnect = new ConnectUserStoreRequest()
            {
                StoreId = superMarketId,
                Username = username,
                Password = password
            };

            ConnectUserStoreResponse userStore = storesController.StoresConnectStore(storeConnect, userId).Result;

            bool storeConnectionValid = CheckStoreValidity(storesController, userId, userStore.Id.Value);
            if (!storeConnectionValid)
            {
                storesController.StoresDeleteSingleStore(userId, userStore.Id.Value);
                throw new Exception("Error: could not connect to store");
            }

            UpdateUserStoreRequest updateUserStoreRequest = new UpdateUserStoreRequest()
            {
                Username = username,
                Password = password
            };

            storesController.StoresUpdateStoreConnection(updateUserStoreRequest, userId, userStore.Id.Value);

            if (!WaitForScrapeToFinish(storesController, userId, userStore.Id.Value))
            {
                throw new Exception("Error: scrape is not finished");
            }

            List<UserStore> stores = storesController.StoresGetAllStores(userId).Result;
            if (stores.Count == 0 || stores[0].Id <= 0)
            {
                throw new Exception("Error: could not get all stores");
            }

            List<ProductData> userProducts = productsController.ProductsGetUserProducts(userId, 1, 15, true, true).Result;
            if (userProducts.Count == 0)
            {
                throw new Exception("Error: get user products");
            }

            List<UserPurchase> userPurchases = purchasesController.PurchasesGetAllUserPurchases(userId, 1, 15, true).Result;
            if (userPurchases.Count == 0)
            {
                throw new Exception("Error: get all user purchases");
            }

            UserPurchase userPurchase = purchasesController.PurchasesGetSingleUserPurchase(userId, userPurchases[0].Id.ToString(), true).Result;
            if (userPurchase == null || userPurchase.Id != userPurchases[0].Id)
            {
                throw new Exception("Error: get single user purchases");
            }

            UploadBarcodeRequest barcodeRequest = new UploadBarcodeRequest()
            {
                BarCode = "021130126026",
                BarCodeType = "UPC-A"
            };

            UploadBarcodeResponse barcodeResponse = barcodeController.BarcodeUploadBarcode(barcodeRequest, userId).Result;
            if (barcodeResponse.BarCodeType != "UPC-A" || barcodeResponse.BarCode != "021130126026")
            {
                throw new Exception("Error: upload barcode");
            }

            storesController.StoresDeleteSingleStore(userId, userStore.Id.Value);

            usersController.UsersDeleteUser(userId);
        }

        private static bool WaitForScrapeToFinish(StoresController storesController, string userIdentifier, int storeId)
        {
            // try to see if the users credentials are valid
            for (int i = 0; i < 30; i++)
            {
                var connectedStore = storesController.StoresGetSingleStore(userIdentifier, storeId);

                if (connectedStore != null &&
                    connectedStore.Result.ScrapeStatus == "Done")
                {
                    return true;
                }

                Thread.Sleep(3000);
            }

            return false;
        }

        private static bool CheckStoreValidity(StoresController storesController, string userIdentifier, int storeId)
        {
            // try to see if the users credentials are valid
            for (int i = 0; i < 15; i++)
            {
                var connectedStore = storesController.StoresGetSingleStore(userIdentifier, storeId);

                if (connectedStore != null &&
                    connectedStore.Result.CredentialsStatus == "Verified")
                {
                    return true;
                }

                Thread.Sleep(3000);
            }

            return false;
        }

        private static void UsersControllerTest(string email, UsersController usersController, string userId)
        {
            RegisterUserRequest registerUserRequest = new RegisterUserRequest()
            {
                Email = email,
                UserId = userId,
                Zip = "21000"
            };

            CreateUserWrapper user = usersController.UsersCreateUser(registerUserRequest);

            if (user.Result.Email != email || user.Result.UserId != userId)
            {
                throw new Exception("Error: create user");
            }

            GetAllUsersWrapper allUsers = usersController.UsersGetAllUsers();

            if (allUsers.Result.Count == 0)
            {
                throw new Exception("Error: get all users");
            }

            GetSingleUserWrapper userResponse = usersController.UsersGetSingleUser(userId);
            if (userResponse.Result.Email != email)
            {
                throw new Exception("Error: get single user");
            }
        }

        private static int LookupControllerTest(LookupController lookupController, string supermarketName)
        {
            int superMarketId = 0;

            var categories = lookupController.LookupGetCategories().Result;
            if (categories.Count == 0 || categories[0].Id != 1)
            {
                throw new Exception("Error: categories");
            }

            var nutrients = lookupController.LookupGetNutrients().Result;
            if (nutrients.Count == 0 || nutrients[0].Id != 7)
            {
                throw new Exception("Error: nutrients");
            }

            var alternatives = lookupController.LookupGetProductAlternativeTypes().Result;
            if (alternatives.Count == 0 || alternatives[0].Id != 1 || alternatives[0].Name != "Reduce Sodium")
            {
                throw new Exception("Error: alternatives");
            }

            var tags = lookupController.LookupGetTags().Result;
            if (tags.Count == 0 || tags[0].Id != 29 || tags[0].Name != "Low Fat")
            {
                throw new Exception("Error: tags");
            }

            var unitsOfMeasurement = lookupController.LookupGetUOMs().Result;
            if (unitsOfMeasurement.Count == 0 || unitsOfMeasurement[0].Id != 1 || unitsOfMeasurement[0].Name != "g")
            {
                throw new Exception("Error: units of measurements");
            }

            var stores = lookupController.LookupGetStores().Result;
            if (stores.Count == 0 || stores[0].Id != 1 || stores[0].Name != "FreshDirect")
            {
                throw new Exception("Error: stores");
            }

            for (int i = 0; i < stores.Count; i++)
            {
                if (stores[i].Name == supermarketName)
                {
                    superMarketId = stores[i].Id.Value;
                    break;
                }
            }

            return superMarketId;
        }

        private static void ProductsControllerTest(ProductsController productsController)
        {
            List<ProductData> kaleProducts = productsController.ProductsGetProducts("Kale", null, 1, 25, true).Result;
            if (kaleProducts.Count == 0 || kaleProducts[0].Name == null)
            {
                throw new Exception("Error: get products");
            }

            if (string.IsNullOrEmpty(kaleProducts[0].Nutrients[0].Name))
            {
                throw new Exception("Error: get detail product info");
            }

            var secondPageKaleProducts = productsController.ProductsGetProducts("Kale", null, 2, 25, true).Result;
            if (secondPageKaleProducts.Count == 0 || secondPageKaleProducts[0].Name == null || secondPageKaleProducts[0].Id == kaleProducts[0].Id)
            {
                throw new Exception("Error: get 2nd page products");
            }

            List<ProductData> upcProduct = productsController.ProductsGetProducts(null, "014100044208", 1, 25, true).Result;
            if (upcProduct.Count == 0 || upcProduct[0].Name != "Pepperidge Farm Classic BBQ Cracker Chips, 6 Oz")
            {
                throw new Exception("Error: get upc products");
            }

            List<ProductData> eanProduct = productsController.ProductsGetProducts(null, "096619872404", null, null, true).Result;
            if (eanProduct.Count == 0 || eanProduct[0].Name != "Beckett Basketball Monthly Houston Rocket English")
            {
                throw new Exception("Error: get ean products");
            }

            ProductData productFull = productsController.ProductsGetProduct("380728", true).Result;
            if (productFull == null || productFull.Name != "Peanut Butter Chocolate Party Size Candies")
            {
                throw new Exception("Error: get full product");
            }

            List<ProductData> productAlternatives = productsController.ProductsGetProductAlternatives("120907", ((int)AlternativeTypes.General).ToString()).Result;
            if (productAlternatives.Count == 0 || productAlternatives[0].Name != "Roland Feng Shui Green Peas Hot Wasabi Coated")
            {
                throw new Exception("Error: get full product");
            }

            List<ProductAlternativesRecord> productsAlternatives = productsController.ProductsGetProductsAlternatives("120907, 120902", ((int)AlternativeTypes.General).ToString()).Result;
            if (productsAlternatives.Count == 0 || productsAlternatives[0].ProductAlternatives[0].Name != "Lightlife Smart Deli Veggie Slices Roast Turkey")
            {
                throw new Exception("Error: get full product");
            }

            List<PriceData> productPrices = productsController.ProductsGetProductPrices("314878, 314328").Result;
            if (productPrices.Count == 0 || productPrices[0].Prices[0].StoreId != 2)
            {
                throw new Exception("Error: get full product");
            }
        }
    }
}