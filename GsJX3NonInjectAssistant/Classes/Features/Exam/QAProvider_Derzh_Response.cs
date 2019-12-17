using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsJX3NonInjectAssistant.Classes.Features.Exam
{
    public class QAProvider_Derzh_Response
    {
        public class Result
        {
            public string ques { get; set; }
            public string answ { get; set; }
        }

        public class Root
        {
            public string question { get; set; }
            public string code { get; set; }
            public List<Result> result { get; set; }
        }
    }
}
