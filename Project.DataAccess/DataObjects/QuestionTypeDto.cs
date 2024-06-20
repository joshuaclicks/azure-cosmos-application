using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccess.DataObjects
{
    public class QuestionTypeDto
    {
        public string Name { get; set; } = null!;
        public bool IsOptionTyped { get; set; }
        public bool IsDateTyped { get; set; }
        public bool IsTextTyped { get; set; }
        public bool IsMultiSelect { get; set; }
        public string Id { get; set; } = null!;
    }
}
