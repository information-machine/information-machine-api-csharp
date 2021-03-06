# Information Machine API

How To Configure:
=================
File clienttest.txt contains several parameters in order to fully test the api using IM.API.ClientTest project.
Each line of this file represents a certain expected value that you need to give in order to test the api.
The file initially contains the following:

1. YOUR_CLIENT_ID
2. YOUR_CLIENT_SECRET
3. STORE_NAME(EXAMPLE:WALMART)
4. USERNAME_FOR_GIVEN_STORE
5. PASSWORD_FOR_GIVEN_STORE

So, you need to replace the lines in clienttest.txt file with corresponding value.

How To Build:
=============
The generated code uses a few NuGet Packages e.g., Newtonsoft.Json, UniRest.
The reference to these packages is already added as in the packages.config file.
If the automatic NuGet package restore is enabled, these dependencies will be
installed automatically. Therefore, you will need internet access for build.

    1. Open the solution (*.sln) file.
    2. Invoke the build process using "F6" key or "CTL+LSHIFT+B" shortcut.

How To Use:
===========
The build process generates a portable class library, which can be used like
a normal class library. The generated library is compatible with Windows Forms,
Windows RT, Windows Phone 8, Silverlight 5, Xamarin iOS, Xamarin Android and
Mono. More information on how to use can be found at the following link.

http://msdn.microsoft.com/en-us/library/vstudio/gg597391(v=vs.100).aspx

The quickest way to see how you should use the API and library itself, set the IM.API.ClientTest project as a StartUp project and debug it to see how the api is used.

Before you use any of the controllers you need to main client class with authorization parameters as shown below:

```
	InformationMachineAPIClient client = new InformationMachineAPIClient(clientId, clientSecret);
```

Then you can use controllers to call methods such as:

```
	ProductData productFull = client.Products.ProductsGetProduct("380728", true).Result;
    List<ProductData> kaleProducts = client.Products.ProductsGetProducts("Kale", null, 1, 25, true).Result;
```

All methods return wrapper object which contains meta information (number of available requests, maximum number of requests per day) and result object. Additionally if the result is of an array type, meta object will contain paging information (current page, items per page, total number of items, url to next page if there is a next page).

For more information on which methods are available please go to [Information Machine](https://www.iamdata.co/docs)