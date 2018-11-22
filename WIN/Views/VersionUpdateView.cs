using CCWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WIN.Views
{
    public partial class VersionUpdateView : Skin_Mac
    {
        private string DownloadPath = "";
        public VersionUpdateView(Model.ClientVersion version)
        {
            InitializeComponent();
            this.labelVersion.Text = version.Version;
            this.richTextBox1.Text = version.VersionDirection;
            this.richTextBox1.Enabled = false;
            this.DownloadPath = version.DownloadPath;
        }

        #region [下载新版]
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.DownloadPath);
        }
        #endregion

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
