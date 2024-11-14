using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using RoleplayingMediaCore;
using RoleplayingVoiceCore;
using RoleplayingVoiceDalamud.Voice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoleplayingVoiceDalamud;
using Concentus.Oggfile;
using Concentus.Structs;
using RoleplayingVoiceDalamud.Datamining;

namespace ArtemisOCRToSpeech {
    public class ArtemisFFXIV {
        private NPCVoiceManager _npcVoiceManager;
        private MediaManager _mediaManager;
        private ushort _territoryType;

        public ArtemisFFXIV() {
            _npcVoiceManager = new NPCVoiceManager(NPCVoiceMapping.GetVoiceMappings(), NPCVoiceMapping.GetExtrasVoiceMappings());
            _mediaManager = new MediaManager(new DummyObject(), new DummyObject(), AppDomain.CurrentDomain.BaseDirectory);
        }
        public async void NPCText(string npcName, string message, bool redoLine = false) {
            if (message != null && !message.Contains("You have submitted")) {
                //bool gender = false;
                //byte race = 0;
                //int body = 0;
                bool isRetainer = false;
                ReportData data = null;
                if (NPCVoiceMapping.SpeakerList.ContainsKey(npcName)) {
                    data = NPCVoiceMapping.SpeakerList[npcName];
                    _territoryType = data.TerritoryId;
                }
                if (data != null) {
                    if (!isRetainer) {
                        string nameToUse = npcName;
                        string value = message;
                        string arcValue = message;
                        string backupVoice = PickVoiceBasedOnTraits(nameToUse, !data.gender, data.race, data.body, 0);
                        KeyValuePair<Stream, bool> stream =
                        await _npcVoiceManager.GetCharacterAudio(value, arcValue, nameToUse, !data.gender, backupVoice, false, true, "", redoLine);
                        if (stream.Key != null) {
                            Task task = null;
                            ushort lipId = 0;
                            bool canDoLipSync = true;
                            WaveStream wavePlayer = GetWavePlayer(npcName, stream.Key);
                            if (wavePlayer != null) {
                                bool useSmbPitch = CheckIfshouldUseSmbPitch(nameToUse, data.body);
                                float pitch = stream.Value ? CheckForDefinedPitch(nameToUse) :
                                CalculatePitchBasedOnTraits(nameToUse, !data.gender, data.race, data.body, 0.09f);
                                bool lipWasSynced = false;
                                _mediaManager.PlayAudioStream(new DummyObject(), wavePlayer, SoundType.NPC,
                                    false, useSmbPitch, pitch, 0, false, null, null, 1);
                            } else {
                            }
                        } else {
                        }
                    }
                }
            }
        }
        public WaveStream GetWavePlayer(string npcName, Stream stream) {
            WaveStream wavePlayer = null;
            try {
                stream.Position = 0;
                wavePlayer = new Mp3FileReader(stream);
            } catch {
                stream.Position = 0;
                if (stream.Length > 0) {
                    float[] data = DecodeOggOpusToPCM(stream);
                    if (data.Length > 0) {
                        WaveFormat waveFormat = new WaveFormat(48000, 16, 1);
                        MemoryStream memoryStream = new MemoryStream();
                        WaveFileWriter writer = new WaveFileWriter(memoryStream, waveFormat);
                        writer.WriteSamples(data.ToArray(), 0, data.Length);
                        writer.Flush();
                        memoryStream.Position = 0;
                        if (memoryStream.Length > 0) {
                            var newPlayer = new WaveFileReader(memoryStream);
                            if (newPlayer.TotalTime.Milliseconds > 100) {
                                wavePlayer = newPlayer;
                            } else {
                            }
                        } else {
                        }
                    } else {
                    }
                } else {
                    //Dalamud.Logging.PluginLog.LogWarning($"Received audio stream for {npcName} is empty.");
                    //if (reportData != null) {
                    //    reportData.ReportToXivVoice();
                    //}
                }
            }
            return wavePlayer;
        }
        public static float[] DecodeOggOpusToPCM(Stream stream) {
            // Read the Opus file
            // Initialize the decoder
            OpusDecoder decoder = new OpusDecoder(48000, 1); // Assuming a sample rate of 48000 Hz and mono audio
            OpusOggReadStream oggStream = new OpusOggReadStream(decoder, stream);

            // Buffer for storing the decoded samples
            List<float> pcmSamples = new List<float>();

            // Read and decode the entire file
            while (oggStream.HasNextPacket) {
                short[] packet = oggStream.DecodeNextPacket();
                if (packet != null) {
                    foreach (var sample in packet) {
                        pcmSamples.Add(sample / 32768f); // Convert to float and normalize
                    }
                }
            }

            return pcmSamples.ToArray();
        }
        private float CalculatePitchBasedOnTraits(string value, bool gender, byte race, int body, float range) {
            string lowered = value.ToLower();
            Random random = new Random(GetSimpleHash(value));
            bool isHigherVoiced = lowered.Contains("way") || body == 4 || (body == 0 && _territoryType == 816)
                || (body == 0 && _territoryType == 152) || (body == 11005) || (body == 278) || (body == 626) || (body == 11051);
            bool isDeepVoiced = false;
            float pitch = CheckForDefinedPitch(value);
            float pitchOffset = (((float)random.Next(-100, 100) / 100f) * range);
            if (!gender && body != 4) {
                switch (race) {
                    case 0:
                        pitchOffset = (((float)Math.Abs(random.Next(-10, 100)) / 100f) * range);
                        break;
                    case 1:
                        pitchOffset = (((float)Math.Abs(random.Next(-50, 100)) / 100f) * range);
                        break;
                    case 2:
                        pitchOffset = (((float)Math.Abs(random.Next(-10, 100)) / 100f) * range);
                        break;
                    case 3:
                        pitchOffset = (((float)Math.Abs(random.Next(-50, 100)) / 100f) * range);
                        break;
                    case 4:
                        pitchOffset = (((float)Math.Abs(random.Next(-100, 100)) / 100f) * range);
                        break;
                    case 5:
                        pitchOffset = (((float)Math.Abs(random.Next(-10, 100)) / 100f) * range);
                        break;
                    case 6:
                        pitchOffset = (((float)Math.Abs(random.Next(-10, 100)) / 100f) * range);
                        break;
                    case 7:
                        pitchOffset = (((float)Math.Abs(random.Next(-100, 100)) / 100f) * range);
                        break;
                    case 8:
                        pitchOffset = (((float)Math.Abs(random.Next(-10, 100)) / 100f) * range);
                        break;
                }
                switch (body) {
                    case 60:
                    case 63:
                    case 239:
                    case 8329:
                    case 626:
                    case 706:
                        pitchOffset = (((float)Math.Abs(random.Next(-100, -10)) / 100f) * range);
                        isDeepVoiced = true;
                        break;
                }
            } else {
                switch (body) {
                    case 4:
                        switch (gender) {
                            case false:
                                pitchOffset = (((float)Math.Abs(random.Next(-100, 100)) / 100f) * range);
                                break;
                            case true:
                                pitchOffset = (((float)random.Next(0, 100) / 100f) * range);
                                break;

                        }
                        break;
                }
            }
            if (pitch == 1) {
                return (isHigherVoiced ? 1.2f : isDeepVoiced ? 0.9f : 1) + pitchOffset;
            } else {
                return pitch;
            }
        }
        private bool CheckIfshouldUseSmbPitch(string npcName, int bodyType) {
            foreach (var value in NPCVoiceMapping.GetEchoType()) {
                if (npcName.Contains(value.Key)) {
                    return value.Value;
                }
            }
            switch (bodyType) {
                case 60:
                case 63:
                case 239:
                case 278:
                case 8329:
                case 626:
                case 11051:
                case 706:
                    return true;
            }
            return false;
        }

