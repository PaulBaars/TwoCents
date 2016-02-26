using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace TopicSubscription
{
    class Program
    {
        static void Main(string[] args)
        {
            string topicName = "sb-salesorder-topic";
            string sbconnection = "Endpoint=sb://sb-twocents-ns.servicebus.windows.net/;SharedAccessKeyName=Admin;SharedAccessKey=mueFV8NQkt5MmSIMzbbXvBw4EAtXLntue/gLP7OfSEE=";
            var ns = NamespaceManager.CreateFromConnectionString(sbconnection);
            SqlFilter filter = new SqlFilter("Priority=1");
            ns.CreateSubscription(topicName, "salesorder-sub-priority", filter);

        }
    }
}
