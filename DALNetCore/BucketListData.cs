﻿using System;
using System.Collections.Generic;
using Shared.dto;
using Shared.interfaces;
using models = DALNetCore.Models;
using System.Linq;

namespace DALNetCore
{
    public class BucketListData : IBucketListData
    {
        private BucketListContext context = null;

        public BucketListData(BucketListContext context) {
            this.context = context;
        }

        public void AddToken(int userId, string token)
        {
            var dbUser = this.context.User
                                   .Where(x => x.UserId == userId)
                                   .FirstOrDefault();
            dbUser.Token = token;

            this.context.Update(dbUser);
            this.context.SaveChanges();
        }
        
        public void LogMsg(string msg)
        {
            var logModel = new models.Log
            {
                LogMessage = msg,
                Created = DateTime.UtcNow
            };
            this.context.Log.Add(logModel);
            this.context.SaveChanges();
        }

        public User GetUser(int id)
        {
            var dbUser = this.context.User
                                   .Where(x => x.UserId == id)
                                   .FirstOrDefault();
            var user = new User()
            {
                UserId = dbUser.UserId,
                UserName = dbUser.UserName,
                Salt = dbUser.Salt,
                Password = dbUser.PassWord,
                Email = dbUser.Email,
                Token = dbUser.Token
            };

            return user;
        }

        public int AddUser(User user)
        {
            var dbUser = new models.User
            {
                UserName = user.UserName,
                Email = user.Email,
                PassWord = user.Password,
                Salt = user.Salt
            };
            this.context.User.Add(dbUser);
            this.context.SaveChanges();

            return dbUser.UserId;
        }

        public void DeleteUser(int userId)
        {
            var dbUser = this.context.User
                                   .Where(x => x.UserId == userId)
                                   .FirstOrDefault();

            this.context.Remove(dbUser);
            this.context.SaveChanges();
        }

        public IList<SystemBuildStatistic> GetSystemBuildStatistics()
        {
            var buildStatistics = this.context.BuildStatistics.ToList();
            var systemBuildStatics = new List<SystemBuildStatistic>();

            foreach (var buildStatistic in buildStatistics) 
            {
                var systemBuildStatistic = new SystemBuildStatistic
                {
                    Start = buildStatistic.Start.ToString(),
                    End = buildStatistic.End.ToString(),
                    BuildNumber = buildStatistic.BuildNumber,
                    Status = buildStatistic.Status
                };

                systemBuildStatics.Add(systemBuildStatistic);
            }

            return systemBuildStatics;
        }

        public IList<SystemStatistic> GetSystemStatistics()
        {
            var systemStatistics = this.context.SystemStatistics.ToList();
            var systemSystemStatics = new List<SystemStatistic>();

            foreach (var systemStatistic in systemStatistics)
            {
                var systemSystemStatistic = new SystemStatistic
                {
                    WebSiteIsUp = systemStatistic.WebsiteIsUp,
                    DatabaseIsUp = systemStatistic.DatabaseIsUp,
                    AzureFunctionIsUp = systemStatistic.AzureFunctionIsUp,
                    Created = systemStatistic.Created.ToString()
                };

                systemSystemStatics.Add(systemSystemStatistic);
            }

            return systemSystemStatics;
        }
        
        public void UpsertBucketListItem(Shared.dto.BucketListItem bucketListItem, string userName)
        {
            var bucketListItemToSave = new models.BucketListItem
            {
                ListItemName = bucketListItem.Name,
                Created = bucketListItem.Created,
                Category = bucketListItem.Category,
                Achieved = bucketListItem.Achieved,
                Latitude = bucketListItem.Latitude,
                Longitude = bucketListItem.Longitude
            };

            this.context.BucketListItem.Add(bucketListItemToSave);
            this.context.SaveChanges();

            var user = this.context.User
                                .Where(x => x.UserName == userName)
                                .FirstOrDefault();
            var bucketListItemUser = new models.BucketListUser
            {
                BucketListItemId = bucketListItemToSave.BucketListItemId,
                UserId = user.UserId
            };

            this.context.BucketListUser.Add(bucketListItemUser);
            this.context.SaveChanges();
        }

        public IList<Shared.dto.BucketListItem> GetBucketList(string userName, string sortString, string srchTerm = "")
        {
            throw new NotImplementedException();
        }
        
        public void DeleteBucketListItem(int bucketListItemDbId)
        {
            throw new NotImplementedException();
        }
    }
}
