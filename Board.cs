using System;
using System.Collections.Generic;
using System.Linq;

namespace Match3Game
{
    /// <summary>
    /// Oyun tahtası mantıklarını yöneten sınıf
    /// Taş oluşturma, karıştırma, patlatma, düşürme, joker uygulama
    /// </summary>
    public class Board
    {
        private int rows;
        private int cols;
        private Tile[,] tiles;
        private Random rand = new Random();

        public event EventHandler OnScoreChanged;
        public event EventHandler OnBoardChanged;
        public event EventHandler<string> OnRainbowUsed;

        private int score;
        public int Score
        {
            get { return score; }
            private set
            {
                score = value;
                OnScoreChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private TileType[] normalTiles = new TileType[] {
            TileType.Blue, TileType.Pink, TileType.Purple, TileType.Yellow
        };

        private TileType[] jokerTiles = new TileType[] {
            TileType.RocketHorizontal, TileType.RocketVertical, TileType.Helicopter, TileType.Bomb, TileType.Rainbow
        };

        private bool isInitializing = false; // Başlangıçta patlamalar puan vermesin

        public Board(int r, int c)
        {
            rows = r;
            cols = c;
            tiles = new Tile[rows, cols];
        }

        public void InitializeBoard()
        {
            isInitializing = true;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // Rastgele normal taş veya nadiren joker
                    int chance = rand.Next(100);
                    if (chance < 10)
                    {
                        tiles[i, j] = new Tile { Type = jokerTiles[rand.Next(jokerTiles.Length)] };
                    }
                    else
                    {
                        tiles[i, j] = new Tile { Type = normalTiles[rand.Next(normalTiles.Length)] };
                    }
                }
            }
        }

        public void ClearInitialMatches()
        {
            // Başlangıçta eşleşme varsa temizle.
            // isInitializing = true olduğundan skor artmayacak.
            while (CheckForMatchesAndDrop()) { /* Devam et */ }

            // Artık başlatma bitti
            isInitializing = false;
            // Skor baştan 0'la
            Score = 0;
        }

        public Tile GetTile(int r, int c)
        {
            return tiles[r, c];
        }

        public void SwapTiles(int r1, int c1, int r2, int c2)
        {
            var temp = tiles[r1, c1];
            tiles[r1, c1] = tiles[r2, c2];
            tiles[r2, c2] = temp;
            OnBoardChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool AreNeighbors(int r1, int c1, int r2, int c2)
        {
            return (Math.Abs(r1 - r2) + Math.Abs(c1 - c2)) == 1;
        }

        public bool CheckForMatchesAndDrop()
        {
            bool matchedSomething = false;

            var matchedPositions = FindMatches();
            while (matchedPositions.Count > 0)
            {
                matchedSomething = true;
                // Patlat
                foreach (var pos in matchedPositions)
                {
                    if (!isInitializing)
                    {
                        Score += 1;
                    }
                    tiles[pos.Item1, pos.Item2] = null;
                }

                // Düşür
                DropTiles();

                // Yenile
                FillEmptySpaces();

                // Yeni eşleşme kontrolü
                matchedPositions = FindMatches();
            }

            OnBoardChanged?.Invoke(this, EventArgs.Empty);
            return matchedSomething;
        }

        private List<Tuple<int, int>> FindMatches()
        {
            List<Tuple<int, int>> matched = new List<Tuple<int, int>>();

            // Yatay kontrol
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols - 2; j++)
                {
                    if (tiles[i, j] != null && !tiles[i, j].IsJoker)
                    {
                        TileType t = tiles[i, j].Type;
                        if (tiles[i, j + 1] != null && tiles[i, j + 1].Type == t && !tiles[i, j + 1].IsJoker &&
                            tiles[i, j + 2] != null && tiles[i, j + 2].Type == t && !tiles[i, j + 2].IsJoker)
                        {
                            int k = j;
                            while (k < cols && tiles[i, k] != null && tiles[i, k].Type == t && !tiles[i, k].IsJoker)
                            {
                                matched.Add(Tuple.Create(i, k));
                                k++;
                            }
                        }
                    }
                }
            }

