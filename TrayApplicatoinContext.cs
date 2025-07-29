using Connect2Cart_Desktop_Hub;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

public class TrayApplicationContext : ApplicationContext
{
    private NotifyIcon trayIcon;
    private HttpListener listener;
    private Thread serverThread;

    public TrayApplicationContext()
    {
        trayIcon = new NotifyIcon()
        {
            Icon = SystemIcons.Application,
            Visible = true,
            Text = "Connect2Cart Desktop Hub",
            ContextMenuStrip = new ContextMenuStrip()
        };

        trayIcon.ContextMenuStrip.Items.Add("Settings", null, OpenSettings);
        trayIcon.ContextMenuStrip.Items.Add("Exit", null, Exit);

        StartHttpServer();
    }

    private void OpenSettings(object sender, EventArgs e)
    {
        new ConfigForm().ShowDialog();
    }

    private void Exit(object sender, EventArgs e)
    {
        StopHttpServer();
        trayIcon.Visible = false;
        Application.Exit();
    }

    private void StartHttpServer()
    {
        listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:51717/");
        listener.Start();

        serverThread = new Thread(() =>
        {
            while (listener.IsListening)
            {
                try
                {
                    var context = listener.GetContext();
                    HandleRequest(context);
                }
                catch
                {
                    // Swallow exceptions
                }
            }
        });

        serverThread.IsBackground = true;
        serverThread.Start();
    }

    private void StopHttpServer()
    {
        listener?.Stop();
        serverThread?.Join();
    }

    private void HandleRequest(HttpListenerContext context)
    {
        string responseText = "";
        int statusCode = 200;

        if (context.Request.HttpMethod == "GET" && context.Request.Url.AbsolutePath == "/weight")
        {
            var weight = ScaleReader.ReadWeight(Configuration.Current.Scale);
            responseText = $"{{ \"weight\": \"{weight}\", \"unit\": \"lb\" }}";
            context.Response.ContentType = "application/json";
        }
        else if (context.Request.HttpMethod == "POST" && context.Request.Url.AbsolutePath == "/print")
        {
            using var reader = new StreamReader(context.Request.InputStream);
            var zpl = reader.ReadToEnd();
            PrinterHelper.SendStringToPrinter(Configuration.Current.Printer, zpl);
            responseText = "Printed";
        }
        else
        {
            statusCode = 404;
            responseText = "Not Found";
        }

        context.Response.StatusCode = statusCode;
        byte[] buffer = Encoding.UTF8.GetBytes(responseText);
        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
        context.Response.Close();
    }
}
