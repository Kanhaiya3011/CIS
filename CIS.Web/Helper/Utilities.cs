using Newtonsoft.Json;

namespace CIS.Web.Helper
{
    public class Utilities : IUtilities
    {
        private HttpClient client = new HttpClient();
        public async Task<T?> HttpPostCall<T>(string url, T content)
        {
            var payload = SerializeObject(content);
            HttpResponseMessage response = await client.PostAsync(url, payload);
            var result = await response.Content.ReadAsStringAsync();
            var returnObject = this.DeserializeObject<T>(result);
            return returnObject;

        }
        private StringContent SerializeObject<T>(T TObject)
        {
            var content = new StringContent(JsonConvert.SerializeObject(TObject),
                System.Text.Encoding.UTF8, "application/json");
            return content;
        }
        public async Task<T> HttpGetCall<T>(string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var returnObject = this.DeserializeObject<T>(result);
            return returnObject;
        }
        public async Task<T> HttpPutCall<T>(string url, T content)
        {
            var payload = SerializeObject(content);
            HttpResponseMessage response = await client.PutAsync(url, payload);
            var result = await response.Content.ReadAsStringAsync();
            var returnObject = this.DeserializeObject<T>(result);
            return returnObject;


        }
        public async Task<HttpResponseMessage> HttDeleteCall(string url)
        {
            HttpResponseMessage response = await client.DeleteAsync(url);
            return response;

        }
        private T DeserializeObject<T>(string content)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<T>(content);
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
    }
}
