using System.Data;
using System.Data.SQLite;

namespace calismasaatleriuyg
{
    public partial class Form1 : Form
    {
        private DateTime startTime;   // Çalýþma baþlama zamaný
        private DateTime stopTime;    // Çalýþma bitiþ zamaný
        private TimeSpan totalTime;   // Toplam çalýþma süresi
        private string imageDirectory = @"C:\Users\Beyza\source\repos\calismasaatleriuyg\MotivationImages\"; // Resimlerin bulunduðu klasör
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateDatabaseTable();  // Veritabaný ve tabloyu oluþtur
            timer1.Interval = 1000; // 1 saniyede bir güncelle
            timer1.Tick += Timer1_Tick;

            ShowRandomImage(); // Rastgele bir fotoðraf göster
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
                    int index = random.Next(imageFiles.Length); // Rastgele bir indeks seç
                    string selectedImagePath = imageFiles[index]; // Seçilen resmin yolunu al

                    // Resmi PictureBox'a yükle
                    pictureBoxMotivation.Image = Image.FromFile(selectedImagePath);

                    // Resmin sýðmasý için zoom yapýyoruz
                    pictureBoxMotivation.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show("Motivasyon resimleri bulunamadý.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Resim yüklenirken hata oluþtu: " + ex.Message);
            }
        }
        // Veritabaný oluþturulmasý
        private void CreateDatabaseTable()
        {
            string dbPath = "Data Source=work_hours.db;Version=3;"; // Veritabaný baðlantý yolu

            using (SQLiteConnection connection = new SQLiteConnection(dbPath))
            {
                try
                {
                    connection.Open();  // Veritabanýna baðlan

                    // Tabloyu silme iþlemi (eðer varsa)
                    //string dropTableQuery = "DROP TABLE IF EXISTS WorkRecords";
                    //using (SQLiteCommand cmd = new SQLiteCommand(dropTableQuery, connection))
                    //{
                    // cmd.ExecuteNonQuery();  // Tabloyu sil
                    // }

                    // Yeni tablo oluþturma sorgusu
                    string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS WorkRecords (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Baslangic TEXT NOT NULL,
                    Bitis TEXT NOT NULL,
                    ToplamSure TEXT NOT NULL
                );";

                    using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, connection))
                    {
                        cmd.ExecuteNonQuery();  // Tabloyu oluþtur
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        // Çalýþma saati kaydetme iþlemi
        private void SaveWorkTime(string startTime, string endTime, string duration)
        {
            try
            {
                string dbPath = "Data Source=work_hours.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(dbPath))
                {
                    connection.Open();  // Veritabanýna baðlan

                    // Veritabanýna veri ekleme sorgusu
                    string insertQuery = "INSERT INTO WorkRecords (Baslangic, Bitis, ToplamSure) VALUES (@StartTime, @EndTime, @Duration)";
                    using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, connection))
                    {
                        // Parametreler doðru formatta ekleniyor
                        cmd.Parameters.AddWithValue("@StartTime", startTime);
                        cmd.Parameters.AddWithValue("@EndTime", endTime);
                        cmd.Parameters.AddWithValue("@Duration", duration);

                        cmd.ExecuteNonQuery();  // Sorguyu çalýþtýr
                    }
                }

                MessageBox.Show("Çalýþma saati baþarýyla kaydedildi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Kaydetme Hatasý: " + ex.Message);
            }
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                startTime = DateTime.Now;
                lblStatus.Text = "Çalýþma Baþladý";
                lblStatus.ForeColor = Color.Green;

                timer1.Start(); // Timer baþlat
            }
            catch (Exception ex)
            {
                MessageBox.Show("Baþlatma Hatasý: " + ex.Message);
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
                lblStatus.Text = "Çalýþma Durdu";
                lblStatus.ForeColor = Color.Red;

                timer1.Stop(); // Timer durdur

                SaveWorkTime(startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                             stopTime.ToString("yyyy-MM-dd HH:mm:ss"),
                             totalTime.ToString(@"hh\:mm\:ss"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Durdurma Hatasý: " + ex.Message);
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

                        // Günlere renk atamak için sözlük
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
                        MessageBox.Show("Veritabanýnda kayýt bulunamadý.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Görüntüleme Hatasý: " + ex.Message);
            }
        }
        

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Seçilen satýrdan baþlangýç tarihi verisini al
                    string selectedStartTime = dataGridView1.SelectedRows[0].Cells["Baslangic"].Value.ToString();

                    string dbPath = "Data Source=work_hours.db;Version=3;";
                    using (SQLiteConnection connection = new SQLiteConnection(dbPath))
                    {
                        connection.Open();

                        // Kayýt silme sorgusu
                        string deleteQuery = "DELETE FROM WorkRecords WHERE Baslangic = @StartTime";
                        using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@StartTime", selectedStartTime);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // DataGridView'i güncelle
                    MessageBox.Show("Kayýt baþarýyla silindi.");
                    btnShowRecords_Click(null, null);  // Kayýtlarý tekrar yükle
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kayýt Silme Hatasý: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Silmek için bir kayýt seçin.");
            }
        }
    }
}

