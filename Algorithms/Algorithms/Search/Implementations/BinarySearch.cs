﻿using System;
using System.Collections.Generic;
using Shared.dto;
using Shared.misc;
using System.Linq;
using AlgorithmsUnit.Searching.models;

namespace Algorithms.Algorithms.Search.Implementations
{
    public class BinarySearch : ISearch
    {
        public Enums.SearchAlgorithms GetSearchingAlgorithm()
        {
            return Enums.SearchAlgorithms.Binary;
        }

        public IList<BucketListItem> Search(IList<BucketListItem> bucketListItems, string srchTerm)
        {
            IList<BucketListItem> matchingBucketListItems = null;
            var bucketListItemNameTerms = CreateBucketListItemIdTermArray(bucketListItems);
            var sortedBucketListItemNameTerms = SortBucketListItemNameTerms(bucketListItemNameTerms);
            var searchResult = SentenceBinarySearch(sortedBucketListItemNameTerms, srchTerm);

            if (searchResult.SearchTermFound)
            {
                var matchingIndexes = FindAdditionalMatches(sortedBucketListItemNameTerms, searchResult.Index, srchTerm);
                matchingBucketListItems = GetMatchingSentences(bucketListItems, matchingIndexes);
            }

            return matchingBucketListItems;
        }

        #region Private Methods

        private List<BucketListItemNameTerm> CreateBucketListItemIdTermArray(IList<BucketListItem> bucketListItems)
        {
            var sentenceTerms = new List<BucketListItemNameTerm>();

            var bucketListItemNameId = 0;
            foreach (var bucketListItem in bucketListItems)
            {
                var curSentenceTerms = bucketListItem.Name.Split(" ").ToList();

                foreach (var sentenceTerm in curSentenceTerms)
                {
                    sentenceTerms.Add(new BucketListItemNameTerm()
                    {
                        Term = sentenceTerm,
                        BucketListItemNameId = bucketListItemNameId
                    });
                }

                bucketListItemNameId++;
            }

            return sentenceTerms;
        }
        private List<BucketListItemNameTerm> SortBucketListItemNameTerms(List<BucketListItemNameTerm> sentenceTerms)
        {
            for (var outer = 0; outer < sentenceTerms.Count; outer++)
            {
                for (var inner = 0; inner < sentenceTerms.Count; inner++)
                {
                    if (inner + 1 >= sentenceTerms.Count) { break; }

                    var term1 = sentenceTerms[inner].Term;
                    var term2 = sentenceTerms[inner + 1].Term;
                    var term1CharVal = '0';
                    var term2CharVal = '0';

                    GetTermCharForComparison(term1, term2, out term1CharVal, out term2CharVal);

                    if (term1CharVal > term2CharVal)
                    {
                        var tmp = sentenceTerms[inner];
                        sentenceTerms[inner] = sentenceTerms[inner + 1];
                        sentenceTerms[inner + 1] = tmp;
                    }
                }
            }

            return sentenceTerms;
        }
        private SentenceBinarySearchResult SentenceBinarySearch(List<BucketListItemNameTerm> sortedSentenceTerms, string searchTerm)
        {
            var srchResult = new SentenceBinarySearchResult();

            int start = 0;
            int end = sortedSentenceTerms.Count - 1;

            while (start < end)
            {
                int mid = start + (end - start) / 2;
                var currentValue = sortedSentenceTerms[mid];

                if (currentValue.Term == searchTerm)
                {
                    srchResult.SearchTermFound = true;
                    srchResult.Index = mid;
                    break;
                }

                var currentTermChar = '0';
                var searchTermChar = '0';

                GetTermCharForComparison(currentValue.Term, searchTerm, out currentTermChar, out searchTermChar);
                if (currentTermChar < searchTermChar) { start = mid + 1; }
                if (currentTermChar > searchTermChar) { end = mid - 1; }
            }

            return srchResult;
        }
        private List<int> FindAdditionalMatches(List<BucketListItemNameTerm> sortedBucketListItemNameTerms, int fndIndex, string srchString)
        {
            var matchingIndexes = new List<int>();
            matchingIndexes.Add(sortedBucketListItemNameTerms[fndIndex].BucketListItemNameId);

            //search greater than index
            matchingIndexes = SearchIndexes(matchingIndexes, sortedBucketListItemNameTerms, fndIndex, srchString, true);

            //search less than index
            matchingIndexes = SearchIndexes(matchingIndexes, sortedBucketListItemNameTerms, fndIndex, srchString, false);

            return matchingIndexes;
        }
        private List<int> SearchIndexes(List<int> matchingIndexes, List<BucketListItemNameTerm> sortedBucketListItemNameTerms, int fndIndex, string srchString, bool greaterThan)
        {
            int ctr = greaterThan ? ++fndIndex : --fndIndex;

            while (true)
            {
                var curSortedSentenceTerm = sortedBucketListItemNameTerms[ctr];

                if (curSortedSentenceTerm.Term == srchString && !matchingIndexes.Contains(curSortedSentenceTerm.BucketListItemNameId))
                {
                    matchingIndexes.Add(curSortedSentenceTerm.BucketListItemNameId);
                }
                else
                {
                    break;
                }

                if (greaterThan && ctr >= sortedBucketListItemNameTerms.Count) { break; }
                if (!greaterThan && ctr <= 0) { break; }

                if (greaterThan) { ctr++; } else { ctr--; }
            }

            return matchingIndexes;
        }
        private List<BucketListItem> GetMatchingSentences(IList<BucketListItem> bucketListItems, List<int> matchingIndexes)
        {
            var matchingSentences = new List<BucketListItem>();

            for (int i = 0; i < bucketListItems.Count; i++)
            {
                if (matchingIndexes.Contains(i))
                {
                    matchingSentences.Add(bucketListItems[i]);
                }
            }

            return matchingSentences;
        }
        private void GetTermCharForComparison(string term1, string term2, out char term1Char, out char term2Char)
        {
            var charCountToIterate = term1.Length > term2.Length ? term2.Length : term1.Length;
            var ctr = 0;
            char term1CharTmp = '0';
            char term2CharTmp = '0';

            while (ctr < charCountToIterate)
            {
                term1CharTmp = Convert.ToChar(term1.Substring(ctr, 1).ToLower());
                term2CharTmp = Convert.ToChar(term2.Substring(ctr, 1).ToLower());

                if (term1CharTmp == term2CharTmp)
                {
                    ctr++;
                    continue;
                }
                else
                {
                    break;
                }
            }

            term1Char = term1CharTmp;
            term2Char = term2CharTmp;
        }

        #endregion
    }
}
