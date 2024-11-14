using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleplayingVoiceDalamud.Voice {
    public class NPCVoiceConfiguration {
        private Dictionary<string, string> _characterToVoiceList;
        private Dictionary<string, List<string>> _nameAndAliasesList;
        private List<KeyValuePair<string, string>> _extrasVoiceList;
        private List<KeyValuePair<string, bool>> _echoValuesList;
        private List<KeyValuePair<string, float>> _pitchValuesList;

        public NPCVoiceConfiguration() {
        }

        public Dictionary<string, string> CharacterToVoiceList { get => _characterToVoiceList; set => _characterToVoiceList = value; }
        public Dictionary<string, List<string>> NameAndAliasesList { get => _nameAndAliasesList; set => _nameAndAliasesList = value; }
        public List<KeyValuePair<string, string>> ExtrasVoiceList { get => _extrasVoiceList; set => _extrasVoiceList = value; }
        public List<KeyValuePair<string, bool>> EchoValuesList { get => _echoValuesList; set => _echoValuesList = value; }
        public List<KeyValuePair<string, float>> PitchValuesList { get => _pitchValuesList; set => _pitchValuesList = value; }
    }
}
