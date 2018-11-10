using CCWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            BuySerialView buy = new BuySerialView();
            buy.ShowDialog();

            if (buy.SerialStr != null && !buy.SerialStr.Equals(""))
            {
                this.skinTextBox1.Text = buy.SerialStr;
            }

        }
        //确定按钮
        private void buttonOK_Click(object sender, EventArgs e)
        {
            //if (this.skinTextBox1.Text.Equals("") || !BLL.Serial.IsValidSerial(this.skinTextBox1.Text))
            //{
            //    MessageBox.Show("序列号错误，请输入正确的序列号", "提示");
            //    return;
            //}iuuuuuuuuuuuuuuuuuuuuuuuuu·1见面呢
            //else
            //{
            //    this.IsValid = true;
            //    //存入本地文件

            //}
        }
    }
}
