using System;
using System.Drawing;
using System.Windows.Forms;

namespace Match3Game
{
    public partial class GameForm : Form
    {
        private Board board;
        private PictureBox[,] boardCells;
        private const int ROWS = 8;
        private const int COLS = 8;
        private System.Windows.Forms.Timer gameTimer;
        private int timeLeft = 60;
        private bool isPaused = false;
        private string playerName;
        private Tile firstSelectedTile = null;
        private Point firstSelectedPos = new Point(-1, -1);
        private bool keyboardSelectionMode = false;
        private Point keyboardPos = new Point(0, 0);
        private bool firstTileSelectedWithKeyboard = false;

        public GameForm(string playerName)
        {
            InitializeComponent();

            // Oyuncu adı çok uzunsa kısalt
            if (playerName.Length > 10)
            {
                playerName = playerName.Substring(0, 10) + "...";
            }

            this.playerName = playerName;

            lblPlayer.Text = "Oyuncu: " + playerName;
            lblScore.Text = "Puan: 0";
            lblTime.Text = "Kalan Süre (sn): " + timeLeft;

            board = new Board(ROWS, COLS);
            board.OnScoreChanged += Board_OnScoreChanged;
            board.OnBoardChanged += Board_OnBoardChanged;
            board.OnRainbowUsed += Board_OnRainbowUsed;

            // Önce boardCells dizisini oluştur
            int startX = 50;
            int startY = 100;
            int cellSize = 50;
            boardCells = new PictureBox[ROWS, COLS];
            for (int r = 0; r < ROWS; r++)
            {
                for (int c = 0; c < COLS; c++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Size = new Size(cellSize, cellSize);
                    pb.Location = new Point(startX + c * cellSize, startY + r * cellSize);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.BorderStyle = BorderStyle.FixedSingle;
                    pb.Click += Pb_Click;
                    boardCells[r, c] = pb;
                    this.Controls.Add(pb);
                }
            }

            // Board'u başlat ve başlangıç eşleşmelerini temizle
            board.InitializeBoard();
            board.ClearInitialMatches();

            UpdateBoardGraphics();
            UpdateKeyboardHighlight();

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            this.KeyPreview = true;
            this.KeyDown += GameForm_KeyDown;
        }

        private void Board_OnRainbowUsed(object sender, string colorName)
        {
            MessageBox.Show(colorName + " taşlar yok edildi!");
        }

        private void Board_OnBoardChanged(object sender, EventArgs e)
        {
            UpdateBoardGraphics();
            UpdateKeyboardHighlight();
        }

        private void Board_OnScoreChanged(object sender, EventArgs e)
        {
            lblScore.Text = "Puan: " + board.Score;
        }

        private void UpdateBoardGraphics()
        {
            if (boardCells == null) return; // BoardCells yoksa çık
            for (int r = 0; r < ROWS; r++)
            {
                for (int c = 0; c < COLS; c++)
                {
                    Tile t = board.GetTile(r, c);
                    boardCells[r, c].Image = t != null ? t.GetImage() : null;
                    // Varsayılan çerçeve
                    boardCells[r, c].BorderStyle = BorderStyle.FixedSingle;
                }
            }
        }


        private void UpdateKeyboardHighlight()
        {
            if (boardCells == null)
            {
                throw new InvalidOperationException("boardCells array is not initialized.");
            }

            // Tüm hücreleri varsayılan stile döndür
            for (int r = 0; r < ROWS; r++)
            {
                for (int c = 0; c < COLS; c++)
                {
                    boardCells[r, c].BorderStyle = BorderStyle.FixedSingle;
                }
            }

            // Seçili konumu vurgula
            if (keyboardPos.Y >= 0 && keyboardPos.Y < ROWS && keyboardPos.X >= 0 && keyboardPos.X < COLS)
            {
                // Eğer ilk taş seçilmişse yeşil vs. yerine borderStyle kullandık
                // İsterseniz farklı renk veya stil değiştirebilirsiniz.
                boardCells[keyboardPos.Y, keyboardPos.X].BorderStyle = BorderStyle.Fixed3D;
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                timeLeft--;
                lblTime.Text = "Kalan Süre (sn): " + timeLeft;
                if (timeLeft <= 0)
                {
                    gameTimer.Stop();
                    EndGame();
                }
            }
        }

