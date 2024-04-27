using System;
using System.Diagnostics;
using System.IO;
using System.Security.Policy;
using System.Threading;
using System.Timers;
using System.Web;
using System.Web.UI;

namespace WebApplication1
{
    public class LogHttpModule : IHttpModule
    {
        private Stopwatch Stopwatch = new Stopwatch();
        public void Dispose()
        {
            // Do Something   
        }
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(StartTime);
            context.EndRequest += new EventHandler(EndTime);
            context.BeginRequest += new EventHandler(LogInformationEvent);
            context.Error += new EventHandler(LogErrorEvent);
        }
        public void StartTime(object sender, EventArgs e)
        {
            Stopwatch.Start();
            WriteLog(HttpContext.Current.Request.Url + " " + Stopwatch.Elapsed.ToString());

        }
        public void EndTime(object sender, EventArgs e)
        {
            Stopwatch.Stop();
            WriteLog(HttpContext.Current.Request.Url + " " + Stopwatch.Elapsed.ToString());
        }
        public void LogInformationEvent(object sender, EventArgs eventArgs)
        {
            WriteLog("information");
        }
        public void LogErrorEvent(object sender, EventArgs e)
        {
            //Log Operation goes here.  
            HttpContext ctx = HttpContext.Current;
            HttpResponse response = ctx.Response;
            HttpRequest request = ctx.Request;
            Exception exception = ctx.Server.GetLastError();
            string errorInfo = "<p/>URL: " + ctx.Request.Url.ToString();
            errorInfo += "<p/>Stacktrace:---<br/>" +
               exception.InnerException.StackTrace.ToString();
            errorInfo += "<p/>Error Message:<br/>" +
               exception.InnerException.Message;
            WriteLog(errorInfo);
            response.Write("<p/>ErrorInfo:<p/>");
            response.Write(errorInfo);
            ctx.Server.ClearError();

        }
        private void WriteLog(string logMessage)
        {
            using (StreamWriter streamWriter = File.AppendText("C:\\temp\\logs"))
            {
                streamWriter.WriteLine(logMessage);
            }
        }
    }
}