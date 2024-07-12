using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandCommon
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [Serializable]
    public class RequiredQueryFieldAttribute : Attribute
    {
        public RequiredQueryFieldAttribute(string clause)
        {
            Clause = clause;
        }
        public string Clause { get; set; }
    }
}
