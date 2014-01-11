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
                OnConnected = newConnection,
                OnConnect = attemptingConnect,
                OnDisconnect = DisconnectedConnection
            };
            ws.Start();

        }

        private void ReceivedMsg(UserContext context)
        {
            string move = context.DataFrame.ToString();
            string[] XandY = move.Split(',');
            int mX = Int32.Parse(XandY[0]);
            int mY = Int32.Parse(XandY[1]);
            MouseController.moveMouse(mX, mY);
        }

        private void newConnection(UserContext context)
        {
            Console.WriteLine("ConnectionMade");
        }

        private void attemptingConnect(UserContext context)
        {
            Console.WriteLine(context.ClientAddress.ToString());
        }

        private void DisconnectedConnection(UserContext context)
        {
            Console.WriteLine(context.ClientAddress.ToString());
        }
    }
}
