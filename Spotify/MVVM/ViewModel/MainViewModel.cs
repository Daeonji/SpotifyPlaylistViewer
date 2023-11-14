using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using Spotify.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spotify.MVVM.ViewModel
{
    internal class MainViewModel
    {
        public ObservableCollection<Item> Songs { get; set; }
        public MainViewModel()
        {
            Songs = new ObservableCollection<Item>();
            PopulateCollection();
        }

        void PopulateCollection()
        {
            var client = new RestClient();
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator("BQBDmNpvnG0_-3sFqYx2-FY86FV-8koH4_AcsuOCnu4Iev4cc6BKjcXRmz2MLR1Xq_EKVKdv-3PHEpeZHY_wZrNNbsJYdpdtVq-0Cciyn2TXpyflp8I", "Bearer");
            var request = new RestRequest("https://api.spotify.com/v1/browse/new-releases", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            var response = client.GetAsync(request).GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<TrackModel>(response.Content);

            for(int i = 0; i < data.Albums.Limit; i++)
            {
                var track = data.Albums.Items[i];
                track.Duration = "2:32";
                Songs.Add(track);
            }
        }
    }
}
