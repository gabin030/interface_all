using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace interface_all
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }
        public delegate void form1EventHandler(string sMsg); //宣告委派
        public event form1EventHandler eventForm1trigger; //傳遞資料event
        private void bt_easy_Click(object sender, EventArgs e)
        {
            Form F_easy = new Form_Easy();
            F_easy.Show();
        }

        private void bt_hard_Click(object sender, EventArgs e)
        {
            Form_Hard F_hard = new Form_Hard();
            F_hard.eventForm2trigger += Form_hard_easy_DataPassed;
            F_hard.Show();
        }
        public void Form_hard_easy_DataPassed(string data)
        {
            if (data == "RE")
            {
                eventForm1trigger("RE");
                label1.Text = 1.ToString();
            }
            else if (data == "M")
            {
               eventForm1trigger("M");
                label1.Text = 2.ToString();
            }
            else if (data == "OV")
            {
                eventForm1trigger("OV");
                label1.Text = 3.ToString();
            }else if (data == "ZO")
            {
                eventForm1trigger("ZO");
                label1.Text = 4.ToString();
            }
            //Console.WriteLine("data = " + data);
            // 在這裡處理接收到的數據
            //Console.WriteLine("data = " + data);
           //MessageBox.Show($"Received data: {data}");

        }
    }
}
