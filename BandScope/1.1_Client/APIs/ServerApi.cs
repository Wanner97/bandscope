using BandScope.Client.APIs.ServerApiObjects;
using Radzen;
using System.Net;
using System.Text;
using System.Text.Json;

namespace BandScope.Client.APIs
{
    public class ServerApi
    {
        private readonly HttpClient _httpClient;
        private readonly NotificationService _notificationService;

        public ServerApi(HttpClient httpClient, NotificationService notificationService)
        {
            _httpClient = httpClient;
            _notificationService = notificationService;
        }

        public async Task<ServerApiBase<T>> GetAsync<T>(string endpoint)
        {
            ServerApiBase<T> returnValue = new ServerApiBase<T>();

            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var des = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                returnValue.IsSuccessStatusCode = response.IsSuccessStatusCode;
                returnValue.Result = des;
                returnValue.StatusCode = response.StatusCode;
            }
            catch (HttpRequestException requestException)
            {
                if (requestException.StatusCode == HttpStatusCode.BadRequest)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Detail = "Bad Request",
                        Summary = "Bad Request",
                        Duration = 4000,
                        Severity = NotificationSeverity.Warning
                    });
                    returnValue.IsSuccessStatusCode = false;
                    returnValue.StatusCode = requestException.StatusCode.Value;
                    returnValue.Result = default;

