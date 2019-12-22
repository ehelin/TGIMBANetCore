using System;
using HttpAPINetCore.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shared.dto.api;
using Shared.dto;
using Shared.interfaces;

namespace TestHttpAPINetCore_Unit
{
    [TestClass]
    public class BucketListTests : BaseTest
    {
        #region DeleteBucketListItem

        [TestMethod]
        public void DeleteBucketListItem_HappyPathTest()
        {
            var request = GetDeleteListItemRequest();

            IActionResult result = tgimbaApi.DeleteBucketListItem(request.EncodedUserName, request.EncodedToken, request.BucketListItemId);
            GoodResultVerify(result);
            tgimbaService.Verify(x => x.DeleteBucketListItem(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void DeleteBucketListItem_ValidationErrorTest()
        {
            var request = GetDeleteListItemRequest();

            validationHelper.Setup(x => x.IsValidRequest
                                        (It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                                            .Throws(new ArgumentNullException(""));

            IActionResult result = tgimbaApi.DeleteBucketListItem(request.EncodedUserName, request.EncodedToken, request.BucketListItemId);
            BadResultVerify(result);
            tgimbaService.Verify(x => x.DeleteBucketListItem(
                                        It.IsAny<int>(), It.IsAny<string>(), 
                                            It.IsAny<string>()), Times.Never);
        }
        
        [TestMethod]
        public void DeleteBucketListItem_GeneralErrorTest()
        {
            var request = GetDeleteListItemRequest();

            tgimbaService.Setup(x => x.DeleteBucketListItem(
                                        It.IsAny<int>(), It.IsAny<string>(),
                                            It.IsAny<string>()))
                                                .Throws(new Exception("I am an exception"));

            IActionResult result = tgimbaApi.DeleteBucketListItem(request.EncodedUserName, request.EncodedToken, request.BucketListItemId);
            BadResultVerify(result, 500);
        }

        #endregion

        #region GetBucketListItem

        [TestMethod]
        public void GetBucketListItem_HappyPathTest()
        {
            var request = GetBucketListItemRequest();

            IActionResult result = tgimbaApi.GetBucketListItem(request);
            GoodResultVerify(result);
            tgimbaService.Verify(x => x.GetBucketListItems(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void GetBucketListItem_ValidationErrorTest()
        {
            var request = GetBucketListItemRequest();

            validationHelper.Setup(x => x.IsValidRequest
                                        (It.IsAny<GetBucketListItemRequest>()))
                                            .Throws(new ArgumentNullException(""));

            IActionResult result = tgimbaApi.GetBucketListItem(request);
            BadResultVerify(result);
            tgimbaService.Verify(x => x.GetBucketListItems(It.IsAny<string>(), It.IsAny<string>(), 
                                                             It.IsAny<string>(), It.IsAny<string>()), 
                                                                Times.Never);
        }
        
        [TestMethod]
        public void GetBucketListItem_GeneralErrorTest()
        {
            var request = GetBucketListItemRequest();

            tgimbaService.Setup(x => x.GetBucketListItems(It.IsAny<string>(), It.IsAny<string>(),
                                                             It.IsAny<string>(), It.IsAny<string>()))
                                                                .Throws(new Exception("I am an exception"));

            IActionResult result = tgimbaApi.GetBucketListItem(request);
            BadResultVerify(result, 500);
        }

        #endregion

        #region UpsertBucketListItem

        [TestMethod]
        public void UpsertBucketListItem_HappyPathTest()
        {
            var request = GetUpsertRequest();

            IActionResult result = tgimbaApi.UpsertBucketListItem(request);
            GoodResultVerify(result);
            tgimbaService.Verify(x => x.UpsertBucketListItem(It.IsAny<BucketListItem>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void UpsertBucketListItem_ValidationErrorTest()
        {
            var request = GetUpsertRequest();
            
            validationHelper.Setup(x => x.IsValidRequest
                                        (It.IsAny<UpsertBucketListItemRequest>()))
                                            .Throws(new ArgumentNullException(""));

            IActionResult result = tgimbaApi.UpsertBucketListItem(request);
            BadResultVerify(result);
            tgimbaService.Verify(x => x.UpsertBucketListItem(It.IsAny<BucketListItem>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }


        [TestMethod]
        public void UpsertBucketListItem_GeneralErrorTest()
        {
            var request = GetUpsertRequest();

            tgimbaService.Setup(x => x.UpsertBucketListItem(It.IsAny<BucketListItem>(),
                                                             It.IsAny<string>(), It.IsAny<string>()))
                                                                .Throws(new Exception("I am an exception"));

            IActionResult result = tgimbaApi.UpsertBucketListItem(request);
            BadResultVerify(result, 500);
        }

        #endregion
    }
}
