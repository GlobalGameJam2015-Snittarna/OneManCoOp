using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OneManCoOp
{
    class Button : GameObject
    {
        public bool Activated { get; set; }

        public byte Tag { get; set; }

        public bool beingPressed;

        public Button(Vector2 position2, Color color2)
        {
            Sprite = new Sprite(TextureManager.button, position2, new Vector2(32, 32), 4, new Point(32, 32), 0);
            Position = position2;
            Sprite.Color = color2;
        }
        public override void Update()
        {
            beingPressed = (Hitbox.Intersects(Game1.player.Hitbox)) ? true : false;
            //if(beingPressed && Sprite.Frame <= 2)
            Sprite.AnimationSpeed = (beingPressed & Sprite.Frame <= 2) ? Sprite.AnimationSpeed = Sprite.AnimationSpeed + 1 : Sprite.AnimationSpeed = 0;
        }
    }
}
