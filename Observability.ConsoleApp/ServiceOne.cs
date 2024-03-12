using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Observability.ConsoleApp
{
    internal class ServiceOne
    {

        static HttpClient httpclient = new HttpClient(); // her yeni http client arka tarafda yeni socketler açar o yüzden static veya Singleton olarak yaşam döngüsü içerisinde oluşturmak önemlidir.

       internal async Task<int> MakeRequestToGoogle()
        {


            using var activity = ActivitySourceProvider.Source.StartActivity();

            var result = await httpclient.GetAsync("https://www.google.com");

            var responseContent = await result.Content.ReadAsStringAsync();


            return responseContent.Length;

        }

    }
}
