using annual_remuneration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Re_ports
{
    public partial class Form1 : Form
    {
        private Dictionary<Control, Rectangle> controlBounds;
        private int originalFormWidth;
        private int originalFormHeight;

        public Form1()
        {
            InitializeComponent();

            // Save the original form size and control bounds
            originalFormWidth = this.Width;
            originalFormHeight = this.Height;

            // Store the original bounds of all controls
            controlBounds = new Dictionary<Control, Rectangle>();
            foreach (Control control in this.Controls)
            {
                controlBounds[control] = control.Bounds;
            }

            // Handle the Resize event for responsive design
            this.Resize += Form1_Resize;

            // Set up number-only validation for textBox7 to textBox15
            TextBox[] inputTextBoxes = {
                textBox7, textBox8, textBox9, textBox10,
                textBox11, textBox12, textBox13, textBox14, textBox15
            };

            foreach (TextBox textBox in inputTextBoxes)
            {
                textBox.KeyPress += TextBox_KeyPress; // Number-only input
                textBox.Leave += TextBox_Leave; // Format numbers with commas on leaving the field
            }

            // Make textBox16 read-only
            textBox16.ReadOnly = true;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Calculate scaling factors
            float scaleFactorWidth = (float)this.Width / originalFormWidth;
            float scaleFactorHeight = (float)this.Height / originalFormHeight;

            // Update each control's position and size
            foreach (var entry in controlBounds)
            {
                Control control = entry.Key;
                Rectangle originalBounds = entry.Value;

                control.Left = (int)(originalBounds.Left * scaleFactorWidth);
                control.Top = (int)(originalBounds.Top * scaleFactorHeight);
                control.Width = (int)(originalBounds.Width * scaleFactorWidth);
                control.Height = (int)(originalBounds.Height * scaleFactorHeight);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Sum up the values from textBox7 to textBox15
                double total = 0;

                // List of textboxes to sum up
                TextBox[] inputTextBoxes = {
                    textBox7, textBox8, textBox9, textBox10,
                    textBox11, textBox12, textBox13, textBox14, textBox15
                };

                foreach (TextBox textBox in inputTextBoxes)
                {
                    if (double.TryParse(textBox.Text, out double value))
                    {
                        total += value;
                    }
                    else if (!string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        MessageBox.Show($"Invalid input in {textBox.Name}. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Display the result in textBox16
                textBox16.Text = total.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FormatDateWithSuffix(DateTime date)
        {
            // Get the day of the month and determine its suffix
            int day = date.Day;
            string suffix = day % 10 == 1 && day != 11 ? "st" :
                            day % 10 == 2 && day != 12 ? "nd" :
                            day % 10 == 3 && day != 13 ? "rd" : "th";

            // Return the formatted date
            return $"{day}{suffix} {date:MMMM yyyy}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Retrieve values from Form1's TextBoxes
            string name = textBox2.Text;
            string position = textBox3.Text;
            string dateIssued = textBox4.Text;
            string startDate = textBox5.Text;
            string employmentStatus = textBox6.Text;
            string school = textBox17.Text;

            // Define the format that matches your input (e.g., "January 25, 2025")
            string format = "MMMM dd, yyyy";

            // Parse the dateIssued input to a DateTime object using TryParseExact
            if (DateTime.TryParseExact(dateIssued, format, null, System.Globalization.DateTimeStyles.None, out DateTime parsedDateIssued))
            {
                // Format the date
                dateIssued = FormatDateWithSuffix(parsedDateIssued);
            }
            else
            {
                MessageBox.Show("Invalid Date Issued format. Please enter a valid date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ensure numeric fields are properly parsed
            double.TryParse(textBox7.Text, out double basicSalaryVal);
            double.TryParse(textBox8.Text, out double augmentationVal);
            double.TryParse(textBox9.Text, out double allowanceVal);
            double.TryParse(textBox10.Text, out double clothingAllowanceVal);
            double.TryParse(textBox11.Text, out double chalkAllowanceVal);
            double.TryParse(textBox12.Text, out double peiVal);
            double.TryParse(textBox13.Text, out double monthBonus13Val);
            double.TryParse(textBox14.Text, out double monthBonus14Val);
            double.TryParse(textBox15.Text, out double cashGiftVal);

            // Calculate total
            double total = basicSalaryVal + augmentationVal + allowanceVal + clothingAllowanceVal +
                           chalkAllowanceVal + peiVal + monthBonus13Val + monthBonus14Val + cashGiftVal;

            // Convert total to string for display
            string totalStr = total.ToString("N2"); // Format with commas and two decimal places

            // Collect checkbox states
            bool[] checkBoxStates = {
        checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked,
        checkBox5.Checked, checkBox6.Checked, checkBox7.Checked, checkBox8.Checked, checkBox9.Checked
    };

            // Pass all data to Form2
            Form2 form2 = new Form2(
                name, position, dateIssued, startDate, employmentStatus, school,
                textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text,
                textBox11.Text, textBox12.Text, textBox13.Text, textBox14.Text,
                textBox15.Text, totalStr, checkBoxStates
            );

            // Show Form2
            form2.Show();
            this.Hide(); // Hide Form1
        }



        private void button4_Click(object sender, EventArgs e)
        {
            // Clear the values of textBox7 to textBox15
            TextBox[] inputTextBoxes = {
                textBox7, textBox8, textBox9, textBox10,
                textBox11, textBox12, textBox13, textBox14, textBox15
            };

            foreach (TextBox textBox in inputTextBoxes)
            {
                textBox.Clear();
            }

            // Clear the total display (optional)
            textBox16.Text = string.Empty;
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers, backspace, and decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Allow only one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            // Format the input with commas and two decimal places
            TextBox textBox = sender as TextBox;

            if (double.TryParse(textBox.Text, out double value))
            {
                textBox.Text = value.ToString("N2");
            }
        }

        // Empty methods to prevent errors caused by missing event handlers
        private void Form1_Load(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void pictureBox3_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void textBox8_TextChanged(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void textBox9_TextChanged(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
        private void textBox10_TextChanged(object sender, EventArgs e) { }
        private void textBox11_TextChanged(object sender, EventArgs e) { }
        private void textBox12_TextChanged(object sender, EventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }
        private void textBox13_TextChanged(object sender, EventArgs e) { }
        private void label14_Click(object sender, EventArgs e) { }
        private void textBox14_TextChanged(object sender, EventArgs e) { }
        private void label15_Click(object sender, EventArgs e) { }
        private void textBox15_TextChanged(object sender, EventArgs e) { }
        private void textBox16_TextChanged(object sender, EventArgs e) { }
        private void label16_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
