using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mobile_Reparation
{
    public partial class Customers : Form
    {
        Functions Con;

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

        public Customers() // les fonction qui vont s'executer l'auret de l'ouverture d'application
        {
            InitializeComponent();

            Con = new Functions();
            ShowCustomers();

            this.Region = Region.FromHrgn(CreateRoundRect(0, 0, this.Width, this.Height, 30, 30));
        }

        private void ShowCustomers()
        {
            string Query = "select * from CustumerTbl";
            CustemersList.DataSource = Con.GetData(Query);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(CustNameTb.Text=="" || CustPhoneTb.Text=="" || CustAddTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                try
                {
                    string CName = CustNameTb.Text;
                    string CPhone = CustPhoneTb.Text;
                    string CAdd = CustAddTb.Text;
                    string Query = "insert into CustumerTbl Values('{0}','{1}','{2}')";
                    Query = string.Format(Query, CName, CPhone, CAdd);
                    Con.SetData(Query);
                    MessageBox.Show("Customer Added !!!");
                    ShowCustomers();
                    Clear();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;
        private void CustemersList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustNameTb.Text = CustemersList.SelectedRows[0].Cells[1].Value.ToString();
            CustPhoneTb.Text = CustemersList.SelectedRows[0].Cells[2].Value.ToString();
            CustAddTb.Text = CustemersList.SelectedRows[0].Cells[3].Value.ToString();
            if (CustNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CustemersList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustPhoneTb.Text == "" || CustAddTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                try
                {
                    string CName = CustNameTb.Text;
                    string CPhone = CustPhoneTb.Text;
                    string CAdd = CustAddTb.Text;
                    string Query = "Update CustumerTbl set CustName = '{0}',CustPhone = '{1}',CustAdd = '{2}' where CustCode = {3}";
                    Query = string.Format(Query, CName, CPhone, CAdd, key);
                    Con.SetData(Query);
                    MessageBox.Show("Customer Updated !!!");
                    ShowCustomers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void Clear()
        {
            CustNameTb.Text = "";
            CustPhoneTb.Text = "";
            CustAddTb.Text = "";
            key = 0;
        }
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key==0)
            {
                MessageBox.Show("Select a Customer!!!");
            }
            else
            {
                try
                {
                    string Query = "delete from CustumerTbl where CustCode = {0}";
                    Query = string.Format(Query,  key);
                    Con.SetData(Query);
                    MessageBox.Show("Customer Deleted !!!");
                    ShowCustomers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            login Obj = new login();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Repaies Obj = new Repaies();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Spares Obj = new Spares();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
