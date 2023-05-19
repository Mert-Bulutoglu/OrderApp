using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterAttribute : Attribute
    {
    }
}
