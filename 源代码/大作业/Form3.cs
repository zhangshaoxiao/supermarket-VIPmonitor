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

namespace 大作业
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            string connStr = "Data Source=(local);Initial Catalog=超市会员卡;Integrated Security=sspi";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string SQL = "Select 用户姓名,用户手机号,用户积分 from 用户信息 where 用户编号 ='" + textBox1.Text + "'"; //查询字符串，根据实际设置
            SqlDataAdapter adapter = new SqlDataAdapter(SQL, conn);   //建立数据适配器
            DataSet testDataSet = new DataSet();   //建立数据仓库，用于存储数据.
            // 以下执行查询，并将数据导入DataSet.，在里面生成一个result_data的表
            adapter.Fill(testDataSet, "result_data");
            
            dataGridView1.DataSource = testDataSet.Tables[0]; //把数据给表格

            SqlCommand cmd = new SqlCommand(SQL, conn);
            SqlDataReader rd = cmd.ExecuteReader();     //把相应信息填充到text里面去
            while (rd.Read())
            {
                textBox2.Text = rd["用户积分"].ToString().Trim();
                textBox3.Text = rd["用户手机号"].ToString().Trim();
                textBox4.Text = rd["用户姓名"].ToString().Trim();
            }

            rd.Close();
            conn.Close();

            if (textBox3.Text == "")
            {
                MessageBox.Show("不存在的会员，请核对编号");

            }
        }

 
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
