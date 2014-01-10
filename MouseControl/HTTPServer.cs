using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

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
            listener.Start();
            
            listener.BeginGetContext(new AsyncCallback(OnRequestReceive), listener);
        }

        private void OnRequestReceive(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            HttpListenerContext context = listener.EndGetContext(result);
            HttpListenerResponse response = context.Response;
            byte[] buff = getFileBytes("C:\\Users\\James\\Documents\\Visual Studio 2010\\Projects\\MouseControl\\MouseControl\\html\\index.htm");
            Console.WriteLine(context.Request.RawUrl);
            response.Close(buff, true);

            listener.BeginGetContext(new AsyncCallback(OnRequestReceive), listener); 
        }

        private byte[] getFileBytes(string path)
        {
            return System.IO.File.ReadAllBytes(path); 
        }

        private string getContentType(string filename)
        {
            return "text/html";
        }
    }
}
