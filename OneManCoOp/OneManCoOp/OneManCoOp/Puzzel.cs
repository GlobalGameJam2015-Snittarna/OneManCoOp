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

        public byte tag;
        public byte buttonsToOpen;
        byte buttonsPressed;

        bool opening;

        bool playedSound;

        public Puzzel(Vector2 position2, Type type2, byte tag2, byte buttonsToOpen2)
        {
            buttonsToOpen = buttonsToOpen2;
            tag = tag2;
            AssignSprite(position2);
            type = type2;
            Sprite.Origin = Vector2.Zero;
        }

        public override void Update()
        {
            //Debug.Print(buttonsPressed.ToString());
            switch(type)
            {
                case Type.Door:
                    if(opening)
                    {
                        if(Sprite.Frame < Sprite.Frames - 1) Sprite.AnimationSpeed = 0.1f;
                        if(Hitbox.Intersects(Game1.player.Hitbox) && Sprite.Frame >= 3)
                        {
                            if (tag > 0) Game1.player.Position = Game1.spawnPoints[tag - 1];
                            else Game1.gameState = Game1.GameState.Won;
                            Game1.maxTime = 500 * (Game1.puzzels.Count + 1 - tag);
                        }
                        if(Sprite.Frame == 4 && !playedSound)
                        {
                            //SoundManager.door.Play();
                            playedSound = true;
                        }
                    }
                    else
                    {
                        playedSound = false;
                    }
                    foreach(Button b in Game1.buttons)
                    {
                        // Vet inte varför det funkar
                        if (b.Tag == tag)
                        {
                            if (!b.AddPress && b.BeingPressed)
                            {
                                buttonsPressed += 1;
                            }

                            if (!b.BeingPressed) buttonsPressed = 0;
                        }
                    }
                    if (buttonsPressed >= buttonsToOpen) opening = true;
                    else opening = false;
                    if (!opening && Sprite.Frame > 1)
                    {
                        //Sprite.AnimationSpeed = -0.3f;
                        Sprite.Frame = 0;
                    }
                    if (!opening && Sprite.Frame <= 0)
                        Sprite.AnimationSpeed = 0;
              
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
