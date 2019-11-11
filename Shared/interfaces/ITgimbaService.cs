﻿using Shared.misc;
using System.Collections.Generic;
using Shared.dto;

namespace Shared.interfaces
{
    public interface ITgimbaService
    {
        string ProcessUser(string encodedUser, string encodedPass);
        bool ProcessUserRegistration(string encodedUserName, string encodedEmail, string encodedPassword);

        IList<Shared.dto.BucketListItem> GetBucketListItems(string encodedUserName, string encodedSortString, 
											string encodedToken, string encodedSrchString = "");
        bool UpsertBucketListItem(string encodedBucketListItems, string encodedUser, string encodedToken);
        bool DeleteBucketListItem(int bucketListDbId, string encodedUser, string encodedToken);
 
        IList<SystemStatistic> GetSystemStatistics();
        IList<SystemBuildStatistic> GetSystemBuildStatistics();
        void Log(string msg);
        string LoginDemoUser();
        string GetTestResult();
    }
}
