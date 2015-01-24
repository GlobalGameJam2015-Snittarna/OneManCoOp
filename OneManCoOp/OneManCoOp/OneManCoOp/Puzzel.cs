using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace OneManCoOp
{
    class Puzzel : GameObject
    {
        public enum Type { Door };

        public Type type;

        byte tag;
        byte buttonsToOpen;
        byte buttonsPressed;

        bool opening;

        public Puzzel(Vector2 position2, Type type2, byte tag2, byte buttonsToOpen2)
        {
            buttonsToOpen = buttonsToOpen2;
            tag = tag2;
            AssignSprite(position2);
            type = type2;
        }

        public override void Update()
        {
            switch(type)
            {
                case Type.Door:
                    if(opening)
                    {
                        Sprite.AnimationSpeed = 0.1f;
                        if(Hitbox.Intersects(Game1.player.Hitbox) && Sprite.Frame >= 3)
                        {
                            Game1.player.Position = Game1.spawnPoints[4-tag];
                        }
                    }
                    foreach(Button b in Game1.buttons)
                    {
                        if (b.Tag == tag)
                        {
                            if (!b.AddPress && b.BeingPressed)
                            {
                                buttonsPressed += 1;
                                b.AddPress = true;
                            }
                            if (!b.BeingPressed) buttonsPressed = 0;
                        }
                    }
                    if (buttonsPressed >= buttonsToOpen) opening = true;
                    else opening = false;
                    if (!opening && Sprite.Frame >= 1)
                    {
                        Sprite.AnimationSpeed = 0;
                        Sprite.Frame = 0;
                    }
                    //if (Sprite.Frame >= 3) Sprite.AnimationSpeed = 0;
                    Sprite.AnimationSpeed = (Sprite.Frame >= 4) ? Sprite.AnimationSpeed = 0 : Sprite.AnimationSpeed = Sprite.AnimationSpeed;
                    break;
            }
        }
        public void AssignSprite(Vector2 position)
        {
            switch(type)
            {
                case Type.Door:
                    Sprite = new Sprite(TextureManager.door, position, new Vector2(32, 32), 5, new Point(32, 32), 0);
                    break;
            }
        }
    }
}
