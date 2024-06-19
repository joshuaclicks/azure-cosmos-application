using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Helpers
{
    public static class ExtensionHelpers
    {
        public static string ToDescription(this Enum value)
        {
            FieldInfo? fieldInfo = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
