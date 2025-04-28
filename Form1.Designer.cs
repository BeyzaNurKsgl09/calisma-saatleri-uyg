namespace calismasaatleriuyg
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnStart = new Button();
            btnStop = new Button();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            btnShowRecords = new Button();
            lblTotalTime = new Label();
            lblStatus = new Label();
            btnDeleteRecord = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            pictureBoxMotivation = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMotivation).BeginInit();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.BackColor = SystemColors.Control;
            btnStart.Font = new Font("Segoe UI", 12F);
            btnStart.Location = new Point(14, 16);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(245, 52);
            btnStart.TabIndex = 0;
            btnStart.Text = "Başlat";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.BackColor = SystemColors.Control;
            btnStop.Font = new Font("Segoe UI", 12F);
            btnStop.Location = new Point(297, 16);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(245, 52);
            btnStop.TabIndex = 1;
            btnStop.Text = "Durdur";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 253);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(581, 288);
            dataGridView1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.Location = new Point(14, 100);
            label1.Name = "label1";
            label1.Size = new Size(234, 28);
            label1.TabIndex = 3;
            label1.Text = "Toplam Çalışma Süresi :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.Location = new Point(14, 140);
            label2.Name = "label2";
            label2.Size = new Size(179, 28);
            label2.TabIndex = 4;
            label2.Text = "Çalışma Durumu :";
            // 
            // btnShowRecords
            // 
            btnShowRecords.Font = new Font("Segoe UI", 12F);
            btnShowRecords.Location = new Point(12, 189);
            btnShowRecords.Name = "btnShowRecords";
            btnShowRecords.Size = new Size(581, 44);
            btnShowRecords.TabIndex = 5;
            btnShowRecords.Text = "Kayıtları Göster";
            btnShowRecords.UseVisualStyleBackColor = true;
            btnShowRecords.Click += btnShowRecords_Click;
            // 
            // lblTotalTime
            // 
            lblTotalTime.AutoSize = true;
            lblTotalTime.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalTime.Location = new Point(259, 100);
            lblTotalTime.Name = "lblTotalTime";
            lblTotalTime.Size = new Size(0, 28);
            lblTotalTime.TabIndex = 6;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblStatus.Location = new Point(199, 140);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 28);
            lblStatus.TabIndex = 7;
            // 
            // btnDeleteRecord
            // 
            btnDeleteRecord.Font = new Font("Segoe UI", 12F);
            btnDeleteRecord.Location = new Point(12, 556);
            btnDeleteRecord.Name = "btnDeleteRecord";
            btnDeleteRecord.Size = new Size(581, 44);
            btnDeleteRecord.TabIndex = 8;
            btnDeleteRecord.Text = "Kayıt Sil";
            btnDeleteRecord.UseVisualStyleBackColor = true;
            btnDeleteRecord.Click += btnDeleteRecord_Click;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            // 
            // pictureBoxMotivation
            // 
            pictureBoxMotivation.Location = new Point(611, 16);
            pictureBoxMotivation.Name = "pictureBoxMotivation";
            pictureBoxMotivation.Size = new Size(572, 573);
            pictureBoxMotivation.TabIndex = 9;
            pictureBoxMotivation.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            ClientSize = new Size(1205, 613);
            Controls.Add(pictureBoxMotivation);
            Controls.Add(btnDeleteRecord);
            Controls.Add(lblStatus);
            Controls.Add(lblTotalTime);
            Controls.Add(btnShowRecords);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "Çalışma Saatleri Uygulaması";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMotivation).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStart;
        private Button btnStop;
        private DataGridView dataGridView1;
        private Label label1;
        private Label label2;
        private Button btnShowRecords;
        private Label lblTotalTime;
        private Label lblStatus;
        private Button btnDeleteRecord;
        private System.Windows.Forms.Timer timer1;
        private PictureBox pictureBoxMotivation;
    }
}
