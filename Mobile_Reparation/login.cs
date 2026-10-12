using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Mobile_Reparation
{
    public partial class login : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRect
            (
                int nLeft,
                int nTop,
                int nRight,
                int nBottom,
                int nWidthEllipse,
                int nHeightEllipse
            );

        public login()
        {
            InitializeComponent();
            PasswordTb.PasswordChar = '*';

            this.Region = Region.FromHrgn(CreateRoundRect(0, 0, this.Width, this.Height, 30, 30));
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Data");
            }else if(UNameTb.Text=="Admin" && PasswordTb.Text == "Admin")
            {
                Repaies Obj = new Repaies();
                Obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong UserName or Password");
            }
        }

        private void PasswordTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
