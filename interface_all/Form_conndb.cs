using Google.Protobuf.WellKnownTypes;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace interface_all
{
    public partial class Form_conndb : Form
    {


        // ----- ADS -----
        private double[,] data;
        private bool DataState = false;
        
        private List<List<double>> saveData_F;
        private DataColumn[] dataColumn = null;
        public DataTable dataTable = null;

        private byte[] serialGet = new byte[24];
        private double[,] serialEEG = new double[9, samples];

        private Thread dataAcquisition,Sqlconn;
        private int sampling = 500;//250 500
        private static int channelCount = 2;
        private static int samples = 50;//每次處理的資料點數
        //顯示波形
        Series series1 = new Series("Ch1"); //改名稱
        Series series2 = new Series("Ch2");
        private int g = 0;
        private int plot_sec = 4;
        // ----- 濾波 -----
        // notch
        public double[,] filterdata = new double[channelCount, samples];
        public double[] An;
        public double[] Bn;
        private double[,] dbuffer;
        public double[,] filter_temp;
        //highpass(2Hz)
        public double[,] filterdata_H = new double[channelCount, samples];
        public double[] Ah;
        public double[] Bh;
        private double[,] dbuffer_H;
        public double[,] filter_temp_H;
        //lowpass(100Hz)
        public double[,] filterdata_L = new double[channelCount, samples];
        public double[] Al;
        public double[] Bl;
        public double[,] dbuffer_L;
        public double[,] filter_temp_L;

        // -----  -----
        //----------------------------------------------------------------最高解析度計時器
        [DllImport("kernel32.dll")]
        public extern static short QueryPerformanceCounter(ref long x);
        [DllImport("kernel32.dll")]
        public extern static short QueryPerformanceFrequency(ref long x);
        //----------------------------------------------------------------DAQ
        
        public long Mctr1=0, Mctr2 = 0, Mctr3, Mfreq;
        public Form_conndb()
        {
            InitializeComponent();
            QueryPerformanceFrequency(ref Mfreq);
            // ----- filter -----
            // 59Hz and 61Hz "stop" //
            An = new double[11] { 1, -7.230969720117846, 25.833784944706085, -58.702661887968140, 93.280351672201820, -107.8370730335750, 91.775250904635020, -56.823580458726970, 24.603348873780366, -6.775449640563339, 0.921885817686209 };
            Bn = new double[11] { 0.960148851837856, -6.999736576219315, 25.212709359257623, -57.760660528660740, 92.534452895367120, -107.8489405311069, 92.534452895367150, -57.760660528660740, 25.2127093592576, -6.99973657621932, 0.960148851837856 };
            // butterworth 3階 1.5Hz "high" //
            Ah = new double[4] { 1, -2.96230144608566, 2.92531013484869, -0.963002115885092 };
            Bh = new double[4] { 0.981326712102431, -2.94398013630729, 2.94398013630729, -0.981326712102431 };
            // butterworth 3階 100Hz "low" //
            Al = new double[12] { 1, -2.19307748924835, 3.54669340494548, -3.64137802069775, 2.90116357030837, -1.70203056062509, 0.774942481487068, -0.262495915353936, 0.0657813800501928, -0.0113571489849672, 0.00122543449100495, -0.0000617472746032351 };
            Bl = new double[12] { 0.000234084662645222, 0.00257493128909745, 0.0128746564454872, 0.0386239693364617, 0.0772479386729234, 0.108147114142093, 0.108147114142093, 0.0772479386729234, 0.0386239693364617, 0.0128746564454872, 0.00257493128909745, 0.000234084662645222 };

        }      

        private void Form_conndb_Load(object sender, EventArgs e)
        {
            bt_connect.Enabled = false;
        }
     
        //輸入姓名並且啟用按扭
        public string input_name;
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            // 檢查 TextBox 是否為空，然後啟用按钮
            bt_connect.Enabled = !string.IsNullOrWhiteSpace(tB_Name.Text);
            if (bt_connect.Enabled == true)
            {
                input_name = tB_Name.Text;
            }
        }
        
        
        //輸入完姓名後才可以按連接SQL
        private void bt_connect_Click(object sender, EventArgs e)
        {
            DateTime currentDateAndTime = DateTime.Now;
            DateTime currentDate = DateTime.Today;
            string currentDateAsString = DateTime.Now.ToString("yyyy-MM-dd");
            Database db = new Database();
            db.InsertNameToMySQL("test_subdata", input_name, currentDateAsString);
            
            loadData();
            //---port讀位置---
            RegistryKey myRegistry = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            foreach (string valuename in myRegistry.GetValueNames())
            {
                if (myRegistry.GetValue(valuename) is String)
                {
                    if (valuename.Contains("ProlificSerial"))
                    {
                        serialPort1.PortName = myRegistry.GetValue(valuename).ToString();
                    }
                }
            }
            serialPort1.BaudRate = 312500; // 128000 312500
            serialPort1.DataBits = 8;
            serialPort1.StopBits = StopBits.One;
            serialPort1.ReadBufferSize = 1048576;
            try
            {
                serialPort1.Open();
                dataAcquisition = new Thread(new ThreadStart(this.dataAcquisitionStart));
                dataAcquisition.IsBackground = true;
                dataAcquisition.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }
        private double[,] temp_ch1 = new double[20,1500];
        private void dataAcquisitionStart()
        {
            filterdata = new double[channelCount, samples];
            filter_temp = new double[channelCount, samples];
            filterdata_H = new double[channelCount, samples];
            filter_temp_H = new double[channelCount, samples];
            filterdata_L = new double[channelCount, samples];
            filter_temp_L = new double[channelCount, samples];
            dbuffer = new double[channelCount, An.Length];
            dbuffer_H = new double[channelCount, Ah.Length];
            dbuffer_L = new double[channelCount, Al.Length];

            int idata = 0;
            int state = 0;
            while (serialPort1.IsOpen)
            {
                switch (state)
                {
                    case 0:
                        if (serialPort1.ReadByte() == 192) //24byte=192bit
                        {
                            if (serialPort1.ReadByte() == 0)
                            {
                                if (serialPort1.ReadByte() == 0)
                                {
                                    state = 1;
                                }
                            }
                        }
                        break;
                    case 1:
                        if (serialPort1.BytesToRead >= 24)
                        {
                            state = 0;
                            serialPort1.Read(serialGet, 0, 24);
                            double temp;

                            for (int i = 0; i < channelCount; i++)
                            {
                                //Console.WriteLine("I = " + i);
                                if (serialGet[i * 3] <= 127)
                                {
                                    //Console.WriteLine("A" + serialGet[i * 3]);
                                    temp = serialGet[i * 3] * 65536 + serialGet[i * 3 + 1] * 256 + serialGet[i * 3 + 2];//2^16+2^8+2^0
                                    //Console.WriteLine("AA" + temp);
                                }
                                else
                                {
                                    //Console.WriteLine("B" + serialGet[i * 3]);
                                    temp = serialGet[i * 3] * 65536 + serialGet[i * 3 + 1] * 256 + serialGet[i * 3 + 2] - 16777216;//2^16+2^8+2^0-2^24
                                    //Console.WriteLine("BB" + temp);
                                }
                                serialEEG[i, idata] = temp * 4 / 16777216 - 0;
                            }
                            idata++;
                            //Console.WriteLine("dd" + idata);
                            if (idata >= samples)
                            {
                                // ------- filter notch -------

                                data = serialEEG;
                                int blength = Bn.GetLength(0);
                                int datalength = data.GetLength(1);

                                for (int dl = 0; dl < datalength; dl++)
                                {
                                    for (int ich = 0; ich < channelCount; ich++)
                                    {
                                        filter_temp[ich, dl] = data[ich, dl];  // 先存data中25個資料點 的 第一個資料點
                                    }
                                    int k = 0;
                                    while (k < blength - 1)
                                    {
                                        for (int ich = 0; ich < channelCount; ich++)
                                        {
                                            dbuffer[ich, k] = dbuffer[ich, k + 1];  // 第一個拿掉，全部往上補
                                        }
                                        k++;
                                    }
                                    for (int ich = 0; ich < channelCount; ich++)
                                    {
                                        dbuffer[ich, blength - 1] = 0; // 最後一個令為0
                                    }
                                    k = 0;
                                    while (k < blength)
                                    {
                                        for (int ich = 0; ich < channelCount; ich++)
                                        {
                                            dbuffer[ich, k] = dbuffer[ich, k] + filter_temp[ich, dl] * Bn[k];
                                        }
                                        k++;
                                    }
                                    k = 0;
                                    while (k < blength - 1)
                                    {
                                        for (int ich = 0; ich < channelCount; ich++)
                                        {
                                            dbuffer[ich, k + 1] = dbuffer[ich, k + 1] - dbuffer[ich, 0] * An[k + 1];
                                        }
                                        k++;
                                    }
                                    for (int ich = 0; ich < channelCount; ich++)
                                    {
                                        filterdata[ich, dl] = dbuffer[ich, 0];
                                    }
                                }
                                // high pass
                                int blength_H = Bh.GetLength(0);

                                for (int dl = 0; dl < datalength; dl++)
                                {
                                    for (int ich = 0; ich < channelCount; ich++)
                                    {
                                        filter_temp_H[ich, dl] = filterdata[ich, dl];  // 先存data中25個資料點 的 第一個資料點
                                    }
                                    int k = 0;
                                    while (k < blength_H - 1)
                                    {
                                        for (int ich = 0; ich < channelCount; ich++)
                                        {
                                            dbuffer_H[ich, k] = dbuffer_H[ich, k + 1];  // 第一個拿掉，全部往上補
                                        }
                                        k++;
                                    }
                                    for (int ich = 0; ich < channelCount; ich++)
                                    {
                                        dbuffer_H[ich, blength_H - 1] = 0; // 最後一個令為0
                                    }
                                    k = 0;
                                    while (k < blength_H)
                                    {
                                        for (int ich = 0; ich < channelCount; ich++)
                                        {
                                            dbuffer_H[ich, k] = dbuffer_H[ich, k] + filter_temp_H[ich, dl] * Bh[k];
                                        }
                                        k++;
                                    }
                                    k = 0;
                                    while (k < blength_H - 1)
                                    {
                                        for (int ich = 0; ich < channelCount; ich++)
                                        {
                                            dbuffer_H[ich, k + 1] = dbuffer_H[ich, k + 1] - dbuffer_H[ich, 0] * Ah[k + 1];
                                        }
                                        k++;
                                    }
                                    for (int ich = 0; ich < channelCount; ich++)
                                    {
                                        filterdata_H[ich, dl] = dbuffer_H[ich, 0];
                                    }
                                }
                                // Low pass
                                int blength_L = Bl.GetLength(0);

                                for (int dl = 0; dl < datalength; dl++)
                                {
                                    for (int ich = 0; ich < channelCount; ich++)
                                    {
                                        filter_temp_L[ich, dl] = filterdata_H[ich, dl];  // 先存data中25個資料點 的 第一個資料點
                                    }
                                    int k = 0;
                                    while (k < blength_L - 1)
                                    {
                                        for (int ich = 0; ich < channelCount; ich++)
                                        {
                                            dbuffer_L[ich, k] = dbuffer_L[ich, k + 1];  // 第一個拿掉，全部往上補
                                        }
                                        k++;
                                    }
                                    for (int ich = 0; ich < channelCount; ich++)
                                    {
                                        dbuffer_L[ich, blength_L - 1] = 0; // 最後一個令為0
                                    }
                                    k = 0;
                                    while (k < blength_L)
                                    {
                                        for (int ich = 0; ich < channelCount; ich++)
                                        {
                                            dbuffer_L[ich, k] = dbuffer_L[ich, k] + filter_temp_L[ich, dl] * Bl[k];
                                        }
                                        k++;
                                    }
                                    k = 0;
                                    while (k < blength_L - 1)
                                    {
                                        for (int ich = 0; ich < channelCount; ich++)
                                        {
                                            dbuffer_L[ich, k + 1] = dbuffer_L[ich, k + 1] - dbuffer_L[ich, 0] * Al[k + 1];
                                        }
                                        k++;
                                    }
                                    for (int ich = 0; ich < channelCount; ich++)
                                    {
                                        filterdata_L[ich, dl] = dbuffer_L[ich, 0];
                                    }
                                }

                                // ==================================

                                idata = 0;
                                state = 0;

                                //data2saveData_ADS(data, filterdata_L);
                                //BP_data(filterdata_L);
                                EEGdata_compute_stoer(filterdata_L);
                                Console.WriteLine("ch1***** = " + ch1.Count);
                                Console.WriteLine("count = " + count);
                                Console.WriteLine("arr = " + temp_ch1.GetLength(1));
                                //Console.WriteLine("EEG_Acquire_mode = " + EEG_Acquire_mode);
                                if (EEG_Acquire_mode == "RE" || EEG_Acquire_mode=="MA" )
                                {
                                    count++;
                                    Thread restart_listt = new Thread(() => restart_list(temp_ch1, ch1));
                                    restart_listt.Start();
                                    Thread UploadEEGdataToSqlThread = new Thread(() => UploadEEGdataToSql(temp_ch1));
                                    UploadEEGdataToSqlThread.IsBackground = true;
                                    UploadEEGdataToSqlThread.Start();
                                    EEG_Acquire_mode = "done";
                                }
                                if(EEG_Acquire_mode == "OV")
                                {
                                    Thread UpdateEEGdataToSqlThread = new Thread(() => GameOver_compute());
                                    UpdateEEGdataToSqlThread.Start();
                                    EEG_Acquire_mode = "done";
                                }

                                //Console.WriteLine(filterdata_L.GetLength(1));
                                MethodInvoker mi = new MethodInvoker(this.DisplayData_ADS);
                                this.BeginInvoke(mi, null);

                            }
                        }
                        break;
                }
            }
        }
      public string EEG_Acquire_mode ="NULL";
        public void restart_list(double[,] array,List<double> channel)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[count-1, j] = channel[j];
            }
            Console.WriteLine("count = " + count);
            channel.Clear();
        }
        private void DisplayData_ADS()
        {
            data = new double[channelCount, samples];
             //DataPoint dp = new DataPoint();
            for (int i = 0; i < channelCount; i++)
            {
                for (int j = 0; j < samples; j++)
                {
                    data[i, j] = filterdata_L[i, j];
                    //Console.WriteLine("DDD = " + j + data[i, j]);
                }
            }
            for (int k = 1; k <= channelCount; k++)
            {
                Chart ch = splitContainer2.Controls.Find("chart" + k.ToString(), true)[0] as Chart;
                ch.ChartAreas[0].AxisX.Maximum = sampling * plot_sec;
                ch.ChartAreas[0].AxisX.Minimum = 0;
                ch.ChartAreas[0].AxisY.Maximum = 0.008;
                ch.ChartAreas[0].AxisY.Minimum = -0.008;
                //Console.WriteLine("k " + k + " " + ch);
            }

            if (g < sampling * plot_sec) //g小於資料長度，持續增加取樣點
            {
                g += samples;
                for (int i = 0; i < samples; i++)
                {
                    chart1.Series[0].Points.Add(data[0, i]);
                    chart2.Series[0].Points.Add(data[1, i]);
                }
            }
            // Remove data points on the left side
            else
            {
                for (int i = 0; i < samples; i++)
                {
                    chart1.Series[0].Points.RemoveAt(0);
                    chart2.Series[0].Points.RemoveAt(0);

                    chart1.Series[0].Points.Add(data[0, i]);
                    chart2.Series[0].Points.Add(data[1, i]);
                }
            }
            //chart3.Series[0].Points.Add(alpha_band);
            //refresh
            chart1.Invalidate();
            chart2.Invalidate();
            //delta_cz.Invalidate();
        }

        private void bt_gotoBCI_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            //訂閱事件
            form1.eventFormtrigger += Form_hard_easy_DataPassed;
            form1.Show();
        }
        //接收到不同狀態要做的事
        public void Form_hard_easy_DataPassed(string data)
        {
            if (data == "RE")
            {
                EEG_Acquire_mode = "RE";
                label1.Text = 1.ToString();
            }
            else if (data == "ZO")
            {
                ch1.Clear();
            }
            else if (data == "M")
            {
                EEG_Acquire_mode = "MA";
                label1.Text = 2.ToString();
            }
            else if (data == "OV")
            {
                //label1.Text = final_score;
                
                EEG_Acquire_mode = "OV";
                string filePath = "C:\\Users\\4090\\Desktop\\temp\\new.csv";
                WriteToCsvFile(filePath, temp_ch1); 
            }

        }



  


        //儲存資料
        int count = 0;
        List<double> ch1 = new List<double>();
       
        public void EEGdata_compute_stoer(double[,] temp_data)
        {

           // double[] doubles = new double[3*sampling];
            List<double> temp1 = new List<double>();
            
            //DataTable dt = new DataTable();
            //dt.Columns.Add("ch1", typeof(double));
            //dt.Columns.Add("ch2", typeof(double));
            for (int m = 0; m < samples; m++)
            {
                /*DataRow row = dt.NewRow();
                  row["ch1"] = temp_data[0, m];
                  row["ch2"] = temp_data[1, m];
                  dt.Rows.Add(row);
                  temp1.Add(temp_data[0, m]);
                  temp2.Add(temp_data[1, m]);*/
                temp1.Add(temp_data[0, m]);
                //doubles[m] = temp_data[0, m];
                //Console.WriteLine(m);
            }
            if (ch1.Count < 1455)
            {
                ch1.AddRange(temp1);
             

            }

                /*foreach (DataRow dataRow in dt.Rows)
                {
                    foreach (var item in dataRow.ItemArray)
                    {
                        // 处理每一列的值
                        Console.Write(item + " ");
                    }
                    Console.WriteLine(); // 换行表示下一行数据
                }*/
                /*if (EEG_Acquire_mode == "RE" && ch1.Count==1500)
                {
                     List<string> rows = new List<string>();
                    foreach (double value in ch1)
                    {
                        rows.Add($"('{value}')");
                    }
                Database db = new Database();
                db.BulkInsertToMySQL_ch1("tryeeg", rows);
                }*/

            }
        int aaa = 0;
        private void UploadEEGdataToSql(double[,] array)
        {
            List<string> rowList = new List<string>();
            for (int j = 0; j < array.GetLength(1); j++)
            {
                rowList.Add($"('{array[aaa, j]}')");
            }
            /*List<string> rows = new List<string>();
           foreach (double value in rowList)
            {
                rows.Add($"('{value}')");
            }*/
            Stopwatch stopwatch = new Stopwatch();
            Database db = new Database();
            db.BulkInsertToMySQL_ch1("tryeeg", rowList);
            stopwatch.Start();
            scoker_pyserver_Cclient s = new scoker_pyserver_Cclient();
            s.SendMessage("connect to mysql ch1");
            string r = s.ReceiveMessage();
            stopwatch.Stop();
            Console.WriteLine("Socket Received data: " + r);
            TimeSpan elapsedTime = stopwatch.Elapsed;
            // 输出结果
            Console.WriteLine($"Elapsed Time: {elapsedTime.TotalMilliseconds} milliseconds");        
            db.DELETETabledata_ch1("tryeeg");
            aaa++;
        }
        
       private void GameOver_compute()
        {
            /* Stopwatch stopwatch = new Stopwatch();
            scoker_pyserver_Cclient s = new scoker_pyserver_Cclient();
            stopwatch.Start();
            s.SendMessage("gameover");
            string r = s.ReceiveMessage();
            stopwatch.Stop();
            Console.WriteLine("Socket Received data: " + r);
            TimeSpan elapsedTime = stopwatch.Elapsed;
            string final_score ="本次測驗分數為：\n" + r;
            // 输出结果
            Console.WriteLine($"Elapsed Time: {elapsedTime.TotalMilliseconds} milliseconds");
            DialogResult  mesSelect =MessageBox.Show(final_score, "測驗結果" ,MessageBoxButtons.OKCancel);
            if (mesSelect == DialogResult.OK){
                System.Environment.Exit(0);
            }*/

        }
        public void WriteToCsvFile(string filePath, double[,] array)
        {
            List<double> rowList = new List<double>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    rowList.Add(array[i, j]);
                }
            }
            try
            {
                // 使用 StreamWriter 写入文件
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (double value in rowList)
                    {
                        writer.WriteLine(value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to CSV file: {ex.Message}");
            }
        }
        //連線資料庫，並且讀取table資料
        private void loadData()
        {
            var database = new Database();
            if (database.connect_db())
            {
                MessageBox.Show("連線成功", "成功");
                string sql = "SELECT * FROM test_subdata";
                MySqlCommand cmd = new MySqlCommand(sql, database.conn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                //設定監控的命令
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                //把抓取到的資料填入DataTable
                da.Fill(dt);
                //把DataTable的資料填入dataGridView
                dataGridView1.DataSource = dt;

                bt_gotoBCI.Visible = true;
                //database.close_db();
            }
            else
            {
                MessageBox.Show("連線失敗", "失敗");
            }
        }
    }
}
