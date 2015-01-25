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

        public Button(Vector2 position2, Color color2, byte tag2)
        {
            Tag = tag2;
            Sprite = new Sprite(TextureManager.button, position2, new Vector2(32, 32), 4, new Point(32, 32), 0);
            Position = position2;
            Sprite.Color = color2;
            Sprite.Origin = Vector2.Zero;
        }
        public override void Update()
        {
            BeingPressed = (Hitbox.Intersects(Game1.player.Hitbox)) ? true : false;
            //if(beingPressed && Sprite.Frame <= 2)
            Sprite.AnimationSpeed = (BeingPressed & Sprite.Frame <= 2) ? Sprite.AnimationSpeed = 0.4f : Sprite.AnimationSpeed = 0;
            Sprite.Frame = (!BeingPressed) ? Sprite.Frame = 0 : Sprite.Frame = Sprite.Frame;
            if (!BeingPressed && !RemovePress)
            {
                
            }

            foreach(Corpse c in Game1.corpses)
            {
                if(c.Hitbox.Intersects(Hitbox))
                {
                    BeingPressed = true;
                    if (!BeingPressed && AddPress)
                    {
                        AddPress = false;
                    }
                    if (BeingPressed)
                        RemovePress = false;
                }
            }
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

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.font, Tag.ToString(), Position, Color.White);
            base.Draw(spriteBatch);
        }
    }
}
