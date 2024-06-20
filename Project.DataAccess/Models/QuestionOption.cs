using Project.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccess.Models
{
    public class QuestionOption : IIdentityCollection
    {
        public string Id { get; set; } = null!;
        public string Option { get; set; } = null!;

    }
}
