/* 
*  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. 
*  See LICENSE in the source repository root for complete license information. 
*/

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DevCommQuestionsTracker.Helpers
{
    public sealed class ApplicationPermissionsAuthProvider : IAuthProvider
    {

        // Properties used to get and manage an access token.
        //private string redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];
        //private string appId = ConfigurationManager.AppSettings["ida:AppId"];
        //private string appSecret = ConfigurationManager.AppSettings["ida:AppSecret"];
        //private string tenantId = ConfigurationManager.AppSettings["ida:GraphScopes"];

        //private static readonly SampleAuthProvider instance = new SampleAuthProvider();

        //public static SampleAuthProvider Instance
        //{
        //    get
        //    {
        //        return instance;
        //    }
        //}

        // Gets an access token. First tries to get the token from the token cache.
        public async Task<string> GetAccessTokenAsync()
        {
            // TODO: Fetch details from App Config.
            string tenant= "blrdev.onmicrosoft.com", appId = "9235c421-99fe-4fa3-8d3e-256712544d92", appSecret = "Gn10B.GOSD9H9ARDA%2Fj%3FnlNXy%40dfNn%40%2F";
            string response = await POST($"https://login.microsoftonline.com/{tenant}/oauth2/v2.0/token",
                              $"grant_type=client_credentials&client_id={appId}&client_secret={appSecret}"
                              + "&scope=https%3A%2F%2Fgraph.microsoft.com%2F.default");

            string accessToken = JsonConvert.DeserializeObject<TokenResponse>(response).access_token;
            return accessToken;
        }

        public static async Task<string> POST(string url, string body)
        {
            HttpClient httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception(responseBody);
            return responseBody;
        }

        public class TokenResponse
        {
            public string access_token { get; set; }
        }
    }
}