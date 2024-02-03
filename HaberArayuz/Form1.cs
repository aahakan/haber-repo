using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace HaberArayuz
{

    public partial class Form1 : Form
    {
        static string connectionString = "Server=172.21.54.148;Port=3306;Database=neis_news;User=NYP23-11;Password=Uludag9512357.;";
        MySqlConnection connection = new MySqlConnection(connectionString);


        public Form1()
        {
            InitializeComponent();
            InitializeTimer();

        }

        private void InitializeTimer()
        {
            // Timer ayarlar�
            timer1.Interval = 20000; // 20 saniyede bir g�ncelle
            timer1.Tick += new EventHandler(timer1_Tick); // Olay i�leyicisi
            timer1.Start(); // Timer'� ba�lat
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadData(); // Veriyi y�klemek i�in metod
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM news";
                MySqlDataAdapter da = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // DataGridView'i temizle
                haberView.DataSource = null;

                // Yeni verileri y�kle
                haberView.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata olu�tu: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


        private void haberView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void haberCek_Click(object sender, EventArgs e)
        {
            if (haberView.CurrentRow != null)
            {
                var haberId = haberView.CurrentRow.Cells["ID"].Value.ToString(); // 'ID' s�tunu varsay�m�
                var haberBasligi = haberView.CurrentRow.Cells["news_eng"].Value.ToString();
                var haberIcerigi = haberView.CurrentRow.Cells["news_turk"].Value.ToString();

                haberGoster gosterForm = new haberGoster(haberBasligi, haberIcerigi, haberId); // ID'yi de parametre olarak ge�
                gosterForm.ShowDialog();
            }
        }
    }
}
