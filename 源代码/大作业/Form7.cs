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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = "Data Source=(local);Initial Catalog=超市会员卡;Integrated Security=sspi";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string SQL = "Select 密保问题 from 用户信息 where 用户编号 ='" + textBox1.Text + "'"; //查询字符串，根据实际设置
            SqlDataAdapter adapter = new SqlDataAdapter(SQL, conn);   //建立数据适配器
            DataSet testDataSet = new DataSet();   //建立数据仓库，用于存储数据.
            // 以下执行查询，并将数据导入DataSet.，在里面生成一个result_data的表
            adapter.Fill(testDataSet, "result_data");

            dataGridView1.DataSource = testDataSet.Tables[0]; //把数据给表格

            SqlCommand cmd = new SqlCommand(SQL, conn);
            SqlDataReader rd = cmd.ExecuteReader();     //把相应信息填充到text里面去
            while (rd.Read())
            {
                textBox2.Text = rd["密保问题"].ToString().Trim();
            }

            rd.Close();
            conn.Close();
            if (textBox2.Text == "")
            {
                MessageBox.Show("不存在此会员，请核对编码");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string connStr = "Data Source=(local);Initial Catalog=超市会员卡;Integrated Security=sspi";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string SQL = "Select 密保答案 from 用户信息 where 用户编号 ='" + textBox1.Text + "'"; //查询字符串，根据实际设置
            SqlDataAdapter adapter = new SqlDataAdapter(SQL, conn);   //建立数据适配器
            DataSet testDataSet = new DataSet();   //建立数据仓库，用于存储数据.
            // 以下执行查询，并将数据导入DataSet.，在里面生成一个result_data的表
            adapter.Fill(testDataSet, "result_data");
            SqlCommand cmd = new SqlCommand(SQL, conn);
            SqlDataReader rd = cmd.ExecuteReader();     //把相应信息填充到text里面去
            if(textBox4.Text=="")
                MessageBox.Show("请填写新密码，不能为空");
            while (rd.Read())
            {
                if (textBox3.Text == rd["密保答案"].ToString().Trim() && textBox4.Text != "")
                {
                    string _connStr = "Data Source=(local);Initial Catalog=超市会员卡;Integrated Security=sspi";


                    SqlConnection _conn = new SqlConnection(connStr);

                    _conn.Open();


                    string _SQL = "  update 用户信息 set 用户密码 ='" + textBox4.Text + "'  where 用户编号 ='" + textBox1.Text + "'";

                    SqlCommand command = new SqlCommand(_SQL, _conn);

                    command.ExecuteNonQuery();  // 执行命令
                    MessageBox.Show("修改成功，请牢记新密码");
                }

                if(textBox3.Text != rd["密保答案"].ToString().Trim()) MessageBox.Show("密保答案错误，无法修改");

            }
            rd.Close();
            conn.Close();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
