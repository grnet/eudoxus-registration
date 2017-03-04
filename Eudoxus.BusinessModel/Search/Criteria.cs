using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace Eudoxus.BusinessModel
{
    public class Criteria
    {
        private bool _UsePaging = true;

        public List<string> Includes { get; set; }

        public bool UsePaging
        {
            get { return _UsePaging; }
            set { _UsePaging = value; }
        }
        public int MaximumRows { get; set; }
        public int StartRowIndex { get; set; }
        public string SortExpression { get; set; }

        public static class Operators
        {
            public new const string Equals = "=";
            public const string NotEquals = "!=";
            public const string NotNull = "IS NOT NULL";
            public const string Null = "IS NULL";
            public const string Or = "OR";
            public const string And = "AND";
        }
    }

    public class Criteria<T> : Criteria where T : EntityObject
    {
        public Criteria()
        {
            Expression = new Imis.Domain.EF.Search.Criteria<T>();
        }
        public Imis.Domain.EF.Search.Criteria<T> Expression { get; set; }
    }
}