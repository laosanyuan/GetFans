namespace WIN.Controls
{
    partial class UserLoginControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserLoginControl));
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            this.skinGroupBox1 = new CCWin.SkinControl.SkinGroupBox();
            this.buttonClean = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelGrouCount = new System.Windows.Forms.Label();
            this.labelBeginTime = new System.Windows.Forms.Label();
            this.labelSuccessCount = new System.Windows.Forms.Label();
            this.labelNowFansCount = new System.Windows.Forms.Label();
            this.labelLoginFansCount = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonChat = new System.Windows.Forms.Button();
            this.buttonComment = new System.Windows.Forms.Button();
            this.labelComment = new System.Windows.Forms.Label();
            this.labelChat = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            this.skinGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(102, 24);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(89, 12);
            label1.TabIndex = 1;
            label1.Text = "登录时粉丝数：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(102, 46);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(77, 12);
            label2.TabIndex = 2;
            label2.Text = "当前粉丝数：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(102, 68);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(77, 12);
            label3.TabIndex = 3;
            label3.Text = "互粉成功数：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(102, 90);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(89, 12);
            label4.TabIndex = 4;
            label4.Text = "开始互粉时间：";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(102, 112);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(77, 12);
            label5.TabIndex = 5;
            label5.Text = "当前群聊数：";
            // 
            // skinGroupBox1
            // 
            this.skinGroupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.skinGroupBox1.BorderColor = System.Drawing.Color.DarkGray;
            this.skinGroupBox1.Controls.Add(this.labelChat);
            this.skinGroupBox1.Controls.Add(label7);
            this.skinGroupBox1.Controls.Add(this.labelComment);
            this.skinGroupBox1.Controls.Add(label6);
            this.skinGroupBox1.Controls.Add(this.buttonComment);
            this.skinGroupBox1.Controls.Add(this.buttonChat);
            this.skinGroupBox1.Controls.Add(this.buttonClean);
            this.skinGroupBox1.Controls.Add(this.buttonExit);
            this.skinGroupBox1.Controls.Add(this.buttonStart);
            this.skinGroupBox1.Controls.Add(this.labelGrouCount);
            this.skinGroupBox1.Controls.Add(this.labelBeginTime);
            this.skinGroupBox1.Controls.Add(this.labelSuccessCount);
            this.skinGroupBox1.Controls.Add(this.labelNowFansCount);
            this.skinGroupBox1.Controls.Add(this.labelLoginFansCount);
            this.skinGroupBox1.Controls.Add(label5);
            this.skinGroupBox1.Controls.Add(label4);
            this.skinGroupBox1.Controls.Add(label3);
            this.skinGroupBox1.Controls.Add(label2);
            this.skinGroupBox1.Controls.Add(label1);
            this.skinGroupBox1.Controls.Add(this.pictureBox1);
            this.skinGroupBox1.ForeColor = System.Drawing.Color.Black;
            this.skinGroupBox1.Location = new System.Drawing.Point(4, 4);
            this.skinGroupBox1.Name = "skinGroupBox1";
            this.skinGroupBox1.RectBackColor = System.Drawing.SystemColors.Control;
            this.skinGroupBox1.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinGroupBox1.Size = new System.Drawing.Size(390, 203);
            this.skinGroupBox1.TabIndex = 0;
            this.skinGroupBox1.TabStop = false;
            this.skinGroupBox1.Text = "登录窗口";
            this.skinGroupBox1.TitleBorderColor = System.Drawing.Color.Transparent;
            this.skinGroupBox1.TitleRadius = 10;
            this.skinGroupBox1.TitleRectBackColor = System.Drawing.SystemColors.Control;
            this.skinGroupBox1.TitleRoundStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // buttonClean
            // 
            this.buttonClean.Location = new System.Drawing.Point(289, 129);
            this.buttonClean.Name = "buttonClean";
            this.buttonClean.Size = new System.Drawing.Size(80, 30);
            this.buttonClean.TabIndex = 13;
            this.buttonClean.Text = "一键清粉";
            this.buttonClean.UseVisualStyleBackColor = true;
            this.buttonClean.Click += new System.EventHandler(this.buttonClean_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(289, 167);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(80, 30);
            this.buttonExit.TabIndex = 12;
            this.buttonExit.Text = "退出登录";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(289, 20);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(80, 30);
            this.buttonStart.TabIndex = 11;
            this.buttonStart.Text = "开始互粉";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // labelGrouCount
            // 
            this.labelGrouCount.AutoSize = true;
            this.labelGrouCount.Location = new System.Drawing.Point(217, 112);
            this.labelGrouCount.Name = "labelGrouCount";
            this.labelGrouCount.Size = new System.Drawing.Size(11, 12);
            this.labelGrouCount.TabIndex = 10;
            this.labelGrouCount.Text = "0";
            // 
            // labelBeginTime
            // 
            this.labelBeginTime.AutoSize = true;
            this.labelBeginTime.Location = new System.Drawing.Point(217, 90);
            this.labelBeginTime.Name = "labelBeginTime";
            this.labelBeginTime.Size = new System.Drawing.Size(0, 12);
            this.labelBeginTime.TabIndex = 9;
            // 
            // labelSuccessCount
            // 
            this.labelSuccessCount.AutoSize = true;
            this.labelSuccessCount.Location = new System.Drawing.Point(217, 68);
            this.labelSuccessCount.Name = "labelSuccessCount";
            this.labelSuccessCount.Size = new System.Drawing.Size(11, 12);
            this.labelSuccessCount.TabIndex = 8;
            this.labelSuccessCount.Text = "0";
            // 
            // labelNowFansCount
            // 
            this.labelNowFansCount.AutoSize = true;
            this.labelNowFansCount.Location = new System.Drawing.Point(217, 46);
            this.labelNowFansCount.Name = "labelNowFansCount";
            this.labelNowFansCount.Size = new System.Drawing.Size(11, 12);
            this.labelNowFansCount.TabIndex = 7;
            this.labelNowFansCount.Text = "0";
            // 
            // labelLoginFansCount
            // 
            this.labelLoginFansCount.AutoSize = true;
            this.labelLoginFansCount.Location = new System.Drawing.Point(217, 24);
            this.labelLoginFansCount.Name = "labelLoginFansCount";
            this.labelLoginFansCount.Size = new System.Drawing.Size(11, 12);
            this.labelLoginFansCount.TabIndex = 6;
            this.labelLoginFansCount.Text = "0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(16, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // buttonChat
            // 
            this.buttonChat.Location = new System.Drawing.Point(289, 56);
            this.buttonChat.Name = "buttonChat";
            this.buttonChat.Size = new System.Drawing.Size(80, 30);
            this.buttonChat.TabIndex = 14;
            this.buttonChat.Text = "开始群推";
            this.buttonChat.UseVisualStyleBackColor = true;
            this.buttonChat.Click += new System.EventHandler(this.ButtonChat_Click);
            // 
            // buttonComment
            // 
            this.buttonComment.Location = new System.Drawing.Point(289, 93);
            this.buttonComment.Name = "buttonComment";
            this.buttonComment.Size = new System.Drawing.Size(80, 30);
            this.buttonComment.TabIndex = 15;
            this.buttonComment.Text = "开始互动";
            this.buttonComment.UseVisualStyleBackColor = true;
            this.buttonComment.Click += new System.EventHandler(this.ButtonComment_Click);
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(102, 134);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(77, 12);
            label6.TabIndex = 16;
            label6.Text = "互动成功数：";
            // 
            // labelComment
            // 
            this.labelComment.AutoSize = true;
            this.labelComment.Location = new System.Drawing.Point(217, 134);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(11, 12);
            this.labelComment.TabIndex = 17;
            this.labelComment.Text = "0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(102, 156);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(65, 12);
            label7.TabIndex = 18;
            label7.Text = "群推次数：";
            // 
            // labelChat
            // 
            this.labelChat.AutoSize = true;
            this.labelChat.Location = new System.Drawing.Point(217, 156);
            this.labelChat.Name = "labelChat";
            this.labelChat.Size = new System.Drawing.Size(11, 12);
            this.labelChat.TabIndex = 19;
            this.labelChat.Text = "0";
            // 
            // UserLoginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.skinGroupBox1);
            this.MinimumSize = new System.Drawing.Size(400, 147);
            this.Name = "UserLoginControl";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.Size = new System.Drawing.Size(400, 213);
            this.Load += new System.EventHandler(this.UserLoginControl_Load);
            this.skinGroupBox1.ResumeLayout(false);
            this.skinGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinGroupBox skinGroupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelGrouCount;
        private System.Windows.Forms.Label labelBeginTime;
        private System.Windows.Forms.Label labelSuccessCount;
        private System.Windows.Forms.Label labelNowFansCount;
        private System.Windows.Forms.Label labelLoginFansCount;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonClean;
        private System.Windows.Forms.Button buttonComment;
        private System.Windows.Forms.Button buttonChat;
        private System.Windows.Forms.Label labelChat;
        private System.Windows.Forms.Label labelComment;
    }
}
