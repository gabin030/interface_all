using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace interface_all
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Image[] pic;
        private void Form1_Load(object sender, EventArgs e)
        {
            pic = new Image[5];
            pic[0] = Properties.Resources._1;
            pic[1] = Properties.Resources._2;
            pic[2] = Properties.Resources._3;
            pic[3] = Properties.Resources._4;
            pic[4] = Properties.Resources._5;
        }
        int pic_num = 0;

        private void bt_right_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("num = " + pic_num);
            try
            {
                this.pictureBox1.Image = pic[pic_num];
                pic_num++;
            }
            catch (System.IndexOutOfRangeException)
            {
                pic_num = 0;
                this.pictureBox1.Image = pic[pic_num];
            }
        }

        private void bt_left_Click(object sender, EventArgs e)
        {
            try
            {
                pic_num--;
                this.pictureBox1.Image = pic[pic_num];
            }
            catch (System.IndexOutOfRangeException)
            {
                pic_num = 4;
                this.pictureBox1.Image = pic[pic_num];
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("num = " + pic_num);
            if (pic_num == 5 || pic_num == 4)
            {
               Form2 f = new Form2();
                f.eventForm1trigger += Form_hard_easy_DataPassed;
               f.Show();
            }
        }
        public delegate void formEventHandler(string sMsg); //宣告委派
        public event formEventHandler eventFormtrigger; //傳遞資料event
        public void Form_hard_easy_DataPassed(string data)
        {
            if (data == "RE")
            {
                eventFormtrigger("RE");
               //Console.WriteLine("data_form1 =  1" );

            }
            else if (data == "M")
            {
                eventFormtrigger("M");
                //Console.WriteLine("data_form1 =  2");

            }
            else if (data == "OV")
            {
                eventFormtrigger("OV");
                //Console.WriteLine("data_form1 =  3");

            }
            else if (data == "ZO")
            {
                eventFormtrigger("ZO");

            }
            //Console.WriteLine("data = " + data);
            // 在這裡處理接收到的數據
            //Console.WriteLine("data_form1 = " + data);
            //MessageBox.Show($"Received data: {data}");

        }
    }
}
