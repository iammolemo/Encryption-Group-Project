﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace Crypyography
{

    public partial class Register : Form
    {
        //private static string conString = @"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=
        //|DataDirectory|\CryptographyDB.mdf; Integrated Security = True"; 

       
        SqlConnection con;
        SqlCommand cmd;

        public Register()
        {
            InitializeComponent();     
        }

        private void Register_Load(object sender, EventArgs e)
        {

            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=
        |DataDirectory|\App_Data\CryptographyDB.mdf; Integrated Security = True";
            con = new SqlConnection(conString);
            
        }

        private void clearAll()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtUserName.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
        }
       
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        /*======================Methods================================*/


        




        public static string encryptPassword(string pass)
        {
            try
            {
                
                byte[] encData_byte = new byte[pass.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(pass);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

       

        public static string decryptPassword(string encodedData)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encodedData);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }



        public static bool validatePassword(string pass, string confirmPass)
        {
           
            if (pass != "" && confirmPass != "")
            {
                if (pass.ToString().Trim().ToLower() == confirmPass.ToString().Trim().ToLower())
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("The Entererd Password/key does not match!.. Please Check..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);  //showing the error message if password and confirm password doesn't match  
                }

            }else{
                MessageBox.Show("Please fill all the fields!..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);  //showing the error message if any fields is empty  
            }
            return false;

        }

        /*======================Methods================================*/


        private void btnRegister_Click(object sender, EventArgs e)
        {

            try
            {
                bool confirmPassword = validatePassword(txtPassword.Text.ToString(), txtConfirmPassword.Text.ToString());
                if (confirmPassword == true && (txtFirstName.Text != "" || txtLastName.Text != "" || txtUserName.Text != "" || txtEmail.Text != " "))
                {
                     if (con.State != ConnectionState.Open)
                        con.Open();
                    
                      string sql = @"INSERT INTO [user](firstName,lastName,userName,email,password) VALUES(@firstName,@lastName,@userName,@email,@password)";
         
                      string pass = encryptPassword(txtConfirmPassword.Text);
                      cmd = new SqlCommand(sql, con);

                      cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                      cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
                      cmd.Parameters.AddWithValue("@userName", txtUserName.Text);
                      cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                      cmd.Parameters.AddWithValue("@password", pass);

                      cmd.ExecuteNonQuery();
                      MessageBox.Show("Record inserted!", "New user inserted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      clearAll();
                  
             
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                else
                {
                    if(confirmPassword == true)
                        MessageBox.Show("Please fill all the fields!..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unsername already taken", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Clear();
                txtUserName.Focus();
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            
            
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {
            LogIn backToLogIN = new LogIn();
            backToLogIN.Show();
            this.Hide();
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
