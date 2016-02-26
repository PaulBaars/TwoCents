using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace TopicSender
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                //ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http;

                string topicName = "sb-salesorder-topic";
                string sbconnection = "Endpoint=sb://sb-twocents-ns.servicebus.windows.net/;SharedAccessKeyName=Sender;SharedAccessKey=TzYnAEaXHAP3dVJ0J/knLc+2+99C/E2ytbo8qDJQ+TI=";
                MessagingFactory factory = MessagingFactory.CreateFromConnectionString(sbconnection);
                TopicClient client = factory.CreateTopicClient(topicName);

                string postBody = "{'ServiceNumber': 'TST100', 'AddressCode': 'HAG', 'ServiceContractNumber': 'SOC920001', Description': 'Testmelding'}";

                BrokeredMessage msg = new BrokeredMessage(postBody);
                msg.Properties["Priority"] = 1;

                client.Send(msg);

                msg = null;

                Console.WriteLine("Press Enter");
                Console.Read();

            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Error: {0}", ex.ToString()));
                Console.Read();
            }

        }

    }
}
