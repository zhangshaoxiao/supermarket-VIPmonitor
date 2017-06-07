using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace 大作业
{
    public partial class Form6 : Form
    {
        string mima;
        string mibao;
        public Form6()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = "Data Source=(local);Initial Catalog=超市会员卡;Integrated Security=sspi";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string SQL = "Select 用户密码,密保问题,密保答案 from 用户信息 where 用户编号 ='" + textBox1.Text + "'"; //查询字符串，根据实际设置
            SqlCommand cmd = new SqlCommand(SQL, conn);
            SqlDataReader rd = cmd.ExecuteReader();     //把相应信息填充到text里面去
            textBox3.Text = "";
            while (rd.Read())
            {
                textBox3.Text = rd["密保问题"].ToString().Trim();
                mibao = rd["密保答案"].ToString().Trim();
                mima = rd["用户密码"].ToString().Trim();
                //   textBox10.Text = rd["age"].ToString().Trim();
            }

            if (textBox3.Text != "")
            {
                MessageBox.Show("此用户存在，可以删除");

            }
            else
            {
                MessageBox.Show("此用户不存在，无法删除");

            }
            rd.Close();

            conn.Close(); //关闭数据库连接


        }

        private void button2_Click(object sender, EventArgs e)
        {



            if (mima == textBox2.Text && mibao == textBox4.Text)
            {
                
                    string connStr = "Data Source=(local);Initial Catalog=超市会员卡;Integrated Security=sspi";

                    SqlConnection conn = new SqlConnection(connStr);
                    conn.Open();

                    string SQL = "delete    from 用户信息 where  用户编号= '" + textBox1.Text + "'";
                    SqlCommand command = new SqlCommand(SQL, conn);
                
                    command.ExecuteNonQuery();  // 执行命令
                MessageBox.Show("删除成功");
                this.Close();
                conn.Close();
            }
            else
            {
                MessageBox.Show("密码或者密保错误，无法删除");
                this.Close();
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connStr = "Data Source=(local);Initial Catalog=超市会员卡;Integrated Security=sspi";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string SQL = "delete    from 用户信息 where  where 用户编号 =  '" + textBox1.Text + "'";
            MessageBox.Show("删除成功");
            this.Close();
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {


            string strCnn = "server=(local);database=超市会员卡;integrated security=sspi";
            SqlConnection cnn = new SqlConnection(strCnn);
            cnn.Open();
            string sql = "delete from 用户信息 where 用户编号=text1";



            cnn.Close();

            MessageBox.Show("已删除");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

 