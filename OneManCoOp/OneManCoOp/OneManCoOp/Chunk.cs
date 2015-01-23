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
        static Color[] tileTypes = new Color[] { Color.Green, Color.Blue, new Color(1f, .5f, 0, 1f) };
        public const byte size = 16;
        public static short sizePx { get { return size * Tile.SIZE; } }

        public Tile[,] Tiles;
        public Vector2 position { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativePosition"></param>
        /// <param name="mapData"></param>
        public Chunk(Vector2 relativePosition, Color[,] mapData)
        {
            Tiles = new Tile[size, size];
            position = relativePosition;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    //if(!tileTypes.Contains(mapData[x, y])) throw new Exception("Color not valid, pixel " + x + ", " + y);
                    byte type = 0;
                    for (byte i = 0; i < tileTypes.Length; i++)
                    {
                        if (tileTypes[i] == mapData[x, y]) type = i;
                    }
                    Tiles[x, y] = new Tile(position + new Vector2(x, y) * Tile.SIZE, type);
                }
            }
        }

        public List<Tile> NonWalkableTiles()
        {
            List<Tile> tiles = new List<Tile>();
            foreach (Tile t in Tiles)
            {
                //if (!t.Properties.IsWalkable) tiles.Add(t); //TODO: fix
            }
            return tiles;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile t in Tiles)
            {
                t.Draw(spriteBatch);
            }
        }
    }
}