            // Dikey kontrol
            for (int j = 0; j < cols; j++)
            {
                for (int i = 0; i < rows - 2; i++)
                {
                    if (tiles[i, j] != null && !tiles[i, j].IsJoker)
                    {
                        TileType t = tiles[i, j].Type;
                        if (tiles[i + 1, j] != null && tiles[i + 1, j].Type == t && !tiles[i + 1, j].IsJoker &&
                            tiles[i + 2, j] != null && tiles[i + 2, j].Type == t && !tiles[i + 2, j].IsJoker)
                        {
                            int k = i;
                            while (k < rows && tiles[k, j] != null && tiles[k, j].Type == t && !tiles[k, j].IsJoker)
                            {
                                matched.Add(Tuple.Create(k, j));
                                k++;
                            }
                        }
                    }
                }
            }

            matched = matched.Distinct().ToList();
            return matched;
        }

        private void DropTiles()
        {
            for (int j = 0; j < cols; j++)
            {
                int emptyCount = 0;
                for (int i = rows - 1; i >= 0; i--)
                {
                    if (tiles[i, j] == null)
                    {
                        emptyCount++;
                    }
                    else if (emptyCount > 0)
                    {
                        tiles[i + emptyCount, j] = tiles[i, j];
                        tiles[i, j] = null;
                    }
                }
            }
        }

        private void FillEmptySpaces()
        {
            for (int j = 0; j < cols; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    if (tiles[i, j] == null)
                    {
                        int chance = rand.Next(100);
                        if (chance < 10)
                            tiles[i, j] = new Tile { Type = jokerTiles[rand.Next(jokerTiles.Length)] };
                        else
                            tiles[i, j] = new Tile { Type = normalTiles[rand.Next(normalTiles.Length)] };
                    }
                }
            }
        }

        public void ApplyJokerEffect(int r, int c)
        {
            Tile t = tiles[r, c];
            if (t == null) return;
            if (!t.IsJoker) return;

            if (t.Type == TileType.RocketHorizontal)
            {
                for (int cc = 0; cc < cols; cc++)
                {
                    if (tiles[r, cc] != null)
                    {
                        if (!isInitializing) Score += 1;
                        tiles[r, cc] = null;
                    }
                }
            }
            else if (t.Type == TileType.RocketVertical)
            {
                for (int rr = 0; rr < rows; rr++)
                {
                    if (tiles[rr, c] != null)
                    {
                        if (!isInitializing) Score += 1;
                        tiles[rr, c] = null;
                    }
                }
            }
            else if (t.Type == TileType.Helicopter)
            {
                int rr = rand.Next(rows);
                int cc = rand.Next(cols);
                if (tiles[rr, cc] != null)
                {
                    if (!isInitializing) Score += 1;
                    tiles[rr, cc] = null;
                }
            }
            else if (t.Type == TileType.Bomb)
            {
                for (int rr = r - 1; rr <= r + 1; rr++)
                {
                    for (int cc = c - 1; cc <= c + 1; cc++)
                    {
                        if (rr >= 0 && rr < rows && cc >= 0 && cc < cols && tiles[rr, cc] != null)
                        {
                            if (!isInitializing) Score += 1;
                            tiles[rr, cc] = null;
                        }
                    }
                }
            }
            else if (t.Type == TileType.Rainbow)
            {
                TileType chosenColor = normalTiles[rand.Next(normalTiles.Length)];
                for (int rr = 0; rr < rows; rr++)
                {
                    for (int cc = 0; cc < cols; cc++)
                    {
                        if (tiles[rr, cc] != null && tiles[rr, cc].Type == chosenColor)
                        {
                            if (!isInitializing) Score += 1;
                            tiles[rr, cc] = null;
                        }
                    }
                }
                // Renk adını bul
                string colorName = GetColorName(chosenColor);
                OnRainbowUsed?.Invoke(this, colorName);
            }

            // Joker kendisi de gider
            tiles[r, c] = null;

            DropTiles();
            FillEmptySpaces();
            CheckForMatchesAndDrop();
        }

        private string GetColorName(TileType t)
        {
            switch (t)
            {
                case TileType.Blue: return "Mavi";
                case TileType.Pink: return "Pembe";
                case TileType.Purple: return "Mor";
                case TileType.Yellow: return "Sarı";
            }
            return "Bilinmeyen renk";
        }
    }
}
