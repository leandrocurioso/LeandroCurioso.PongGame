using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LeandroCurioso.PongGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Ball Ball { get; set; }

        Texture2D goalTexture;
        Point centerScreen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            centerScreen = new Point(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D barTexture = Content.Load<Texture2D>("assets/bar");
            Texture2D ballTexture = Content.Load<Texture2D>("assets/ball");
            goalTexture = Content.Load<Texture2D>("assets/goal");
            Ball = new Ball(this, ballTexture);
            Ball.SetStartPosition();
            Player1 = new Player(this, new Vector2(10, 100), barTexture, Keys.W, Keys.S);
            Player2 = new Player(this, new Vector2(760, 100), barTexture, Keys.Up, Keys.Down);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (Ball.OutOfBounds)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    Ball.SetStartPosition();
                }
            }

            Player1.Update();
            Player2.Update();
            Ball.Update();
            Player1.HasCollided(Ball);
            Player2.HasCollided(Ball);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            Player1.Draw(_spriteBatch);
            Player2.Draw(_spriteBatch);
            Ball.Draw(_spriteBatch);


            if (Ball.OutOfBounds)
            {
                _spriteBatch.Draw(
                    goalTexture,
                    new Vector2(centerScreen.X - goalTexture.Width / 2, centerScreen.Y - goalTexture.Height / 2),
                    Color.White
                );
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
