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


    
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == ""
                    || textBox5.Text.Trim() == "" || textBox6.Text.Trim() == "")
                {
                    MessageBox.Show("请将用户信息填写完整");
                    return;
                }
                if (textBox3.Text.Trim() != textBox6.Text.Trim())
                {
                    MessageBox.Show("两次输入密码不一致请重新输入");
                    textBox3.Text = "";
                    textBox3.Focus();
                    return;
                }


                string strCnn = "server=(local);database=超市会员卡;integrated security=sspi";
                SqlConnection cnn = new SqlConnection(strCnn);
                cnn.Open();
  
               
                string sql = "insert into 用户信息 values(@用户编号,@用户姓名,@用户密码,@用户手机号,@用户积分,@密保问题,@密保答案)";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                SqlParameter 用户编号 = new SqlParameter("@用户编号", SqlDbType.VarChar);
                用户编号.Value = textBox1.Text;
                cmd.Parameters.Add(用户编号);
                SqlParameter 用户姓名 = new SqlParameter("@用户姓名", SqlDbType.VarChar);
                用户姓名.Value = textBox2.Text;
                cmd.Parameters.Add(用户姓名);
                SqlParameter 用户密码 = new SqlParameter("@用户密码", SqlDbType.VarChar);
                用户密码.Value = textBox3.Text;
                cmd.Parameters.Add(用户密码);
                SqlParameter 用户手机号 = new SqlParameter("@用户手机号", SqlDbType.VarChar);
                用户手机号.Value = textBox4.Text;
                cmd.Parameters.Add(用户手机号);
                SqlParameter 用户积分 = new SqlParameter("@用户积分", SqlDbType.VarChar);
                用户积分.Value =textBox5.Text;
                cmd.Parameters.Add(用户积分);
                SqlParameter 密保问题 = new SqlParameter("@密保问题", SqlDbType.VarChar);
                密保问题.Value = textBox7.Text;
                cmd.Parameters.Add(密保问题);
                SqlParameter 密保答案 = new SqlParameter("@密保答案", SqlDbType.VarChar);
                密保答案.Value = textBox8.Text;
                cmd.Parameters.Add(密保答案);
                cmd.ExecuteNonQuery();

                MessageBox.Show("注册成功");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();  
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
