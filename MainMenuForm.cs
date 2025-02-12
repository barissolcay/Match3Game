using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Match3Game
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += MainMenuForm_KeyDown;

            btnStart.Click += BtnStart_Click;
            btnHighScores.Click += BtnHighScores_Click;
            btnInfo.Click += BtnInfo_Click;
            btnBackFromScores.Click += BtnBackFromScores_Click;
            btnBackFromInfo.Click += BtnBackFromInfo_Click;
        }

        private void MainMenuForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtPlayerName.Visible && txtPlayerName.Text.Trim() != "")
            {
                StartGame();
            }
        }

        private void BtnInfo_Click(object sender, EventArgs e)
        {
            ShowInfo();
        }

        private void BtnHighScores_Click(object sender, EventArgs e)
        {
            ShowHighScores();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlayerName.Text))
            {
                MessageBox.Show("Lütfen oyuncu adını giriniz.");
                return;
            }
            StartGame();
        }

        private void BtnBackFromScores_Click(object sender, EventArgs e)
        {
            HideHighScores();
        }

        private void BtnBackFromInfo_Click(object sender, EventArgs e)
        {
            HideInfo();
        }

        private void ShowHighScores()
        {
            txtPlayerName.Visible = false;
            lblName.Visible = false; // Oyuncu Adı label da gizlensin
            btnStart.Visible = false;
            btnHighScores.Visible = false;
            btnInfo.Visible = false;

            lstHighScores.Visible = true;
            btnBackFromScores.Visible = true;

            lstHighScores.Items.Clear();
            var scores = ScoreManager.ReadScores();
            foreach (var s in scores)
            {
                lstHighScores.Items.Add(s.PlayerName + " - " + s.ScoreValue);
            }
        }

        private void HideHighScores()
        {
            txtPlayerName.Visible = true;
            lblName.Visible = true; // Oyuncu Adı label geri gelsin
            btnStart.Visible = true;
            btnHighScores.Visible = true;
            btnInfo.Visible = true;

            lstHighScores.Visible = false;
            btnBackFromScores.Visible = false;
        }

        private void ShowInfo()
        {
            txtPlayerName.Visible = false;
            lblName.Visible = false;
            btnStart.Visible = false;
            btnHighScores.Visible = false;
            btnInfo.Visible = false;

            lblInfo.Visible = true;
            btnBackFromInfo.Visible = true;
        }

        private void HideInfo()
        {
            txtPlayerName.Visible = true;
            lblName.Visible = true;
            btnStart.Visible = true;
            btnHighScores.Visible = true;
            btnInfo.Visible = true;

            lblInfo.Visible = false;
            btnBackFromInfo.Visible = false;
        }

        private void StartGame()
        {
            GameForm gf = new GameForm(txtPlayerName.Text);
            gf.Show();
            this.Hide();
        }
    }
}
