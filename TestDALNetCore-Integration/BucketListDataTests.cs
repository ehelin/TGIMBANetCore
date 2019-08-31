using DALNetCore;
using DALNetCore.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.interfaces;
using Shared.misc;
using System;
using System.Linq;
using dto = Shared.dto;

namespace TestDALNetCore_Integration
{
    [TestClass]
    public class HappyPathTests
    {
        [TestMethod]
        public void UserHappyPath_Test()
        {
            var token = "token";
            var user = GetUser(token);

            var dbContext = new BucketListContext();
            IBucketListData bd = new BucketListData(dbContext);

            var userId = bd.AddUser(user);
            bd.AddToken(userId, token);

            var savedUser = bd.GetUser(userId);

            Assert.AreEqual(user.UserName, savedUser.UserName);
            Assert.AreEqual(user.Password, savedUser.Password);
            Assert.AreEqual(user.Salt, savedUser.Salt);
            Assert.AreEqual(user.Email, savedUser.Email);
            Assert.AreEqual(token, savedUser.Token);

            bd.DeleteUser(savedUser.UserId);
        }

        [TestMethod]
        public void GetSystemBuildStatisticsHappyPath_Test()
        {
            //set up ------------------------------------------------------
            var dbContext = new BucketListContext();
            var now = DateTime.Now;
            var buildStatisticsToSave = new BuildStatistics
            {
                Start = now,
                End = now.AddMinutes(1),
                BuildNumber = "123",
                Status = "Succeeded",
                Type = "CICD Pipeline - Website"
            };            
            dbContext.BuildStatistics.Add(buildStatisticsToSave);
            dbContext.SaveChanges();

            //test ---------------------------------------------------------
            IBucketListData bd = new BucketListData(dbContext);
            var buildStatistics = bd.GetSystemBuildStatistics();

            Assert.IsNotNull(buildStatistics);
            var buildStatistic = buildStatistics
                                    .OrderByDescending(x => Convert.ToDateTime(x.Start))
                                    .FirstOrDefault();
            Assert.AreEqual(buildStatistic.Start, buildStatisticsToSave.Start.ToString());
            Assert.AreEqual(buildStatistic.End, buildStatisticsToSave.End.ToString());
            Assert.AreEqual(buildStatistic.BuildNumber, buildStatisticsToSave.BuildNumber);
            Assert.AreEqual(buildStatistic.Status, buildStatisticsToSave.Status);

            //clean up ------------------------------------------------------
            dbContext.Remove(buildStatisticsToSave);
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void GetSystemSystemStatisticsGetSystemBuildStatisticsHappyPath_Test()
        {        
            //set up ------------------------------------------------------
            var dbContext = new BucketListContext();
            var now = DateTime.Now;
            var systemStatisticsToSave = new SystemStatistics
            {
                WebsiteIsUp = true,
                DatabaseIsUp = true,
                AzureFunctionIsUp = true,
                Created = now
            };
            dbContext.SystemStatistics.Add(systemStatisticsToSave);
            dbContext.SaveChanges();

            //test ---------------------------------------------------------
            IBucketListData bd = new BucketListData(dbContext);
            var systemStatistics = bd.GetSystemStatistics();

            Assert.IsNotNull(systemStatistics);
            var systemStatistic = systemStatistics
                                    .OrderByDescending(x => Convert.ToDateTime(x.Created))
                                    .FirstOrDefault();
            Assert.AreEqual(systemStatistic.WebSiteIsUp, systemStatisticsToSave.WebsiteIsUp);
            Assert.AreEqual(systemStatistic.DatabaseIsUp, systemStatisticsToSave.DatabaseIsUp);
            Assert.AreEqual(systemStatistic.AzureFunctionIsUp, systemStatisticsToSave.AzureFunctionIsUp);
            Assert.AreEqual(systemStatistic.Created, systemStatisticsToSave.Created.ToString());

            //clean up ------------------------------------------------------
            dbContext.Remove(systemStatisticsToSave);
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void LogMsgHappyPath_Test()
        {
            var dbContext = new BucketListContext();
            IBucketListData bd = new BucketListData(dbContext);

            //test ----------------------------
            var msg = "I am a log message";
            bd.LogMsg(msg);

            var logModel = dbContext.Log
                                    .Where(x => x.LogMessage == msg)
                                    .FirstOrDefault();

            Assert.IsNotNull(logModel);
            Assert.AreEqual(msg, logModel.LogMessage);

            //clean up ------------------------------------------------------
            dbContext.Remove(logModel);
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void BucketListItemHappyPath_Test()
        {
            // set up ------------------------------------------------------
            var user = GetUser("token");
            var dbContext = new BucketListContext();
            IBucketListData bd = new BucketListData(dbContext);
            var bucketListItemToSave = GetBucketListItem();

            // test ---------------------------------------------------------
            var userId = bd.AddUser(user);
            bd.UpsertBucketListItem(bucketListItemToSave, user.UserName);
            var savedBucketListItem = bd.GetBucketList(user.UserName, "").FirstOrDefault();
            
            // we have a saved object that object matches our created object
            Assert.IsNotNull(savedBucketListItem);
            Assert.AreEqual(bucketListItemToSave.Name, savedBucketListItem.Name);
            Assert.AreEqual(bucketListItemToSave.Created, savedBucketListItem.Created);
            Assert.AreEqual(bucketListItemToSave.Category, savedBucketListItem.Category);
            Assert.AreEqual(bucketListItemToSave.Achieved, savedBucketListItem.Achieved);
            Assert.AreEqual(bucketListItemToSave.Latitude, savedBucketListItem.Latitude);
            Assert.AreEqual(bucketListItemToSave.Longitude, savedBucketListItem.Longitude);

            // we can update that object and save it
            // TODO - upsert update part not working...fix
            savedBucketListItem.Name = savedBucketListItem.Name + " modified";
            bd.UpsertBucketListItem(savedBucketListItem, user.UserName);
            var savedBucketListItemUpdated = bd.GetBucketList(user.UserName, "").FirstOrDefault();
            Assert.AreEqual(savedBucketListItem.Name, savedBucketListItemUpdated.Name);

            // we can delete the bucket list item
            bd.DeleteBucketListItem(savedBucketListItemUpdated.Id);
            var deletedBucketListItem = bd.GetBucketList(user.UserName, "").FirstOrDefault();
            Assert.IsNotNull(savedBucketListItem);

            //clean up user
            bd.DeleteUser(userId);
        }

        [TestMethod]
        public void BucketListItemSortHappyPath_Test()
        {
            // TODO - complete a way to specify specific columns EF 6 will understand

            // set up ------------------------------------------------------
            var user = GetUser("token");
            var dbContext = new BucketListContext();
            IBucketListData bd = new BucketListData(dbContext);

            var userId = bd.AddUser(user);
            bd.UpsertBucketListItem(GetBucketListItem("Bucket List Item 1"), user.UserName);
            bd.UpsertBucketListItem(GetBucketListItem("Bucket List Item 2"), user.UserName);
            bd.UpsertBucketListItem(GetBucketListItem("Bucket List Item 3"), user.UserName);

            // test ---------------------------------------------------------
            //asc 
            var savedBucketListItems = bd.GetBucketList(user.UserName, "Name", true, "");
            Assert.IsNotNull(savedBucketListItems);
            Assert.AreEqual(savedBucketListItems.FirstOrDefault().Name, "Bucket List Item 1");

            if (savedBucketListItems != null && savedBucketListItems.Count > 0 );
            {
                savedBucketListItems.Clear();
                savedBucketListItems = null;
            }

            //desc 
            savedBucketListItems = bd.GetBucketList(user.UserName, "Name", false, "");
            Assert.IsNotNull(savedBucketListItems);
            Assert.AreEqual(savedBucketListItems.FirstOrDefault().Name, "Bucket List Item 3");

            //clean up
            foreach(var savedBucketListItem in savedBucketListItems) 
            {
                bd.DeleteBucketListItem(savedBucketListItem.Id);
            }
            bd.DeleteUser(userId);
        }

        #region Private Methods

        private dto.BucketListItem GetBucketListItem(string name = "I am a bucket list item")
        {
            var bucketListItem = new dto.BucketListItem
            {
                Name = name,
                Created = DateTime.Now,
                Category = Enums.BucketListItemTypes.Hot.ToString(),
                Achieved = false,
                Latitude = (decimal)81.12,
                Longitude = (decimal)41.34
            };

            return bucketListItem;
        }

        private dto.User GetUser(string token) 
        {
            var user = new dto.User()
            {
                UserName = "user",
                Salt = "salt",
                Password = "password",
                Email = "user@email.com",
                Token = token
            };

            return user;
        }

        #endregion
    }
}