        private void EndGame()
        {
            MessageBox.Show("Süre bitti! Puanınız: " + board.Score);
            ScoreManager.AddScore(playerName, board.Score);
            Hide();
            MainMenuForm mm = new MainMenuForm();
            mm.Show();
        }

        private void Pb_Click(object sender, EventArgs e)
        {
            if (isPaused) return;
            if (keyboardSelectionMode) return;
            PictureBox clicked = sender as PictureBox;
            Point pos = GetCellPosition(clicked);

            Tile t = board.GetTile(pos.Y, pos.X);
            if (t == null) return;

            // Joker ise hemen uygula
            if (t.IsJoker)
            {
                board.ApplyJokerEffect(pos.Y, pos.X);
                board.CheckForMatchesAndDrop();
                return;
            }

            if (firstSelectedTile == null)
            {
                firstSelectedTile = t;
                firstSelectedPos = pos;
            }
            else
            {
                if (board.AreNeighbors(firstSelectedPos.Y, firstSelectedPos.X, pos.Y, pos.X))
                {
                    board.SwapTiles(firstSelectedPos.Y, firstSelectedPos.X, pos.Y, pos.X);
                    bool matched = board.CheckForMatchesAndDrop();
                    if (!matched)
                    {
                        board.SwapTiles(firstSelectedPos.Y, firstSelectedPos.X, pos.Y, pos.X);
                    }
                }
                firstSelectedTile = null;
                firstSelectedPos = new Point(-1, -1);
            }
        }

        private Point GetCellPosition(PictureBox pb)
        {
            for (int r = 0; r < ROWS; r++)
            {
                for (int c = 0; c < COLS; c++)
                {
                    if (boardCells[r, c] == pb)
                        return new Point(c, r);
                }
            }
            return new Point(-1, -1);
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P)
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    gameTimer.Stop();
                }
                else
                {
                    gameTimer.Start();
                }
            }

            if (isPaused) return;

            if (e.KeyCode == Keys.Up)
            {
                if (keyboardPos.Y > 0) keyboardPos.Y--;
                UpdateKeyboardHighlight();
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (keyboardPos.Y < ROWS - 1) keyboardPos.Y++;
                UpdateKeyboardHighlight();
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (keyboardPos.X > 0) keyboardPos.X--;
                UpdateKeyboardHighlight();
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (keyboardPos.X < COLS - 1) keyboardPos.X++;
                UpdateKeyboardHighlight();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                keyboardSelectionMode = true;
                Tile selected = board.GetTile(keyboardPos.Y, keyboardPos.X);
                if (selected == null) return;

                // Joker ise hemen uygula
                if (selected.IsJoker)
                {
                    board.ApplyJokerEffect(keyboardPos.Y, keyboardPos.X);
                    board.CheckForMatchesAndDrop();
                    keyboardSelectionMode = false;
                    firstTileSelectedWithKeyboard = false;
                    firstSelectedTile = null;
                    firstSelectedPos = new Point(-1, -1);
                    UpdateKeyboardHighlight();
                    return;
                }

                if (!firstTileSelectedWithKeyboard)
                {
                    firstSelectedPos = keyboardPos;
                    firstSelectedTile = selected;
                    firstTileSelectedWithKeyboard = true;
                    UpdateKeyboardHighlight();
                }
                else
                {
                    Point secondPos = keyboardPos;
                    if (board.AreNeighbors(firstSelectedPos.Y, firstSelectedPos.X, secondPos.Y, secondPos.X))
                    {
                        board.SwapTiles(firstSelectedPos.Y, firstSelectedPos.X, secondPos.Y, secondPos.X);
                        bool matched = board.CheckForMatchesAndDrop();
                        if (!matched)
                        {
                            board.SwapTiles(firstSelectedPos.Y, firstSelectedPos.X, secondPos.Y, secondPos.X);
                        }
                    }
                    firstTileSelectedWithKeyboard = false;
                    keyboardSelectionMode = false;
                    firstSelectedTile = null;
                    firstSelectedPos = new Point(-1, -1);
                    UpdateKeyboardHighlight();
                }
            }
        }
    }
}
