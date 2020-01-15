using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;


namespace GsJX3NIA.Classes.Features.Exam
{
    class QAProvider_Derzh : IQAProvider
    {

        private static readonly HttpClient httpClient = new HttpClient();
        private const string baseURL = "https://jx3.derzh.com/exam/?m=1&csrf=&q=";

        public QAProvider_Derzh()
        {
            httpClient.DefaultRequestHeaders.Add("accept", "application/json");
        }

        public async Task<List<QuestionAndAnswer>> SearchAsync(string keyword)
        {
            List<QuestionAndAnswer> QAs = new List<QuestionAndAnswer>();

            var query = $"{baseURL}{keyword}";
            
            Console.WriteLine($"Sending Request: {query}");
            
            var responseString = await httpClient.GetStringAsync(query);
            
            Console.WriteLine($"Response:");
            Console.WriteLine(responseString);

            QAProvider_Derzh_Response.Root result_root = JsonConvert.DeserializeObject<QAProvider_Derzh_Response.Root>(responseString);
            List<QAProvider_Derzh_Response.Result> results = result_root.result;
            foreach (var result in results)
            {
                QAs.Add(new QuestionAndAnswer(result.ques, result.answ));
            }

            return QAs;
        }

        public async Task<List<QuestionAndAnswer>> Search(List<string> keywords)
        {
            var QAs = new List<QuestionAndAnswer>();
            foreach(var keyword in keywords)
            {
                var QAs_kw = await SearchAsync(keyword);
                foreach (var QA in QAs_kw)
                {
                    QAs.Add(QA);
                }
            }

            return QAs.Distinct().ToList();
        }
    }
}
