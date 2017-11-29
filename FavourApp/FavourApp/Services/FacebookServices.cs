using System.Net.Http;
using System.Threading.Tasks;
using FavourApp.Models;
using Newtonsoft.Json;

namespace FavourApp.Services
{
    public class FacebookServices
    {
        public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl =
                "https://graph.facebook.com/v2.7/me/?fields="+
                "id," +
                "picture.type(large)," +
                "first_name," +
                "last_name&" +              
                "access_token=" + accessToken;

            var httpClient = new HttpClient();
            var userJson = await httpClient.GetStringAsync(requestUrl);
            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);
            return facebookProfile;
        }
    
        public  FacebookProfile RemoveFacebookProfile()
        {
            return null;
        }

    }
}
