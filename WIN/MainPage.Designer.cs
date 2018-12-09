namespace WIN
{
    partial class MainPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.panelWeibo = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelSerialType = new System.Windows.Forms.Label();
            this.labelSerialTime = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonUpdateSerial = new System.Windows.Forms.Button();
            this.buttonBuySerial = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxOutput.Location = new System.Drawing.Point(574, 182);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.Size = new System.Drawing.Size(229, 261);
            this.richTextBoxOutput.TabIndex = 4;
            this.richTextBoxOutput.Text = "";
            // 
            // panelWeibo
            // 
            this.panelWeibo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelWeibo.AutoScroll = true;
            this.panelWeibo.BackColor = System.Drawing.Color.Honeydew;
            this.panelWeibo.Location = new System.Drawing.Point(3, 35);
            this.panelWeibo.Name = "panelWeibo";
            this.panelWeibo.Size = new System.Drawing.Size(565, 408);
            this.panelWeibo.TabIndex = 3;
            this.panelWeibo.SizeChanged += new System.EventHandler(this.panelWeibo_SizeChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Honeydew;
            this.panel1.Controls.Add(this.buttonUpdateSerial);
            this.panel1.Controls.Add(this.buttonBuySerial);
            this.panel1.Controls.Add(this.labelSerialType);
            this.panel1.Controls.Add(this.labelSerialTime);
            this.panel1.Controls.Add(this.buttonLogin);
            this.panel1.Location = new System.Drawing.Point(575, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(229, 141);
            this.panel1.TabIndex = 0;
            // 
            // labelSerialType
            // 
            this.labelSerialType.AutoSize = true;
            this.labelSerialType.Location = new System.Drawing.Point(3, 10);
            this.labelSerialType.Name = "labelSerialType";
            this.labelSerialType.Size = new System.Drawing.Size(113, 12);
            this.labelSerialType.TabIndex = 2;
            this.labelSerialType.Text = "未检测到有效序列号";
            // 
            // labelSerialTime
            // 
            this.labelSerialTime.AutoSize = true;
            this.labelSerialTime.Location = new System.Drawing.Point(3, 32);
            this.labelSerialTime.Name = "labelSerialTime";
            this.labelSerialTime.Size = new System.Drawing.Size(113, 12);
            this.labelSerialTime.TabIndex = 1;
            this.labelSerialTime.Text = "未检测到有效序列号";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(64, 56);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(105, 32);
            this.buttonLogin.TabIndex = 0;
            this.buttonLogin.Text = "登录账号";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonUpdateSerial
            // 
            this.buttonUpdateSerial.Location = new System.Drawing.Point(122, 94);
            this.buttonUpdateSerial.MaximumSize = new System.Drawing.Size(95, 34);
            this.buttonUpdateSerial.Name = "buttonUpdateSerial";
            this.buttonUpdateSerial.Size = new System.Drawing.Size(95, 34);
            this.buttonUpdateSerial.TabIndex = 4;
            this.buttonUpdateSerial.Text = "更新序列号";
            this.buttonUpdateSerial.UseVisualStyleBackColor = true;
            this.buttonUpdateSerial.Click += new System.EventHandler(this.buttonUpdateSerial_Click);
            // 
            // buttonBuySerial
            // 
            this.buttonBuySerial.Location = new System.Drawing.Point(21, 94);
            this.buttonBuySerial.MaximumSize = new System.Drawing.Size(95, 34);
            this.buttonBuySerial.Name = "buttonBuySerial";
            this.buttonBuySerial.Size = new System.Drawing.Size(95, 34);
            this.buttonBuySerial.TabIndex = 3;
            this.buttonBuySerial.Text = "获取序列号";
            this.buttonBuySerial.UseVisualStyleBackColor = true;
            this.buttonBuySerial.Click += new System.EventHandler(this.buttonBuySerial_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 450);
            this.Controls.Add(this.richTextBoxOutput);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelWeibo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainPage";
            this.Text = "小火箭互粉精灵";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainPage_FormClosing);
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelWeibo;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.RichTextBox richTextBoxOutput;
        private System.Windows.Forms.Label labelSerialTime;
        private System.Windows.Forms.Label labelSerialType;
        private System.Windows.Forms.Button buttonUpdateSerial;
        private System.Windows.Forms.Button buttonBuySerial;
    }
}