                    return returnValue;
                }
                else if (requestException.StatusCode == HttpStatusCode.InternalServerError)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Detail = "Internal Server Error",
                        Summary = "Error",
                        Duration = 4000,
                        Severity = NotificationSeverity.Error
                    });
                    returnValue.IsSuccessStatusCode = false;
                    returnValue.StatusCode = requestException.StatusCode.Value;
                    returnValue.Result = default;

                    return returnValue;
                }
                else if (requestException.StatusCode == HttpStatusCode.Forbidden)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Detail = "You are not permitted to load this content.",
                        Summary = "No Permission",
                        Duration = 4000,
                        Severity = NotificationSeverity.Warning
                    });
                    returnValue.IsSuccessStatusCode = false;
                    returnValue.StatusCode = requestException.StatusCode.Value;
                    returnValue.Result = default;

                    return returnValue;
                }
                else if (requestException.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Detail = "You are not permitted to load this content.",
                        Summary = "No Permission",
                        Duration = 4000,
                        Severity = NotificationSeverity.Warning
                    });
                    returnValue.IsSuccessStatusCode = false;
                    returnValue.StatusCode = requestException.StatusCode.Value;
                    returnValue.Result = default;

                    return returnValue;
                }
                else if (requestException.StatusCode == HttpStatusCode.NotFound)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Detail = "The Data you are looking for can not be found.",
                        Summary = "Not Found",
                        Duration = 4000,
                        Severity = NotificationSeverity.Error
                    });
                    returnValue.IsSuccessStatusCode = false;
                    returnValue.StatusCode = requestException.StatusCode.Value;
                    returnValue.Result = default;

                    return returnValue;
                }
            }
            catch (Exception e)
            {
                _notificationService.Notify(new NotificationMessage
                {
                    Detail = "Internal Server Error",
                    Summary = "Error",
                    Duration = 4000,
                    Severity = NotificationSeverity.Error
                });
                returnValue.IsSuccessStatusCode = false;
                returnValue.StatusCode = HttpStatusCode.InternalServerError;
                returnValue.Result = default;

                return returnValue;
            }

            return returnValue;
        }

        public async Task<ServerApiBase<T>> PostAsync<T>(string endpoint, object? data)
        {
            ServerApiBase<T> returnValue = new ServerApiBase<T>();

            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(endpoint, content);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var des = JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                returnValue.IsSuccessStatusCode = response.IsSuccessStatusCode;
                returnValue.Result = des;
                returnValue.StatusCode = response.StatusCode;
            }
            catch (HttpRequestException requestException)
            {
                if (requestException.StatusCode == HttpStatusCode.BadRequest)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Detail = "Bad Request",
                        Summary = "Bad Request",
                        Duration = 4000,
                        Severity = NotificationSeverity.Warning
                    });
                    returnValue.IsSuccessStatusCode = false;
                    returnValue.StatusCode = requestException.StatusCode.Value;
                    returnValue.Result = default;

                    return returnValue;
                }
                else if (requestException.StatusCode == HttpStatusCode.InternalServerError)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Detail = "Internal Server Error",
                        Summary = "Error",
                        Duration = 4000,
                        Severity = NotificationSeverity.Error
                    });
                    returnValue.IsSuccessStatusCode = false;
                    returnValue.StatusCode = requestException.StatusCode.Value;
                    returnValue.Result = default;

                    return returnValue;
                }
                else if (requestException.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Detail = "You are not permitted to load this content.",
                        Summary = "No Permission",
                        Duration = 4000,
                        Severity = NotificationSeverity.Warning
                    });
                    returnValue.IsSuccessStatusCode = false;
                    returnValue.StatusCode = requestException.StatusCode.Value;
                    returnValue.Result = default;

                    return returnValue;
                }
            }
            catch (Exception e)
            {
                _notificationService.Notify(new NotificationMessage
                {
                    Detail = "Internal Server Error",
                    Summary = "Error",
                    Duration = 4000,
                    Severity = NotificationSeverity.Error
                });
                returnValue.IsSuccessStatusCode = false;
                returnValue.StatusCode = HttpStatusCode.InternalServerError;
                returnValue.Result = default;

                return returnValue;
            }

            return returnValue;
        }

        public async Task<ServerApiBase<T>> PutAsync<T>(string endpoint, object data)
        {
            ServerApiBase<T> returnValue = new ServerApiBase<T>();

            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(endpoint, content);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var des = JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                returnValue.IsSuccessStatusCode = response.IsSuccessStatusCode;
                returnValue.Result = des;
                returnValue.StatusCode = response.StatusCode;
            }
            catch (HttpRequestException requestException)
            {
                if (requestException.StatusCode == HttpStatusCode.BadRequest)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Detail = "Bad Request",
                        Summary = "Bad Request",
                        Duration = 4000,
                        Severity = NotificationSeverity.Warning
                    });
                    returnValue.IsSuccessStatusCode = false;
                    returnValue.StatusCode = requestException.StatusCode.Value;
                    returnValue.Result = default;

                    return returnValue;
                }
                else if (requestException.StatusCode == HttpStatusCode.InternalServerError)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Detail = "Internal Server Error",
                        Summary = "Error",
                        Duration = 4000,
                        Severity = NotificationSeverity.Error
                    });
                    returnValue.IsSuccessStatusCode = false;
                    returnValue.StatusCode = requestException.StatusCode.Value;
                    returnValue.Result = default;

                    return returnValue;
                }
                else if (requestException.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Detail = "You are not permitted to load this content.",
                        Summary = "No Permission",
                        Duration = 4000,
                        Severity = NotificationSeverity.Warning
                    });
                    returnValue.IsSuccessStatusCode = false;
                    returnValue.StatusCode = requestException.StatusCode.Value;
                    returnValue.Result = default;

                    return returnValue;
                }
            }
            catch (Exception e)
            {
                _notificationService.Notify(new NotificationMessage
                {
                    Detail = "Internal Server Error",
                    Summary = "Error",
                    Duration = 4000,
                    Severity = NotificationSeverity.Error
                });
                returnValue.IsSuccessStatusCode = false;
                returnValue.StatusCode = HttpStatusCode.InternalServerError;
                returnValue.Result = default;

                return returnValue;
            }

            return returnValue;
        }

        public async Task<ServerApiBase<T>> DeleteAsync<T>(string endpoint)
        {
            ServerApiBase<T> returnValue = new ServerApiBase<T>();

            try
            {
                var response = await _httpClient.DeleteAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var des = JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                returnValue.IsSuccessStatusCode = true;
                returnValue.Result = des;
                returnValue.StatusCode = response.StatusCode;
            }
            catch (HttpRequestException requestException)
            {
                if (requestException.StatusCode == HttpStatusCode.BadRequest)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Detail = "Bad Request",
                        Summary = "Bad Request",
                        Duration = 4000,
                        Severity = NotificationSeverity.Warning
                    });
                    returnValue.IsSuccessStatusCode = false;
                    returnValue.StatusCode = requestException.StatusCode.Value;
                    returnValue.Result = default;

                    return returnValue;
                }
                else if (requestException.StatusCode == HttpStatusCode.InternalServerError)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Detail = "Internal Server Error",
                        Summary = "Error",
                        Duration = 4000,
                        Severity = NotificationSeverity.Error
                    });
                    returnValue.IsSuccessStatusCode = false;
                    returnValue.StatusCode = requestException.StatusCode.Value;
                    returnValue.Result = default;

                    return returnValue;
                }
                else if (requestException.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Detail = "You are not permitted to load this content.",
                        Summary = "No Permission",
                        Duration = 4000,
                        Severity = NotificationSeverity.Warning
                    });
                    returnValue.IsSuccessStatusCode = false;
                    returnValue.StatusCode = requestException.StatusCode.Value;
                    returnValue.Result = default;

                    return returnValue;
                }
            }
            catch (Exception e)
            {
                _notificationService.Notify(new NotificationMessage
                {
                    Detail = "Internal Server Error",
                    Summary = "Error",
                    Duration = 4000,
                    Severity = NotificationSeverity.Error
                });
                returnValue.IsSuccessStatusCode = false;
                returnValue.StatusCode = HttpStatusCode.InternalServerError;
                returnValue.Result = default;

                return returnValue;
            }

            return returnValue;
        }
    }
}
