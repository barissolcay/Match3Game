using System;
using System.Drawing;

namespace Match3Game
{
    /// <summary>
    /// Taş sınıfı
    /// Renkli taş veya joker olabilir.
    /// </summary>
    public enum TileType
    {
        Blue,
        Pink,
        Purple,
        Yellow,
        RocketHorizontal,
        RocketVertical,
        Helicopter,
        Bomb,
        Rainbow
    }

    public class Tile
    {
        public TileType Type { get; set; }
        public bool IsJoker
        {
            get
            {
                return Type == TileType.RocketHorizontal ||
                       Type == TileType.RocketVertical ||
                       Type == TileType.Helicopter ||
                       Type == TileType.Bomb ||
                       Type == TileType.Rainbow;
            }
        }

        public Image GetImage()
        {
            switch (Type)
            {
                case TileType.Blue:
                    return Properties.Resources.blue;
                case TileType.Pink:
                    return Properties.Resources.pink;
                case TileType.Purple:
                    return Properties.Resources.purple;
                case TileType.Yellow:
                    return Properties.Resources.yellow;
                case TileType.RocketHorizontal:
                    return Properties.Resources.rocket_horizontal;
                case TileType.RocketVertical:
                    return Properties.Resources.rocket_vertical;
                case TileType.Helicopter:
                    return Properties.Resources.helicopter;
                case TileType.Bomb:
                    return Properties.Resources.bomb;
                case TileType.Rainbow:
                    return Properties.Resources.rainbow;
            }
            return null;
        }
    }
}
