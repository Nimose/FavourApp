using System.Net.Http;
using System.Threading.Tasks;
using Favourpp.Models;
using Newtonsoft.Json;

namespace Favourpp.Services
{
    public class FacebookServices
    {
        public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl =
                "https://graph.facebook.com/v2.7/me/?fields=" +
                "id," +
                "picture.type(large)," +
                "first_name," +
                "last_name&" +
                "access_token=" + accessToken;

            HttpClient _client = new HttpClient();
            var userJson = await _client.GetStringAsync(requestUrl);
            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);
            return facebookProfile;
        }        
    }
}
