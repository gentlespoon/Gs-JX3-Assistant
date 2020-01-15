using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsJX3NIA.Classes.Features.Exam
{
    interface IQAProvider
    {
        public Task<List<QuestionAndAnswer>> SearchAsync(string question);
        //public Task<List<QuestionAndAnswer>> Search(List<string> keywords);
    }
}
