using Re_ports;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace annual_remuneration
{
    public partial class Form2 : Form
    {
        public Form2(
            string name, string position, string dateIssued, string startDate, string employmentStatus, string school,
            string basicSalary, string augmentation, string allowance, string clothingAllowance,
            string chalkAllowance, string pei, string monthBonus13, string monthBonus14,
            string cashGift, string total, bool[] checkBoxStates)
        {
            InitializeComponent();

            // Make the RichTextBox scrollable
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Both;

            // Set margins (approx. 1 inch on left & right)
            richTextBox1.Margin = new Padding(72, 10, 72, 10);

            // Set tab stops for alignment
            richTextBox1.SelectionTabs = new int[] { 450 };

            // Title (Centered)
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.SelectionFont = new Font("Baskerville Old Face", 36, FontStyle.Bold);
            richTextBox1.AppendText("C E R T I F I C A T I O N\n\n");

            // TO WHOM IT MAY CONCERN (Left-aligned)
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            richTextBox1.SelectionFont = new Font("Century Gothic", 13, FontStyle.Bold);
            richTextBox1.AppendText("TO WHOM IT MAY CONCERN:\n\n");

            // Main Text (With Bold Names, Position, etc.)
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Regular);
            richTextBox1.AppendText("    This certifies that ");
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Bold);
            richTextBox1.AppendText(name);
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Regular);
            richTextBox1.AppendText(", has been employed in this division since ");
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Bold);
            richTextBox1.AppendText(startDate);
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Regular);
            richTextBox1.AppendText(", and presently holding the position of ");
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Bold);
            richTextBox1.AppendText(position);
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Regular);
            richTextBox1.AppendText(" at ");
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Bold);
            richTextBox1.AppendText(school);
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Regular);
            richTextBox1.AppendText(", under ");
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Bold);
            richTextBox1.AppendText(employmentStatus);
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Regular);
            richTextBox1.AppendText(" status with the following annual remuneration:\n\n");

            // Salary Breakdown (Aligned Properly)
            int tabStop = 600; // Adjust tab stop as needed for alignment
            richTextBox1.SelectionTabs = new int[] { tabStop };

            Font breakdownFont = new Font("Book Antiqua", 11, FontStyle.Bold);

            // Apply font and append each line with consistent tabbing
            if (checkBoxStates[0])
            {
                richTextBox1.SelectionFont = breakdownFont;
                richTextBox1.AppendText("    Basic Salary\t\t\t" + basicSalary + "\n");
            }
            if (checkBoxStates[1])
            {
                richTextBox1.SelectionFont = breakdownFont;
                richTextBox1.AppendText("    Augmentation Allowance\t\t\t" + augmentation + "\n");
            }
            if (checkBoxStates[2])
            {
                richTextBox1.SelectionFont = breakdownFont;
                richTextBox1.AppendText("    Allowance (PERA/ADCOM)\t\t\t" + allowance + "\n");
            }
            if (checkBoxStates[3])
            {
                richTextBox1.SelectionFont = breakdownFont;
                richTextBox1.AppendText("    Clothing Allowance\t\t\t" + clothingAllowance + "\n");
            }
            if (checkBoxStates[4])
            {
                richTextBox1.SelectionFont = breakdownFont;
                richTextBox1.AppendText("    Chalk Allowance\t\t\t" + chalkAllowance + "\n");
            }
            if (checkBoxStates[5])
            {
                richTextBox1.SelectionFont = breakdownFont;
                richTextBox1.AppendText("    PEI\t\t\t" + pei + "\n");
            }
            if (checkBoxStates[6])
            {
                richTextBox1.SelectionFont = breakdownFont;
                richTextBox1.AppendText("    13th month Bonus\t\t\t" + monthBonus13 + "\n");
            }
            if (checkBoxStates[7])
            {
                richTextBox1.SelectionFont = breakdownFont;
                richTextBox1.AppendText("    14th month Bonus\t\t\t" + monthBonus14 + "\n");
            }
            if (checkBoxStates[8])
            {
                richTextBox1.SelectionFont = breakdownFont;
                richTextBox1.AppendText("    Cash Gift\t\t\t" + cashGift + "\n");
            }

            // Add the total with proper formatting
        
            richTextBox1.SelectionFont = new Font("Book Antiqua", 11, FontStyle.Bold);
            richTextBox1.AppendText("    Total\t\t\t" + total + "\n\n");


            // Issue Date Section
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Regular);
            richTextBox1.AppendText("    Issued this ");
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Regular);
            // Split the dateIssued into day, month, and year
            string[] dateParts = dateIssued.Split(' '); // Assuming the format is "29 November 2024"
            if (dateParts.Length == 3) // Ensure it has day, month, and year
            {
                richTextBox1.AppendText(dateParts[0]); // Day
                richTextBox1.AppendText(" day of ");   // Add "day of"
                richTextBox1.AppendText(dateParts[1] + " " + dateParts[2]); // Month and Year
            }
            else
            {
                // Fallback: Append the full dateIssued if the format is incorrect
                richTextBox1.AppendText(dateIssued);
            }
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Regular);
            richTextBox1.AppendText(" upon the request of ");
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Bold);
            richTextBox1.AppendText(name);
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Regular);
            richTextBox1.AppendText(" for whatever legal purpose it may serve.\n\n");

            // Signature Section (Right-Aligned, Increased Font)
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Regular);
            richTextBox1.AppendText("For the Office of the Schools Division Superintendent:\n\n");

            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Bold);
            richTextBox1.AppendText("EDERLYN N. CASTILLO\n");
            richTextBox1.SelectionFont = new Font("Century Gothic", 12, FontStyle.Regular);
            richTextBox1.AppendText("Administrative Officer IV\n");
        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = Application.OpenForms["Form1"] as Form1;
            if (form1 != null)
            {
                form1.Show();
            }
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e) { }
    }
}
