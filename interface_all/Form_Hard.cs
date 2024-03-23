using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace interface_all
{
    
    public partial class Form_Hard : Form
    {
        Record record = new Record();//呼叫record class 儲存資料
        public double timeavg = 0;
        public int counttotal = 0;
        public int countcorrect = 0;
        public string showscore = "";
        public Form_Hard()
        {
            InitializeComponent();
        }
        /*To catch the reaction time*/
        private long freq = 0, start = 0, stop = 0;
        public static class QueryPerformanceMethd
        {
            [DllImport("kernel32.dll")]
            public extern static short QueryPerformanceCounter(ref long x);


            [DllImport("kernel32.dll")]
            public extern static short QueryPerformanceFrequency(ref long x);
        }
        /*when we load the form ,it will be set*/
        private void Form_Hard_Load(object sender, EventArgs e)
        {
            QueryPerformanceMethd.QueryPerformanceFrequency(ref freq);
            timer1.Start();
            label_count_backward.Text = " 5 ";
            pictureBox_blackcross.Visible = false;
            label_plus.Visible = false;
            label_compare.Visible = false;

        }
        private int t = -5;
        public delegate void form2EventHandler(string sMsg); //宣告委派
        public event form2EventHandler eventForm2trigger; //傳遞資料event
        private int re = -1;
        private int ma = -1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            t++;
            Debug.WriteLine("現在是 " + t + " 秒");
            int a = int.Parse(label_count_backward.Text);
            a--;
            label_count_backward.Text = a.ToString();
            Console.WriteLine("j = " + re);
            Console.WriteLine("ma = " + ma);
            if (t == 0)
            {
                 re = 0;
                //eventForm2trigger.Invoke("REST");
                eventForm2trigger?.Invoke("ZO");
                Thread socketThread = new Thread(() => SqlconnStart());
                socketThread.Start();
                label_count_backward.Visible = false;
                pictureBox_blackcross.Visible = true; ;//blackcross 90s
              
            }
            else if (t == 30)
            {
                //eventForm2trigger?.Invoke("relax");
                pictureBox_blackcross.Image = Properties.Resources._break;//break 30s
            }
            else if (t == 40)
            {
                ma = 0;
                eventForm2trigger?.Invoke("ZO");
                GenerateRandomNumbersandplus();
                pictureBox_blackcross.Visible = false;
                //mode = 3; //mental arithmetic 90s
            }
            else if (t == 70)
            {
                interval = (double)(stop - start) / (double)freq;
                Debug.WriteLine("interval = " + interval);
                record.print();
                timeavg = Math.Round(record.timeavg(),2);
                counttotal= record.count();
                countcorrect = record.countrightanswer();
                Debug.WriteLine("timeavg = " + timeavg + "正確題數 = " +countcorrect+ "所有答題數 = "+counttotal);
                MessageBox.Show("等待計算結果","測驗結束");
                //mode = 4;//test over
            }else if(t == 73)
            {
                eventForm2trigger?.Invoke("OV");
                 timer1.Stop();
                 Stopwatch stopwatch = new Stopwatch();
                 scoker_pyserver_Cclient s = new scoker_pyserver_Cclient();
                 stopwatch.Start();
                 s.SendMessage("gameover");
                showscore = s.ReceiveMessage();
                 stopwatch.Stop();
                Console.WriteLine("Socket Received data: " + showscore);
                TimeSpan elapsedTime = stopwatch.Elapsed;
                string final_score = "本次測驗分數為：\n" + showscore;
                // 输出结果
                Console.WriteLine($"Elapsed Time: {elapsedTime.TotalMilliseconds} milliseconds");
                DialogResult mesSelect = MessageBox.Show("按OK進入測驗結果", "測驗結果", MessageBoxButtons.OKCancel);
                if (mesSelect == DialogResult.OK)
                {
                    Form_Finalscore f = new Form_Finalscore(showscore, timeavg, countcorrect, counttotal);
                    f.Show();
                    //System.Environment.Exit(0);
                }
            }                    

            if (t >= 0 && t <= 30)
            {
                if (re % 3 == 0 && t != 0)
                {
                    eventForm2trigger?.Invoke("RE");
                }
                re++;
            }

            if (t >= 40 && t < 72)
            {
                if (ma % 3 == 0 && t != 40)
                {
                    eventForm2trigger?.Invoke("M");
                }
                ma++;
            }
           
        }

        private void SqlconnStart()
        {
            scoker_pyserver_Cclient socker = new scoker_pyserver_Cclient();
            socker.SendMessage("Hello from C# client");
            string received = socker.ReceiveMessage();
            Console.WriteLine("Socket Received data: " + received);

        }

        private Random random = new Random();
        private bool isBigger = false, isSmaller = false, isEqual = false;
        /*Generate a two-digit number & a one-digit number then boolean the answer*/
        private void GenerateRandomNumbersandplus()
        {
            QueryPerformanceMethd.QueryPerformanceCounter(ref start);
            label_plus.Visible = true;
            int threeDigitNumber = random.Next(100, 1000); // Generate a random two-digit number
            int twoDigitNumber = random.Next(10, 100); // Generate a random one-digit number
            int result = threeDigitNumber + twoDigitNumber; // Add the two numbers together
            label_plus.Text = threeDigitNumber.ToString() + " + " + twoDigitNumber.ToString();
            int randomnumber = random.Next(100, 1000);
            label_compare.Text = "你的答案    ?    " + randomnumber.ToString();
            isBigger = result > randomnumber;
            isSmaller = result < randomnumber;
            isEqual = result == randomnumber;

        }
        private double interval = 0;

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
