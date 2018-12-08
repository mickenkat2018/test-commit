using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Loan.Application.Common.Framework
{
    public class CommonMethods
    {
        public string GetAppConfigPropertyValue(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                var reader = new AppSettingsReader();
                return (string)reader.GetValue(propertyName, typeof(string));
            }

            return string.Empty;
        }
    }
}
