using System.Diagnostics;
using System.Net;
using WebSocket4Net;

namespace WebSocket4NetTest;

public partial class MainPage : ContentPage
{
	public static WebSocket ws;

	public MainPage()
	{
		InitializeComponent();

		Start();

    }

	[Obsolete]
	public async Task Start()
	{

        try
        {
            status.Text = "Started";
            string url = "wss://demo.piesocket.com/v3/channel_1?api_key=VCXCEuvhGcBDP7XhiJJUDvR1e1D3eiVjgZ9VRiaV&notify_self";
            ws = new WebSocket(url);

            ws.Security.AllowCertificateChainErrors = true;
            ws.Security.AllowNameMismatchCertificate = true;
            ws.Security.AllowUnstrustedCertificate = true;
            ws.Security.AllowNameMismatchCertificate = true;

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Tls13;

            ws.MessageReceived += Ws_MessageReceived;
            ws.Closed += Ws_Closed;
            ws.Error += Ws_Error;
            ws.Opened += Ws_Opened;
            Device.BeginInvokeOnMainThread(() => { 
            ws.OpenAsync();
            });
        }catch(Exception e)
        {
            Shell.Current.DisplayAlert("Error", $"e:{e.Message.ToString()}", "Ok");
        }

    }

	private void Ws_Opened(object sender, EventArgs e)
	{
        //status.Text = "Opened";
        ws.Send("Hello world from .NET MAUI");
    }

	private void Ws_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
	{
        Device.BeginInvokeOnMainThread(() =>
        {
            status.Text = "Err "+ e.Exception.Message.ToString()+ "\n" + e.Exception.StackTrace.ToString()+ "\n"+ e.Exception.Source.ToString();
            Debug.WriteLine(e.Exception.Message.ToString());
        });
    }

	private void Ws_Closed(object sender, EventArgs e)
	{
        /*Device.BeginInvokeOnMainThread(() =>
        {
            status.Text = "Closed "+ e.ToString();
        });*/
    }

	[Obsolete]
	private void Ws_MessageReceived(object sender, MessageReceivedEventArgs e)
	{
		Device.BeginInvokeOnMainThread(() =>
		{
			msg.Text = e.Message;
		});
	}
}

