﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using AppFrame.Extensions;

namespace AppFrame.DataLayer
{
    public class LinqCriteria<T> 
    {
        private IList<Expression<Func<T, bool>>> _where;
        private IList<Expression<Func<T, bool>>> _order;
        private IList<string> _fetchProps;

        public LinqCriteria()
        {
            _where = new List<Expression<Func<T, bool>>>();
            _order = new List<Expression<Func<T, bool>>>();
            _fetchProps = new List<string>();
        }
        public void AddCriteria(Expression<Func<T, bool>> lambdaFunc)
        {
            _where.Add(lambdaFunc);
        }

        public void AddOrder(Expression<Func<T, bool>> lambdaFunc)
        {
            
            _order.Add(lambdaFunc);
        }
        
        public void AddFetchPath(string fetchProp)
        {
            _fetchProps.Add(fetchProp);
        }

        public void AddFetchPath<TK>(Expression<Func<T, TK>> fetchProp)
        {
            _fetchProps.Add(TypedExpandExtension.ProcessSelector(fetchProp));
        }

        public IList<Expression<Func<T, bool>>> Where
        {
            get
            {
                return _where;
            }
            set
            {
                _where = value;
            }
        }
        public IList<Expression<Func<T, bool>>> Order
        {
            get
            {
                return _order;
            }
            set
            {
                _order = value;
            }
        }

        public IList<string> FetchProps
        {
            get
            {
                return _fetchProps;
            }
            set
            {
                _fetchProps = value; 
            }
        }
    }
}
