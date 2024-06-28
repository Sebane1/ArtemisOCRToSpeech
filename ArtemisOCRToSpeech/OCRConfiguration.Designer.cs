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
            configureDescriptionAreaButton.Location = new Point(4, 28);
            configureDescriptionAreaButton.Name = "configureDescriptionAreaButton";
            configureDescriptionAreaButton.Size = new Size(168, 23);
            configureDescriptionAreaButton.TabIndex = 1;
            configureDescriptionAreaButton.Text = "Configure Dialogue Area";
            configureDescriptionAreaButton.UseVisualStyleBackColor = true;
            configureDescriptionAreaButton.Click += configureDescriptionAreaButton_Click;
            // 
            // startFFXIVReaderButton
            // 
            startFFXIVReaderButton.Location = new Point(5, 52);
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
            // OCRConfiguration
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(179, 77);
            Controls.Add(startFFXIVReaderButton);
            Controls.Add(configureDescriptionAreaButton);
            Controls.Add(configureNameAreaButton);
            Name = "OCRConfiguration";
            Text = "OCRConfiguration";
            ResumeLayout(false);
        }

        #endregion

        private Button configureNameAreaButton;
        private Button configureDescriptionAreaButton;
        private Button startFFXIVReaderButton;
        private System.Windows.Forms.Timer ocrChecker;
    }
}