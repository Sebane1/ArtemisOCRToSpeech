using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
namespace RoleplayingVoiceDalamud.Datamining {
    public class ReportData {
        private ushort territoryId;
        public string speaker { get; set; }
        public string sentence { get; set; }
        public ulong npcid { get; set; }
        public int body { get; set; }
        public bool gender { get; set; }
        public byte race { get; set; }
        public byte tribe { get; set; }
        public byte eyes { get; set; }
        public byte folder { get; set; }
        public string user { get; set; }
        public ushort TerritoryId { get => territoryId; set => territoryId = value; }

        public ReportData(string name, string message, ulong objectId, int body, bool gender, byte race, byte tribe, byte eyes, ushort territoryId) {
            speaker = name;
            sentence = message;
            npcid = objectId;
            this.body = body;
            this.gender = gender;
            this.race = race;
            this.tribe = tribe;
            this.eyes = eyes;
            this.territoryId = territoryId;
            user = "ArtemisRoleplayingKit";
        }
        public async void ReportToXivVoice() {
            try {
                using (HttpClient httpClient = new HttpClient()) {
                    httpClient.BaseAddress = new Uri("https://arcsidian.com/report_to_seb.php");
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var post = await httpClient.PostAsync(httpClient.BaseAddress, new StringContent(JsonConvert.SerializeObject(this)));
                    if (post.StatusCode != HttpStatusCode.OK) {
                    }
                }
            } catch { }
        }
    }
}
