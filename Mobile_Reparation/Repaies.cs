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
    public partial class Repaies : Form
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

        public Repaies()
        {
            InitializeComponent();

            Con = new Functions();
            ShowRpaies();
            GetCustomer();
            GetSpares();


            this.Region = Region.FromHrgn(CreateRoundRect(0, 0, this.Width, this.Height, 30, 30));
        }

        private void GetCost()
        {
            string Query = "select * from SpareTbl where SpCode = {0}";
            Query = string.Format(Query, RepSpareTb.SelectedValue.ToString());
            foreach(DataRow dr in Con.GetData(Query).Rows)
            {
                SpareCostTb.Text = dr["SpCost"].ToString();
            }
        }

        private void GetCustomer()
        {
            string Query = "Select * from CustumerTbl";
            RepCustomerTb.DisplayMember = Con.GetData(Query).Columns["CustName"].ToString();
            RepCustomerTb.ValueMember = Con.GetData(Query).Columns["CustCode"].ToString();
            RepCustomerTb.DataSource = Con.GetData(Query);
        }

        private void GetSpares()
        {
            string Query = "Select * from SpareTbl";
            RepSpareTb.DisplayMember = Con.GetData(Query).Columns["SpName"].ToString();
            RepSpareTb.ValueMember = Con.GetData(Query).Columns["SpCode"].ToString();
            RepSpareTb.DataSource = Con.GetData(Query);
        }

        private void ShowRpaies()
        {
            string Query = "select * from RepairTbl";
            RepaiesList.DataSource = Con.GetData(Query);
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Repaies_Load(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (RepCustomerTb.SelectedIndex == -1 || RepPhoneTb.Text == "" || RepDeviceNameTb.Text == "" || RepModelTb.Text == "" || RepProblemTb.Text == "" || RepSpareTb.SelectedIndex == -1 || SpareCostTb.Text == "" || TotalCostTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                try
                {
                    string RDate = RepDateTb.Value.Date.ToString();
                    string myString = DateTime.Parse(RDate).ToString("yyyy-MM-dd");
                    int Customer = Convert.ToInt32(RepCustomerTb.SelectedValue.ToString());
                    string CPhone = RepPhoneTb.Text;
                    string CDName = RepDeviceNameTb.Text;
                    string CModel = RepModelTb.Text;
                    string CProblem = RepProblemTb.Text;
                    int Spare = Convert.ToInt32(RepSpareTb.SelectedValue.ToString());
                    int Total = Convert.ToInt32(TotalCostTb.Text);
                    int GrdTotal = Convert.ToInt32(SpareCostTb.Text) + Total;
                    string Query = "insert into RepairTbl Values('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7})";
                    Query = string.Format(Query, myString, Customer, CPhone, CDName, CModel, CProblem, Spare, GrdTotal);
                    Con.SetData(Query);
                    MessageBox.Show("Repair Added !!!");
                    ShowRpaies();
                    //Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void RepSpareTb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCost();
        }

        private void RepCustomerTb_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void RepSpareTb_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (key==0)
            {
                MessageBox.Show("Select Repair!!!");
            }
            else
            {
                try
                {
                    string Query = "delete from RepairTbl where RepCode = {0}";
                    Query = string.Format(Query, key);
                    Con.SetData(Query);
                    MessageBox.Show("Repair deleted !!!");
                    ShowRpaies();
                    //Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;
        private void RepaiesList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            key = Convert.ToInt32(RepaiesList.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            login Obj = new login();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Spares Obj = new Spares();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
