using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Data.Common;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Security.Policy;

namespace kyrsova_212
{
    public partial class AptekaForm : Form
    {
        private SqlConnection sqlConnection = null;
        public string PriceMarketGloball;
        public string UnitMagazGloball;
        public int CashMagaz;
        public int Cash;
        public AptekaForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AptekaDB"].ConnectionString);

            sqlConnection.Open();

            if (sqlConnection.State == ConnectionState.Open)
            {
                //MessageBox.Show("Підключення встановлено");
            }
        }

        private void button_insert_Click(object sender, EventArgs e)
        {
            if (textBox_Name.Text != "" && textBox_Unit.Text != "" && textBox_Number.Text != "" && textBox_Price.Text != "")
            {
                if (comboBox_Class.Text == "Таблетки" | comboBox_Class.Text == "Сироп" | comboBox_Class.Text == "Ампула" | comboBox_Class.Text == "Каплі" | comboBox_Class.Text == "Крем" | comboBox_Class.Text == "Гель")
                {
                    SqlCommand command = new SqlCommand("INSERT INTO[Apteka] (Name, Unit, Number, Price, Class) Values(@Name, @Unit, @Number, @Price, @Class)", sqlConnection);
                    command.Parameters.AddWithValue("Name", textBox_Name.Text);
                    command.Parameters.AddWithValue("Unit", textBox_Unit.Text);
                    command.Parameters.AddWithValue("Number", textBox_Number.Text);
                    command.Parameters.AddWithValue("Price", textBox_Price.Text);
                    command.Parameters.AddWithValue("Class", comboBox_Class.Text);
                    command.ExecuteNonQuery().ToString();
                }
                else
                {
                    MessageBox.Show("Вибери з запропонованих варіантів");
                }
            }
            else
            {
                MessageBox.Show("Заповніть поля");
            }
        }


        private void button_Show_Click_1(object sender, EventArgs e)
        {
            if (comboBox_Show.Text == "Таблетки")
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * FROM Apteka WHERE Class = N'Таблетки'", sqlConnection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
            }
            else
            {
                if (comboBox_Show.Text == "Сироп")
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * FROM Apteka WHERE Class = N'Сироп'", sqlConnection);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    dataGridView1.DataSource = dataSet.Tables[0];
                }
                else 
                {
                    if (comboBox_Show.Text == "Ампула")
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * FROM Apteka WHERE Class = N'Ампула'", sqlConnection);
                        DataSet dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                        dataGridView1.DataSource = dataSet.Tables[0];
                    }
                    else
                    {
                        if (comboBox_Show.Text == "Каплі")
                        {
                            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * FROM Apteka WHERE Class = N'Каплі'", sqlConnection);
                            DataSet dataSet = new DataSet();
                            dataAdapter.Fill(dataSet);
                            dataGridView1.DataSource = dataSet.Tables[0];
                        }
                        else
                        {
                            if (comboBox_Show.Text == "Крем")
                            {
                                SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * FROM Apteka WHERE Class = N'Крем'", sqlConnection);
                                DataSet dataSet = new DataSet();
                                dataAdapter.Fill(dataSet);
                                dataGridView1.DataSource = dataSet.Tables[0];
                            }
                            else
                            {
                                if (comboBox_Show.Text == "Гель")
                                {
                                    SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * FROM Apteka WHERE Class = N'Гель'", sqlConnection);
                                    DataSet dataSet = new DataSet();
                                    dataAdapter.Fill(dataSet);
                                    dataGridView1.DataSource = dataSet.Tables[0];
                                }
                                else
                                {
                                    if (comboBox_Show.Text == "Касса")
                                    {
                                        SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * FROM CashRegister", sqlConnection);
                                        DataSet dataSet = new DataSet();
                                        dataAdapter.Fill(dataSet);
                                        dataGridView1.DataSource = dataSet.Tables[0];
                                    }
                                    else
                                    {
                                        SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * FROM Apteka", sqlConnection);
                                        DataSet dataSet = new DataSet();
                                        dataAdapter.Fill(dataSet);
                                        dataGridView1.DataSource = dataSet.Tables[0];
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button_change_Click(object sender, EventArgs e)
        {
            if (textBox_Num.Text != "")
            {

                string queryString = $"SELECT Name, Unit, Price, Class FROM Apteka WHERE number = {textBox_Num.Text}";

                DataTable dataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AptekaDB"].ConnectionString))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(queryString, connection))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                }

                if (dataTable.Rows.Count > 0)
                {
                    string name = dataTable.Rows[0]["Name"].ToString();
                    string unit = dataTable.Rows[0]["Unit"].ToString();
                    string price = dataTable.Rows[0]["Price"].ToString();
                    string clas = dataTable.Rows[0]["Class"].ToString();

                        label_Name.Text = name;
                        label_Unit.Text = unit;
                        label_Price.Text = price;
                        label_Class.Text = clas;
                        }
                    else
                    {
                        // Якщо результати запиту відсутні, виводимо повідомлення
                        label_Name.Text = "Немає назви";
                        label_Unit.Text = "-";
                        label_Price.Text = "-";
                        label_Class.Text = "-";
                    }

            }
            else
            {
                MessageBox.Show("Введіть номер препарату", "eror");
            }
            textBox_NameUpdate.Text = label_Name.Text;
            textBox_UnitUpdate.Text = label_Unit.Text;
            textBox_PriceUpdate.Text = label_Price.Text;
            comboBox_ClassUpdate.Text = label_Class.Text;

        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            string updateQuery = "UPDATE [Apteka] SET Name = @Name, Unit = @Unit, Price = @Price, Class = @Class WHERE Number = @Number";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AptekaDB"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", textBox_NameUpdate.Text);
                    command.Parameters.AddWithValue("@Unit", textBox_UnitUpdate.Text);
                    command.Parameters.AddWithValue("@Price", textBox_PriceUpdate.Text);
                    command.Parameters.AddWithValue("@Class", comboBox_ClassUpdate.Text);
                    command.Parameters.AddWithValue("@Number", textBox_Num.Text);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Дані оновлено.");
                    }
                    else
                    {}
                }
            }
        }

        private void button_PoiskMagaz_Click(object sender, EventArgs e)
        {
            string queryString = $"SELECT Name, Unit, Price FROM Apteka WHERE number = {textBox_NumMagaz.Text}";

            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AptekaDB"].ConnectionString))
            {
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(queryString, connection))
                {
                    dataAdapter.Fill(dataTable);
                }
            }

            if (dataTable.Rows.Count > 0)
            {
                string nameMagaz = dataTable.Rows[0]["Name"].ToString();
                string unitMagaz = dataTable.Rows[0]["Unit"].ToString();
                string priceMagaz = dataTable.Rows[0]["Price"].ToString();
                label_NameMagaz.Text = nameMagaz;
                label_PriceMagaz.Text = priceMagaz;
                label_UnitMagaz.Text = unitMagaz;
                UnitMagazGloball = unitMagaz;
                PriceMarketGloball = priceMagaz;
            }
        }

