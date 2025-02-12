using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Match3Game
{
    /// <summary>
    /// Skor bilgisini tutar
    /// </summary>
    public class PlayerScore
    {
        public string PlayerName { get; set; }
        public int ScoreValue { get; set; }
    }

    /// <summary>
    /// En iyi skorları yönetir
    /// scores.txt dosyasında saklar.
    /// Format: playerName;score
    /// </summary>
    public static class ScoreManager
    {
        private static string scoreFile = "scores.txt";

        public static List<PlayerScore> ReadScores()
        {
            List<PlayerScore> scores = new List<PlayerScore>();
            if (!File.Exists(scoreFile))
            {
                File.Create(scoreFile).Close();
            }

            var lines = File.ReadAllLines(scoreFile);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(';');
                if (parts.Length == 2)
                {
                    string name = parts[0];
                    int sc;
                    if (int.TryParse(parts[1], out sc))
                    {
                        scores.Add(new PlayerScore { PlayerName = name, ScoreValue = sc });
                    }
                }
            }

            scores = scores.OrderByDescending(s => s.ScoreValue).ToList();
            return scores;
        }

        public static void AddScore(string playerName, int score)
        {
            var scores = ReadScores();
            scores.Add(new PlayerScore { PlayerName = playerName, ScoreValue = score });
            scores = scores.OrderByDescending(s => s.ScoreValue).ToList();

            // İlk 5'i tutalım
            if (scores.Count > 5)
                scores = scores.Take(5).ToList();

            List<string> lines = new List<string>();
            foreach (var s in scores)
            {
                lines.Add(s.PlayerName + ";" + s.ScoreValue);
            }

            File.WriteAllLines(scoreFile, lines);
        }
    }
}
