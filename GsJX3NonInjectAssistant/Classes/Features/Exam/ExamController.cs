using GsJX3NonInjectAssistant.Classes.HID.Display;
using GsJX3NonInjectAssistant.Classes.HID.Mouse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace GsJX3NonInjectAssistant.Classes.Features.Exam
{

    class ExamController
    {

        public ScreenCaptureConfiguration ScreenCaptureConfiguration = new ScreenCaptureConfiguration();

        private IDisplayHelper displayHelper;
        private IQAProvider qAProvider;

        public ExamController(IDisplayHelper displayHelper, IQAProvider qAProvider)
        {
            this.displayHelper = displayHelper;
            this.qAProvider = qAProvider;

        }

        public async Task<List<QuestionAndAnswer>> Search(List<string> keywords)
        {
            List<QuestionAndAnswer> QAs = await qAProvider.Search(keywords);
            return QAs;
        }

        public List<string> RunOCR(Bitmap bitmap_screenshot)
        {
            List<string> results = new List<string>();

            byte[] screenshot = Common.BitmapToByteArray(bitmap_screenshot);
            try
            {
                // initialize TesseractEngine
                using TesseractEngine engine = new TesseractEngine(@"./tessdata", "chi_sim", EngineMode.Default);

                // set image source
                using Pix img = Pix.LoadFromMemory(screenshot);
                using Page page = engine.Process(img);


                string text = page.GetText();
                using (var iter = page.GetIterator())
                {
                    iter.Begin();

                    do
                    {
                        do
                        {
                            do
                            {
                                var line = "";
                                do
                                {
                                    string keyword = iter.GetText(PageIteratorLevel.Word);
                                    line += keyword;
                                    //if (keyword.Length > 1)
                                    //{
                                    //    results.Add(keyword);
                                    //    Console.WriteLine(keyword);
                                    //}
                                } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                                line = line.Trim();
                                if (line.Length > 1)
                                {
                                    results.Add(line);
                                }

                            } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                        } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                    } while (iter.Next(PageIteratorLevel.Block));
                }

                return results.Distinct().ToList();
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                Console.WriteLine("Unexpected Error: " + e.Message);
                Console.WriteLine("Details: ");
                Console.WriteLine(e.ToString());
                throw e;
            }

        }


    }
}
