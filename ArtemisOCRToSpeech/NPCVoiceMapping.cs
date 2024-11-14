using Newtonsoft.Json;
using RoleplayingVoiceDalamud.Datamining;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace RoleplayingVoiceDalamud.Voice {
    public static class NPCVoiceMapping {
        public static NPCVoiceConfiguration _npcVoiceConfiguration;
        private static Dictionary<string, ReportData>? _speakerList;

        public static Dictionary<string, ReportData>? SpeakerList { get => _speakerList; set => _speakerList = value; }

        public static void Initialize() {
            Task.Run(async () => {
                try {
                    HttpClient httpClient = new HttpClient();
                    string json = await httpClient.GetStringAsync("https://raw.githubusercontent.com/Sebane1/RoleplayingVoiceDalamud/master/npcVoiceConfiguration.json");
                    _npcVoiceConfiguration = JsonConvert.DeserializeObject<NPCVoiceConfiguration>(json);
                    httpClient = new HttpClient();
                    json = await httpClient.GetStringAsync("https://raw.githubusercontent.com/Sebane1/RoleplayingVoiceDalamud/master/speakers.json");
                    _speakerList = JsonConvert.DeserializeObject<Dictionary<string, ReportData>>(json);
                } catch (Exception ex) {

                }
            });
        }

        public static string AliasDetector(string name) {
            foreach (var key in _npcVoiceConfiguration.NameAndAliasesList.Keys) {
                foreach (var aliases in _npcVoiceConfiguration.NameAndAliasesList[key]) {
                    if (aliases.Contains(name)) {
                        return key;
                    }
                }
            }
            return name;
        }

        public static Dictionary<string, string> GetVoiceMappings() {
            return _npcVoiceConfiguration.CharacterToVoiceList;
        }
        public static List<KeyValuePair<string, string>> GetExtrasVoiceMappings() {
            return _npcVoiceConfiguration.ExtrasVoiceList;
        }
        public static List<KeyValuePair<string, bool>> GetEchoType() {
            return _npcVoiceConfiguration.EchoValuesList;
        }
        public static List<KeyValuePair<string, float>> GetPitchValues() {
            return _npcVoiceConfiguration.PitchValuesList;
        }
    }
}