        private float CheckForDefinedPitch(string npcName) {
            foreach (var value in NPCVoiceMapping.GetPitchValues()) {
                if (npcName.Contains(value.Key)) {
                    return value.Value;
                }
            }
            return 1;
        }
        public string PickVoiceBasedOnTraits(string npcName, bool gender, byte race, int body, uint territory) {
            string[] maleVoices = GetVoicesBasedOnTerritory(territory, false);
            string[] femaleVoices = GetVoicesBasedOnTerritory(territory, true);
            string[] femaleViera = new string[] { "Aet", "Cet", "Uet" };
            if (npcName.EndsWith("way") || body == 11052) {
                return "Lrit";
            }
            if (npcName.ToLower().Contains("kup") || npcName.ToLower().Contains("puk")
                || npcName.ToLower().Contains("mog") || npcName.ToLower().Contains("moogle")
                || npcName.ToLower().Contains("furry creature") || body == 11006) {
                return "Kop";
            }
            if (body == 11029) {
                gender = true;
            }
            if (npcName.ToLower().Contains("siren")) {
                gender = true;
            }
            switch (race) {
                case 0:
                case 1:
                case 2:
                case 3:
                case 5:
                case 6:
                case 4:
                case 7:
                    return !gender && body != 4 ?
                    PickVoice(npcName, maleVoices) :
                    PickVoice(npcName, femaleVoices);
                case 8:
                    return gender ? PickVoice(npcName, femaleViera) :
                    PickVoice(npcName, maleVoices);
            }
            return "";
        }
        private string PickVoice(string name, string[] choices) {
            Random random = new Random(GetSimpleHash(name));
            return choices[random.Next(0, choices.Length)];
        }
        private int GetSimpleHash(string s) {
            return s.Select(a => (int)a).Sum();
        }
        string[] GetVoicesBasedOnTerritory(uint territory, bool gender) {
            string[] maleVoices = new string[] { "Mciv", "Zin", "udm1", "gm1", "Beggarly", "gnat", "ig1", "thord", "vark", "ckeep", "pide", "motanist", "lator", "sail", "lodier" };
            string[] femaleThavnair = new string[] { "tf1", "tf2", "tf3", "tf4" };
            string[] femaleVoices = new string[] { "Maiden", "Dla", "irhm", "ouncil", "igate" };
            string[] maleThavnair = new string[] { "tm1", "tm2", "tm3", "tm4" };
            string[] femaleViera = new string[] { "Aet", "Cet", "Uet" };
            switch (territory) {
                case 963:
                case 957:
                    return gender ? femaleThavnair : maleThavnair;
                default:
                    return gender ? femaleVoices : maleVoices;
            }

        }
    }
}
