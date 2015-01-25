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
            Random random = new Random();
            //Game1.particles.Add(new Particle(new Vector2(Position.X+random.Next(32-8), Position.Y +  random.Next(32)), 0, random.Next(-110,-70), random.Next(1, 4), Color.Orange));
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
