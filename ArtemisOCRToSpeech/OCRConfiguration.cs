using FFXIVLooseTextureCompiler.ImageProcessing;
using Newtonsoft.Json;
using RoleplayingVoiceDalamud.Voice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using static System.Net.Mime.MediaTypeNames;
using Rectangle = System.Drawing.Rectangle;

namespace ArtemisOCRToSpeech {
    public partial class OCRConfiguration : Form {
        private string _configPath;
        Configuration configuration = new Configuration() { DialoguePosition = new Rectangle(0, 0, 300, 200), SpeakerNamePosition = new Rectangle(0, 0, 300, 200) };
        private ArtemisFFXIV _artemis;
        private string _lastDialogue;
        private TesseractEngine ocr;
        private string _lastValidDialogue;
        private string _lastValidSpeakerName;

        public OCRConfiguration() {
            InitializeComponent();
            _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration.json");
            if (File.Exists(_configPath)) {
                configuration = OpenConfig();
            }
            NPCVoiceMapping.Initialize();
            ocr = new TesseractEngine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata\\"), "eng");
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
                if (_artemis == null) {
                    _artemis = new ArtemisFFXIV();
                }
                startFFXIVReaderButton.Text = "Stop FFXIV Reader";
            } else {
                startFFXIVReaderButton.Text = "Start FFXIV Reader";
            }
        }

        private void ocrChecker_Tick(object sender, EventArgs e) {
            Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
            try {
                Bitmap captureBitmap = new Bitmap(captureRectangle.Width, captureRectangle.Height, PixelFormat.Format32bppArgb);
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                var speakerImage = Contrast.AdjustContrast(Invert(captureBitmap.Clone(configuration.SpeakerNamePosition, captureBitmap.PixelFormat)), 1.5f);
                var dialogueImage = captureBitmap.Clone(configuration.DialoguePosition, captureBitmap.PixelFormat);
                var speakerPage = ocr.Process(speakerImage);
                string originalSpeaker = speakerPage.GetText();
                string speaker = NameFinder(CleanString(originalSpeaker));
                speakerPage.Dispose();
                var dialogPage = ocr.Process(dialogueImage);

                string originalDialogueText = dialogPage.GetText();
                string[] newDialogue = dialogPage.GetText().Split("  ");
                string dialogue = CleanString(GetNextFullString(newDialogue).Trim()).Trim();
                dialogPage.Dispose();
                if (!string.IsNullOrEmpty(speaker)) {
                    _lastValidSpeakerName = speaker;
                }
                if (!string.IsNullOrEmpty(dialogue) && 
                (dialogue.EndsWith(".") || dialogue.EndsWith("?") || dialogue.EndsWith("!"))) {
                    _lastValidDialogue = dialogue;
                }
                if (!string.IsNullOrEmpty(_lastValidSpeakerName)) {
                    if (_lastDialogue == null || (!string.IsNullOrEmpty(_lastValidDialogue) && !_lastDialogue.Contains(_lastValidDialogue))) {
                        _artemis?.NPCText(_lastValidSpeakerName, _lastValidDialogue);
                        _lastDialogue = _lastValidDialogue;
                        _lastValidSpeakerName = null;
                        _lastValidDialogue = null;

                    }
                }
                speakerLabel.Text = speaker;
                dialogueTextBox.Text = dialogue + "\r\n\r\n\r\n\r\n" + originalDialogueText;
                speakerImageBox.BackgroundImage = speakerImage;
                dialogueImageBox.BackgroundImage = dialogueImage;
            } catch {

            }
        }
        string GetNextFullString(string[] strings) {
            string lastValidValue = strings[0];
            foreach (string value in strings) {
                if (!string.IsNullOrEmpty(value.Trim())) {
                    lastValidValue = value;
                }
            }
            return lastValidValue.Replace("\r\n", " ");
        }
        string CleanString(string value) {
            Regex rgx = new Regex("[^a-zA-Z0-9.?!', -]");
            return rgx.Replace(value.Replace("-", ", ").Replace("—", ", ").Replace("1", "I").Replace("|", "I").Replace("/", "l"), "").Replace(".", ". ").Replace("  ", " ").Replace(". . . ", "...").TrimStart('.').Trim();
        }
        string NameFinder(string name) {
            var mappings = NPCVoiceMapping.SpeakerList.Keys;
            foreach (var mapping in mappings) {
                if (CleanString(name.ToLower()).Contains(CleanString(mapping.ToLower()))) {
                    return mapping;
                }
            }
            return "";
        }
        public Bitmap Invert(Bitmap file) {
            Bitmap invertedImage = file;
            using (LockBitmap invertedBits = new LockBitmap(invertedImage)) {
                for (int y = 0; y < invertedBits.Height; y++) {
                    for (int x = 0; x < invertedBits.Width; x++) {
                        Color invertedPixel = invertedBits.GetPixel(x, y);
                        invertedPixel = Color.FromArgb(invertedPixel.A, (255 - invertedPixel.R), (255 - invertedPixel.G), (255 - invertedPixel.B));
                        invertedBits.SetPixel(x, y, invertedPixel);
                    }
                }
            }
            return invertedImage;
        }
    }
}
