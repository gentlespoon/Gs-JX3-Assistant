using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace GsJX3NIA.Classes.Features.Exam
{
    class QAProvider_LocalJSON : IQAProvider
    {
        private readonly string _name = "本地JSON";
        private string jsonPath = "QA.json";
        private List<QuestionAndAnswer> QAs = new List<QuestionAndAnswer>();

        public string Name { get { return _name; } }

        public QAProvider_LocalJSON()
        {
            using (StreamReader r = new StreamReader(jsonPath))
            {
                string json = r.ReadToEnd();
                List<string[]> items = JsonConvert.DeserializeObject<List<string[]>>(json);

                foreach(var QA in items)
                {
                    QAs.Add(new QuestionAndAnswer(QA[0], QA[1]));
                }
                QAs = QAs.Distinct().ToList();
            }

        }

        
        public async Task<List<QuestionAndAnswer>> SearchAsync(string question)
        {
            // dictionary<index_of_QAs, matching_score>
            Dictionary<QuestionAndAnswer, int> MatchedQAs = new Dictionary<QuestionAndAnswer, int>();
            
            question = question.Replace("单选题：", "").Replace("单选题:", "");

            // Question too short. Skip search and return empty set;
            if (question.Length < 2)
            {
                return new List<QuestionAndAnswer>();
            }
            
            Random random = new Random();
            int rInt;
            string keyword;
            int kw_length = 2;

            Task<Dictionary<QuestionAndAnswer, int>> searchTask = Task.Run(() =>
            {
                Dictionary<QuestionAndAnswer, int> MatchedQAs = new Dictionary<QuestionAndAnswer, int>();
                // each question
                foreach (QuestionAndAnswer QA in QAs)
                {

                    // randomly get some keywords (short)
                    
                    for (int i_kw = 0; i_kw < 64; i_kw++)
                    {
                        rInt = random.Next(0, question.Length - kw_length);
                        keyword = question.Substring(rInt, kw_length);
                        if (QA.Question.Contains(keyword))
                        {
                            MatchedQAs[QA] = MatchedQAs.ContainsKey(QA) ? MatchedQAs[QA] + 1 : 1;
                        }
                    }
                }
                return MatchedQAs;
            });


            MatchedQAs = await searchTask;
            

            var result = MatchedQAs.OrderByDescending(QA => QA.Value).ToList();

            int i = 0;
            List<QuestionAndAnswer> output = new List<QuestionAndAnswer>();

            while (i < 3 && i < result.Count)
            {
                output.Add(result[i].Key);
                i++;
            }

            return output;
        }

    }
}
