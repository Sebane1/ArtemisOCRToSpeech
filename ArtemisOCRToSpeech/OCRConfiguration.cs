using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtemisOCRToSpeech {
    public partial class OCRConfiguration : Form {
        private string _configPath;
        Configuration configuration = new Configuration() { DialoguePosition = new Rectangle(0, 0, 300, 200), SpeakerNamePosition = new Rectangle(0, 0, 300, 200) };
        public OCRConfiguration() {
            InitializeComponent();
            _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration.json");
            if (File.Exists(_configPath)) {
                configuration = OpenConfig();
            }
        }

        private void configureNameAreaButton_Click(object sender, EventArgs e) {
            AreaBoundsSelector areaBoundsSelector = new AreaBoundsSelector();
            areaBoundsSelector.StartPosition = FormStartPosition.Manual;
            areaBoundsSelector.Size = configuration.SpeakerNamePosition.Size;
            areaBoundsSelector.Location = configuration.SpeakerNamePosition.Location;
            areaBoundsSelector.LabelName = "Resize To Cover NPC Name And Click Window";
            if (areaBoundsSelector.ShowDialog() == DialogResult.OK) {
                configuration.SpeakerNamePosition = new Rectangle(areaBoundsSelector.Location, areaBoundsSelector.Size);
                SaveConfig();
            }
        }

        private void configureDescriptionAreaButton_Click(object sender, EventArgs e) {
            AreaBoundsSelector areaBoundsSelector = new AreaBoundsSelector();
            areaBoundsSelector.StartPosition = FormStartPosition.Manual;
            areaBoundsSelector.Size = configuration.DialoguePosition.Size;
            areaBoundsSelector.Location = configuration.DialoguePosition.Location;
            areaBoundsSelector.LabelName = "Resize To Cover NPC Dialogue And Click Window";
            if (areaBoundsSelector.ShowDialog() == DialogResult.OK) {
                configuration.DialoguePosition = new Rectangle(areaBoundsSelector.Location, areaBoundsSelector.Size);
                SaveConfig();
            }
        }
        private void SaveConfig() {
            string json = JsonConvert.SerializeObject(configuration);
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration.json"), json);
        }
        private Configuration OpenConfig() {
            using (var stream = File.OpenText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration.json"))) {
                string json = stream.ReadToEnd();
                return JsonConvert.DeserializeObject<Configuration>(json);
            }
        }

        private void startFFXIVReaderButton_Click(object sender, EventArgs e) {
            ocrChecker.Start();
        }
    }
}
