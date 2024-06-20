using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.ViewModels.Requests
{
    public class CandidateAnswerRequest
    {
        public string QuestionId { get; set; } = null!;
        public List<string> SelectedOptionIds { get; set; } = [];
        public string ResponseText { get; set; } = null!;
    }
}
