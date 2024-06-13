using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading.Channels;

namespace anotherCollisiontest {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D obj;
        Vector2 position;
        List<ScriptableObj> tiles;
        ScriptableObj player;
        int speed;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            position = new Vector2(0, 0);
            speed = 1;
            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            obj = Content.Load<Texture2D>("obj");
            tiles = new List<ScriptableObj>();
            tiles.Add(new ScriptableObj(new Vector2(30, 160), obj, 40));
            tiles.Add(new ScriptableObj(new Vector2(200, 40), obj, 40));
            player = new ScriptableObj(position, obj, 40);
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            int[] move = {0, 0};
            if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                move[1] -= speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                move[1] += speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                move[0] -= speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                move[0] += speed;
            }
            Move(move, tiles);
            player.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            player.Draw(_spriteBatch);

            foreach(var tile in tiles) {
                tile.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        void Move(int[] move, List<ScriptableObj> tiles) {
            player.pos.X += move[0];
            List<Rectangle> collided = collissions(player.rect, tiles);
            foreach (var tile in collided) {
                if (move[0] > 0) {
                    player.pos.X = -player.rect.Width + tile.Left;
                }
                if (move[0] < 0) {
                    player.pos.X = tile.Right;
                }

            }
            player.pos.Y += move[1];
            collided = collissions(player.rect, tiles);
            foreach (var tile in collided) {
                if (move[1] > 0) {
                    player.pos.Y = player.rect.Height + tile.Top; 
                }
                if (move[1] < 0) {
                    player.pos.Y = tile.Bottom;
                }

            }
        }

        List<Rectangle> collissions(Rectangle rect, List<ScriptableObj> tiles) {
            List<Rectangle> collided = new List<Rectangle>();
            foreach(var tile in tiles) {
                if (rect.Intersects(tile.rect)) {
                    collided.Add(tile.rect);
                }
            }
            return collided;
        }
    }
}