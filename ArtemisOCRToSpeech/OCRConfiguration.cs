using IronOcr;
using IronSoftware.Drawing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rectangle = System.Drawing.Rectangle;

namespace ArtemisOCRToSpeech {
    public partial class OCRConfiguration : Form {
        private string _configPath;
        Configuration configuration = new Configuration() { DialoguePosition = new Rectangle(0, 0, 300, 200), SpeakerNamePosition = new Rectangle(0, 0, 300, 200) };
        private ArtemisFFXIV _artemis;
        private string _lastDialogue;

        public OCRConfiguration() {
            InitializeComponent();
            _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration.json");
            if (File.Exists(_configPath)) {
                configuration = OpenConfig();
            }
            _artemis = new ArtemisFFXIV();
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
            ocrChecker.Enabled = !ocrChecker.Enabled;
            if (ocrChecker.Enabled) {
                startFFXIVReaderButton.Text = "Stop FFXIV Reader";
            } else {
                startFFXIVReaderButton.Text = "Start FFXIV Reader";
            }
        }

        private void ocrChecker_Tick(object sender, EventArgs e) {
            Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
            Bitmap captureBitmap = new Bitmap(captureRectangle.Width, captureRectangle.Height, PixelFormat.Format32bppArgb);
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);
            captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
            var speakerImage = captureBitmap.Clone(configuration.SpeakerNamePosition, captureBitmap.PixelFormat);
            var dialogueImage = captureBitmap.Clone(configuration.DialoguePosition, captureBitmap.PixelFormat);
            Tesseract.
            string speaker = new Tesseract().Read(speakerImage).Text;
            string dialogue = new Tesseract().Read(dialogueImage).Text;
            if (dialogue != _lastDialogue) {
                _artemis.NPCText(speaker, dialogue);
                _lastDialogue = dialogue;
                MessageBox.Show(dialogue);
            }
        }
    }
}
