using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    enum PlayerState { Disabled = 0, Active, Explosion }

    class Player
    {
        const int MAX_SHOTS = 20;
        const int MAX_SHOTS1 = 20;
        const int MAX_SHOTS2 = 20;
        const int MAX_LIFE = 500;

        public Rectangle Bounds { get; private set; }
        public static bool Life { get; set; }
        public static Vector2 Pos { get; private set; }

        Vector2 position;
        Vector2 speed;
        Texture2D texture;
        Color color;

        int life;

        Texture2D bLife;
        Texture2D bMana;
        Texture2D barS;
        Texture2D bNoLife;
        Texture2D bNoMana;

        Explosion explosion;
        Shot[] shots;
        Shot1[] shots1;
        Shot2[] shots2;

        int framesCounter;
        int framesCounter1;
        int framesCounter2;

        PlayerState state;

        public Player(Texture2D texture, Texture2D texExplo, Texture2D texShot,
                      Texture2D barlife01, Texture2D barmana01, Texture2D barplayer,
                      Texture2D barlife02, Texture2D barmana02, Texture2D superShot1,
                      Texture2D superShot2)
        {
            this.texture = texture;
            position = new Vector2(100, SpaceGame.SCREEN_HEIGHT / 2 - texture.Height / 2);
            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            speed = new Vector2(8);
            color = Color.White;
            life = 500;

            Life = false;

            barS = barplayer;
            bLife = barlife01;
            bMana = barmana01;
            bNoLife = barlife02;
            bNoMana = barmana02;

            framesCounter = 20;
            framesCounter1 = 20;
            framesCounter2 = 80;

            explosion = new Explosion(texExplo, 5, 1);

            //SHOTS
            shots = new Shot[MAX_SHOTS];

            for(int i = 0; i < MAX_SHOTS; i++) shots[i] = new Shot(texShot);

            shots1 = new Shot1[MAX_SHOTS1];

            for(int i = 0; i < MAX_SHOTS1; i++) shots1[i] = new Shot1(superShot1);

            shots2 = new Shot2[MAX_SHOTS2];

            for(int i = 0; i < MAX_SHOTS2; i++) shots2[i] = new Shot2(superShot2);

            state = PlayerState.Active;
        }

        public void Update(InputManager input)
        {
            switch(state)
            {
                case PlayerState.Disabled:
                    {
                        Reset();
                    }
                    break;
                case PlayerState.Active:
                    {
                        //PLAYER MOVE
                        if(input.IsKeyDown(Keys.Right)) position.X += speed.X;
                        if(input.IsKeyDown(Keys.Left)) position.X -= speed.X;
                        if(input.IsKeyDown(Keys.Down)) position.Y += speed.Y;
                        if(input.IsKeyDown(Keys.Up)) position.Y -= speed.Y;

                        if(input.IsKeyDown(Keys.LeftShift)) speed = new Vector2(15);
                        if(input.IsKeyUp(Keys.LeftShift)) speed = new Vector2(8);

                        Pos = position;

                        //PLAYER LIMITS
                        if(position.X <= 0) position.X = 0;
                        else if(position.X >= (SpaceGame.SCREEN_WIDTH - texture.Width)) position.X = SpaceGame.SCREEN_WIDTH - texture.Width;
                        else if(position.Y <= 50) position.Y = 50;
                        if(position.Y >= (SpaceGame.SCREEN_HEIGHT - texture.Height - 50)) position.Y = SpaceGame.SCREEN_HEIGHT - texture.Height - 50;

                        //SHOT
                        //NORMAL
                        if(input.IsKeyDown(Keys.D))
                        {
                            framesCounter++;

                            if(framesCounter >= 20)
                            {
                                for(int i = 0; i < MAX_SHOTS; i++)
                                {
                                    if(!shots[i].IsActive)
                                    {
                                        shots[i].Fire(position + new Vector2(100, 50));
                                        framesCounter = 0;

                                        break;
                                    }
                                }
                            }
                        }
                        else framesCounter = 20;

                        //UP
                        if(input.IsKeyDown(Keys.W))
                        {
                            framesCounter1++;

                            if(framesCounter1 >= 20)
                            {
                                for(int i = 0; i < MAX_SHOTS1; i++)
                                {
                                    if(!shots1[i].IsActive)
                                    {
                                        shots1[i].Fire(position);
                                        framesCounter1 = 0;

                                        break;
                                    }
                                }
                            }
                        }
                        else framesCounter1 = 20;

                        //SUPERSHOT
                        if(input.IsKeyDown(Keys.S))
                        {
                            framesCounter2++;

                            if(framesCounter2 >= 80)
                            {
                                for(int i = 0; i < MAX_SHOTS2; i++)
                                {
                                    if(!shots2[i].IsActive)
                                    {
                                        shots2[i].Fire(position);
                                        framesCounter2 = 0;

                                        break;
                                    }
                                }
                            }
                        }
                        else framesCounter2 = 80;

                        //UPDATE BOUNDS
                        Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                    }
                    break;
                case PlayerState.Explosion:
                    {
                        //UPDATE EXPLOSION
                        explosion.Update();

                        if(!explosion.IsActive) state = PlayerState.Disabled;

                        Life = true;
                    }
                    break;
                default:
                    break;
            }

            // Update shots
            for(int i = 0; i < MAX_SHOTS; i++) shots[i].Update();
            for(int i = 0; i < MAX_SHOTS1; i++) shots1[i].Update();
            for(int i = 0; i < MAX_SHOTS2; i++) shots2[i].Update();
        }

        public void Draw(SpriteBatch sb)
        {
            switch(state)
            {
                case PlayerState.Disabled: break;
                case PlayerState.Active:
                    {
                        // DRAW PLAYER
                        sb.Draw(texture, position, color);

                        // DRAW LIFE
                        sb.Draw(barS, new Vector2(0), Color.White);

                        sb.Draw(bNoLife, new Rectangle(0, 0, MAX_LIFE, 50), Color.White);
                        sb.Draw(bLife, new Rectangle(0, 0, life, 50), Color.White);
                    }
                    break;
                case PlayerState.Explosion:
                    {
                        // DRAW EXPLOSION
                        explosion.Draw(sb);
                    }
                    break;
                default:
                    break;
            }

            //DRAW SHOTS
            for(int i = 0; i < MAX_SHOTS; i++) shots[i].Draw(sb);
            for(int i = 0; i < MAX_SHOTS1; i++) shots1[i].Draw(sb);
            for(int i = 0; i < MAX_SHOTS2; i++) shots2[i].Draw(sb);
#if DEBUG
            //sb.Draw(SpaceGame.Pixel, Bounds, Color.Red * 0.5f);
#endif
        }

        public bool CheckCollision(Rectangle bounds)
        {
            bool collision = false;

            //COLLISION PLAYER BOUNDS
            if((state == PlayerState.Active) && this.Bounds.Intersects(bounds))
            {
                collision = true;
            }

            return collision;
        }

        public bool CheckCollisionShots(Rectangle bounds)
        {
            bool collision = false;

            //COLLISION PLAYER SHOTS
            for(int i = 0; i < MAX_SHOTS; i++)
            {
                if(shots[i].IsActive && shots[i].Bounds.Intersects(bounds))
                {
                    collision = true;
                    shots[i].Reset();
                    break;
                }
            }

            return collision;
        }

        public bool CheckCollisionShots1(Rectangle bounds)
        {
            bool collision = false;

            //COLLISION PLAYER SHOTS
            for(int i = 0; i < MAX_SHOTS1; i++)
            {
                if(shots1[i].IsActive && shots1[i].Bounds.Intersects(bounds))
                {
                    collision = true;
                    shots1[i].Reset();
                    break;
                }
            }

            return collision;
        }

        public bool CheckCollisionShots2(Rectangle bounds)
        {
            bool collision = false;

            //COLLISION PLAYER SHOTS
            for(int i = 0; i < MAX_SHOTS2; i++)
            {
                if(shots2[i].IsActive && shots2[i].Bounds.Intersects(bounds))
                {
                    collision = true;
                    shots2[i].Reset();
                    break;
                }
            }

            return collision;
        }

        public void ReceiveDamage(int damage)
        {
            life -= damage;

            if(life <= 0)
            {
                life = 0;
                explosion.Explode(position);
                state = PlayerState.Explosion;

                SpaceGame.CurrentScreen = GameScreen.Lose;
            }
        }

        public void Reset()
        {
            position = new Vector2(100, 200);
            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            life = MAX_LIFE;
            state = PlayerState.Active;
        }
    }
}

