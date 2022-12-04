namespace CIS.Web.Helper
{
    public interface IUtilities
    {
        Task<HttpResponseMessage> HttDeleteCall(string url);
        Task<T?> HttpGetCall<T>(string url);
        Task<T?> HttpPostCall<T>(string url, T content);
        Task<T?> HttpPutCall<T>(string url, T content);
    }
}