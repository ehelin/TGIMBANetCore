using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedWeb = TgimbaNetCoreWebShared;
using TestsAPIIntegration;
using System;
using Shared.misc;									  

namespace TestTgimbaNetCoreWeb
{
    [TestClass]
    public class UtilitiesTest : BaseTest
    {			 																				   
		[TestMethod]
		public void Test_ConvertModelToStringArray()
		{					 
			var bucketListItemString = TestUtilities.GetBucketListItemSingleString("base64EncodedGoodUser", "testBucketLIstItem", null, true);
			var bucketListItemModel = TestUtilities.GetBucketListItemModel("base64EncodedGoodUser", "testBucketLIstItem", null, true);
									  
			var convertedBucketListItem = SharedWeb.Utilities.ConvertModelToString(bucketListItemModel);
		  
			Assert.AreEqual(bucketListItemString, convertedBucketListItem);	 	
		}

		[TestMethod]
		public void Test_ConvertStringArrayToModel()
		{
			var bucketListItemSingleLine = TestUtilities.GetBucketListItemSingleString("base64EncodedGoodUser", "testBucketLIstItem", null, true);
			string[] bucketListItemArray = new string[] { bucketListItemSingleLine };
			var bucketListItemModel = TestUtilities.GetBucketListItemModel("base64EncodedGoodUser", "testBucketLIstItem", null, true);
									  
			var convertedBucketListItem = SharedWeb.Utilities.ConvertStringArrayToModelList(bucketListItemArray);
	
			Assert.AreEqual(bucketListItemModel.Name, convertedBucketListItem[0].Name);	 
			Assert.AreEqual(bucketListItemModel.DateCreated, convertedBucketListItem[0].DateCreated);
			Assert.AreEqual(bucketListItemModel.BucketListItemType, convertedBucketListItem[0].BucketListItemType);
			Assert.AreEqual(bucketListItemModel.Completed, convertedBucketListItem[0].Completed);
			Assert.AreEqual(bucketListItemModel.Latitude, convertedBucketListItem[0].Latitude);
			Assert.AreEqual(bucketListItemModel.Longitude, convertedBucketListItem[0].Longitude);
			Assert.AreEqual(bucketListItemModel.DatabaseId, convertedBucketListItem[0].DatabaseId);
			//Assert.AreEqual(bucketListItemModel.UserName, convertedBucketListItem[0].UserName);
		}

		[TestMethod]
		public void Test_ConvertCategoryHotToEnum()
		{	
			var enumValue = SharedWeb.Utilities.ConvertCategoryToEnum("Hot");
	
			Assert.AreEqual(Enums.BucketListItemTypes.Hot, enumValue);
		}

		[TestMethod]
		public void Test_ConvertCategoryWarmToEnum()
		{	
			var enumValue = SharedWeb.Utilities.ConvertCategoryToEnum("Warm");
	
			Assert.AreEqual(Enums.BucketListItemTypes.Warm, enumValue);
		}

		[TestMethod]
		public void Test_ConvertCategoryColdToEnum()
		{	
			var enumValue = SharedWeb.Utilities.ConvertCategoryToEnum("Cool");
	
			Assert.AreEqual(Enums.BucketListItemTypes.Cold, enumValue);
		}

		[TestMethod]
		public void Test_ConvertCategoryBadInputToEnumThrowsException()
		{	
			try 
			{
				var enumValue = SharedWeb.Utilities.ConvertCategoryToEnum("UnrecognizedCategory");
				Assert.AreEqual(1, 2);	  // Fail if this line executes
			} 
			catch (Exception e) {
				Assert.IsNotNull(e);
				Assert.AreEqual("Unknown category: UnrecognizedCategory", e.Message);
			}
		}
    }
}
