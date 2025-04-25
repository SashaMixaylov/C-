namespace WinFormsApp1_Client_Num3
{
    partial class ContactInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtEmail = new TextBox();
            txtUserName = new TextBox();
            lblEmail = new Label();
            lblUserName = new Label();
            okButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(115, 20);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(100, 23);
            txtEmail.TabIndex = 0;
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(112, 68);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(100, 23);
            txtUserName.TabIndex = 1;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(33, 20);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(36, 15);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email";
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Location = new Point(34, 69);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(62, 15);
            lblUserName.TabIndex = 3;
            lblUserName.Text = "UserName";
            // 
            // okButton
            // 
            okButton.Location = new Point(270, 20);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 4;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += button1_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(270, 68);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += button2_Click;
            // 
            // ContactInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Yellow;
            ClientSize = new Size(394, 189);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(lblUserName);
            Controls.Add(lblEmail);
            Controls.Add(txtUserName);
            Controls.Add(txtEmail);
            Name = "ContactInfo";
            Text = "ContactInfo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public TextBox txtEmail;
        public TextBox txtUserName;
        public Label lblEmail;
        public Label lblUserName;
        public Button okButton;
        public Button cancelButton;
    }
}