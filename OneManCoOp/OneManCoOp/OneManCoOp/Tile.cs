using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneManCoOp
{
    class Tile
    {
        public static Color[] tileTypes = new Color[] { Color.Black, new Color(255, 106, 0) };
        public static TileProperties[] TilePrefabs = new TileProperties[] { new TileProperties(0, true), new TileProperties(1, false), new TileProperties(2, true)};

        public enum TileType { Grass = 0, Sea = 1, Wood = 2 }

        public const byte SIZE = 48;

        public Vector2 Position { get; private set; }
        public TileType Type { get; private set; }

        public TileProperties Properties { get { return TilePrefabs[(int)Type]; } }
        public Rectangle Hitbox { get { return new Rectangle((int)Position.X, (int)Position.Y, SIZE, SIZE); } }

        byte frame, animCounter;

        public Tile(Vector2 position, byte type)
        {
            frame = 0;
            animCounter = 0;
            this.Position = position;
            this.Type = (TileType)type;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Properties.IsAnimated) Properties.Animate(ref frame, ref animCounter);
            spriteBatch.Draw(TextureManager.tiles, Hitbox, new Rectangle(1 + frame * 34, 1 + Properties.TextureIndex * 34, 32, 32), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1); //TODO: use a spritesheet instead
        }
    }
}
