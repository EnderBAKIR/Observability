using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            using var activity = ActivitySourceProvider.Source.StartActivity(System.Diagnostics.ActivityKind.Producer, name: "CustomMakeRequestToGoogle");

            try
            {
                //activity => Span
                var urlBody = "https://www.google.com";
                var eventTags = new ActivityTagsCollection();

                activity?.AddEvent(new("google a istek başladı", tags: eventTags));//eventler T anında görmek istediğiniz olayları tracede izlemek için eklenir.
                activity?.AddTag("request.url", urlBody);
                activity?.AddTag("request.schema", "https");
                activity?.AddTag("request.method", "get");

                

                var result = await httpclient.GetAsync(urlBody);
                
                var responseContent = await result.Content.ReadAsStringAsync();

                activity?.AddTag("response.lenght", responseContent.Length);

                eventTags.Add("google body Lenght", responseContent.Length);

                activity?.AddEvent(new("google a istek tamamlandı", tags: eventTags));

                var serviceTwo = new ServiceTwo();

                var fileLength = await serviceTwo.WriteToFile("Merhaba DÜnya");


                return responseContent.Length;
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error,ex.Message);
                return -1;
               
            }

            

        }

    }
}
