namespace Corrlinks
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_open_browser = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_status = new System.Windows.Forms.RichTextBox();
            this.btn_scan_contact = new System.Windows.Forms.Button();
            this.btn_accept_contacts = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_open_browser
            // 
            this.btn_open_browser.Location = new System.Drawing.Point(258, 11);
            this.btn_open_browser.Name = "btn_open_browser";
            this.btn_open_browser.Size = new System.Drawing.Size(104, 23);
            this.btn_open_browser.TabIndex = 0;
            this.btn_open_browser.Text = "Open CorrLinks";
            this.btn_open_browser.UseVisualStyleBackColor = true;
            this.btn_open_browser.Click += new System.EventHandler(this.btn_open_browser_Click);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(258, 38);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(104, 23);
            this.btn_start.TabIndex = 1;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // txt_username
            // 
            this.txt_username.Location = new System.Drawing.Point(75, 13);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(177, 20);
            this.txt_username.TabIndex = 4;
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(75, 39);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(177, 20);
            this.txt_password.TabIndex = 5;
            // 
            // txt_status
            // 
            this.txt_status.Location = new System.Drawing.Point(15, 124);
            this.txt_status.Name = "txt_status";
            this.txt_status.ReadOnly = true;
            this.txt_status.Size = new System.Drawing.Size(347, 304);
            this.txt_status.TabIndex = 7;
            this.txt_status.Text = "";
            this.txt_status.TextChanged += new System.EventHandler(this.statusChaged);
            // 
            // btn_scan_contact
            // 
            this.btn_scan_contact.Location = new System.Drawing.Point(15, 95);
            this.btn_scan_contact.Name = "btn_scan_contact";
            this.btn_scan_contact.Size = new System.Drawing.Size(169, 23);
            this.btn_scan_contact.TabIndex = 8;
            this.btn_scan_contact.Text = "Scan Contacts";
            this.btn_scan_contact.UseVisualStyleBackColor = true;
            this.btn_scan_contact.Click += new System.EventHandler(this.btn_scan_contact_Click);
            // 
            // btn_accept_contacts
            // 
            this.btn_accept_contacts.Location = new System.Drawing.Point(190, 95);
            this.btn_accept_contacts.Name = "btn_accept_contacts";
            this.btn_accept_contacts.Size = new System.Drawing.Size(172, 23);
            this.btn_accept_contacts.TabIndex = 9;
            this.btn_accept_contacts.Text = "Accept New Contacts";
            this.btn_accept_contacts.UseVisualStyleBackColor = true;
            this.btn_accept_contacts.Click += new System.EventHandler(this.btn_accept_contacts_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(258, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Stop);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 440);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_accept_contacts);
            this.Controls.Add(this.btn_scan_contact);
            this.Controls.Add(this.txt_status);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_username);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_open_browser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Corrlinks Automation - v1.8";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.onClose);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_open_browser;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.RichTextBox txt_status;
        private System.Windows.Forms.Button btn_scan_contact;
        private System.Windows.Forms.Button btn_accept_contacts;
        private System.Windows.Forms.Button button1;
    }
}

