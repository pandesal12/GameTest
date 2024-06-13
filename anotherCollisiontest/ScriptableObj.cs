using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Channels;


namespace anotherCollisiontest {
    internal class ScriptableObj {

        public Vector2 pos;
        public Rectangle rect;
        Texture2D texture;
        int dimension;
        public ScriptableObj(Vector2 pos, Texture2D texture, int dimension) {
            this.pos = pos;
            this.texture = texture;
            this.dimension = dimension;
            this.rect = new Rectangle((int)pos.X, (int)pos.Y, dimension, dimension);
        }

        public void Update() {
            this.rect = new Rectangle((int)pos.X, (int)pos.Y, dimension, dimension);
        }

        public void Draw(SpriteBatch _sprite) {
            _sprite.Draw(texture, pos, new Rectangle(0, 0, dimension, dimension), Color.White);
        }


    }
}
