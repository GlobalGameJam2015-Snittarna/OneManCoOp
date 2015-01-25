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
        const byte levels = 5;

        public const byte sizeX = 26, sizeY = 85;
        public static Point sizePx { get { return new Point(sizeX * Tile.SIZE, sizeY * Tile.SIZE); } }

        // 0 = ladder, 1 = spawnPoint, 2 = button, 3 = door, 4 = lava, 5 = booster
        static Color[] itemColors = new Color[] { new Color(255, 106, 0), new Color(64, 64, 64), new Color(255, 216, 0), new Color(0, 38, 255), new Color(255, 0, 0), new Color(76, 255, 0) };

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

                        byte tag = (byte)((y * levels) / Chunk.sizeY);
                        switch((int)index)
                        {
                            case 0:
                                Game1.ladders.Add(new Ladder(new Vector2(x, y) * Tile.SIZE));
                                break;
                            case 1:
                                Game1.spawnPoints.Add(new Vector2(x, y) * Tile.SIZE);
                                break;
                            case 2:
                                Game1.buttons.Add(new Button(new Vector2(x, y) * Tile.SIZE, Color.Red, tag));
                                break;
                            case 3:
                                byte buttonsToPress = 0;
                                switch(tag)
                                {
                                    case 4:
                                        buttonsToPress = 1;
                                        break;
                                    case 3:
                                        buttonsToPress = 2;
                                        break;
                                    case 2:
                                        buttonsToPress = 4;
                                        break;
                                    case 1:
                                        buttonsToPress = 2;
                                        break;
                                    case 0:
                                        buttonsToPress = 6;
                                        break;
                                }
                                Game1.puzzels.Add(new Puzzel(new Vector2(x, y) * Tile.SIZE, Puzzel.Type.Door, tag, buttonsToPress));
                                break;
                            case 4:
                                Game1.lavas.Add(new Lava(new Vector2(x, y) * Tile.SIZE));
                                break;
                            case 5:
                                Game1.boosters.Add(new Booster(new Vector2(x, y) * Tile.SIZE));
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
