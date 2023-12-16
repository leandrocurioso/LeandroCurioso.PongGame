using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LeandroCurioso.PongGame
{
    public class Ball
    {
        Game game;
        Vector2 position;
        Vector2 velocity;
        Texture2D texture;
        bool outOfBounds = false;
        public Vector2 Velocity { get => velocity; }
        public bool OutOfBounds { get => outOfBounds; }
        public const float BALL_VELOCITY = 2.5F;

        public Ball(Game game, Texture2D texture)
        {
            this.game = game;
            this.texture = texture;
        }

        public void SetStartPosition()
        {
            var viewPort = game.GraphicsDevice.Viewport;
            position.X = (viewPort.Width / 2) - (texture.Width / 2);
            position.Y = (viewPort.Height / 2) - (texture.Height / 2);
            velocity = new Vector2(BALL_VELOCITY);
            outOfBounds = false;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void SetPosition(float x)
        {
            position.X = x;
        }

        public void InvertVelocity()
        {
            velocity.X += velocity.X * 0.3F;
            velocity.X *= -1;
        }

        public void Update()
        {
            var viewPort = game.GraphicsDevice.Viewport;

            if (position.Y < 0)
            {
                position.Y = 0;
                velocity.Y *= -1;
            }

            if (position.Y + texture.Height > viewPort.Height)
            {
                position.Y = viewPort.Height - texture.Height;
                velocity.Y *= -1;
            }

            position += velocity;

            if (position.X + texture.Width < 0 || position.X > viewPort.Width)
            {
                outOfBounds = true;
                velocity = Vector2.Zero;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
