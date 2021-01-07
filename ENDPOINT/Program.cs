using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ENDPOINT
{
    class Program
    {
        private static HttpListener _listener;

        static void Main(string[] args)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://127.0.0.1:80/");
            _listener.Start();
            _listener.BeginGetContext(OnContext, null);
            Console.WriteLine("Listening..");
            Console.ReadLine();
        }

        private static void OnContext(IAsyncResult ar)
        {
            var ctx = _listener.EndGetContext(ar);
            _listener.BeginGetContext(OnContext, null);

            Console.WriteLine(DateTime.UtcNow.ToString("HH:mm:ss.fff") + " Handling request");

            // Création de l'objet Bdd pour l'intéraction avec la base de donnée MySQL
            Mysql bdd = new Mysql();
            string res = bdd.SelectTest();

            // la faut ask la bdd
            Thread.Sleep(5000);

            //Et on répond a l'appli si c ok ou pas
            var buf = Encoding.ASCII.GetBytes("Response : " + res);
            ctx.Response.ContentType = "text/plain";

            ctx.Response.OutputStream.Write(buf, 0, buf.Length);
            ctx.Response.OutputStream.Close();


            Console.WriteLine(DateTime.UtcNow.ToString("HH:mm:ss.fff") + " completed");
        }
    }
}
