﻿namespace WIN
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
            this.skinTabControl1 = new CCWin.SkinControl.SkinTabControl();
            this.skinTabPageFans = new CCWin.SkinControl.SkinTabPage();
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.panelWeibo = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.skinTabPageSerial = new CCWin.SkinControl.SkinTabPage();
            this.panelSeria = new System.Windows.Forms.Panel();
            this.labelSeria = new System.Windows.Forms.Label();
            this.buttonUpdateSerial = new System.Windows.Forms.Button();
            this.buttonBuySerial = new System.Windows.Forms.Button();
            this.skinTabPageHelp = new CCWin.SkinControl.SkinTabPage();
            this.skinTabControl1.SuspendLayout();
            this.skinTabPageFans.SuspendLayout();
            this.panel1.SuspendLayout();
            this.skinTabPageSerial.SuspendLayout();
            this.panelSeria.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinTabControl1
            // 
            this.skinTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinTabControl1.AnimationStart = true;
            this.skinTabControl1.AnimatorType = CCWin.SkinControl.AnimationType.Custom;
            this.skinTabControl1.BackColor = System.Drawing.Color.Silver;
            this.skinTabControl1.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.skinTabControl1.Controls.Add(this.skinTabPageFans);
            this.skinTabControl1.Controls.Add(this.skinTabPageSerial);
            this.skinTabControl1.Controls.Add(this.skinTabPageHelp);
            this.skinTabControl1.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinTabControl1.HeadBack = null;
            this.skinTabControl1.HotTrack = true;
            this.skinTabControl1.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.skinTabControl1.ItemSize = new System.Drawing.Size(100, 36);
            this.skinTabControl1.Location = new System.Drawing.Point(0, 32);
            this.skinTabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.skinTabControl1.Name = "skinTabControl1";
            this.skinTabControl1.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowDown")));
            this.skinTabControl1.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowHover")));
            this.skinTabControl1.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseHover")));
            this.skinTabControl1.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseNormal")));
            this.skinTabControl1.PageDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageDown")));
            this.skinTabControl1.PageHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageHover")));
            this.skinTabControl1.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.skinTabControl1.PageNorml = null;
            this.skinTabControl1.SelectedIndex = 1;
            this.skinTabControl1.Size = new System.Drawing.Size(811, 414);
            this.skinTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.skinTabControl1.TabIndex = 0;
            // 
            // skinTabPageFans
            // 
            this.skinTabPageFans.BackColor = System.Drawing.Color.White;
            this.skinTabPageFans.Controls.Add(this.richTextBoxOutput);
            this.skinTabPageFans.Controls.Add(this.panelWeibo);
            this.skinTabPageFans.Controls.Add(this.panel1);
            this.skinTabPageFans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabPageFans.Location = new System.Drawing.Point(0, 36);
            this.skinTabPageFans.Name = "skinTabPageFans";
            this.skinTabPageFans.Size = new System.Drawing.Size(811, 378);
            this.skinTabPageFans.TabIndex = 0;
            this.skinTabPageFans.TabItemImage = null;
            this.skinTabPageFans.Text = "互粉状态";
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxOutput.Location = new System.Drawing.Point(575, 108);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.Size = new System.Drawing.Size(229, 270);
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
            this.panelWeibo.Location = new System.Drawing.Point(4, 4);
            this.panelWeibo.Name = "panelWeibo";
            this.panelWeibo.Size = new System.Drawing.Size(565, 374);
            this.panelWeibo.TabIndex = 3;
            this.panelWeibo.SizeChanged += new System.EventHandler(this.panelWeibo_SizeChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonLogin);
            this.panel1.Location = new System.Drawing.Point(575, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(229, 98);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "未检测到有效序列号";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(64, 44);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(105, 32);
            this.buttonLogin.TabIndex = 0;
            this.buttonLogin.Text = "登录账号";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // skinTabPageSerial
            // 
            this.skinTabPageSerial.BackColor = System.Drawing.Color.White;
            this.skinTabPageSerial.Controls.Add(this.panelSeria);
            this.skinTabPageSerial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabPageSerial.Location = new System.Drawing.Point(0, 36);
            this.skinTabPageSerial.Name = "skinTabPageSerial";
            this.skinTabPageSerial.Size = new System.Drawing.Size(811, 378);
            this.skinTabPageSerial.TabIndex = 1;
            this.skinTabPageSerial.TabItemImage = null;
            this.skinTabPageSerial.Text = "序列号信息";
            // 
            // panelSeria
            // 
            this.panelSeria.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSeria.BackColor = System.Drawing.Color.Honeydew;
            this.panelSeria.Controls.Add(this.labelSeria);
            this.panelSeria.Controls.Add(this.buttonUpdateSerial);
            this.panelSeria.Controls.Add(this.buttonBuySerial);
            this.panelSeria.Location = new System.Drawing.Point(4, 4);
            this.panelSeria.Name = "panelSeria";
            this.panelSeria.Size = new System.Drawing.Size(800, 374);
            this.panelSeria.TabIndex = 0;
            // 
            // labelSeria
            // 
            this.labelSeria.AutoSize = true;
            this.labelSeria.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSeria.Location = new System.Drawing.Point(122, 66);
            this.labelSeria.Name = "labelSeria";
            this.labelSeria.Size = new System.Drawing.Size(576, 19);
            this.labelSeria.TabIndex = 2;
            this.labelSeria.Text = "未检测到有效序列号，请重新获取新的序列号或更新序列号！";
            // 
            // buttonUpdateSerial
            // 
            this.buttonUpdateSerial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdateSerial.Location = new System.Drawing.Point(344, 221);
            this.buttonUpdateSerial.MaximumSize = new System.Drawing.Size(95, 34);
            this.buttonUpdateSerial.Name = "buttonUpdateSerial";
            this.buttonUpdateSerial.Size = new System.Drawing.Size(95, 34);
            this.buttonUpdateSerial.TabIndex = 1;
            this.buttonUpdateSerial.Text = "更新序列号";
            this.buttonUpdateSerial.UseVisualStyleBackColor = true;
            this.buttonUpdateSerial.Click += new System.EventHandler(this.buttonUpdateSerial_Click);
            // 
            // buttonBuySerial
            // 
            this.buttonBuySerial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBuySerial.Location = new System.Drawing.Point(344, 169);
            this.buttonBuySerial.MaximumSize = new System.Drawing.Size(95, 34);
            this.buttonBuySerial.Name = "buttonBuySerial";
            this.buttonBuySerial.Size = new System.Drawing.Size(95, 34);
            this.buttonBuySerial.TabIndex = 0;
            this.buttonBuySerial.Text = "获取序列号";
            this.buttonBuySerial.UseVisualStyleBackColor = true;
            this.buttonBuySerial.Click += new System.EventHandler(this.buttonBuySerial_Click);
            // 
            // skinTabPageHelp
            // 
            this.skinTabPageHelp.BackColor = System.Drawing.Color.White;
            this.skinTabPageHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabPageHelp.Location = new System.Drawing.Point(0, 36);
            this.skinTabPageHelp.Name = "skinTabPageHelp";
            this.skinTabPageHelp.Size = new System.Drawing.Size(811, 378);
            this.skinTabPageHelp.TabIndex = 2;
            this.skinTabPageHelp.TabItemImage = null;
            this.skinTabPageHelp.Text = "帮助";
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 450);
            this.Controls.Add(this.skinTabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainPage";
            this.Text = "小火箭互粉精灵";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainPage_FormClosing);
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.skinTabControl1.ResumeLayout(false);
            this.skinTabPageFans.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.skinTabPageSerial.ResumeLayout(false);
            this.panelSeria.ResumeLayout(false);
            this.panelSeria.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinTabControl skinTabControl1;
        private CCWin.SkinControl.SkinTabPage skinTabPageFans;
        private CCWin.SkinControl.SkinTabPage skinTabPageSerial;
        private CCWin.SkinControl.SkinTabPage skinTabPageHelp;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelWeibo;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.RichTextBox richTextBoxOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelSeria;
        private System.Windows.Forms.Button buttonUpdateSerial;
        private System.Windows.Forms.Button buttonBuySerial;
        private System.Windows.Forms.Label labelSeria;
    }
}