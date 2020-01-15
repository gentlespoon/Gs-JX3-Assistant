using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsJX3NIA.Classes.Features.Exam
{
    public class QuestionAndAnswer
    {
        public string Question { get; set; }
        public string Answer { get; set; }

        public QuestionAndAnswer(string q, string a)
        {
            Question = q;
            Answer = a;
        }
    }
}
