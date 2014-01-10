using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace MouseControl
{
    class HTTPServer
    {
        private string folder;

        public HTTPServer(string FileFolder)
        {
            folder = FileFolder;
            Console.WriteLine("Creating Web server");
            
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Prefixes.Add("http://" + getIPAddress() + ":8080/");
            listener.Start();
            
            listener.BeginGetContext(new AsyncCallback(OnRequestReceive), listener);
        }

        private void OnRequestReceive(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            HttpListenerContext context = listener.EndGetContext(result);
            HttpListenerResponse response = context.Response;

            byte[] buff = getFile(context.Request.RawUrl);
            response.ContentType = getContentType(context.Request.RawUrl);
            response.Close(buff, true);

            listener.BeginGetContext(new AsyncCallback(OnRequestReceive), listener); 
        }

        private string getContentType(string requestURI)
        {
            string type = "";
            requestURI = requestURI.ToLower();

            if (requestURI.Contains(".htm") || requestURI.Contains(".html")) 
            {
                type = "text/html";
            }
            else if (requestURI.Contains(".css")) 
            {
                type = "text/css";
            }
            else if (requestURI.Contains(".js")) 
            {
                type = "text/js";
            }
            else if (requestURI.Contains(".png") || requestURI.Contains(".jpg") || requestURI.Contains(".jpeg"))
            {
                type = "text/jpeg";
            }
            else
            {
                type = "text/html";
            }

            return type;
        }

        private byte[] getFile(String requestURI)
        {
            // Remove the / from the URI
            requestURI = requestURI.Remove(0, 1);
            if (requestURI == "")
            {
                requestURI = "index.htm";
            }
            string path = folder + requestURI;

            //Ensure file exists
            byte[] file;
            if (System.IO.File.Exists(path))
            {
                file = System.IO.File.ReadAllBytes(path);
            }
            else
            {
                file = System.Text.Encoding.UTF8.GetBytes("File not found");
            }

            return file; 

        }

        private string getIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                };
            }
            return "";
        }
    }
}
