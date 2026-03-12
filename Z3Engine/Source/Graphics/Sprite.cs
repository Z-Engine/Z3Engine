using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Z3Engine.Source.Graphics
{
    internal class Sprite : StaticSprite
    {
        protected Rectangle sourceRect;

        public Sprite(Texture2D texture, Rectangle destRect, Rectangle sourceRect, Color color) : base(texture, destRect, color)
        {
            // Error check
            if (texture == null) texture = Global.noImg;

            // Initialize sprite
            SetTexture(texture);
            SetDestRect(destRect);
            SetSourceRect(sourceRect);
            SetColor(color);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GetTexture(), GetDestRect(), sourceRect, GetColor());
        }

        // Getters

        public Rectangle GetSourceRect()
        {
            return this.sourceRect;
        }

        // Setters

        public void SetSourceRect(Rectangle newRect)
        {
            this.sourceRect = newRect;
        }
    }
}
