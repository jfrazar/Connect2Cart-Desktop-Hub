using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Connect2Cart_Desktop_Hub
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent(); // This must come first
            PopulateDropdowns();   // This comes after
        }

        private void PopulateDropdowns()
        {
            comboBoxScale.Items.Clear();
            comboBoxPrinter.Items.Clear();

            comboBoxScale.Items.AddRange(new string[]
            {
                "ONYX 5lb",
                "ONYX 70lb",
                "Stamps.com 5lb",
                "Stamps.com 70lb",
                "DYMO M10",
                "DYMO 25lb",
                "DYMO 100lb",
                "DYMO Mailing M10",
                "DYMO S50",
                "Mettler Toledo BC-60U"
            });

            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                comboBoxPrinter.Items.Add(printer);
            }

            // Load current config
            var config = Configuration.Current;
            comboBoxScale.SelectedItem = config.Scale ?? "";
            comboBoxPrinter.SelectedItem = config.Printer ?? "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var selectedScale = comboBoxScale.SelectedItem?.ToString() ?? "";
            var selectedPrinter = comboBoxPrinter.SelectedItem?.ToString() ?? "";

            Configuration.Current.Scale = selectedScale;
            Configuration.Current.Printer = selectedPrinter;
            Configuration.Save(Configuration.Current);

            MessageBox.Show("Settings saved.", "Connect2Cart Desktop Hub");
            this.Hide();
        }
    }
}
