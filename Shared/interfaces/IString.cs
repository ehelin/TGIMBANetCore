﻿using Shared.dto;

namespace Shared.interfaces
{
    public interface IString
    {
        string DecodeBase64String(string val);
        string EncodeBase64String(string val);
        BucketListItem GetBucketListItem(string[] bucketListItemArray);
    }
}
