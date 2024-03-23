using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace interface_all
{
    public partial class Form_Finalscore : Form
    {
        public Form_Finalscore(string score,double avgtime,int ranswer,int tanswer)
        {
            InitializeComponent();
            label_score.Text = score;
            double panswer =  Convert.ToDouble(ranswer) / Convert.ToDouble(tanswer);
            panswer = Math.Round(panswer,2) * 100;
            progressBar1.Value = Convert.ToInt32(panswer);
            label_panswer.Text = panswer.ToString() + "%";
            label_timeanswer.Text = avgtime.ToString();
           
            var database = new Database();
            //MessageBox.Show("連線成功", "成功");
            string query1 = "SELECT * FROM ch1bp LIMIT 10";
            MySqlCommand cmd = new MySqlCommand(query1, database.conn);
            MySqlDataAdapter da = new MySqlDataAdapter();
            //設定監控的命令
            da.SelectCommand = cmd;
            DataSet dt = new DataSet();
            //把抓取到的資料填入DataTable
            da.Fill(dt);
            string query2 = "SELECT * FROM ch1bp LIMIT 10 OFFSET 10";
            MySqlCommand cmd2 = new MySqlCommand(query2, database.conn);
            MySqlDataAdapter da2 = new MySqlDataAdapter();
            //設定監控的命令
            da2.SelectCommand = cmd2;
            DataSet dt2 = new DataSet();
            //把抓取到的資料填入DataTable
            da2.Fill(dt2);

            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {
                DataRow row = dt.Tables[0].Rows[i];
                double yAxisValue = Convert.ToDouble(row["bp1"]); // 將 Y 座標值轉換為 Double
                int xAxisValue = 3 * i; // 設定 X 座標值從 3 開始
                chart1.Series[0].Points.AddXY(xAxisValue, yAxisValue);
            }
            for (int i = 0; i < dt2.Tables[0].Rows.Count; i++)
            {
                DataRow row = dt2.Tables[0].Rows[i];
                double yAxisValue = Convert.ToDouble(row["bp1"]); // 將 Y 座標值轉換為 Double
                int xAxisValue = 3 * i; // 設定 X 座標值從 3 開始
                chart1.Series[1].Points.AddXY(xAxisValue, yAxisValue);
            }
            double  s = Convert.ToDouble(score);
            if (s >= 1)
            {
                pictureBox1.Image = Properties.Resources.Excellent;
                label2.Text = "Excellent";

            }else if(s<1 && s >= 0.8)
            {
                pictureBox1.Image = Properties.Resources.good;
                label2.Text = "Good";
            }else if(s<0.6 && s >= 0.3)
            {
                pictureBox1.Image = Properties.Resources.soso;
                label2.Text = "Not bad";
            }else if(s<0.3)
            {
                pictureBox1.Image = Properties.Resources.bad;
                label2.Text = "Bad";
            }

        }



        private DataTable loadData()
        {
                 var database = new Database();
                //MessageBox.Show("連線成功", "成功");
                string sql = "SELECT * FROM ch1bp LIMIT 10";
                MySqlCommand cmd = new MySqlCommand(sql, database.conn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                //設定監控的命令
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                //把抓取到的資料填入DataTable
                da.Fill(dt);


            return dt;
      
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {

            DialogResult mesSelect = MessageBox.Show("確定要離開?", "提醒", MessageBoxButtons.OKCancel);
           if (mesSelect == DialogResult.OK)
            {
                System.Environment.Exit(0);
            }

        }
    }
}
