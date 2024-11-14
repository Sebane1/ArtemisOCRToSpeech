namespace ArtemisOCRToSpeech {
    partial class OCRConfiguration {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            configureNameAreaButton = new Button();
            configureDescriptionAreaButton = new Button();
            startFFXIVReaderButton = new Button();
            ocrChecker = new System.Windows.Forms.Timer(components);
            speakerImageBox = new PictureBox();
            dialogueImageBox = new PictureBox();
            speakerLabel = new Label();
            dialogueTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)speakerImageBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dialogueImageBox).BeginInit();
            SuspendLayout();
            // 
            // configureNameAreaButton
            // 
            configureNameAreaButton.Location = new Point(4, 4);
            configureNameAreaButton.Name = "configureNameAreaButton";
            configureNameAreaButton.Size = new Size(168, 23);
            configureNameAreaButton.TabIndex = 0;
            configureNameAreaButton.Text = "Configure Name Area";
            configureNameAreaButton.UseVisualStyleBackColor = true;
            configureNameAreaButton.Click += configureNameAreaButton_Click;
            // 
            // configureDescriptionAreaButton
            // 
            configureDescriptionAreaButton.Location = new Point(178, 4);
            configureDescriptionAreaButton.Name = "configureDescriptionAreaButton";
            configureDescriptionAreaButton.Size = new Size(168, 23);
            configureDescriptionAreaButton.TabIndex = 1;
            configureDescriptionAreaButton.Text = "Configure Dialogue Area";
            configureDescriptionAreaButton.UseVisualStyleBackColor = true;
            configureDescriptionAreaButton.Click += configureDescriptionAreaButton_Click;
            // 
            // startFFXIVReaderButton
            // 
            startFFXIVReaderButton.Location = new Point(352, 4);
            startFFXIVReaderButton.Name = "startFFXIVReaderButton";
            startFFXIVReaderButton.Size = new Size(168, 23);
            startFFXIVReaderButton.TabIndex = 2;
            startFFXIVReaderButton.Text = "Start FFXIV Reader";
            startFFXIVReaderButton.UseVisualStyleBackColor = true;
            startFFXIVReaderButton.Click += startFFXIVReaderButton_Click;
            // 
            // ocrChecker
            // 
            ocrChecker.Interval = 1000;
            ocrChecker.Tick += ocrChecker_Tick;
            // 
            // speakerImageBox
            // 
            speakerImageBox.BackgroundImageLayout = ImageLayout.Stretch;
            speakerImageBox.Location = new Point(4, 33);
            speakerImageBox.Name = "speakerImageBox";
            speakerImageBox.Size = new Size(217, 45);
            speakerImageBox.TabIndex = 3;
            speakerImageBox.TabStop = false;
            // 
            // dialogueImageBox
            // 
            dialogueImageBox.BackgroundImageLayout = ImageLayout.Stretch;
            dialogueImageBox.Location = new Point(4, 84);
            dialogueImageBox.Name = "dialogueImageBox";
            dialogueImageBox.Size = new Size(618, 183);
            dialogueImageBox.TabIndex = 4;
            dialogueImageBox.TabStop = false;
            // 
            // speakerLabel
            // 
            speakerLabel.AutoSize = true;
            speakerLabel.Location = new Point(8, 283);
            speakerLabel.Name = "speakerLabel";
            speakerLabel.Size = new Size(48, 15);
            speakerLabel.TabIndex = 5;
            speakerLabel.Text = "Speaker";
            // 
            // dialogueTextBox
            // 
            dialogueTextBox.Location = new Point(8, 302);
            dialogueTextBox.Multiline = true;
            dialogueTextBox.Name = "dialogueTextBox";
            dialogueTextBox.Size = new Size(605, 117);
            dialogueTextBox.TabIndex = 6;
            // 
            // OCRConfiguration
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(625, 431);
            Controls.Add(dialogueTextBox);
            Controls.Add(speakerLabel);
            Controls.Add(dialogueImageBox);
            Controls.Add(speakerImageBox);
            Controls.Add(startFFXIVReaderButton);
            Controls.Add(configureDescriptionAreaButton);
            Controls.Add(configureNameAreaButton);
            Name = "OCRConfiguration";
            Text = "OCRConfiguration";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)speakerImageBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)dialogueImageBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button configureNameAreaButton;
        private Button configureDescriptionAreaButton;
        private Button startFFXIVReaderButton;
        private System.Windows.Forms.Timer ocrChecker;
        private PictureBox speakerImageBox;
        private PictureBox dialogueImageBox;
        private Label speakerLabel;
        private TextBox dialogueTextBox;
    }
}