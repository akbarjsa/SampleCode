using RestSharp;
using System.Net;

namespace QACodingChallenge.ApiCommon
{
    public class SearchApi
    {
        public static IRestResponse InvokeRepositoriesApi(string query)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient("https://api.github.com/search/repositories" + query);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            return client.Execute(request);
        }
    }
}
