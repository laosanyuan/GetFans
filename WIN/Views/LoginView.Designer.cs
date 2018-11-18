namespace WIN.Views
{
    partial class LoginView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginView));
            this.skinTextBoxUserName = new CCWin.SkinControl.SkinTextBox();
            this.skinTextBoxPassword = new CCWin.SkinControl.SkinTextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.pictureBoxCode = new System.Windows.Forms.PictureBox();
            this.skinTextBoxCheck = new CCWin.SkinControl.SkinTextBox();
            this.pictureBoxErrorUserName = new System.Windows.Forms.PictureBox();
            this.pictureBoxErrorPassword = new System.Windows.Forms.PictureBox();
            this.pictureBoxErrorCheck = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErrorUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErrorPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErrorCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // skinTextBoxUserName
            // 
            this.skinTextBoxUserName.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBoxUserName.DownBack = null;
            this.skinTextBoxUserName.Icon = null;
            this.skinTextBoxUserName.IconIsButton = false;
            this.skinTextBoxUserName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBoxUserName.IsPasswordChat = '\0';
            this.skinTextBoxUserName.IsSystemPasswordChar = false;
            this.skinTextBoxUserName.Lines = new string[0];
            this.skinTextBoxUserName.Location = new System.Drawing.Point(76, 62);
            this.skinTextBoxUserName.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBoxUserName.MaxLength = 32767;
            this.skinTextBoxUserName.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBoxUserName.MouseBack = null;
            this.skinTextBoxUserName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBoxUserName.Multiline = false;
            this.skinTextBoxUserName.Name = "skinTextBoxUserName";
            this.skinTextBoxUserName.NormlBack = null;
            this.skinTextBoxUserName.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBoxUserName.ReadOnly = false;
            this.skinTextBoxUserName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.skinTextBoxUserName.Size = new System.Drawing.Size(185, 28);
            // 
            // 
            // 
            this.skinTextBoxUserName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBoxUserName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBoxUserName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBoxUserName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBoxUserName.SkinTxt.Name = "BaseText";
            this.skinTextBoxUserName.SkinTxt.Size = new System.Drawing.Size(175, 18);
            this.skinTextBoxUserName.SkinTxt.TabIndex = 0;
            this.skinTextBoxUserName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBoxUserName.SkinTxt.WaterText = "用户名";
            this.skinTextBoxUserName.TabIndex = 1;
            this.skinTextBoxUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.skinTextBoxUserName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBoxUserName.WaterText = "用户名";
            this.skinTextBoxUserName.WordWrap = true;
            // 
            // skinTextBoxPassword
            // 
            this.skinTextBoxPassword.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBoxPassword.DownBack = null;
            this.skinTextBoxPassword.Icon = null;
            this.skinTextBoxPassword.IconIsButton = false;
            this.skinTextBoxPassword.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBoxPassword.IsPasswordChat = '*';
            this.skinTextBoxPassword.IsSystemPasswordChar = false;
            this.skinTextBoxPassword.Lines = new string[0];
            this.skinTextBoxPassword.Location = new System.Drawing.Point(76, 110);
            this.skinTextBoxPassword.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBoxPassword.MaxLength = 32767;
            this.skinTextBoxPassword.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBoxPassword.MouseBack = null;
            this.skinTextBoxPassword.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBoxPassword.Multiline = false;
            this.skinTextBoxPassword.Name = "skinTextBoxPassword";
            this.skinTextBoxPassword.NormlBack = null;
            this.skinTextBoxPassword.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBoxPassword.ReadOnly = false;
            this.skinTextBoxPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.skinTextBoxPassword.Size = new System.Drawing.Size(185, 28);
            // 
            // 
            // 
            this.skinTextBoxPassword.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBoxPassword.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBoxPassword.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBoxPassword.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBoxPassword.SkinTxt.Name = "BaseText";
            this.skinTextBoxPassword.SkinTxt.PasswordChar = '*';
            this.skinTextBoxPassword.SkinTxt.Size = new System.Drawing.Size(175, 18);
            this.skinTextBoxPassword.SkinTxt.TabIndex = 0;
            this.skinTextBoxPassword.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBoxPassword.SkinTxt.WaterText = "密码";
            this.skinTextBoxPassword.TabIndex = 2;
            this.skinTextBoxPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.skinTextBoxPassword.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBoxPassword.WaterText = "密码";
            this.skinTextBoxPassword.WordWrap = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.Location = new System.Drawing.Point(49, 187);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 30);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLogin.Location = new System.Drawing.Point(213, 187);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 30);
            this.buttonLogin.TabIndex = 3;
            this.buttonLogin.Text = "确定";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // pictureBoxCode
            // 
            this.pictureBoxCode.Location = new System.Drawing.Point(76, 145);
            this.pictureBoxCode.Name = "pictureBoxCode";
            this.pictureBoxCode.Size = new System.Drawing.Size(74, 30);
            this.pictureBoxCode.TabIndex = 4;
            this.pictureBoxCode.TabStop = false;
            this.pictureBoxCode.Click += new System.EventHandler(this.pictureBoxCode_Click);
            // 
            // skinTextBoxCheck
            // 
            this.skinTextBoxCheck.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBoxCheck.DownBack = null;
            this.skinTextBoxCheck.Icon = null;
            this.skinTextBoxCheck.IconIsButton = false;
            this.skinTextBoxCheck.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBoxCheck.IsPasswordChat = '\0';
            this.skinTextBoxCheck.IsSystemPasswordChar = false;
            this.skinTextBoxCheck.Lines = new string[0];
            this.skinTextBoxCheck.Location = new System.Drawing.Point(172, 145);
            this.skinTextBoxCheck.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBoxCheck.MaxLength = 32767;
            this.skinTextBoxCheck.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBoxCheck.MouseBack = null;
            this.skinTextBoxCheck.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBoxCheck.Multiline = true;
            this.skinTextBoxCheck.Name = "skinTextBoxCheck";
            this.skinTextBoxCheck.NormlBack = null;
            this.skinTextBoxCheck.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBoxCheck.ReadOnly = false;
            this.skinTextBoxCheck.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.skinTextBoxCheck.Size = new System.Drawing.Size(89, 30);
            // 
            // 
            // 
            this.skinTextBoxCheck.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBoxCheck.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBoxCheck.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBoxCheck.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBoxCheck.SkinTxt.Multiline = true;
            this.skinTextBoxCheck.SkinTxt.Name = "BaseText";
            this.skinTextBoxCheck.SkinTxt.Size = new System.Drawing.Size(79, 20);
            this.skinTextBoxCheck.SkinTxt.TabIndex = 0;
            this.skinTextBoxCheck.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBoxCheck.SkinTxt.WaterText = "验证码";
            this.skinTextBoxCheck.TabIndex = 5;
            this.skinTextBoxCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.skinTextBoxCheck.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBoxCheck.WaterText = "验证码";
            this.skinTextBoxCheck.WordWrap = true;
            // 
            // pictureBoxErrorUserName
            // 
            this.pictureBoxErrorUserName.Image = global::WIN.Properties.Resources.erreur;
            this.pictureBoxErrorUserName.InitialImage = null;
            this.pictureBoxErrorUserName.Location = new System.Drawing.Point(271, 65);
            this.pictureBoxErrorUserName.Name = "pictureBoxErrorUserName";
            this.pictureBoxErrorUserName.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxErrorUserName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxErrorUserName.TabIndex = 6;
            this.pictureBoxErrorUserName.TabStop = false;
            this.pictureBoxErrorUserName.Visible = false;
            // 
            // pictureBoxErrorPassword
            // 
            this.pictureBoxErrorPassword.Image = global::WIN.Properties.Resources.erreur;
            this.pictureBoxErrorPassword.Location = new System.Drawing.Point(271, 114);
            this.pictureBoxErrorPassword.Name = "pictureBoxErrorPassword";
            this.pictureBoxErrorPassword.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxErrorPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxErrorPassword.TabIndex = 7;
            this.pictureBoxErrorPassword.TabStop = false;
            this.pictureBoxErrorPassword.Visible = false;
            // 
            // pictureBoxErrorCheck
            // 
            this.pictureBoxErrorCheck.Image = global::WIN.Properties.Resources.erreur;
            this.pictureBoxErrorCheck.Location = new System.Drawing.Point(271, 150);
            this.pictureBoxErrorCheck.Name = "pictureBoxErrorCheck";
            this.pictureBoxErrorCheck.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxErrorCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxErrorCheck.TabIndex = 8;
            this.pictureBoxErrorCheck.TabStop = false;
            this.pictureBoxErrorCheck.Visible = false;
            // 
            // LoginView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 235);
            this.Controls.Add(this.pictureBoxErrorCheck);
            this.Controls.Add(this.pictureBoxErrorPassword);
            this.Controls.Add(this.pictureBoxErrorUserName);
            this.Controls.Add(this.skinTextBoxCheck);
            this.Controls.Add(this.pictureBoxCode);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.skinTextBoxPassword);
            this.Controls.Add(this.skinTextBoxUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginView";
            this.Text = "登录";
            this.Load += new System.EventHandler(this.LoginView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErrorUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErrorPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxErrorCheck)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinTextBox skinTextBoxUserName;
        private CCWin.SkinControl.SkinTextBox skinTextBoxPassword;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.PictureBox pictureBoxCode;
        private CCWin.SkinControl.SkinTextBox skinTextBoxCheck;
        private System.Windows.Forms.PictureBox pictureBoxErrorUserName;
        private System.Windows.Forms.PictureBox pictureBoxErrorPassword;
        private System.Windows.Forms.PictureBox pictureBoxErrorCheck;
    }
}