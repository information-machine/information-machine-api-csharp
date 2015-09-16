/*
 * InformationMachineAPI.PCL
 *
 * 
 */
using System;

namespace InformationMachineAPI.PCL
{
    public partial class Configuration
    {
        //The base Uri for API calls
        public static string BaseUri = "https://api.iamdata.co";

        //Id of your app
        //TODO: Replace the ClientId with an appropriate value
        public static string ClientId = "";

        //Secret key which authorizes you to use this API
        //TODO: Replace the ClientSecret with an appropriate value
        public static string ClientSecret = "";

    }
}