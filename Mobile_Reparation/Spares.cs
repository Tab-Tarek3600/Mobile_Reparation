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
    public partial class Spares : Form
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

        public Spares()
        {
            InitializeComponent();

            Con = new Functions();
            ShowSpares();

            this.Region = Region.FromHrgn(CreateRoundRect(0, 0, this.Width, this.Height, 30, 30));
        }

        private void ShowSpares()
        {
            string Query = "select * from SpareTbl";
            PartList.DataSource = Con.GetData(Query);
        }

        private void Clear()
        {
            PartNameTb.Text = "";
            PartCostTb.Text = "";
        }

        private void Spares_Load(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PartNameTb.Text == "" || PartCostTb.Text == "" )
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                try
                {
                    string PName = PartNameTb.Text;
                    int PCost = Convert.ToInt32(PartCostTb.Text) ;
                    string Query = "insert into SpareTbl Values('{0}',{1})";
                    Query = string.Format(Query, PName, PCost);
                    Con.SetData(Query);
                    MessageBox.Show("Spare Added !!!");
                    ShowSpares();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int key = 0;
        private void PartList_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            PartNameTb.Text = PartList.SelectedRows[0].Cells[1].Value.ToString();
            PartCostTb.Text = PartList.SelectedRows[0].Cells[2].Value.ToString();

            if (PartNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(PartList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (PartNameTb.Text == "" || PartCostTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                try
                {
                    string PName = PartNameTb.Text;
                    int PCost = Convert.ToInt32(PartCostTb.Text);
                    string Query = "Update SpareTbl set SpName = '{0}',SpCost = '{1}' where SpCode = {2}";
                    Query = string.Format(Query, PName, PCost, key);
                    Con.SetData(Query);
                    MessageBox.Show("Spare Updated !!!");
                    ShowSpares();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select a Spare!!!");
            }
            else
            {
                try
                {
                    string Query = "delete from SpareTbl where SpCode = {0}";
                    Query = string.Format(Query, key);
                    Con.SetData(Query);
                    MessageBox.Show("Spare Deleted !!!");
                    ShowSpares();
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
