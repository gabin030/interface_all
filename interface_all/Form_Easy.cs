using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace interface_all
{
    public partial class Form_Easy : Form
    {
        /*To catch the reaction time*/
        private long freq = 0, start = 0, stop = 0;
        public static class QueryPerformanceMethd
        {
            [DllImport("kernel32.dll")]
            public extern static short QueryPerformanceCounter(ref long x);


            [DllImport("kernel32.dll")]
            public extern static short QueryPerformanceFrequency(ref long x);
        }
            public Form_Easy()
        {
            InitializeComponent();
        }

        /*when we load the form ,it will be set*/
        private void Form_Easy_Load_1(object sender, EventArgs e)
        {
            QueryPerformanceMethd.QueryPerformanceFrequency(ref freq);
            timer1.Start();
            label_count_backward.Text = " 2 ";
            pictureBox_blackcross.Visible = false;
            label_plus.Visible = false;
            label_compare.Visible = false;
        }
        private int t = 0;
        /*according the timer to driver the interface*/
        private void timer1_Tick(object sender, EventArgs e)
        {
            t++;
            Debug.WriteLine("現在是 " + t + " 秒");
            int i = int.Parse(label_count_backward.Text);
            i--;

            label_count_backward.Text = i.ToString();
            if (i == 0)
            {
                t = 0;
                label_count_backward.Visible = false;
                pictureBox_blackcross.Visible = true; ;//blackcross 90s
            }
            else if (t == 3)
            {
                pictureBox_blackcross.Image = Properties.Resources._break;//break 30s
            }
            else if (t == 5)
            {
                GenerateRandomNumbersandplus();
                pictureBox_blackcross.Visible = false;
                //mode = 3; //mental arithmetic 90s
            }
            else if (t == 30)
            {
                interval = (double)(stop - start) / (double)freq;
                Debug.WriteLine("interval = " + interval);
                record.print();
                timer1.Stop();
                MessageBox.Show("測驗結束");
                //mode = 4;//test over
            }
        }
        private Random random = new Random();
        private bool isBigger = false, isSmaller = false, isEqual = false;
        /*Generate a two-digit number & a one-digit number then boolean the answer*/
        private void GenerateRandomNumbersandplus()
        {
            QueryPerformanceMethd.QueryPerformanceCounter(ref start);
            label_plus.Visible = true;
            int twoDigitNumber = random.Next(10, 100); // Generate a random two-digit number
            int oneDigitNumber = random.Next(0, 10); // Generate a random one-digit number
            int result = twoDigitNumber + oneDigitNumber; // Add the two numbers together
            label_plus.Text = twoDigitNumber.ToString() + " + " + oneDigitNumber.ToString();
            int randomnumber = random.Next(0, 100);
            label_compare.Text = "你的答案    ?    " + randomnumber.ToString();
            isBigger = result > randomnumber;
            isSmaller = result < randomnumber;
            isEqual = result == randomnumber;

        }
        private double interval = 0;
        Record record = new Record();//呼叫record class 儲存資料
        /*判斷按下的按鍵為何*/

        private void keydown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    QueryPerformanceMethd.QueryPerformanceCounter(ref stop);
                    label_plus.Visible = false;
                    label_compare.Visible = true;
                    break;
                case Keys.Right:
                    interval = (double)(stop - start) / (double)freq;
                    if (isBigger == true)
                    {
                        record.store(1, interval);
                        isBigger = false;
                    }
                    else
                    {
                        record.store(0, interval);
                    }
                    label_compare.Hide();
                    GenerateRandomNumbersandplus();
                    break;
                case Keys.Left:
                    interval = (double)(stop - start) / (double)freq;
                    if (isSmaller == true)
                    {
                        record.store(1, interval);
                        isSmaller = false;
                    }
                    else
                    {
                        record.store(0, interval);
                    }
                    label_compare.Hide();
                    GenerateRandomNumbersandplus();
                    break;
                case Keys.Down:
                    interval = (double)(stop - start) / (double)freq;
                    if (isEqual == true)
                    {
                        record.store(1, interval);
                        isEqual = false;
                    }
                    else
                    {
                        record.store(0, interval);
                    }
                    label_compare.Hide();
                    GenerateRandomNumbersandplus();
                    break;
            }
        }

    }


}
