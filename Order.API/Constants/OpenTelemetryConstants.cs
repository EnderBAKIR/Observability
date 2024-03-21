using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.API.Controllers.Constants
{
    internal class OpenTelemetryConstants
    {
        internal const string ServiceName = "Order.API"; //"CompanyName.AppName.ComponentName" 
        internal const string ServiceVersion = "1.0.0";
        internal const string ActiviySourceName = "ActivitySource.Order.API";
    }
}
