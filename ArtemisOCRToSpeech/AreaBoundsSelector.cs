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
    public partial class AreaBoundsSelector : Form {
        public string LabelName {
            get => resizeConfirmation.Text; set => resizeConfirmation.Text = value;
        }
        public AreaBoundsSelector() {
            InitializeComponent();
        }

        private void resizeConfirmation_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
