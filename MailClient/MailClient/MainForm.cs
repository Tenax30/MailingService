using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MailServerClassLibrary;

namespace MailClient
{
    public partial class MainForm : Form
    {

        bool addRecepientMode = false;

        List<string> recepients = new List<string>();

        List<NetFile> files = new List<NetFile>();

        public struct Sender
        {
            public string email;
            public string password;
        }

        public Sender emailSender;

        enum DataType { SenderData, RecepientsEmails, MessageTheme, MessageText, File, Send };

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AuthorizationForm authForm = new AuthorizationForm();
            authForm.ShowDialog(this);
        }

        private void changeUserBtn_Click(object sender, EventArgs e)
        {
            AuthorizationForm authForm = new AuthorizationForm(true);
            authForm.ShowDialog(this);
        }
        private void addUserBtn_Click(object sender, EventArgs e)
        {
            if(!addRecepientMode)
            {
                recepientsTB.ReadOnly = false;
                addRecepientBtn.Text = "ОК";
                ActiveControl = recepientsTB;
                recepientsLabel.Text = "Получатели (через пробел): ";
                sendBtn.Enabled = false;
                addRecepientMode = true;
            }
            else if(addRecepientMode)
            {
                recepients.Clear();

                try
                {
                    recepients.AddRange(recepientsTB.Text.Trim().Split(' '));

                    for (int i = 0; i < recepients.Count; i++)
                        if (!isValidEmail(recepients[i]))
                            throw new FormatException();

                    recepientsTB.ReadOnly = true;
                    addRecepientBtn.Text = "Добавить получателей";
                    recepientsLabel.Text = "Получатели:";
                    sendBtn.Enabled = true;
                    addRecepientMode = false;
                }
                catch(FormatException)
                {
                    MessageBox.Show("Неверный формат Email");
                }
            }
        }

        private bool isValidEmail(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            if(msgTextTB.Text == string.Empty)
            {
                MessageBox.Show("Введите текст сообщения.");
                return;
            }

            try
            {
                const int bufferSize = 8192;

                byte[] from = new byte[bufferSize];

                byte[] to = new byte[bufferSize];

                int bytesRec;

                string response;

                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3080);

                const int CONNECTIONS_COUNT = (int)DataType.Send + 1;

                for(int i = 0; i < CONNECTIONS_COUNT; i++)
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(endPoint);

                    string flag = i.ToString();

                    switch (i)
                    {
                        case (int)DataType.SenderData:

                            to = Encoding.UTF8.GetBytes(flag + emailSender.email +
                                " " + emailSender.password);
                            break;

                        case (int)DataType.RecepientsEmails:

                            string recepientsFormat = string.Empty;

                            for (int k = 0; k < recepients.Count; k++)
                                recepientsFormat += recepients[k] + " ";

                            recepientsFormat = recepientsFormat.TrimEnd(' ');

                            to = Encoding.UTF8.GetBytes(flag + recepientsFormat);
                            break;

                        case (int)DataType.MessageTheme:

                            if (msgThemeTB.Text == string.Empty)
                            {
                                socket.Shutdown(SocketShutdown.Both);
                                socket.Close();
                                continue;
                            }

                            to = Encoding.UTF8.GetBytes(flag + msgThemeTB.Text);
                            break;

                        case (int)DataType.MessageText:
                            to = Encoding.UTF8.GetBytes(flag + msgTextTB.Text);
                            break;

                        case (int)DataType.File:

                            if(files.Count == 0)
                            {
                                socket.Shutdown(SocketShutdown.Both);
                                socket.Close();

                                continue;
                            }

                            socket.Shutdown(SocketShutdown.Both);
                            socket.Close();

                            for (int k = 0; k < files.Count; k++)
                            {
                                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                socket.Connect(endPoint);

                                byte[] byteFlag = Encoding.UTF8.GetBytes(flag);
                                byte[] byteFile = files[k].ToArray();
                                byte[] byteFileLength = BitConverter.GetBytes(byteFile.LongLength);

                                to = byteFlag.Concat(byteFileLength).Concat(byteFile).ToArray();
                                socket.Send(to);

                                bytesRec = socket.Receive(from);

                                socket.Shutdown(SocketShutdown.Both);
                                socket.Close();

                                response = Encoding.UTF8.GetString(from, 0, bytesRec);
                                if (response != "Successfully")
                                    throw new Exception();
                            }

                            continue;

                        case (int)DataType.Send:
                            to = Encoding.UTF8.GetBytes(((int)DataType.Send).ToString());
                            break;

                    }

                    socket.Send(to);

                    bytesRec = socket.Receive(from);

                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();

                    response = Encoding.UTF8.GetString(from, 0, bytesRec);
                    if (response != "Successfully")
                        throw new Exception();
                }

                MessageBox.Show("Письмо успешно отправлено.");
            }
            catch(Exception)
            {
                MessageBox.Show("Ошибка отправки сообщения. Проверьте подключение к Интернету.");
            }
        }

        private void addFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            try
            {
                using (FileStream stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = new byte[stream.Length];
                    int length = stream.Read(data, 0, data.Length);
                    NetFile file = new NetFile();
                    file.FileName = Path.GetFileName(stream.Name);
                    file.Data = data;

                    files.Add(file);
                }

                ToolStripDropDownButton tool = new ToolStripDropDownButton();
                tool.DropDownItems.Add("Открепить");
                tool.DropDownItems[0].Click += DetachFileToolStripMenuItem_Click;
                tool.Text = openFileDialog.SafeFileName;
                filesStrip.Items.Add(tool);
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DetachFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            files.RemoveAt(filesStrip.Items.IndexOf(((ToolStripItem)sender).OwnerItem) - 1);
            filesStrip.Items.Remove(((ToolStripItem)sender).OwnerItem);
        }
    }
}
