namespace Match3Game
{
    partial class MainMenuForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnHighScores;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListBox lstHighScores;
        private System.Windows.Forms.Button btnBackFromScores;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnBackFromInfo;
        private System.Windows.Forms.Label lblName;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnHighScores = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.lstHighScores = new System.Windows.Forms.ListBox();
            this.btnBackFromScores = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnBackFromInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MainMenuForm
            // 
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Match-3 Game - Menu";

            // lblTitle
            this.lblTitle.Text = "2024-2025 Güz Dönemi NDP Proje Ödevi";
            this.lblTitle.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(50, 20);

            // lblName
            this.lblName.Text = "Oyuncu Adı:";
            this.lblName.Font = new System.Drawing.Font("Arial", 12);
            this.lblName.Location = new System.Drawing.Point(50, 70);
            // MainMenuForm.Designer.cs
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.txtPlayerName.Visible = true;
            this.lblName.AutoSize = true;

            // txtPlayerName
            this.txtPlayerName.Location = new System.Drawing.Point(160, 68);
            this.txtPlayerName.Width = 200;
            this.txtPlayerName.Font = new System.Drawing.Font("Arial", 12);

            // btnStart
            this.btnStart.Text = "Oyuna Başla (Enter)";
            this.btnStart.Font = new System.Drawing.Font("Arial", 12);
            this.btnStart.Location = new System.Drawing.Point(50, 120);
            this.btnStart.Size = new System.Drawing.Size(200, 40);

            // btnHighScores
            this.btnHighScores.Text = "En İyi Skorlar";
            this.btnHighScores.Font = new System.Drawing.Font("Arial", 12);
            this.btnHighScores.Location = new System.Drawing.Point(50, 180);
            this.btnHighScores.Size = new System.Drawing.Size(200, 40);

            // btnInfo
            this.btnInfo.Text = "Oyun Bilgisi";
            this.btnInfo.Font = new System.Drawing.Font("Arial", 12);
            this.btnInfo.Location = new System.Drawing.Point(50, 240);
            this.btnInfo.Size = new System.Drawing.Size(200, 40);

            // lstHighScores
            this.lstHighScores.Font = new System.Drawing.Font("Arial", 12);
            this.lstHighScores.Location = new System.Drawing.Point(50, 60);
            this.lstHighScores.Size = new System.Drawing.Size(300, 200);
            this.lstHighScores.Visible = false;

            // btnBackFromScores
            this.btnBackFromScores.Text = "Geri";
            this.btnBackFromScores.Font = new System.Drawing.Font("Arial", 12);
            this.btnBackFromScores.Location = new System.Drawing.Point(50, 270);
            this.btnBackFromScores.Size = new System.Drawing.Size(100, 40);
            this.btnBackFromScores.Visible = false;

            // lblInfo
            this.lblInfo.Text = "Oyun Bilgisi:\n" +
                "- Aynı renkten en az 3 taşı yan yana veya alt alta getir.\n" +
                "- Taşları fare ile tıkla, ardından değiştirmek istediğin taşı seç.\n" +
                "- Klavye yön tuşları ile haritada gez, Enter ile taşı seç, ikinci Enter ile değiştir.\n" +
                "- P tuşu ile oyunu duraklat.\n" +
                "- Joker taşlar özel patlamalar gerçekleştirir.\n\n" +
                "Joker Türleri:\n" +
                "- Roket: Yatay/Dikey tüm satırı/sütunu patlatır.\n" +
                "- Kopter: Rastgele bir taşı patlatır.\n" +
                "- Bomba: Etrafındaki 8 taşı patlatır.\n" +
                "- Gökkuşağı: Rastgele bir renk seçerek o renkteki tüm taşları patlatır.\n";
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Arial", 12);
            this.lblInfo.Location = new System.Drawing.Point(50, 60);
            this.lblInfo.Visible = false;

            // btnBackFromInfo
            this.btnBackFromInfo.Text = "Geri";
            this.btnBackFromInfo.Font = new System.Drawing.Font("Arial", 12);
            this.btnBackFromInfo.Location = new System.Drawing.Point(50, 300);
            this.btnBackFromInfo.Size = new System.Drawing.Size(100, 40);
            this.btnBackFromInfo.Visible = false;

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtPlayerName);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnHighScores);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.lstHighScores);
            this.Controls.Add(this.btnBackFromScores);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnBackFromInfo);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
