using Microsoft.AspNetCore.Mvc;
using Shared.interfaces;		 					  
using TgimbaNetCoreWebShared;  	 					  
using TgimbaNetCoreWebShared.Models;
using TgimbaNetCoreWebShared.Controllers;

namespace TgimbaNetCoreWeb.Controllers
{				 
	//[RequireHttpsAttribute]
    public class LoginController : Controller
    {
		private SharedLoginController sharedLoginController = null;

        public LoginController(ITgimbaService service, IWebClient webClient)
        {
			sharedLoginController = new SharedLoginController(service, webClient);
		}		  

        [HttpPost]
        public string Login([FromQuery] string encodedUser, string encodedPass)
        {						 
            return sharedLoginController.Login(encodedUser, encodedPass);
        }
    }
}
