﻿using System.Collections.Generic;

namespace Jericho.Core
{
    public class PagedList<T> : List<T>, IPagedList
    {
        public PagedList(IEnumerable<T> source, int index, int pageSize, int totalCount)
        {
            PagerInfo = new PagerInfo
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                PageIndex = index,
                TotalPages = totalCount / pageSize
            };

            if (PagerInfo.TotalCount % pageSize > 0)
                PagerInfo.TotalPages++;

            AddRange(source);
        }

        public PagerInfo PagerInfo { get; set; }
    }
}