        private void button_SaleMagaz_Click(object sender, EventArgs e)
        {
            int UnitMagaz = Convert.ToInt32(label_UnitMagaz.Text);
            int KolMagaz = Convert.ToInt32(textBox_KolMagaz.Text);
            if (KolMagaz <= UnitMagaz)
            {
                int a = Convert.ToInt32(label_UnitMagaz.Text);
                int b = Convert.ToInt32(textBox_KolMagaz.Text);
                int v = a - b;
                label_UnitMagaz.Text = v.ToString();
            }
            else
            {
                MessageBox.Show("Кількість на складі меньше ніж покупки", "Eror!");
            }
            string updateQuery = "UPDATE [Apteka] SET Unit = @Unit WHERE Number = @Number";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AptekaDB"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Unit", label_UnitMagaz.Text);
                    command.Parameters.AddWithValue("@Number", textBox_NumMagaz.Text);
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
            string queryString = $"SELECT cash FROM CashRegister";

            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AptekaDB"].ConnectionString))
            {
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(queryString, connection))
                {
                    dataAdapter.Fill(dataTable);
                }
            }

            if (dataTable.Rows.Count > 0)
            {
                string cash = dataTable.Rows[0]["cash"].ToString();
                Cash = Convert.ToInt32(cash);
            }

            string updateQuery1 = "UPDATE CashRegister SET cash = @cash";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AptekaDB"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(updateQuery1, connection))
                {
                    int oi = Cash + CashMagaz;
                    command.Parameters.AddWithValue ("@cash", oi);
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }
        private void textBox_KolMagaz_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int a = Convert.ToInt32(label_PriceMagaz.Text);
                int b = Convert.ToInt32(textBox_KolMagaz.Text);
                int v = a * b;
                label_ZagalPrice.Text = v.ToString() + " Грн";
                CashMagaz = Convert.ToInt32(v);
    }
        }
    }
}
