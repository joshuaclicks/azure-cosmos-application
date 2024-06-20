using Project.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccess.Models
{
    public class Country : IIdentityCollection
    {
        public string Name { get; set; } = null!;
        public string ShortCode { get; set; } = null!;
        public string DialingCode { get; set; } = null!;
        public string FlagUrl { get; set; } = null!;
        public string Id { get; set; } = null!;
    }
}
