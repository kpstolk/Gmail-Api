﻿using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace KP.GmailApi
{
    /// <summary>
    /// Handles requests to the Gmail service and parses the response.
    /// </summary>
    internal class GmailClient
    {
        /// <summary>
        /// The URL to send requests to the Gmail API service
        /// </summary>
        public const string ApiBaseUrl = "https://www.googleapis.com/gmail/v1/users/";
        private const string HttpGet = "GET";
        private const string HttpPost = "POST";
        private const string HttpPut = "PUT";
        private const string HttpPatch = "PATCH";
        private const string HttpDelete = "DELETE";

        private readonly TokenManager _tokenManager;
        private readonly Uri _baseAddress;

        internal GmailClient(TokenManager tokenManager)
        {
            // user ID:
            //The user's email address. The special value 'me' can be used to indicate the authenticated user.
            //userId = HttpUtility.UrlEncode(userId);

            _baseAddress = new Uri(string.Concat(ApiBaseUrl, "me/"));// for personal accounts no other value than 'me' can be used?!
            _tokenManager = tokenManager;

            // Set default (de)serializing for enums
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter { CamelCaseText = true } }
            };
        }

        internal T Get<T>(string queryString)
        {
            string response = GetResponse(HttpGet, queryString);

            return JsonConvert.DeserializeObject<T>(response);
        }

        internal T Get<T>(string queryString, ParseOptions options)
        {
            string response = GetResponse(HttpGet, queryString);

            var jo = JObject.Parse(response);
            return jo.SelectToken(options.Path, true).ToObject<T>();
        }

        internal T Post<T>(string queryString, object content = null)
        {
            string response = GetResponse(HttpPost, queryString, content);

            return JsonConvert.DeserializeObject<T>(response);
        }

        internal T Put<T>(string queryString, object content = null)
        {
            string response = GetResponse(HttpPut, queryString, content);

            return JsonConvert.DeserializeObject<T>(response);
        }

        internal T Patch<T>(string queryString, object content = null)
        {
            string response = GetResponse(HttpPatch, queryString, content);

            return JsonConvert.DeserializeObject<T>(response);
        }

        internal void Delete(string queryString)
        {
            GetResponse(HttpDelete, queryString);
        }

        private HttpClient GetClient()
        {
            // For example: https://www.googleapis.com/gmail/v1/users/{userId}/
            var client = new HttpClient { BaseAddress = _baseAddress };

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _tokenManager.GetToken());
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        private string GetResponse(string httpMethod, string queryString, object content = null)
        {
            HttpContent httpContent = content == null
               ? null
               : new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(new HttpMethod(httpMethod), queryString)
            {
                Content = httpContent
            };

            HttpClient client = GetClient();
            HttpResponseMessage response = client.SendAsync(request).Result;

            string contentString = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
                return contentString;

            GmailException ex = ErrorResponseParser.Parse(response.StatusCode, contentString);
            throw ex;
        }
    }
}
