using System;
using MailKit.Net.Smtp;
using System.Windows.Forms;

namespace MailClient
{
    public partial class AuthorizationForm : Form
    {
        bool accessIsAllowed = false;

        bool loginIsDone;

        public AuthorizationForm(bool loginIsDone = false)
        {
            InitializeComponent();

            this.loginIsDone = loginIsDone;
        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587, false);

                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    client.Authenticate(emailTextBox.Text, passTextBox.Text);

                    if (client.IsAuthenticated)
                    {
                        accessIsAllowed = true;
                        Close();
                    }
                }
                catch (MailKit.Security.AuthenticationException)
                {
                    MessageBox.Show("Неверный логин или пароль. Попробуйте снова.\n");
                }
                catch (System.Net.Sockets.SocketException)
                {
                    MessageBox.Show("Ошибка соеденения. Проверьте подключение к Интернету. \n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                client.Disconnect(true);
            }
        }

        private void AuthorizationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!loginIsDone && !accessIsAllowed)
                Application.Exit();
            else if (accessIsAllowed)
            {
                MainForm mainForm = (MainForm)Owner;

                MainForm.Sender snd = new MainForm.Sender();

                snd.email = emailTextBox.Text;
                snd.password = passTextBox.Text;

                mainForm.senderTextBox.Text = emailTextBox.Text;
                mainForm.emailSender = snd;
            }
                
        }
    }
}
