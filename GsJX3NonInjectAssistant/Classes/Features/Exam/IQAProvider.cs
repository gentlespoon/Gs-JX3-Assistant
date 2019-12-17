using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsJX3NonInjectAssistant.Classes.Features.Exam
{
    interface IQAProvider
    {
        public Task<List<QuestionAndAnswer>> Search(string keyword);
        public Task<List<QuestionAndAnswer>> Search(List<string> keywords);
    }
}
