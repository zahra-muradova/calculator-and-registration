using System;
using System.Drawing;
using System.Windows.Forms;

// Alias definitions to prevent any ambiguity errors with Reflection or WPF
using WinLabel = System.Windows.Forms.Label;
using WinButton = System.Windows.Forms.Button;
using WinTextBox = System.Windows.Forms.TextBox;
using WinComboBox = System.Windows.Forms.ComboBox;
using WinCheckBox = System.Windows.Forms.CheckBox;
using WinGroupBox = System.Windows.Forms.GroupBox;

namespace CodeOnlyApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Change this to 'new SignInForm()' if you want to run the Sign In form instead!
            Application.Run(new SignInForm());
        }
    }

    // ==========================================
    // PHOTO 1: CALCULATOR FORM
    // ==========================================
    public class CalculatorForm : Form
    {
        private WinTextBox txtNum1;
        private WinTextBox txtNum2;
        private WinComboBox cmbCommand;
        private WinLabel lblAnswer;
        private WinButton btnResult;
        private WinButton btnClear;

        public CalculatorForm()
        {
            // Form Setup
            this.Text = "Calculator";
            this.Size = new Size(350, 480);
            this.BackColor = Color.Teal;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Number One Label & Input
            WinLabel lbl1 = new WinLabel { Text = "Number one", ForeColor = Color.White, Location = new Point(30, 20), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtNum1 = new WinTextBox { Text = "0", Location = new Point(30, 45), Size = new Size(270, 25) };

            // Number Two Label & Input
            WinLabel lbl2 = new WinLabel { Text = "Number two", ForeColor = Color.White, Location = new Point(30, 85), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtNum2 = new WinTextBox { Text = "0", Location = new Point(30, 110), Size = new Size(270, 25) };

            // Command Dropdown Label & Input
            WinLabel lbl3 = new WinLabel { Text = "Command", ForeColor = Color.White, Location = new Point(30, 150), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            cmbCommand = new WinComboBox { Location = new Point(30, 175), Size = new Size(270, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbCommand.Items.AddRange(new string[] { "Add", "Subtract", "Multiply", "Divide" });
            cmbCommand.SelectedIndex = 0;

            // Answer Display
            WinLabel lblAnsTitle = new WinLabel { Text = "Answer:", ForeColor = Color.White, Location = new Point(30, 220), AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            lblAnswer = new WinLabel { Text = "0", ForeColor = Color.White, Location = new Point(120, 220), AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold) };

            // Buttons
            btnResult = new WinButton { Text = "Result", Location = new Point(30, 260), Size = new Size(270, 40), BackColor = Color.LightGray, FlatStyle = FlatStyle.Flat };
            btnClear = new WinButton { Text = "Clear", Location = new Point(30, 315), Size = new Size(270, 40), BackColor = Color.LightGray, FlatStyle = FlatStyle.Flat };

            // Attach Event Handlers
            btnResult.Click += BtnResult_Click;
            btnClear.Click += BtnClear_Click;

            // Add Controls to Form
            this.Controls.AddRange(new Control[] { lbl1, txtNum1, lbl2, txtNum2, lbl3, cmbCommand, lblAnsTitle, lblAnswer, btnResult, btnClear });
        }

        private void BtnResult_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtNum1.Text, out double n1) || !double.TryParse(txtNum2.Text, out double n2))
            {
                MessageBox.Show("Please enter valid numbers.");
                return;
            }

            double ans = cmbCommand.SelectedItem?.ToString() switch
            {
                "Add" => n1 + n2,
                "Subtract" => n1 - n2,
                "Multiply" => n1 * n2,
                "Divide" => n2 != 0 ? n1 / n2 : double.NaN,
                _ => 0
            };

            lblAnswer.Text = double.IsNaN(ans) ? "Cannot divide by 0" : ans.ToString();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtNum1.Text = "0";
            txtNum2.Text = "0";
            lblAnswer.Text = "0";
            cmbCommand.SelectedIndex = 0;
        }
    }

    // ==========================================
    // PHOTO 2: SIGN IN & REGISTRATION FORM
    // ==========================================
    public class SignInForm : Form
    {
        private WinTextBox txtSignUser, txtSignPass, txtRegUser, txtRegPass;
        private WinCheckBox chkSignPass, chkRegPass;

        public SignInForm()
        {
            // Form Setup
            this.Text = "Sign in";
            this.Size = new Size(650, 360);
            this.BackColor = Color.Teal;
            this.StartPosition = FormStartPosition.CenterScreen;

            // --- SIGN IN GROUPBOX ---
            WinGroupBox gbSignIn = new WinGroupBox { Text = "Sign in", ForeColor = Color.White, Location = new Point(20, 20), Size = new Size(280, 270), Font = new Font("Segoe UI", 9, FontStyle.Bold) };

            WinLabel lbl1 = new WinLabel { Text = "Username", Location = new Point(20, 30), AutoSize = true };
            txtSignUser = new WinTextBox { Location = new Point(20, 52), Size = new Size(235, 25) };

            WinLabel lbl2 = new WinLabel { Text = "Password", Location = new Point(20, 90), AutoSize = true };
            txtSignPass = new WinTextBox { Location = new Point(20, 112), Size = new Size(235, 25), PasswordChar = '*' };

            chkSignPass = new WinCheckBox { Text = "Show me password", Location = new Point(20, 150), AutoSize = true };
            chkSignPass.CheckedChanged += (s, e) => txtSignPass.PasswordChar = chkSignPass.Checked ? '\0' : '*';

            WinButton btnSignIn = new WinButton { Text = "Sign in", Location = new Point(45, 190), Size = new Size(185, 40), BackColor = Color.DarkGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSignIn.Click += (s, e) => MessageBox.Show($"Logged in as {txtSignUser.Text}");

            gbSignIn.Controls.AddRange(new Control[] { lbl1, txtSignUser, lbl2, txtSignPass, chkSignPass, btnSignIn });

            // --- REGISTRATION GROUPBOX ---
            WinGroupBox gbReg = new WinGroupBox { Text = "Registration", ForeColor = Color.White, Location = new Point(320, 20), Size = new Size(280, 270), Font = new Font("Segoe UI", 9, FontStyle.Bold) };

            WinLabel lbl3 = new WinLabel { Text = "Username", Location = new Point(20, 30), AutoSize = true };
            txtRegUser = new WinTextBox { Location = new Point(20, 52), Size = new Size(235, 25) };

            WinLabel lbl4 = new WinLabel { Text = "Password", Location = new Point(20, 90), AutoSize = true };
            txtRegPass = new WinTextBox { Location = new Point(20, 112), Size = new Size(235, 25), PasswordChar = '*' };

            chkRegPass = new WinCheckBox { Text = "Show me password", Location = new Point(20, 150), AutoSize = true };
            chkRegPass.CheckedChanged += (s, e) => txtRegPass.PasswordChar = chkRegPass.Checked ? '\0' : '*';

            WinButton btnSignUp = new WinButton { Text = "Sign up", Location = new Point(45, 190), Size = new Size(185, 40), BackColor = Color.DarkGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSignUp.Click += (s, e) => MessageBox.Show($"Registered {txtRegUser.Text}!");

            gbReg.Controls.AddRange(new Control[] { lbl3, txtRegUser, lbl4, txtRegPass, chkRegPass, btnSignUp });

            // Add GroupBoxes to main form
            this.Controls.Add(gbSignIn);
            this.Controls.Add(gbReg);
        }
    }
}