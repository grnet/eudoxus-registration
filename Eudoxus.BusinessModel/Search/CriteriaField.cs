using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public class CriteriaField<T>
    {
        bool _isEmpty = true;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set { _isEmpty = value; }
        }

        T _FieldValue;
        public T FieldValue
        {
            get { return _FieldValue; }
            set
            {
                _FieldValue = value;
                IsEmpty = value == null;
            }
        }

        string _Operator = Criteria.Operators.Equals;
        public string Operator
        {
            get { return _Operator; }
            set
            {
                if (value == Criteria.Operators.Null || value == Criteria.Operators.NotNull)
                    IsEmpty = false;
                _Operator = value;
            }
        }
    }
}
