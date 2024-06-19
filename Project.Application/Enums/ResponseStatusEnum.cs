using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Enums
{
    enum ResponseStatusEnum
    {
        [Description("Failed")]
        Failed = 1,

        [Description("Successful")]
        Successful = 2
    }
}
