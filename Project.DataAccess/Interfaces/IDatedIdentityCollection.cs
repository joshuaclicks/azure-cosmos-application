using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccess.Interfaces
{
    public interface IDatedIdentityCollection : IIdentityCollection
    {
        public DateTime DateCreated { get; set; }
    }
}
