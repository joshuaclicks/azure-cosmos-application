using Project.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccess.DataObjects
{
    public class QuestionResponseDto
    {
        public string QuestionId { get; set; } = null!;

        [NotMapped]
        public ICollection<QuestionOption> SelectedOptions { get; set; } = [];
        public string QuestionText { get; set; } = null!;
    }
}
