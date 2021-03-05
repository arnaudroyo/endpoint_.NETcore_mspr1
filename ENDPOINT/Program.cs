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
            _listener.Prefixes.Add("http://*:420/");
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

            // Création de l'objet Bdd pour l'intéraction avec la base de donnée MySQL

            string res = "false";
            Mysql bdd = new Mysql();
            res = bdd.getPromo(code);


            //Et on répond à l'appli si c'est ok ou pas en fonction de la réponse de bdd.getPromo
            var buf = Encoding.ASCII.GetBytes(res);
            ctx.Response.ContentType = "text/plain";

            ctx.Response.OutputStream.Write(buf, 0, buf.Length);

            ctx.Response.OutputStream.Close();



            Console.WriteLine(DateTime.UtcNow.ToString("HH:mm:ss.fff") + " completed");
        }
    }
}
