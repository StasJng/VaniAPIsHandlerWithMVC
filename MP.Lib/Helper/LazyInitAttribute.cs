using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MP.Lib.Helper
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]    
    public class LazyInitAttribute : System.Attribute
    {
        private bool _isLazyInit;
        public bool IsLazyInit
        {
            get { return this._isLazyInit; }
        }
        public LazyInitAttribute(bool _lazyInit)
        {
            this._isLazyInit = _lazyInit;
        }
    }

    [AttributeUsageAttribute(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class LazyInitRequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
    {
        private bool _isLazyInit;
        public bool IsLazyInit
        {
            get { return this._isLazyInit; }
        }
        public LazyInitRequiredAttribute(bool _lazyInit)
        {
            this._isLazyInit = _lazyInit;
        }
    }
}
