using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using DGVPrinterHelper;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using iTextSharp.text.html.simpleparser;
using System.Drawing;
using System.Xml;
using System.Text;
using System.Xml.Serialization;

namespace Customer_Detail_Project
{
    public partial class Form1 : Form
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        DataGridViewRow selectedRow = new DataGridViewRow();
        DataTable table = new DataTable("tbl");

        public object SubTitleFormatFlags { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label6.Visible = false;
            GetStudentRecord();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void GetStudentRecord()
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from CustomerData ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView2.DataSource = dt;
            da.Fill(table);           
        }

        private void dataGridView1_CellClickEvent(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = this.dataGridView1.Rows[e.RowIndex];
                selectedRow = row;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM dbo.[CustomerData] WHERE [Id] = @Id", con);
            cmd.Parameters.AddWithValue("@Id", selectedRow.Cells[0].Value);
            cmd.ExecuteNonQuery();
            con.Close();
            GetStudentRecord();
            MessageBox.Show("successfull Deleted");

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into CustomerData values (@Name,@Address,@Pincode,@Contact_No)", con);

            cmd.Parameters.AddWithValue("@Name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@Address", textBoxAddress.Text);
            cmd.Parameters.AddWithValue("@Pincode", (textBoxPincode.Text));
            cmd.Parameters.AddWithValue("@Contact_No", (textBoxContact.Text));

            cmd.ExecuteNonQuery();
            con.Close();
            GetStudentRecord();
            textBoxName.Clear();
            textBoxAddress.Clear();
            textBoxPincode.Clear();
            textBoxContact.Clear();
            MessageBox.Show("successfull Added");

        }

        private void Editbutton_Click(object sender, EventArgs e)
        {
            AddButton.Enabled = false;
            EditButton.Enabled = false;
            DeleteButton.Enabled = false;

            var selectedRowData = selectedRow;
            label6.Text = selectedRowData.Cells[0].Value.ToString();
            textBoxName.Text = selectedRowData.Cells[1].Value.ToString();
            textBoxAddress.Text = selectedRowData.Cells[2].Value.ToString();
            textBoxPincode.Text = selectedRowData.Cells[3].Value.ToString();
            textBoxContact.Text = selectedRowData.Cells[4].Value.ToString();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE dbo.[CustomerData] set [Name] = @Name, [Address] = @Address, [Contact_No]= @Contact_No, [Pincode]= @Pincode  WHERE Id = @Id ", con);

            cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(label6.Text));
            cmd.Parameters.AddWithValue("@Name", textBoxName.Text);
            cmd.Parameters.AddWithValue("@Address", textBoxAddress.Text);
            cmd.Parameters.AddWithValue("@Pincode", textBoxPincode.Text);
            cmd.Parameters.AddWithValue("@Contact_No", textBoxContact.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            GetStudentRecord();
            AddButton.Enabled = true;
            EditButton.Enabled = true;
            DeleteButton.Enabled = true;

            textBoxName.Clear();
            textBoxAddress.Clear();
            textBoxPincode.Clear();
            textBoxContact.Clear();

            MessageBox.Show("Successfull Updated !!");
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            DataTable import = new DataTable();
            import.ReadXml(@"C:\Users\SHANKAR\Desktop\c#\Customer Detail Project\Customer Detail Project\XMLFile1.xml");
            dataGridView2.DataSource = import;
            MessageBox.Show("Data Imported");
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {

            table.WriteXml(@"C:\Users\SHANKAR\Desktop\c#\Customer Detail Project\Customer Detail Project\XMLFile1.xml", XmlWriteMode.WriteSchema);
            MessageBox.Show("Data Exported");
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Customer report";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PropotionalColumn = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "CADMASCRO";
            printer.FooterSpacing = 15;
            printer.PrintDataGridView(dataGridView2);
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPincode_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxContact_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from CustomerData where Name like('" + textBoxSearch.Text + "%')", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();

        }
    }

    internal class GridViewRow
    {
    }
}
