using System.Data;
using System.Data.SQLite;

namespace calismasaatleriuyg
{
    public partial class Form1 : Form
    {
        private DateTime startTime;   // �al��ma ba�lama zaman�
        private DateTime stopTime;    // �al��ma biti� zaman�
        private TimeSpan totalTime;   // Toplam �al��ma s�resi
        private string imageDirectory = @"C:\Users\Beyza\source\repos\calismasaatleriuyg\MotivationImages\"; // Resimlerin bulundu�u klas�r
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateDatabaseTable();  // Veritaban� ve tabloyu olu�tur
            timer1.Interval = 1000; // 1 saniyede bir g�ncelle
            timer1.Tick += Timer1_Tick;

            ShowRandomImage(); // Rastgele bir foto�raf g�ster
        }
        private void ShowRandomImage()
        {
            try
            {
                string[] imageFiles = Directory.GetFiles(imageDirectory, "*.jpg"); // .jpg resimleri al
                imageFiles = imageFiles.Concat(Directory.GetFiles(imageDirectory, "*.png")).ToArray(); // .png resimleri de ekle

                if (imageFiles.Length > 0)
                {
                    Random random = new Random();
                    int index = random.Next(imageFiles.Length); // Rastgele bir indeks se�
                    string selectedImagePath = imageFiles[index]; // Se�ilen resmin yolunu al

                    // Resmi PictureBox'a y�kle
                    pictureBoxMotivation.Image = Image.FromFile(selectedImagePath);

                    // Resmin s��mas� i�in zoom yap�yoruz
                    pictureBoxMotivation.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show("Motivasyon resimleri bulunamad�.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Resim y�klenirken hata olu�tu: " + ex.Message);
            }
        }
        // Veritaban� olu�turulmas�
        private void CreateDatabaseTable()
        {
            string dbPath = "Data Source=work_hours.db;Version=3;"; // Veritaban� ba�lant� yolu

            using (SQLiteConnection connection = new SQLiteConnection(dbPath))
            {
                try
                {
                    connection.Open();  // Veritaban�na ba�lan

                    // Tabloyu silme i�lemi (e�er varsa)
                    //string dropTableQuery = "DROP TABLE IF EXISTS WorkRecords";
                    //using (SQLiteCommand cmd = new SQLiteCommand(dropTableQuery, connection))
                    //{
                    // cmd.ExecuteNonQuery();  // Tabloyu sil
                    // }

                    // Yeni tablo olu�turma sorgusu
                    string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS WorkRecords (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Baslangic TEXT NOT NULL,
                    Bitis TEXT NOT NULL,
                    ToplamSure TEXT NOT NULL
                );";

                    using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, connection))
                    {
                        cmd.ExecuteNonQuery();  // Tabloyu olu�tur
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        // �al��ma saati kaydetme i�lemi
        private void SaveWorkTime(string startTime, string endTime, string duration)
        {
            try
            {
                string dbPath = "Data Source=work_hours.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(dbPath))
                {
                    connection.Open();  // Veritaban�na ba�lan

                    // Veritaban�na veri ekleme sorgusu
                    string insertQuery = "INSERT INTO WorkRecords (Baslangic, Bitis, ToplamSure) VALUES (@StartTime, @EndTime, @Duration)";
                    using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, connection))
                    {
                        // Parametreler do�ru formatta ekleniyor
                        cmd.Parameters.AddWithValue("@StartTime", startTime);
                        cmd.Parameters.AddWithValue("@EndTime", endTime);
                        cmd.Parameters.AddWithValue("@Duration", duration);

                        cmd.ExecuteNonQuery();  // Sorguyu �al��t�r
                    }
                }

                MessageBox.Show("�al��ma saati ba�ar�yla kaydedildi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Kaydetme Hatas�: " + ex.Message);
            }
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                startTime = DateTime.Now;
                lblStatus.Text = "�al��ma Ba�lad�";
                lblStatus.ForeColor = Color.Green;

                timer1.Start(); // Timer ba�lat
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ba�latma Hatas�: " + ex.Message);
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;
            lblTotalTime.Text = elapsedTime.ToString(@"hh\:mm\:ss");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                stopTime = DateTime.Now;
                totalTime = stopTime - startTime;

                lblTotalTime.Text = totalTime.ToString(@"hh\:mm\:ss");
                lblStatus.Text = "�al��ma Durdu";
                lblStatus.ForeColor = Color.Red;

                timer1.Stop(); // Timer durdur

                SaveWorkTime(startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                             stopTime.ToString("yyyy-MM-dd HH:mm:ss"),
                             totalTime.ToString(@"hh\:mm\:ss"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Durdurma Hatas�: " + ex.Message);
            }
        }

        private void btnShowRecords_Click(object sender, EventArgs e)
        {

            try
            {
                string dbPath = "Data Source=work_hours.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(dbPath))
                {
                    connection.Open();

                    string selectQuery = "SELECT Baslangic, Bitis, ToplamSure FROM WorkRecords";
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectQuery, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dataTable;

                        // Renk listesi
                        Color[] renkler = { Color.LightPink, Color.LightBlue, Color.LightYellow, Color.LightGreen, Color.Orange, Color.MediumPurple, Color.LightGray };

                        // G�nlere renk atamak i�in s�zl�k
                        Dictionary<string, Color> gunRenkleri = new Dictionary<string, Color>();

                        int renkIndex = 0;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["Baslangic"].Value != null)
                            {
                                string tarih = DateTime.Parse(row.Cells["Baslangic"].Value.ToString()).ToShortDateString();

                                if (!gunRenkleri.ContainsKey(tarih))
                                {
                                    gunRenkleri[tarih] = renkler[renkIndex % renkler.Length];
                                    renkIndex++;
                                }

                                row.DefaultCellStyle.BackColor = gunRenkleri[tarih];
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Veritaban�nda kay�t bulunamad�.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri G�r�nt�leme Hatas�: " + ex.Message);
            }
        }
        

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Se�ilen sat�rdan ba�lang�� tarihi verisini al
                    string selectedStartTime = dataGridView1.SelectedRows[0].Cells["Baslangic"].Value.ToString();

                    string dbPath = "Data Source=work_hours.db;Version=3;";
                    using (SQLiteConnection connection = new SQLiteConnection(dbPath))
                    {
                        connection.Open();

                        // Kay�t silme sorgusu
                        string deleteQuery = "DELETE FROM WorkRecords WHERE Baslangic = @StartTime";
                        using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@StartTime", selectedStartTime);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // DataGridView'i g�ncelle
                    MessageBox.Show("Kay�t ba�ar�yla silindi.");
                    btnShowRecords_Click(null, null);  // Kay�tlar� tekrar y�kle
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kay�t Silme Hatas�: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Silmek i�in bir kay�t se�in.");
            }
        }
    }
}

