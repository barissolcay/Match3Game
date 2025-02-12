namespace Match3Game
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblPlayer;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblTime;

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
            this.lblPlayer = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GameForm
            // 
            this.ClientSize = new System.Drawing.Size(600, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Match-3 Game - Oyun";
            // 
            // lblPlayer
            // 
            this.lblPlayer.AutoSize = true;
            this.lblPlayer.Font = new System.Drawing.Font("Arial", 12F);
            this.lblPlayer.Location = new System.Drawing.Point(50, 10);
            this.lblPlayer.Name = "lblPlayer";
            this.lblPlayer.Text = "Oyuncu:";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Arial", 12F);
            this.lblScore.Location = new System.Drawing.Point(200, 10);
            this.lblScore.Name = "lblScore";
            this.lblScore.Text = "Puan:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Arial", 12F);
            this.lblTime.Location = new System.Drawing.Point(350, 10);
            this.lblTime.Name = "lblTime";
            this.lblTime.Text = "Kalan Süre (sn):";
            // 
            // Add Controls
            this.Controls.Add(this.lblPlayer);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblTime);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
