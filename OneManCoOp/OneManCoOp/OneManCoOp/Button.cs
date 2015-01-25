using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace OneManCoOp
{
    class Button : GameObject
    {
        public bool Activated { get; set; }

        public byte Tag { get; set; }

        public bool BeingPressed { get; set; }
        public bool AddPress;
        public bool RemovePress;

        public bool playedSound;

        bool hasRotated;

        bool countedFor;

        public Button(Vector2 position2, Color color2, byte tag2)
        {
            Tag = tag2;
            Sprite = new Sprite(TextureManager.button, position2, new Vector2(32, 32), 4, new Point(32, 32), 0);
            Position = position2;
            Sprite.Color = color2;
            Sprite.Position += Sprite.Origin;
        }

        private bool TileIsSolid(byte x, byte y)
        {
            if (Map.chunks[0, 0].Tiles[x, y] != null) return Map.chunks[0, 0].Tiles[x, y].Properties.Solid;
            else return false;
        }

        public override void Update()
        {
            if(!hasRotated)
            {
                hasRotated = true;
                byte tileX = (byte)(Position.X / Tile.SIZE), tileY = (byte)(Position.Y / Tile.SIZE);
                if (TileIsSolid((byte)(tileX - 1), tileY)) Sprite.Rotation = (float)(Math.PI / 2);
                if (TileIsSolid((byte)(tileX + 1), tileY)) Sprite.Rotation = -(float)(Math.PI / 2);
                if (TileIsSolid(tileX, (byte)(tileY + 1))) Sprite.Rotation = 0;
                if (TileIsSolid(tileX, (byte)(tileY - 1))) Sprite.Rotation = (float)(Math.PI);
            }

            BeingPressed = (Hitbox.Intersects(Game1.player.Hitbox) || IsPressedByCorpse()) ? true : false;

            Sprite.AnimationSpeed = (BeingPressed & Sprite.Frame <= 2) ? Sprite.AnimationSpeed = 0.4f : Sprite.AnimationSpeed = 0;
            Sprite.Frame = (!BeingPressed) ? Sprite.Frame = 0 : Sprite.Frame = Sprite.Frame;
            
            if (Sprite.Frame == 3)
            {
                if (!playedSound)
                {
                    SoundManager.buttonPress.Play();
                    playedSound = true;
                }
            }
            if (!BeingPressed) playedSound = false;
            if (!BeingPressed && AddPress)
            {
                AddPress = false;
            }
            if (BeingPressed)
                RemovePress = false;
        }

        bool IsPressedByCorpse()
        {
            foreach (Corpse c in Game1.corpses)
            {
                if (c.Hitbox.Intersects(Hitbox))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
