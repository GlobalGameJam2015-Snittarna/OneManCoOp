using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OneManCoOp
{
    class Lava : GameObject
    {
        bool playedSound;
        public Lava(Vector2 position2)
        {
            Sprite = new Sprite(TextureManager.lava, position2, new Vector2(32, 32), 4, new Point(32, 32), 0.2f);
            Sprite.Origin = Vector2.Zero;

        }
        public override void Update()
        {
            if(Game1.player.Hitbox.Intersects(Hitbox))
            {
                if (!playedSound)
                {
                    SoundManager.lava2.Play();
                    playedSound = false;
                }
                Game1.GlobalTimer = Game1.maxTime;
            }
        }
    }
}
