using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEE_dotNET.API
{
    //Move from APP.xaml.cs => option login plan in order to call this class's funtions
    public static class Publisher
    {
        //Request-Response
        public static void Initialize_Req_Res() 
        {
            Task.Run(() => 
            {
                using (var responder = new ResponseSocket())
                {
                    responder.Bind("tcp://*:5555");
                    while (true)
                    {
                        string str = responder.ReceiveFrameString();
                        Debug.WriteLine("Received {0}-{1}",str,DateTime.Now);
                        Task.Delay(1000);
                        responder.SendFrame("World");
                    }
                }
            });
        }
        //Subscriber --- use in sebcriber mode (Workstaion mode)
        public static void Initialize_Subcriber(string topic) 
        {
            Task.Run(() =>
            {
                using (var subscriber = new SubscriberSocket())
                {
                    subscriber.Connect("tcp://127.0.0.1:5556");
                    subscriber.Subscribe(topic);

                    while (true)
                    {
                        var topic = subscriber.ReceiveFrameString();
                        var msg = subscriber.ReceiveFrameString();
                        Debug.WriteLine("From Publisher: {0} {1}", topic, msg);
                    }
                }
            });
        }
        //Pubnlisher use in Plan option
        /// <summary>
        /// Publish message to subsrcibers in network (Workstation node)
        /// </summary>
        /// <param name="mQMessage"></param>
        public static async Task Publisher_MQ(MQMessage mQMessage) 
        {
            using (var publisher = new PublisherSocket())
            {
                publisher.Bind("tcp://*:5556");
                await Task.Delay(500);
                if (mQMessage!=null && mQMessage.Topic!=null && mQMessage.Content!=null) 
                {
                    publisher
                        .SendMoreFrame(mQMessage.Topic) // Topic
                        .SendFrame(mQMessage.Content); // Message 
                }
            }
        }
    }

    /// <summary>
    /// Object to create instance Message to workstaions node
    /// </summary>
    public class MQMessage
    {
        public string? Topic { get; set; }
        public string? Content { get; set; }
    }
}
