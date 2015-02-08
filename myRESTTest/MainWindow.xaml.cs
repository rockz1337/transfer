using System;
using System.Net;
using System.Text;
using System.Windows;
using System.Xml;

namespace myRESTTest
{
    /*static public string Beautify(this XmlDocument doc)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }
     * */
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void Anfrage(String funktion)
        {
            ausgabe.Content = "Starte Anfrage";

            String method = "GET";
            String url = "https://www.mkmapi.eu/ws/v1.1/" + funktion;

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;
            HttpWebResponse response;

            XmlDocument doc = new XmlDocument();
            try
            {
                response = request.GetResponse() as HttpWebResponse;
                doc.Load(response.GetResponseStream());

                StringBuilder sb = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    NewLineChars = "\r\n",
                    NewLineHandling = NewLineHandling.Replace
                };
                using (XmlWriter writer = XmlWriter.Create(sb, settings))
                {
                    doc.Save(writer);
                    
                }
                ausgabe.Content = sb.ToString();//doc.OuterXml;
            }
            catch (WebException ex)
            {
                ausgabe.Content = "Kein Zugriff" + Environment.NewLine + ex.ToString();
            }
        }
            
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Anfrage("account");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Anfrage("games");
        }

    }
}
