using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.Common.CustomeAttribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class PastOrPresentAttribute : ValidationAttribute
    {


        public override bool IsValid(object? value)
        {

            DateTime dateNow = DateTime.Now;
            long unixTime = ((DateTimeOffset)dateNow).ToUnixTimeMilliseconds();
            if ((long)value >= unixTime)
            {
                return false;
            }

            return true;
        }
    }
}
