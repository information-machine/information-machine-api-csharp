/*
 * InformationMachineAPI.PCL
 *
 * 
 */
using System;

using InformationMachineAPI.PCL.Http.Client;

namespace InformationMachineAPI.PCL.Controllers
{
	public partial class BaseController
    {
        internal IHttpClient ClientInstance = null;

        public BaseController()
        {
            ClientInstance = UnirestClient.SharedClient;
        }

        public BaseController(IHttpClient client)
        {
            ClientInstance = client;
        }

    }
} 