﻿namespace DAL.misc.sql
{
    public class BucketListSqlV2
    {
        public const string GET_BUCKET_LIST = "select bli.ListItemName, "
                                                       + " bli.Created, "
                                                       + " bli.Category, "
                                                       + " bli.Achieved, "
                                                       + " bli.BucketListItemId, "
                                                       + " bli.Latitude, "
                                                       + " bli.Longitude "
                                                + " from Bucket.BucketListItem bli "
                                                + " inner join Bucket.BucketListUser blu on bli.BucketListItemId = blu.BucketListItemId "
                                                + " inner join [Bucket].[User] u on blu.UserId = u.UserId "
                                                + " where u.UserName = @userName  ";

        public const string GET_SYSTEM_STATISTICS = "SELECT top 2	[WebsiteIsUp], "
                                                    + "             [DatabaseIsUp], "
                                                    + "             [AzureFunctionIsUp], "
                                                    + "             [Created] "
                                                    + "from[Bucket].[SystemStatistics] ss "
                                                    + "order by ss.id desc";

        public const string GET_SYSTEM_BUILD_STATISTICS = "SELECT TOP 2	[Start], "
                                                        + " 			[End], "
                                                        + " 			[BuildNumber], "
                                                        + " 			[Status], "
                                                        + " 			[Type] "
                                                        + " FROM [Bucket].[BuildStatistics] "
                                                        + " order by id desc ";

        public const string DELETE_BUCKET_LIST_ITEM = "delete from Bucket.BucketListUser "
                                                    + " where BucketListItemId = @BucketListItemId "
                                                    + "  "
                                                    + " delete from Bucket.BucketListItem  "
                                                    + " where BucketListItemId = @BucketListItemId ";

        public const string UPSERT_BUCKET_LIST_ITEM = " declare @InsertDbId int "
                                                    + " declare @UserDbId int "
                                                    + " declare @CategorySortOrder int "
                                                    + "  "
                                                    + " if (select count(*)  "
                                                    + "           from [Bucket].[BucketListItem]  "
                                                    + " 		  where BucketListItemId = @BucketListItemId) > 0 "
                                                    + " begin  "
                                                    + " 	update [Bucket].[BucketListItem] "
                                                    + " 	set ListItemName = @ListItemName, "
                                                    + " 		Created = @Created, "
                                                    + " 		Category = @Category, "
                                                    + " 		Achieved = @Achieved, "
                                                    + "         Latitude = @Latitude, "
                                                    + "         Longitude = @Longitude "
                                                    + " 	where BucketListItemId = @BucketListItemId "
                                                    + " end "
                                                    + " else "
                                                    + " begin "
                                                    + " if @Category = 'Hot' "
                                                    + "     set @CategorySortOrder = 1 "
                                                    + "  else if @Category = 'Warm' "
                                                    + "     set @CategorySortOrder = 2 "
                                                    + "  else  "
                                                    + "      set @CategorySortOrder = 3 "
                                                    + "        "
                                                    + "  INSERT INTO[Bucket].[BucketListItem] "
                                                    + "       ([ListItemName], "
                                                    + "        [Created], "
                                                    + "        [Category], "
                                                    + "        [Achieved], "
                                                    + "        [CategorySortOrder], "
                                                    + "        [Latitude],  "
                                                    + "        [Longitude]) "
                                                    + "   VALUES "
                                                    + "        (@ListItemName, "
                                                    + "         @Created, "
                                                    + "         @Category, "
                                                    + "         @Achieved, "
                                                    + "         @CategorySortOrder, "
                                                    + "         @Latitude, "
                                                    + "         @Longitude) "
                                                    + "      "
                                                    + "     select @UserDbId = UserId "
                                                    + "     from [Bucket].[User] "
                                                    + "     where UserName = @UserName "
                                                    + "      "
                                                    + " 	SELECT @InsertDbId = SCOPE_IDENTITY()     "
                                                    + " 	insert into Bucket.BucketListUser "
                                                    + " 	select @InsertDbId, @UserDbId "
                                                    + " end ";

        public const string LOG_ACTION = "INSERT INTO [Bucket].[Log] select @LogMessage, getdate()";
        public const string LOG_BROWSER_CAPABIILITY = "INSERT INTO [Bucket].[BrowserCapability] "
                                                           + " ([BrowserLogId] "
                                                           + " ,[Key] "
                                                           + " ,[Value]) "
                                                     + " VALUES "
                                                           + " (@BrowserLogId, "
                                                           + "  @Key,"
                                                           + "  @Value) ";

        public const string SP_LOG_BROWSER = "[Bucket].[InsertBrowser]";

        public const string DELETE_USER = "delete from [Bucket].[User] where UserName = @userName";
        public const string DELETE_TEST_USER = "delete from [Bucket].[BucketListItem]   "
                                                + " where bucketlistitemid in (select bucketListItemId   "
                                                + "                            from [Bucket].[BucketListUser]   "
                                                + " 						   where userid in (select userid   "
                                                + " 						                    from [Bucket].[User]   "
                                                + " 										    where UserName = 'testUser')   "
                                                + " 						   )   "
                                                + "    "
                                                + " delete from [Bucket].[BucketListUser]   "
                                                + " where userid in (select userid   "
                                                + " 				from [Bucket].[User]   "
                                                + " 				where UserName = 'testUser')   "
                                                + "    "
                                                + " delete from [Bucket].[User]   "
                                                + " where UserName = 'testUser' ";
    }
}
