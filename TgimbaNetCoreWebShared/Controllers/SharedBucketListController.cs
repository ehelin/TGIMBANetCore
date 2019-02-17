﻿using Shared.interfaces;		
using TgimbaNetCoreWebShared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;  

namespace TgimbaNetCoreWebShared.Controllers
{
    public class SharedBucketListController : SharedBaseController
    {
		public SharedBucketListController(ITgimbaService service, IWebClient webClient)
            : base(service, webClient) { }

		[HttpPost]
		public bool AddBucketListItem(SharedBucketListModel model, string encodedUser, string encodedToken)
		{ 
			var itemAdded = webClient.AddBucketListItem(model, encodedUser, encodedToken);

			return itemAdded;
		}
	
		[HttpPost]
		public bool EditBucketListItem(SharedBucketListModel model, string encodedUser, string encodedToken)
		{ 
			var itemAdded = webClient.EditBucketListItem(model, encodedUser, encodedToken);

			return itemAdded;
		}
			   
		[HttpDelete]
		public bool DeleteBucketListItem(string dbIt, string encodedUser, string encodedToken)
		{ 
			var itemAdded = webClient.DeleteBucketListItem(dbIt, encodedUser, encodedToken);

			return itemAdded;
		}

		[HttpGet]
		public List<SharedBucketListModel> GetBucketListItems(string encodedUserName, string encodedSortString, string encodedToken) 
		{					
			var bucketListItem = webClient.GetBucketListItems(encodedUserName, encodedSortString, encodedToken);

			return bucketListItem;
		}
	}
}