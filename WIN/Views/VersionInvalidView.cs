using CCWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WIN.Views
{
    public partial class VersionInvalidView : Skin_Mac
    {
        private string DownloadPath = "";
        public VersionInvalidView(string downloadPath)
        {
            InitializeComponent();
            this.DownloadPath = downloadPath;
        }

        #region 关闭进程
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            this.VersionInvalidView_FormClosed(sender, new FormClosedEventArgs(CloseReason.None));
        }
        private void VersionInvalidView_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();

            Application.Exit(); // 关闭程序
        }
        #endregion

        #region [打开下载]
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.DownloadPath);
        }
        #endregion
    }
}
