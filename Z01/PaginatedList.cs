using System;
using System.Collections.Generic;
using System.Linq;

namespace Z01
{
    public class PaginatedList<T> : List<T>
        {
            public int PageIndex { get; private set; }
            public int TotalPages { get; private set; }

            public PaginatedList(List<T> items, int pageIndex, int pageSize)
            {
                PageIndex = pageIndex;
                TotalPages = (int)Math.Ceiling(items.Count / (double)pageSize);

                this.AddRange(items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
                Console.WriteLine("Paginated List " + this.First());
            }

            public bool HasPreviousPage
            {
                get
                {
                    return (PageIndex > 1);
                }
            }

            public bool HasNextPage
            {
                get
                {
                    return (PageIndex < TotalPages);
                }
            }
        }
}