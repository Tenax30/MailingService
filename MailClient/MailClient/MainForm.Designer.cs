namespace MailClient
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.msgThemeTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.recepientsLabel = new System.Windows.Forms.Label();
            this.recepientsTB = new System.Windows.Forms.TextBox();
            this.addRecepientBtn = new System.Windows.Forms.Button();
            this.msgTextTB = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.senderTextBox = new System.Windows.Forms.TextBox();
            this.changeUserBtn = new System.Windows.Forms.Button();
            this.sendBtn = new System.Windows.Forms.Button();
            this.addFileButton = new System.Windows.Forms.Button();
            this.filesStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.filesStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // msgThemeTB
            // 
            this.msgThemeTB.Location = new System.Drawing.Point(12, 110);
            this.msgThemeTB.Name = "msgThemeTB";
            this.msgThemeTB.Size = new System.Drawing.Size(682, 20);
            this.msgThemeTB.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Тема сообщения:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Текст сообщения:";
            // 
            // recepientsLabel
            // 
            this.recepientsLabel.AutoSize = true;
            this.recepientsLabel.Location = new System.Drawing.Point(12, 52);
            this.recepientsLabel.Name = "recepientsLabel";
            this.recepientsLabel.Size = new System.Drawing.Size(69, 13);
            this.recepientsLabel.TabIndex = 5;
            this.recepientsLabel.Text = "Получатели:";
            // 
            // recepientsTB
            // 
            this.recepientsTB.Location = new System.Drawing.Point(12, 68);
            this.recepientsTB.Name = "recepientsTB";
            this.recepientsTB.ReadOnly = true;
            this.recepientsTB.Size = new System.Drawing.Size(540, 20);
            this.recepientsTB.TabIndex = 4;
            // 
            // addRecepientBtn
            // 
            this.addRecepientBtn.Location = new System.Drawing.Point(558, 66);
            this.addRecepientBtn.Name = "addRecepientBtn";
            this.addRecepientBtn.Size = new System.Drawing.Size(136, 23);
            this.addRecepientBtn.TabIndex = 6;
            this.addRecepientBtn.Text = "Добавить получателей";
            this.addRecepientBtn.UseVisualStyleBackColor = true;
            this.addRecepientBtn.Click += new System.EventHandler(this.addUserBtn_Click);
            // 
            // msgTextTB
            // 
            this.msgTextTB.Location = new System.Drawing.Point(12, 161);
            this.msgTextTB.Name = "msgTextTB";
            this.msgTextTB.Size = new System.Drawing.Size(682, 176);
            this.msgTextTB.TabIndex = 0;
            this.msgTextTB.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Отправитель:";
            // 
            // senderTextBox
            // 
            this.senderTextBox.Location = new System.Drawing.Point(12, 29);
            this.senderTextBox.Name = "senderTextBox";
            this.senderTextBox.ReadOnly = true;
            this.senderTextBox.Size = new System.Drawing.Size(540, 20);
            this.senderTextBox.TabIndex = 7;
            // 
            // changeUserBtn
            // 
            this.changeUserBtn.Location = new System.Drawing.Point(558, 27);
            this.changeUserBtn.Name = "changeUserBtn";
            this.changeUserBtn.Size = new System.Drawing.Size(136, 23);
            this.changeUserBtn.TabIndex = 9;
            this.changeUserBtn.Text = "Сменить пользователя";
            this.changeUserBtn.UseVisualStyleBackColor = true;
            this.changeUserBtn.Click += new System.EventHandler(this.changeUserBtn_Click);
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(582, 343);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(112, 37);
            this.sendBtn.TabIndex = 10;
            this.sendBtn.Text = "Отправить";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // addFileButton
            // 
            this.addFileButton.Location = new System.Drawing.Point(12, 343);
            this.addFileButton.Name = "addFileButton";
            this.addFileButton.Size = new System.Drawing.Size(131, 37);
            this.addFileButton.TabIndex = 11;
            this.addFileButton.Text = "Прикрепить файл";
            this.addFileButton.UseVisualStyleBackColor = true;
            this.addFileButton.Click += new System.EventHandler(this.addFileButton_Click);
            // 
            // filesStrip
            // 
            this.filesStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.filesStrip.Location = new System.Drawing.Point(0, 388);
            this.filesStrip.Name = "filesStrip";
            this.filesStrip.Size = new System.Drawing.Size(706, 22);
            this.filesStrip.TabIndex = 12;
            this.filesStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(142, 17);
            this.toolStripStatusLabel1.Text = "Прикрепленные файлы:";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 410);
            this.Controls.Add(this.filesStrip);
            this.Controls.Add(this.addFileButton);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.changeUserBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.senderTextBox);
            this.Controls.Add(this.addRecepientBtn);
            this.Controls.Add(this.recepientsLabel);
            this.Controls.Add(this.recepientsTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.msgThemeTB);
            this.Controls.Add(this.msgTextTB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MailClient";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.filesStrip.ResumeLayout(false);
            this.filesStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox msgThemeTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label recepientsLabel;
        private System.Windows.Forms.TextBox recepientsTB;
        private System.Windows.Forms.Button addRecepientBtn;
        private System.Windows.Forms.RichTextBox msgTextTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button changeUserBtn;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Button addFileButton;
        private System.Windows.Forms.StatusStrip filesStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.TextBox senderTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

