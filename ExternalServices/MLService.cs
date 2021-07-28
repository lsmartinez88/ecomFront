using ecomFront.Data;
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.MLModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ecomFront.Services
{
    public static class MLService
    {
        public static Item GetItemById(String itemId)
        {
            HttpClient client = new HttpClient();
            String url = "https://api.mercadolibre.com/items/" + itemId;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            HttpResponseMessage response = null;
            try
            {
                response = client.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                {
                    String stringResponse= response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Item>(stringResponse);
                }
                else
                    throw new Exception("Failed to get item " + itemId + " from mercadolibre api.");
            }
            catch (Exception e)
            {
                throw new Exception("Failed to get item " + itemId + " from mercadolibre api. Message: " + e.Message);
            }
            finally
            {
                try
                {
                    if (client != null)
                        client.Dispose();
                }
                catch (Exception e)
                {
                    throw  new Exception("Failed to get item " + itemId + " from mercadolibre api. Message: " + e.Message);
                }
            }
        }

        public static List<CategoryML> GetBaseCategories()
        {
            HttpClient client = new HttpClient();
            String url = "https://api.mercadolibre.com/sites/MLA/categories";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            HttpResponseMessage response = null;
            try
            {
                response = client.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                {
                    String stringResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<CategoryML>>(stringResponse);
                }
                else
                    throw new Exception("Failed to get base categories from mercadolibre api.");
            }
            catch (Exception e)
            {
                throw new Exception("Failed to get base categories from mercadolibre api.");
            }
            finally
            {
                try
                {
                    if (client != null)
                        client.Dispose();
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to get base categories from mercadolibre api." );
                }
            }
        }


        public static CategoryML GetCategoryById(String categoryId)
        {
            HttpClient client = new HttpClient();
            String url = "https://api.mercadolibre.com/categories/" + categoryId;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            HttpResponseMessage response = null;
            try
            {
                response = client.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                {
                    String stringResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<CategoryML>(stringResponse);
                }
                else
                    throw new Exception("Failed to get base categories from mercadolibre api.");
            }
            catch (Exception e)
            {
                throw new Exception("Failed to get base categories from mercadolibre api.");
            }
            finally
            {
                try
                {
                    if (client != null)
                        client.Dispose();
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to get base categories from mercadolibre api.");
                }
            }
        }

        public static String GetAccessToken(Boolean refreshToken, IAuthData _authData)
        {
            List<Auth> tokenList = _authData.GetAuth();
            Auth auth = null;
             if (refreshToken)
             {
                AuthML token = GetAccessToken("1586142976127673", "mBboRFhUNrkkNGaShPUqvMxPVO2iL97r",_authData);

                if (tokenList.Count == 0)
                {
                    auth = new Auth();
                    auth.ClientId = "1586142976127673";
                    auth.ClientSecret = "mBboRFhUNrkkNGaShPUqvMxPVO2iL97r";
                    auth.AccessToken = token.Access_Token;
                    auth.TokenType = token.Token_Type;
                    auth.DateCreated = DateTime.Now;
                    auth.LastUpdated = DateTime.Now;
                }
                else
                {
                    auth = tokenList[0];
                    auth.AccessToken = token.Access_Token;
                    auth.TokenType = token.Token_Type;
                    auth.LastUpdated = DateTime.Now;
                }
                _authData.SaveAuth(auth);
            }
            else
            {
                if (auth == null && tokenList.Count > 0)
                    auth = tokenList[0];
            }
            return auth.AccessToken;

         }

        public static AuthML GetAccessToken(String clientId, String clientSecret, IAuthData _authData)
        {
            HttpClient client = new HttpClient();
            String url = "https://api.mercadolibre.com/oauth/token";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

            var param = new Dictionary<string, string>()
            {
                { "grant_type", "client_credentials" },
                {"client_id", clientId},
                {"client_secret", clientSecret }
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            String content = JsonConvert.SerializeObject(param);
            request.Content = new StringContent(content);
            HttpResponseMessage response = null;
            try
            {
                response = client.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                {
                    String stringResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<AuthML>(stringResponse);
                }
                else
                    throw new Exception("Failed to get auth token from mercadolibre api.");
            }
            catch (Exception e)
            {
                throw new Exception("Failed to get auth token from mercadolibre api.Message :" + e.Message);
            }
            finally
            {
                try
                {
                    if (client != null)
                        client.Dispose();
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to get auth token from mercadolibre api.Message :" + e.Message);
                }
            }
        }




    }
}
