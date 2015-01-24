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

        // 1 = ladder, 2 = spawnPoint
        static Color[] itemColors = new Color[] { new Color(255, 106, 0), new Color(64, 64, 64) };

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
                    //=============== ADD TILES ===============
                    byte type = 255;
                    for (byte i = 0; i < Tile.tileTypes.Length; i++)
                    {
                        if (Tile.tileTypes[i] == mapData[x, y]) type = i;
                    }
                    if(type != 255) Tiles[x, y] = new Tile(position + new Vector2(x, y) * Tile.SIZE, type);

                    //=========== ADD OTHER ITEMS =============
                    else
                    {
                        byte index = 255;
                        for(byte i = 0; i < itemColors.Length; i++)
                        {
                            if (mapData[x, y] == itemColors[i])
                            {
                                index = i;
                                break;
                            }
                        }

                        switch((int)index)
                        {
                            case 0:
                                Game1.ladders.Add(new Ladder(new Vector2(x, y) * Tile.SIZE));
                                break;
                            case 1:
                                Game1.spawnPoints.Add(new Vector2(x, y) * Tile.SIZE);
                                break;
                        }
                    }
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
