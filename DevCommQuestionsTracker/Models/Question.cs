using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCommQuestionsTracker.Models
{
    public class Question
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime PostedDate { get; set; }

        public DateTime ResolvedDate { get; set; }

        public QuestionType Type { get; set; }

        public QuestionSubType SubType { get; set; }

        // Take this from predefined list.
        public string Forum { get; set; }

        public Status Status { get; set; }

        // Take this from predefined list.
        public string Module { get; set; }

        public string AssignedTo { get; set; }

        public string Comment { get; set; }
    }
}
