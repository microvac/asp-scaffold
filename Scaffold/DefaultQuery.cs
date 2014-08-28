﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
    public class DefaultQuery<TModel>: IQuery<TModel>
    {
        public int? PageBegin { get; set; }
        public int? PageLength { get; set; }
        public String SortField { get; set; }
        public String SortOrder { get; set; }

        public IQueryable<TModel> Sort(IQueryable<TModel> query)
        {
            if (string.IsNullOrWhiteSpace(SortField))
                SortField = "ID";
            if (SortOrder != "ASC" || SortOrder != "DESC")
                SortOrder = "ASC";
            return query.OrderBy(SortField.Trim()+" "+SortOrder.Trim());
        }

        public IQueryable<TModel> Page(IQueryable<TModel> query)
        {
            if (PageBegin.HasValue && PageBegin > 0)
                query = query.Skip(PageBegin.Value);
            if (PageLength.HasValue && PageLength > 0)
                query = query.Take(PageLength.Value);
            return query;
        }

        public IQueryable<TModel> Filter(IQueryable<TModel> query)
        {
            return query;
        }
    }
}