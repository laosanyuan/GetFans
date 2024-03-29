﻿using CCWin;
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
    public partial class SerialNumberView : Skin_Mac
    {
        public bool IsValid { get; private set; } //序列号是否有效

        public SerialNumberView()
        {
            InitializeComponent();
        }

        //获取序列号
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(BLL.Serial.GetBuySerialPath());
        }
        //确定按钮
        private void buttonOK_Click(object sender, EventArgs e)
        {
            //判断序列号是否有效
            if (this.skinTextBox1.Text.Equals("") || !BLL.Serial.IsValidSerial(this.skinTextBox1.Text))
            {
                MessageBox.Show("序列号错误，请输入正确的序列号", "提示");
                return;
            }
            else
            {
                this.IsValid = true;
                //存入本地文件
                Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                cfa.AppSettings.Settings["Serial"].Value = this.skinTextBox1.Text;
                cfa.Save();
                ConfigurationManager.RefreshSection("appSettings");
                this.Close();
            }
        }
    }
}
