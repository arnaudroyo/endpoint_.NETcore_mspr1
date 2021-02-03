using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ENDPOINT
{
    class Program
    {
        private static HttpListener _listener;

        static void Main(string[] args)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://locahost:420/");
            _listener.Start();
            _listener.BeginGetContext(OnContext, null);
            Console.WriteLine("Listening..");
            Console.ReadLine();
        }

        private static void OnContext(IAsyncResult ar)
        {
            var ctx = _listener.EndGetContext(ar);
            _listener.BeginGetContext(OnContext, null);


            HttpListenerRequest request = ctx.Request;
            //Console.WriteLine($"Recived request for {request.Url}");
            string code = HttpUtility.ParseQueryString(request.Url.Query).Get("code");

            Console.WriteLine(DateTime.UtcNow.ToString("HH:mm:ss.fff") + $" Handling request with code = {code}");

            // la faut ask la bdd (simul wait)
            //Thread.Sleep(4000);
            // Création de l'objet Bdd pour l'intéraction avec la base de donnée MySQL
            Mysql bdd = new Mysql();
            string res = bdd.getPromo(code);


            //Et on répond a l'appli si c ok ou pas
            var buf = Encoding.ASCII.GetBytes($"Code : {code}\nResponse : " + res);
            ctx.Response.ContentType = "text/plain";

            ctx.Response.OutputStream.Write(buf, 0, buf.Length);

            ctx.Response.OutputStream.Close();



            Console.WriteLine(DateTime.UtcNow.ToString("HH:mm:ss.fff") + " completed");
        }
    }
}
