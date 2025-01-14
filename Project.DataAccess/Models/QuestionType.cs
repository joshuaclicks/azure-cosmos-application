﻿using Project.DataAccess.Interfaces;

namespace Project.DataAccess.Models
{
    public class QuestionType : IIdentityCollection
    {
        public string Name { get; set; } = null!;
        public bool IsOptionTyped { get; set; }
        public bool IsDateTyped { get; set; }
        public bool IsTextTyped { get; set; }
        public bool IsMultiSelect { get; set; }
        public string Id { get; set; } = null!;
    }
}
