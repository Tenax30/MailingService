using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using MailServerClassLibrary;

namespace MailServer
{
    class Program
    {
        enum DataType { SenderData, RecepientsEmails, MessageTheme, MessageText, File, Send };

        struct Sender
        {
            public string email;
            public string password;
        }

        static void Main()
        {

            List<string> recepients = new List<string>();
            List<NetFile> files = new List<NetFile>();
            Sender sender = new Sender();
            string messageTheme = string.Empty;
            string messageText = string.Empty;

            const string IP_ADRESS = "127.0.0.1";
            const int PORT = 3080;

            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(IP_ADRESS), PORT);

                Socket listenSoc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                listenSoc.Bind(endPoint);
                listenSoc.Listen(10);

                Console.WriteLine("Сервер запущен...\n");

                //int bytes = 0;
                const int bufferSize = 8192;

                const string SUCCESSFULY_RESPONSE = "Successfully";
                const string ERROR_RESPONSE = "Error";

                while(true)
                {
                    Console.WriteLine("Ожидание соеденения...");

                    Socket handler = listenSoc.Accept();

                    Console.WriteLine("Соеденение установлено.");

                    byte[] buffer = new byte[bufferSize];

                    int received = handler.Receive(buffer);

                    //Игнорируем, если данные не переданы
                    if (received == 0)
                        continue;

                    byte[] response;

                    //Получаем тип принимаемых данных и вырезаем флаг данных из массива
                    int dataType = Convert.ToInt32(Encoding.UTF8.GetString(buffer.ToList().GetRange(0, 1).ToArray()));
                    
                    Array.Copy(buffer, 1, buffer, 0, buffer.Length - 1);
                    Array.Resize(ref buffer, buffer.Length - 1);
                    received = buffer.Length;

                    try
                    {
                        switch (dataType)
                        {
                            case (int)DataType.SenderData:
                                sender = GetSenderData(Encoding.UTF8.GetString(buffer).Trim('\0'));

                                Console.WriteLine("Отправитель: \n" + sender.email + "\n");

                                break;

                            case (int)DataType.RecepientsEmails:
                                recepients = GetRecepientsEmails(Encoding.UTF8.GetString(buffer).Trim('\0'));

                                Console.WriteLine("Получатели: ");

                                for (int i = 0; i < recepients.Count; i++)
                                    Console.WriteLine(recepients[i]);

                                Console.WriteLine();

                                break;

                            case (int)DataType.MessageTheme:
                                messageTheme = GetMessageTheme(Encoding.UTF8.GetString(buffer).Trim('\0'));

                                Console.WriteLine("Тема сообщения: " + messageTheme + '\n');

                                break;

                            case (int)DataType.MessageText:
                                messageText = GetMessageText(Encoding.UTF8.GetString(buffer).Trim('\0'));

                                const int ShortenedMessageLength = 50;

                                if (messageText.Length > ShortenedMessageLength)
                                    Console.WriteLine("Сообщение:\n" + messageText.Substring(0, ShortenedMessageLength) + "..." + '\n');
                                else
                                    Console.WriteLine("Сообщение: \n" + messageText + '\n');

                                break;

                            case (int)DataType.File:
                                NetFile file = GetFile(buffer, handler, received);

                                if(file == null)
                                    throw new Exception();

                                files.Add(file);

                                Console.WriteLine("Файл: " + files.Last().FileName + '\n');

                                break;

                            case (int)DataType.Send:
                                if (SendMessage(sender, recepients, messageTheme, messageText, files))
                                {
                                    Console.WriteLine("Сообщения успешно отправлены.\n");

                                    RemoveFiles(files);
                                }
                                    
                                else
                                    throw new Exception();
                                break;

                        }

                        response = Encoding.UTF8.GetBytes(SUCCESSFULY_RESPONSE);
                    }

                    catch
                    {
                        response = Encoding.UTF8.GetBytes(ERROR_RESPONSE);

                        RemoveFiles(files);
                    }

                    handler.Send(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static Sender GetSenderData(string data)
        {
            Sender sender;

            sender.email = data.Substring(0, data.IndexOf(" "));
            sender.password = data.Substring(data.IndexOf(" ") + 1);

            return sender;
        }

        static List<string> GetRecepientsEmails(string data)
        {
            List<string> recepients = new List<string>();

            recepients.AddRange(data.Split(' '));

            return recepients;
        }

        static string GetMessageTheme(string data)
        {
            return data;
        }

        static string GetMessageText(string data)
        {
            return data;
        }

        static NetFile GetFile(byte[] buffer, Socket handler, int received)
        {
            NetFile file;

            long remainingFileBytes = BitConverter.ToInt64(buffer.ToList().GetRange(0, sizeof(long)).ToArray(), 0);

            Array.Copy(buffer, sizeof(long), buffer, 0, buffer.Length - sizeof(long));
            Array.Resize(ref buffer, buffer.Length - sizeof(long));
            received = buffer.Length;

            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    memStream.Write(buffer, 0, received);
                    remainingFileBytes -= received;

                    while(remainingFileBytes > 0)
                    {
                        received = handler.Receive(buffer);
                        memStream.Write(buffer, 0, received);
                        remainingFileBytes -= received;
                    }
                    
                    file = new NetFile(memStream.ToArray());
                }

                using (FileStream stream = new FileStream(file.FileName, FileMode.Create, FileAccess.Write))
                {
                    stream.Write(file.Data, 0, file.Data.Length);
                }

                return file;
            }
            catch(IOException)
            {
                return null;
            }
        }

        static bool SendMessage(Sender sender, List<string> recepients, string messageTheme, string messageText, List<NetFile> files)
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                smtp.Credentials = new NetworkCredential(sender.email, sender.password);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(sender.email);

                for(int i = 0; i < recepients.Count; i++)
                    mail.To.Add(new MailAddress(recepients[i]));

                if(messageTheme != string.Empty)
                    mail.Subject = messageTheme;

                mail.Body = messageText;

                for(int i = 0; i < files.Count; i++)
                {
                    string file = files[i].FileName;
                    Attachment attach = new Attachment(file, MediaTypeNames.Application.Octet);
                    mail.Attachments.Add(attach);
                }

                smtp.EnableSsl = true;

                smtp.Send(mail);

                mail.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
        }

        static void RemoveFiles(List<NetFile> files)
        {
            foreach(NetFile f in files)
            {
                try
                {
                    File.Delete(f.FileName);
                }
                catch(IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            files.Clear();
        }
    }
}
