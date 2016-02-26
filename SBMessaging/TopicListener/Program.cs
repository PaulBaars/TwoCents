using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace TopicListener
{
    class Program
    {
        static void Main(string[] args)
        {

            //ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http;

            string topicName = "sb-salesorder-topic";
            string subscription = "salesorder-sub-priority";
            string sbconnection = "Endpoint=sb://sb-twocents-ns.servicebus.windows.net/;SharedAccessKeyName=Receiver;SharedAccessKey=yugvPBtkSMHv4jOm38ZQrRRiTAndsP4YOs/CepXK5dE=";

            MessagingFactory factory = MessagingFactory.CreateFromConnectionString(sbconnection);
            SubscriptionClient client = factory.CreateSubscriptionClient(topicName, subscription);

            Console.WriteLine("Start");


            //while (true)
            //{
            try
            {
                BrokeredMessage msg = client.Receive();

                if (msg != null)
                {
                    try
                    {
                        Console.WriteLine(String.Format("MessageId: {0}", msg.MessageId));
                        Console.WriteLine(String.Format("Priority: {0}", msg.Properties["Priority"]));
                        Console.WriteLine(String.Format("Body: {0}", msg.GetBody<string>()));
                        msg.Complete();

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.ToString());
                        msg.Abandon();
                    }
                }
                else
                {
                    msg.Abandon();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            //}

            Console.WriteLine("Press Enter");
            Console.Read();

        }
    }
}

