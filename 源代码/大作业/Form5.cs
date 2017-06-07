using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using System.Linq;

namespace 大作业
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = "Data Source=(local);Initial Catalog=超市会员卡;Integrated Security=sspi";
       
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string SQL = "Select 用户积分 from 用户信息 where 用户编号 ='" + textBox1.Text + "'"; //查询字符串，根据实际设置
            SqlDataAdapter adapter = new SqlDataAdapter(SQL, conn);   //建立数据适配器
            DataSet testDataSet = new DataSet();   //建立数据仓库，用于存储数据.
            // 以下执行查询，并将数据导入DataSet.，在里面生成一个result_data的表
            adapter.Fill(testDataSet, "result_data");
           
            dataGridView1.DataSource = testDataSet.Tables[0]; //把数据给表格

            SqlCommand cmd = new SqlCommand(SQL, conn);
            SqlDataReader rd = cmd.ExecuteReader();     //把相应信息填充到text里面去
            while (rd.Read())
            {
                textBox3.Text = rd["用户积分"].ToString().Trim();
            

               
            }

            rd.Close();
            conn.Close();
            if(textBox3.Text=="")
            {
                MessageBox.Show("不存在此会员，请核对编码");
                
            }
            else if (textBox2.Text != "" && textBox3.Text != "")
            {
                double jifen_ = Convert.ToDouble(textBox3.Text);
                double zhekou;
                if (jifen_ > 1000) zhekou = 0.9;
                else zhekou = 1.0;
                double money = Convert.ToDouble(textBox2.Text);
                double nowmoney = zhekou * money;
                textBox4.Text = Convert.ToString(nowmoney);

            }
            else
            {
                MessageBox.Show("请将有关信息填写完整");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if( textBox1.Text != ""&& textBox2.Text!= "")
            { 



            double m = Convert.ToDouble(textBox3.Text) + Convert.ToDouble(textBox2.Text);

            textBox5.Text = Convert.ToString(m);
            string connStr = "Data Source=(local);Initial Catalog=超市会员卡;Integrated Security=sspi";
        
           
            SqlConnection conn = new SqlConnection(connStr);
          
            conn.Open();


            string SQL = "  update 用户信息 set 用户积分 ='" + textBox5.Text + "'  where 用户编号 ='" + textBox1.Text + "'";

            SqlCommand command = new SqlCommand(SQL, conn);
           
              command.ExecuteNonQuery();  // 执行命令
             
              textBox1.Text = "";
              textBox2.Text = "";
              textBox3.Text = "";
              textBox4.Text = "";
              textBox5.Text = "";
            }
            else  MessageBox.Show("请将有关信息填写完整");
             
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connStr = "Data Source=(local);Initial Catalog=超市会员卡;Integrated Security=sspi";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string SQL = "Select 用户积分 from 用户信息 where 用户编号 ='" + textBox1.Text + "'"; //查询字符串，根据实际设置
            SqlDataAdapter adapter = new SqlDataAdapter(SQL, conn);   //建立数据适配器
            DataSet testDataSet = new DataSet();   //建立数据仓库，用于存储数据.
            // 以下执行查询，并将数据导入DataSet.，在里面生成一个result_data的表
            adapter.Fill(testDataSet, "result_data");

            dataGridView1.DataSource = testDataSet.Tables[0]; //把数据给表格

            SqlCommand cmd = new SqlCommand(SQL, conn);
            SqlDataReader rd = cmd.ExecuteReader();     //把相应信息填充到text里面去
            while (rd.Read())
            {
                textBox3.Text = rd["用户积分"].ToString().Trim();



            }

            rd.Close();
            conn.Close();
            if (textBox3.Text == "")
            {
                MessageBox.Show("不存在此会员，请核对编号");

            }
            else if (textBox2.Text != "" && textBox3.Text != "")
            {
                double jifen_ = Convert.ToDouble(textBox3.Text);
                double zhekou;
                if (jifen_ > 1000) zhekou = 0.9;
                else zhekou = 1.0;
                double money = Convert.ToDouble(textBox2.Text);
                double nowmoney = zhekou * money;
                textBox4.Text = Convert.ToString(nowmoney);

            }
            else
            {
                MessageBox.Show("请将有关信息填写完整");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();  
        }
    }
}
