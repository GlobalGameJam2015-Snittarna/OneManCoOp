﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneManCoOp
{
    class Map
    {
        public const byte width = 1, height = 1; //number of chunks. width * Tile.size * chunk.size should equal the width of the map texture, same for height.

        public static Chunk[,] chunks { get; set; }

        public static void Initialize()
        {
            chunks = new Chunk[width, height];

            Color[] colors1D = new Color[TextureManager.map.Width * TextureManager.map.Height];
            TextureManager.map.GetData(colors1D);
            Color[,] mapData = new Color[TextureManager.map.Width, TextureManager.map.Height];
            for (int x = 0; x < TextureManager.map.Width; x++)
            {
                for (int y = 0; y < TextureManager.map.Height; y++)
                {
                    mapData[x, y] = colors1D[x + y * TextureManager.map.Width];
                }
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    chunks[x, y] = new Chunk(new Vector2(Chunk.sizePx.X, Chunk.sizePx.Y) * new Vector2(x, y), subChunk(mapData, x * Chunk.sizeX, y * Chunk.sizeY, Chunk.sizeX, Chunk.sizeY));
                }
            }

            Game1.spawnPoints.Sort(
                delegate(Vector2 p1, Vector2 p2)
                {
                    int compareDate = p1.Y.CompareTo(p2.Y);
                    return compareDate;
                }
            );
        }

        static Color[,] subChunk(Color[,] source, int x, int y, int w, int h)
        {
            Color[,] c = new Color[w, h];
            for (int ix = 0; ix < w; ix++)
            {
                for (int iy = 0; iy < h; iy++)
                {
                    c[ix, iy] = source[ix + x, iy + y];
                }
            }
            return c;
        }

        public static List<Chunk> VisibleChunks
        {
            get
            {
                List<Chunk> c = new List<Chunk>();
                foreach (Chunk ch in chunks) if (Camera.AreaIsVisible(ch.position, new Vector2(Chunk.sizePx.X, Chunk.sizePx.Y))) c.Add(ch);
                return c;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Chunk c in chunks)
            {
                c.Draw(spriteBatch);
            }
        }
    }
}
