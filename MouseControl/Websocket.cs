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
            string message = context.DataFrame.ToString();
            if (message.Contains("left")) {
                MouseController.MouseLeft();
            }
            else {
                string move = context.DataFrame.ToString();
                string[] XandY = move.Split(',');
                int mX = Int32.Parse(XandY[0]);
                int mY = Int32.Parse(XandY[1]);

                int[] mousePos = MouseController.GetMousePosition();
                mX = mousePos[0] + mX;
                mY = mousePos[1] + mY;

                MouseController.moveMouse(mX, mY);
            }
        }

        private void newConnection(UserContext context)
        {
            
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
