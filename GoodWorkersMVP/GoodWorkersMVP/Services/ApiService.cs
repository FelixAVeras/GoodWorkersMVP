
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Models.ModelResponse;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace GoodWorkersMVP.Services
{
    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.NoInternetDialog
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");

            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.NoIntertnetWorkingDialog
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "OK"
            };
        }

        public async Task<TokenResponse> GetToken(string urlBase, string prefix, string endpoint, string email, string password, string deviceName)
        {
            try
            {
                var loginRequest = new LoginRequest
                {
                    Email = email,
                    Password = password,
                    DeviceName = deviceName
                };

                var jsonRequest = JsonConvert.SerializeObject(loginRequest);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlBase);
                    client.DefaultRequestHeaders.Add("Accept", "Application/json");

                    var url = prefix + endpoint;
                    var response = await client.PostAsync(url, httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var resultJson = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TokenResponse>(resultJson);
                        
                        return result;
                    }
                    else
                    {
                        var errorJson = await response.Content.ReadAsStringAsync();
                        var error = JsonConvert.DeserializeObject<TokenResponse>(errorJson);
                        
                        return new TokenResponse { ErrorDescription = error.ErrorDescription };
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error de red: {ex.Message}");
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                
                return null;
            }
        }

        public async Task<Response> Get<T>(string urlBase, string servicePrefix, string controller, int id)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                var url = string.Format("{0}{1}/{2}", servicePrefix, controller, id);

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = model,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Get<T>(string urlBase, string servicePrefix, string controller, int id, string accessToken)
        {
            try
            {
                if (string.IsNullOrEmpty(accessToken))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Access token is null or empty.",
                    };
                }

                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri(urlBase);

                var url = string.Format("{0}{1}/{2}", servicePrefix, controller, id);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = model,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<UserType> GetByName(string urlBase, string servicePrefix, string controller, string name)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                var url = string.Format("{0}{1}/{2}", servicePrefix, controller, name);

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    UserType userType = JsonConvert.DeserializeObject<UserType>(jsonContent);
                    
                    return userType;
                }
                else
                {
                    Console.WriteLine($"Error al obtener el tipo de usuario por nombre. Código de estado: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el tipo de usuario por nombre: {ex.Message}");
                return null;
            }
        }

        public async Task<Response> GetList<T>(string urlBase, string servicePrefix, string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetList<T>(string urlBase, string servicePrefix, string controller, string accessToken)
        {
            try
            {
                if (string.IsNullOrEmpty(accessToken))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Access token is null or empty.",
                    };
                }

                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri(urlBase);
                
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            int id)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    id);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetList<T>(string urlBase, string servicePrefix, string controller, int id)
        {
            try
            {
                var client = new HttpClient();
                
                client.BaseAddress = new Uri(urlBase);
                
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    id);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Post<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request, Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record added OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Post<T>(string urlBase, string servicePrefix, string controller, T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                    Timeout = TimeSpan.FromSeconds(60)
                };

                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record added OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Put<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request,
                    Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    model.GetHashCode());
                var response = await client.PutAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Delete<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    model.GetHashCode());
                var response = await client.DeleteAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
