using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OneManCoOp
{
    class Particle : GameObject
    {
        float angle;
        float angle2;

        float speed;

        Vector2 vel;

        byte type;

        public Particle(Vector2 position2, byte type2, float ang, float speed2, Color color)
        {
            Sprite = new Sprite(TextureManager.particle, position2, new Vector2(TextureManager.particle.Width, TextureManager.particle.Height));
            Sprite.Color = color;
            Position = position2;
            type = type2;
            angle2 = ang;
            speed = speed2;
        }
        public override void Update()
        {
            angle = (angle2 * 180 / (float)Math.PI);
            vel = new Vector2((float)Math.Cos(angle)*speed, (float)Math.Sin(angle)*speed);
            switch(type)
            {
                case 0:
                    speed -= 0.3f;
                    Sprite.Alpha -= 0.1f;
                    Position += vel;
                    if (Sprite.Alpha < 0) Dead = true;
                    break;
            }
        }
    }
}
