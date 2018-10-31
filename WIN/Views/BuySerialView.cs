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
    public partial class BuySerialView : Skin_Mac
    {
        public string SerialStr { get; private set; }//购买到的序列号

        public BuySerialView()
        {
            InitializeComponent();
        }
    }
}
