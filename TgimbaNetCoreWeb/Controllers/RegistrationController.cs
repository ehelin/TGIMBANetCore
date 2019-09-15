using Microsoft.AspNetCore.Mvc;
using Shared.interfaces;		 							 		  
using TgimbaNetCoreWebShared;  	 					  
using TgimbaNetCoreWebShared.Models;
using TgimbaNetCoreWebShared.Controllers;

namespace TgimbaNetCoreWeb.Controllers
{
    #if !DEBUG
    [RequireHttpsAttribute]
    #endif
    public class RegistrationController	: Controller
    {
		private SharedRegistrationController sharedRegistrationController = null;

        public RegistrationController(ITgimbaService_Old service, IWebClient webClient)
        {
			sharedRegistrationController = new SharedRegistrationController(service, webClient);
		}	                   	
															   
		[HttpPost]
		public bool Registration(
			string user,  
			string email,
			string pass
		) {					
            return sharedRegistrationController.Registration(user, email, pass);
		} 
    }
}
