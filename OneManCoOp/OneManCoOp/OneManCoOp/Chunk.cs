using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneManCoOp
{
    class Chunk
    {
        public const byte sizeX = 26, sizeY = 100;
        public static Point sizePx { get { return new Point(sizeX * Tile.SIZE, sizeY * Tile.SIZE); } }

        public Tile[,] Tiles;
        public Vector2 position { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativePosition"></param>
        /// <param name="mapData"></param>
        public Chunk(Vector2 relativePosition, Color[,] mapData)
        {
            Tiles = new Tile[sizeX, sizeY];
            position = relativePosition;

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    byte type = 255;
                    for (byte i = 0; i < Tile.tileTypes.Length; i++)
                    {
                        if (Tile.tileTypes[i] == mapData[x, y]) type = i;
                    }
                    if(type != 255) Tiles[x, y] = new Tile(position + new Vector2(x, y) * Tile.SIZE, type);
                }
            }
        }

        public List<Tile> SolidTiles()
        {
            List<Tile> tiles = new List<Tile>();
            foreach (Tile t in Tiles)
            {
                if (t == null) continue;
                if (t.Properties.Solid) tiles.Add(t); 
            }
            return tiles;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile t in Tiles)
            {
                if (t == null) continue;
                t.Draw(spriteBatch);
            }
        }
    }
}
