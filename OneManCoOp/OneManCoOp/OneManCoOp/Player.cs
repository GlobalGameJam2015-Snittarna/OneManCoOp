using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneManCoOp
{
    class Player : GameObject
    {
        const float JUMP_SPEED = 13;
        const float FRICTION_GROUND = .80f;
        const float FRICTION_AIR = .92f;
        const float ACC_GROUND = 1.5f;
        const float ACC_AIR = .50f;

        new private bool CollidedOnX, CollidedOnY, isOnCorpse;

        public byte AnimationFrame { get { return Sprite.Frame; } }
        public SpriteEffects Effects { get { return Sprite.SpriteEffects; } }

        public Player(Vector2 position)
        {
            Sprite = new Sprite(TextureManager.player, position, new Vector2(32, 64), 4, new Point(32, 64), 10 / 60f);
        }

        public override void Update()
        {
            //================= ANIMATION ====================
            if (Velocity.Length() > .05f)
            {
                Sprite.AnimationSpeed = Velocity.Length() / 60;
                if (Velocity.X < 0) Sprite.SpriteEffects = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
                else if (Velocity.X > 0) Sprite.SpriteEffects = Microsoft.Xna.Framework.Graphics.SpriteEffects.None;
            }
            else
            {
                Sprite.AnimationSpeed = 0;
                Sprite.Frame = 0;
            }

            if (!CollidedOnY)
            {
                Sprite.Frame = 3;
                Sprite.AnimationSpeed = 0;
            }

            //=========== LADDERS ===========================

            bool collidesWithALadder = false;
            foreach(Ladder l in Game1.ladders)
            {
                if(Hitbox.Intersects(l.Hitbox))
                {
                    collidesWithALadder = true;
                    break;
                }
            }

            if(collidesWithALadder)
            {
                //================ LADDER MOVEMENT ==============
                Vector2 acceleration = new Vector2(0);
                if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A)) acceleration.X -= 1;
                if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D)) acceleration.X += 1;
                if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S) && !base.CollidedOnY) acceleration.Y += 1;
                if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W)) acceleration.Y -= 1;
                acceleration += Input.newGs.ThumbSticks.Left * new Vector2(1, -1);
                if (acceleration != Vector2.Zero) acceleration.Normalize();
                Velocity = acceleration * 3;
                Move(true);
            }
            else
            {
                //====================== MOVEMENT ================
                Vector2 acceleration = new Vector2(0);
                if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A)) acceleration.X -= 1;
                if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D)) acceleration.X += 1;
                if ((Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) || Input.newGs.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && !CollidedOnY) acceleration.Y -= 1;
                if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S) && !CollidedOnY) acceleration.Y += 1;
                if ((Input.KeyWasJustPressed(Microsoft.Xna.Framework.Input.Keys.Space) || Input.ButtonJustPressed(Microsoft.Xna.Framework.Input.Buttons.A)) && CollidedOnY)
                {
                    if(IsTouchingABooster())
                    {
                        Velocity -= new Vector2(0, JUMP_SPEED) * 3;

                    }
                    else Velocity -= new Vector2(0, JUMP_SPEED);
                }

                if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S) || Input.newGs.ThumbSticks.Left.Y < -.5f) isOnCorpse = false;
                if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W) || Input.newGs.ThumbSticks.Left.Y > .5f) isOnCorpse = true;
                acceleration.X += Input.newGs.ThumbSticks.Left.X;

                if (acceleration != Vector2.Zero) acceleration.Normalize();

                Velocity *= new Vector2((CollidedOnY) ? FRICTION_GROUND : FRICTION_AIR, 1);

                Velocity += acceleration * (CollidedOnY ? ACC_GROUND : ACC_AIR);
                 

                Velocity += new Vector2(0, Game1.GRAVITY);

                Move();
            }
        }

        bool IsTouchingABooster()
        {
            foreach (Booster b in Game1.boosters) if (Hitbox.Intersects(b.Hitbox))
                {
                    b.Extend();
                    return true;
                }
            return false;
        }

        new void Move()
        {
            CollidedOnX = CollidedOnY = false;

            List<Tile> solidTiles = CloseSolidTiles;

            Move(Velocity.X, 0);
            int x = Velocity.X.CompareTo(0);
            if (x == 0) x = 1;
            while (IsCollidingWithAny(solidTiles))
            {
                Move(-x, 0);
                Velocity = new Vector2(0, Velocity.Y);
                CollidedOnX = true;
            }

            int y = Velocity.Y.CompareTo(0);
            for (int yi = 0; yi < Math.Abs(Velocity.Y); yi++)
            {
                Move(0, y);
                if (IsCollidingWithAny(solidTiles))
                {
                    Move(0, -y);
                    Velocity = new Vector2(Velocity.X, 0);
                    CollidedOnY = true;
                    isOnCorpse = false;
                    break;
                }
                if(WillTouchCorpse() && y > 0 && (isOnCorpse))
                {
                    Move(0, -y);
                    Velocity = new Vector2(Velocity.X, 0);
                    CollidedOnY = true;
                    isOnCorpse = true;
                    break;
                }
            }
        }

        bool WillTouchCorpse()
        {
            foreach (Corpse c in Game1.corpses)
            {
                if (!Hitbox.Intersects(c.Hitbox))
                {
                    Move(0, 1);
                    if (Hitbox.Intersects(c.Hitbox))
                    {
                        Move(0, -1);
                        return true;
                    }
                    Move(0, -1);
                }
            }
            //isOnCorpse = false;
            return false;
        }
    }
}
