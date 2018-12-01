namespace SocketOnline.Views
{
    partial class MainPageView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNickName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEMail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 343);
            this.panel1.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(448, 13);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(231, 343);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnNumber,
            this.ColumnUserName,
            this.ColumnNickName,
            this.ColumnStartTime,
            this.ColumnEndTime,
            this.ColumnEMail});
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(428, 343);
            this.dataGridView1.TabIndex = 0;
            // 
            // ColumnNumber
            // 
            this.ColumnNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnNumber.Frozen = true;
            this.ColumnNumber.HeaderText = "序号";
            this.ColumnNumber.MinimumWidth = 55;
            this.ColumnNumber.Name = "ColumnNumber";
            this.ColumnNumber.ReadOnly = true;
            this.ColumnNumber.Width = 55;
            // 
            // ColumnUserName
            // 
            this.ColumnUserName.HeaderText = "账号";
            this.ColumnUserName.MinimumWidth = 120;
            this.ColumnUserName.Name = "ColumnUserName";
            this.ColumnUserName.ReadOnly = true;
            // 
            // ColumnNickName
            // 
            this.ColumnNickName.HeaderText = "昵称";
            this.ColumnNickName.MinimumWidth = 120;
            this.ColumnNickName.Name = "ColumnNickName";
            this.ColumnNickName.ReadOnly = true;
            // 
            // ColumnStartTime
            // 
            this.ColumnStartTime.HeaderText = "开始时间";
            this.ColumnStartTime.MinimumWidth = 120;
            this.ColumnStartTime.Name = "ColumnStartTime";
            this.ColumnStartTime.ReadOnly = true;
            // 
            // ColumnEndTime
            // 
            this.ColumnEndTime.HeaderText = "结束时间";
            this.ColumnEndTime.MinimumWidth = 120;
            this.ColumnEndTime.Name = "ColumnEndTime";
            this.ColumnEndTime.ReadOnly = true;
            // 
            // ColumnEMail
            // 
            this.ColumnEMail.HeaderText = "收信邮箱";
            this.ColumnEMail.MinimumWidth = 120;
            this.ColumnEMail.Name = "ColumnEMail";
            this.ColumnEMail.ReadOnly = true;
            // 
            // MainPageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 368);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel1);
            this.Name = "MainPageView";
            this.Text = "小火箭服务器端";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNickName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEMail;
    }
}