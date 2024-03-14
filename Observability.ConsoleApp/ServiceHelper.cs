using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observability.ConsoleApp
{
    internal class ServiceHelper
    {
        internal async Task Work1()
        {

            using var activity = ActivitySourceProvider.Source.StartActivity();


            var serviceOne = new ServiceOne();
            activity.SetTag("work 1 tag", "work 1 tag value");
            activity.AddEvent(new ActivityEvent("work 1 event"));// eventleri bir log gibi değilde bir olayın durumunu belirtmek için kullanmak daha iyi. örneğin : Sipariş Tamamlandı gibi.
            Console.WriteLine($"google response lenght: {await serviceOne.MakeRequestToGoogle()}");

            Console.WriteLine("Work1 tamamlandı.");

        }
    }
}
