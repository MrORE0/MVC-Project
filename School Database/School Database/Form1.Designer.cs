
namespace School_Database
{
    partial class Log_In
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Log_In));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.customTxtBox_Password = new School_Database.CustomTxtBox();
            this.customTxtBox_UserName = new School_Database.CustomTxtBox();
            this.btn_Cancel = new School_Database.Design();
            this.btn_LogIn = new School_Database.Design();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username:";
            this.toolTip1.SetToolTip(this.label1, "The User Id should look something like this: \r\n0987568226");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password:";
            this.toolTip1.SetToolTip(this.label2, "The password you need in order to log in, is the password \r\nthat the administrato" +
        "r gave you.");
            // 
            // customTxtBox_Password
            // 
            this.customTxtBox_Password.BackColor = System.Drawing.SystemColors.ControlLight;
            this.customTxtBox_Password.BorderColor = System.Drawing.Color.Gray;
            this.customTxtBox_Password.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.customTxtBox_Password.BorderSize = 4;
            this.customTxtBox_Password.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTxtBox_Password.ForeColor = System.Drawing.Color.Gray;
            this.customTxtBox_Password.Location = new System.Drawing.Point(146, 89);
            this.customTxtBox_Password.Margin = new System.Windows.Forms.Padding(4);
            this.customTxtBox_Password.Multiline = false;
            this.customTxtBox_Password.Name = "customTxtBox_Password";
            this.customTxtBox_Password.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.customTxtBox_Password.PasswordChar = true;
            this.customTxtBox_Password.Size = new System.Drawing.Size(272, 31);
            this.customTxtBox_Password.TabIndex = 9;
            this.customTxtBox_Password.Texts = "";
            this.customTxtBox_Password.UnderlinedStyle = true;
            // 
            // customTxtBox_UserName
            // 
            this.customTxtBox_UserName.BackColor = System.Drawing.SystemColors.ControlLight;
            this.customTxtBox_UserName.BorderColor = System.Drawing.Color.Gray;
            this.customTxtBox_UserName.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.customTxtBox_UserName.BorderSize = 4;
            this.customTxtBox_UserName.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTxtBox_UserName.ForeColor = System.Drawing.Color.Gray;
            this.customTxtBox_UserName.Location = new System.Drawing.Point(146, 26);
            this.customTxtBox_UserName.Margin = new System.Windows.Forms.Padding(4);
            this.customTxtBox_UserName.Multiline = false;
            this.customTxtBox_UserName.Name = "customTxtBox_UserName";
            this.customTxtBox_UserName.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.customTxtBox_UserName.PasswordChar = false;
            this.customTxtBox_UserName.Size = new System.Drawing.Size(272, 31);
            this.customTxtBox_UserName.TabIndex = 8;
            this.customTxtBox_UserName.Texts = "";
            this.customTxtBox_UserName.UnderlinedStyle = true;
            this.customTxtBox_UserName._TextChanged += new System.EventHandler(this.customTxtBox_UserID__TextChanged);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.LightSlateGray;
            this.btn_Cancel.BackgroundColor = System.Drawing.Color.LightSlateGray;
            this.btn_Cancel.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_Cancel.BorderRadius = 22;
            this.btn_Cancel.BorderSize = 0;
            this.btn_Cancel.FlatAppearance.BorderSize = 0;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.ForeColor = System.Drawing.Color.White;
            this.btn_Cancel.Location = new System.Drawing.Point(312, 160);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(106, 40);
            this.btn_Cancel.TabIndex = 7;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.TextColor = System.Drawing.Color.White;
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_LogIn
            // 
            this.btn_LogIn.BackColor = System.Drawing.Color.LightSlateGray;
            this.btn_LogIn.BackgroundColor = System.Drawing.Color.LightSlateGray;
            this.btn_LogIn.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_LogIn.BorderRadius = 22;
            this.btn_LogIn.BorderSize = 0;
            this.btn_LogIn.FlatAppearance.BorderSize = 0;
            this.btn_LogIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LogIn.ForeColor = System.Drawing.Color.White;
            this.btn_LogIn.Location = new System.Drawing.Point(146, 160);
            this.btn_LogIn.Name = "btn_LogIn";
            this.btn_LogIn.Size = new System.Drawing.Size(106, 40);
            this.btn_LogIn.TabIndex = 6;
            this.btn_LogIn.Text = "Log In";
            this.btn_LogIn.TextColor = System.Drawing.Color.White;
            this.btn_LogIn.UseVisualStyleBackColor = false;
            this.btn_LogIn.Click += new System.EventHandler(this.btn_LogIn_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(6, 209);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(122, 18);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Смяна на парола";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Log_In
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(471, 236);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.customTxtBox_Password);
            this.Controls.Add(this.customTxtBox_UserName);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_LogIn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.Name = "Log_In";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LOGIN";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Log_In_FormClosing);
            this.Load += new System.EventHandler(this.Log_In_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private Design btn_LogIn;
        private Design btn_Cancel;
        private CustomTxtBox customTxtBox_UserName;
        private CustomTxtBox customTxtBox_Password;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

