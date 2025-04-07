using System.Net.Http.Headers;
using DAL.Exceptions;
using DAL.External.Interfaces;
using DAL.Model;
using Newtonsoft.Json;

namespace DAL.External;

public class ExternalRequestDal : IExternalRequestDal
{
     public async Task<ResponseDto<TResponse>> GetRequest<TResponse>(string url, string apiEndpoint)
        {
            var uri = new Uri(url);
            var baseUrl = uri.GetLeftPart(UriPartial.Authority);
            var pathAndQuery = uri.PathAndQuery;
            var endPoint = string.IsNullOrEmpty(pathAndQuery.Trim()) ? apiEndpoint : $"{pathAndQuery}{apiEndpoint}";
            try
            {
                using var handler = new HttpClientHandler();
                using var client = new HttpClient(handler);

                client.BaseAddress = new Uri($"{baseUrl}/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                

                var response = await client.GetAsync(endPoint.Replace("//", "/"));
                var jsonResult = await response.Content.ReadAsStringAsync();
                

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var result = JsonConvert.DeserializeObject<ResponseDto<TResponse>>(jsonResult);
                        return result;
                    }
                    catch (JsonSerializationException jsonEx)
                    {
                        throw new Exception(
                            $"Failed to deserialize success response: {jsonEx.Message} Raw response: {jsonResult}");
                    }
                }

                throw new TooManyRequestException();
            }
            catch (TooManyRequestException)
            {
                throw;
            }

            catch (HttpRequestException e)
            {
                throw new Exception($"Connection error: {e.Message}");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return default;
        }

}