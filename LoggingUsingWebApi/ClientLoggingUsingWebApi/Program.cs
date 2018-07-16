using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace ClientLoggingUsingWebApi
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                throw new Exception("Divide By Zero Exception", new DivideByZeroException());
            }
            catch (Exception ex)
            {
                LoggingGetInfoAfterLoggingToFile(ex.Message).Wait();

                LoggingGetInfoAfterLoggingToEventViewer(ex.Message).Wait();

                LoggingGetInfoUsingFileStream(ex.Message).Wait();
                Console.WriteLine("Press any key to close the application");
                Console.ReadKey();
            }
        }

        static async Task LoggingGetInfoAfterLoggingToFile(string exception)
        {
            HttpClient cons = new HttpClient();
            cons.BaseAddress = new Uri("http://localhost:62292/");
            cons.DefaultRequestHeaders.Accept.Clear();
            cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            using (cons)
            {
                HttpResponseMessage res = await cons.GetAsync("api/Logging/?exception=" + exception);
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    Console.WriteLine("successfully logged");
                    Console.ReadLine();
                }
            }
        }

        static async Task LoggingGetInfoAfterLoggingToEventViewer(string exception)
        {
            HttpClient cons = new HttpClient();
            cons.BaseAddress = new Uri("http://localhost:62292/");
            cons.DefaultRequestHeaders.Accept.Clear();
            cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            using (cons)
            {
                HttpResponseMessage res = await cons.GetAsync("api/Logging/?message=" + exception + "&source=EventSource");
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    Console.WriteLine("successfully logged");
                    Console.ReadLine();
                }
            }
        }

        static async Task LoggingGetInfoUsingFileStream(string exception)
        {
            HttpClient cons = new HttpClient();
            cons.BaseAddress = new Uri("http://localhost:62292/");
            cons.DefaultRequestHeaders.Accept.Clear();
            cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            using (cons)
            {
                HttpResponseMessage res = await cons.GetAsync("api/Logging/?strFileName=Error&strMessage=" + exception);
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    Console.WriteLine("successfully logged");
                    Console.ReadLine();
                }
            }
        }
    }
}
