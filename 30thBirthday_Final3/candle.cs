using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Graphics;

namespace _30thBirthday_Final3
{
    internal class candle1
    {
        public Texture2D texture;
        public Vector2 position;
        public candle1(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;

        }
    }
}
