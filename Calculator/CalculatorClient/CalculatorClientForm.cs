using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;


namespace CalculatorClient
{
    public partial class CalculatorClient : Form
    {
        public CalculatorClient()
        {
            InitializeComponent();
        }

        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        Uri redirectUri = new Uri(ConfigurationManager.AppSettings["ida:RedirectUri"]);

        private static string authority = String.Format(aadInstance, tenant);

        private static string apiResourceId = ConfigurationManager.AppSettings["ApiResourceId"];
        private static string apiBaseAddress = ConfigurationManager.AppSettings["ApiBaseAddress"];

        private AuthenticationContext authContext = null;

        private async void btnCallDirect_Click(object sender, EventArgs e)
        {

            try
            {
                authContext = new AuthenticationContext(authority);
                AuthenticationResult authResult = authContext.AcquireToken(apiResourceId, clientId, redirectUri);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
                HttpResponseMessage response = await client.GetAsync(apiBaseAddress + "api/add?a=2&b=2");
                response.EnsureSuccessStatusCode();

                string responseString = await response.Content.ReadAsStringAsync();

                MessageBox.Show(responseString);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            { 
            var client = new HttpClient();
                //var queryString = HttpUtility.ParseQueryString(string.Empty);

                // Request headers
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "81e796dc7f0e47df912ebe780934d30d");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSIsImtpZCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSJ9.eyJhdWQiOiJodHRwczovL2FkdHdvY2VudHMub25taWNyb3NvZnQuY29tL0NhbGN1bGF0b3JTZXJ2aWNlIiwiaXNzIjoiaHR0cHM6Ly9zdHMud2luZG93cy5uZXQvYzkyODIyNDQtZjdlZi00NTE0LWI0OGEtYzY5MzA5N2I1YTQyLyIsImlhdCI6MTQ1NDA2MDY2MSwibmJmIjoxNDU0MDYwNjYxLCJleHAiOjE0NTQwNjQ1NjEsImFjciI6IjEiLCJhbXIiOlsicHdkIl0sImFwcGlkIjoiMTNlODY3ZWUtODFlYi00NTdhLTk5MTctODUxMWM4ODQ5MjM4IiwiYXBwaWRhY3IiOiIxIiwiZmFtaWx5X25hbWUiOiJDb250b3NvIiwiZ2l2ZW5fbmFtZSI6IlNlcnZpY2VBY2NvdW50IiwiaXBhZGRyIjoiNDAuMTE0LjI1MS4xNTIiLCJuYW1lIjoiU2VydmljZSBBY2NvdW50IENvbnRvc28iLCJvaWQiOiJhZTYzOWNhZS00ODc0LTQ1ODUtYmE5My03MWM2NzE1MDBlNmMiLCJzY3AiOiJ1c2VyX2ltcGVyc29uYXRpb24iLCJzdWIiOiJkMXh3SlBKa2cwUl9xNGJobDhoT3lGNkdHaHJyRUcxWF8zLWJfWjA1TzRrIiwidGlkIjoiYzkyODIyNDQtZjdlZi00NTE0LWI0OGEtYzY5MzA5N2I1YTQyIiwidW5pcXVlX25hbWUiOiJzcnZfY29udG9zb0BhZHR3b2NlbnRzLm9ubWljcm9zb2Z0LmNvbSIsInVwbiI6InNydl9jb250b3NvQGFkdHdvY2VudHMub25taWNyb3NvZnQuY29tIiwidmVyIjoiMS4wIn0.i3HHdElDHVZqti_gNu635Q_1Tqw0t_0oQXSZ8mUFd9tIHMXS129cPNEqyGorNzLtwQv7a7xFjBc9htLJPxRhfIZwm7GCKFcV1RsFgOikz-s1ydejf47Kc-guREuHzfbtnDwHbnKIqqTEyLA8k2nwZALWtvOLqHPBU9wQ73Q3GHaCMOpUbplFrzfT1x2hw9xTpij8NV-sAibDke3IZSvD93En0fI4SFu-w6U6Z7FYK38DkDIpNh5v1ziVpHfuXVGulzis2b2nzWIEJb9OPuneNPLEpcr_w3Gxw2BndOUy8BgH1Ph0TI0jjhOzIZY_aE3pT5lkr8Ufleq3-kMt2K_ISw");

                string uri = "https://twocents.azure-api.net/Calculator/add?a={a}&b={b}&";

            HttpResponseMessage response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();

            MessageBox.Show(responseString);
        }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
