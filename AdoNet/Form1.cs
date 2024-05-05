using System.Data.SqlClient;

namespace AdoNet
{
    public partial class Form1 : Form
    {
        string connectionString = "Server=DESKTOP-1SN2ERQ\\SQLEXPRESS;Database=Spotify;Trusted_Connection=True;";
        private object nameinput;

        public Form1()
        {
            
            InitializeComponent();

            dataGridView1.Columns.Add("Id", "Id");
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Surname", "Surname");
            dataGridView1.Columns.Add("Gender", "Gender");
            dataGridView1.Columns.Add("Username", "Uasername");
            dataGridView1.Columns.Add("Password", "Password");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String name = nameinput.text;
            String surname = surnameinput.Text;
            String gender = radioButton1.Checked ? "gadin" : "kishi";
            String username = usernameinput.Text;
            String password = passwordinput.Text;
            InsertData(name, surname, radioButton1.Checked ? "kishi" : "gadin", username, password);
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void GetDatas()
        {
            ResetTable();
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlCommand command = new SqlCommand("select * from Users", conn);
            using SqlDataReader datas = command.ExecuteReader();




            while (datas.Read())
            {
                dataGridView1.Rows.Add(datas[0], datas[1], datas[2], datas[3], datas[4], datas[5]);
            }

        }
        bool InsertData(string name, string surname, string gender, string username, string password)
        {
            if (name.Length < 3 || surname.Length < 3 || gender.Length < 3 || username.Length < 3 || password.Length < 6)
            {
                MessageBox.Show("error");
                return false;
            }
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlCommand command = new SqlCommand($"Insert Into Users Values (N'{name}',N'{surname}',N'{gender}',N'{username}',N'{password}')", conn);
            int count = command.ExecuteNonQuery();
            if (count > 0) { return true; }
            return false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var data = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
            string id = data.Cells[0].Value.ToString();
            Delete(id);
            GetDatas();
        }
        void Delete(string id)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlCommand command = new SqlCommand($"delete users WHERE Id = {id}", conn);
            var result = command.ExecuteNonQuery();
            if (result > 0)
            {
                MessageBox.Show("deleted");
            }
            else
            {
                MessageBox.Show(" error");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var data = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
            string id = data.Cells[0].Value.ToString();
            string name = nameinput.Text;
            string surname = surnameinput.Text;
            string gender = radioButton1.Checked ? "gadin" : "kishi";
            string username = usernameinput.Text;
            string password = passwordinput.Text;
            UpdateData(id, name, surname, gender, username, password);
           
        }
        bool UpdateData(string id, string name, string surname, string gender, string username, string password)
        {
            if (name.Length < 3 || surname.Length < 3 || username.Length < 3 || password.Length < 6)
            {
                MessageBox.Show("There is an error in the entered values.");
                return false;
            }
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlCommand command = new SqlCommand($"UPDATE Users SET Name = N'{name}', Surname = N'{surname}', Gender = N'{gender}', Username = N'{username}', Password = N'{password}' WHERE Id = {id}", conn);

            int count = command.ExecuteNonQuery();
            if (count > 0)
            {
                MessageBox.Show("Data updated successfully");
                return true;
            }
            else
            {
                MessageBox.Show("Data could not be updated");
                return false;
            }
        }
    }
}
}
