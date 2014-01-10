using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alchemy;
using Alchemy.Classes;

namespace MouseControl
{
    class Websocket
    {
        private WebSocketServer ws;
        public Websocket()
        {
            ws = new WebSocketServer(9000, System.Net.IPAddress.Any)
            {
                OnReceive = ReceivedMsg,
                OnConnected = newConnection
            };

            ws.Start();

        }

        private void ReceivedMsg(UserContext context)
        {
           Console.WriteLine(context.DataFrame.ToString());
        }

        private void newConnection(UserContext context)
        {
            Console.WriteLine("ConnectionMade");
        }
    }
}
