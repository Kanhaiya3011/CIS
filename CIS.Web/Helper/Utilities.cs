using Newtonsoft.Json;

namespace CIS.Web.Helper
{
    public class Utilities
    {
        private static HttpClient client = new HttpClient();
        public static async Task<HttpResponseMessage> HttpPostCall(string url, StringContent content)
        {
            HttpResponseMessage response = await client.PostAsync(url, content);
            return response;

        }
        public static StringContent SerializeObject<T>(T TObject)
        {
            var content = new StringContent(JsonConvert.SerializeObject(TObject),
                System.Text.Encoding.UTF8, "application/json");
            return content;
        }
        public static async Task<HttpResponseMessage> HttpGetCall(string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            return response;
        }
        public static async Task<HttpResponseMessage> HttpPutCall(string url, StringContent content)
        {
            HttpResponseMessage response = await client.PutAsync(url, content);
            return response;

        }
        public static async Task<HttpResponseMessage> HttDeleteCall(string url)
        {
            HttpResponseMessage response = await client.DeleteAsync(url);
            return response;

        }
        public static T DeSerializeObject<T>(string content, T TObject)
        {
            var obj = JsonConvert.DeserializeObject<T>(content);
            return (T)obj;
        }
    }
}
