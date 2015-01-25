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
        const byte levels = 6;

        public const byte sizeX = 26, sizeY = 85;
        public static Point sizePx { get { return new Point(sizeX * Tile.SIZE, sizeY * Tile.SIZE); } }

        // 0 = ladder, 1 = spawnPoint, 2 = button, 3 = door, 4 = lava
        static Color[] itemColors = new Color[] { new Color(255, 106, 0), new Color(64, 64, 64), new Color(255, 216, 0), new Color(0, 38, 255), new Color(255, 0, 0) };

        public Tile[,] Tiles;
        public Vector2 position { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativePosition"></param>
        /// <param name="mapData"></param>
        public Chunk(Vector2 relativePosition, Color[,] mapData)
        {
            Vector2[] doorPositions = new Vector2[levels];
            List<Button>[] buttons = new List<Button>[levels];
            for (int i = 0; i < levels; i++ )
            {
                buttons[i] = new List<Button>();
            }

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

                        byte tag = (byte)((y / 16));
                        switch((int)index)
                        {
                            case 0:
                                Game1.ladders.Add(new Ladder(new Vector2(x, y) * Tile.SIZE));
                                break;
                            case 1:
                                Game1.spawnPoints.Add(new Vector2(x, y) * Tile.SIZE);
                                break;
                            case 2:
                                buttons[tag].Add(new Button(new Vector2(x, y) * Tile.SIZE, Color.Red, tag));
                                break;
                            case 3:
                                doorPositions[tag] = new Vector2(x, y) * Tile.SIZE;
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                        }
                    }
                }
            }

            foreach (List<Button> bl in buttons) foreach(Button b in bl) Game1.buttons.Add(b);
            for (int i = 0; i < doorPositions.Length; i++)
            {
                Game1.puzzels.Add(new Puzzel(doorPositions[i], Puzzel.Type.Door, (byte)i, (byte)buttons[i].Count));
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
