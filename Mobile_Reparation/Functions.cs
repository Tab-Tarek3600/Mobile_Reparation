using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Mobile_Reparation
{
    class Functions
    {
        private SqlConnection Con;
        private SqlCommand Cmd;
        private DataTable dt;
        private SqlDataAdapter sda;
        private string ConStr;

        public Functions() //method that is called when an object of this class is created it do the connection to database
        {
            ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\MobileREpaireDb.mdf;Integrated Security=True;Connect Timeout=30";
            // <== the location of the database file, the authentication mode, and the connection timeout
            Con = new SqlConnection(ConStr);
            Cmd = new SqlCommand();
            Cmd.Connection = Con; // This line sets the Connection property of the Cmd object to the Con object, which means that the command will use this connection to execute on the database
        }

        public DataTable GetData(String Query) // afficher les information(getters)
        {
            dt = new DataTable();
            sda = new SqlDataAdapter(Query, ConStr);
            sda.Fill(dt);
            return dt;
        }

        public int SetData(string Query) // ajouter les information(setters)
        {
            int Cnt = 0;
            if (Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
            Cmd.CommandText = Query;
            Cnt = Cmd.ExecuteNonQuery();
            return Cnt;
        }
    }
}
