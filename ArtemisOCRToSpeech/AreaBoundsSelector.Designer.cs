namespace ArtemisOCRToSpeech {
    partial class AreaBoundsSelector {
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
            label1 = new Label();
            resizeConfirmation = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(0, 30);
            label1.TabIndex = 0;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // resizeConfirmation
            // 
            resizeConfirmation.AutoSize = true;
            resizeConfirmation.Dock = DockStyle.Fill;
            resizeConfirmation.FlatStyle = FlatStyle.Flat;
            resizeConfirmation.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            resizeConfirmation.Location = new Point(0, 0);
            resizeConfirmation.Name = "resizeConfirmation";
            resizeConfirmation.Size = new Size(473, 62);
            resizeConfirmation.TabIndex = 1;
            resizeConfirmation.Text = "Resize To Cover Area And Then Click Window";
            resizeConfirmation.UseVisualStyleBackColor = true;
            resizeConfirmation.Click += resizeConfirmation_Click;
            // 
            // AreaBoundsSelector
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(128, 128, 255);
            ClientSize = new Size(473, 62);
            Controls.Add(resizeConfirmation);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "AreaBoundsSelector";
            Text = "AreaBoundsSelector";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button resizeConfirmation;
    }
}