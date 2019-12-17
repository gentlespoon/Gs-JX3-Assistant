using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace GsJX3NonInjectAssistant.Classes.Features.Exam
{
    class QAProvider_LocalJSON : IQAProvider
    {

        private string jsonPath = "QA.json";
        private List<QuestionAndAnswer> QAs = new List<QuestionAndAnswer>();
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

        public async Task<List<QuestionAndAnswer>> Search(string keyword)
        {
            var QAs_kw = new List<QuestionAndAnswer>();
            foreach(var QA in QAs)
            {
                if (QA.Question.Contains(keyword))
                {
                    QAs_kw.Add(QA);
                }
            }
            return QAs_kw;
        }

        public async Task<List<QuestionAndAnswer>> Search(List<string> keywords)
        {
            Dictionary<QuestionAndAnswer, int> QAs_kws_all = new Dictionary<QuestionAndAnswer, int>();
            foreach (var keyword in keywords)
            {
                var QAs_kws = await Search(keyword);
                foreach (var QA in QAs_kws)
                {
                    if (QAs_kws_all.Keys.Contains(QA))
                    {
                        QAs_kws_all[QA]++;
                    }
                    else
                    {
                        QAs_kws_all.Add(QA, 1);
                    }
                }
            }

            var myList = QAs_kws_all.ToList();
            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            var resultList = new List<QuestionAndAnswer>();
            for (int i = myList.Count - 1; i >= 0; i--)
            {
                if (resultList.Count > 2)
                {
                    break;
                }
                resultList.Add(myList[i].Key);
            }


            return resultList;
        }

    }
}
