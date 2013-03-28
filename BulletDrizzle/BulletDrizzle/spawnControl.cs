using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletDrizzle
{
    static class spawnControl
    {
        //Texture2D gruntTexture;
        //static public void setup(Texture2D grunt, eNBtex)
        static public void spawn(string input, Vector2 screenDimensions, List<grunt> gruntList)
        {
            for (int count = 0; count < input.Count(); count++)
            {
                switch (input[count])
                {
                    case '0': //0 = no spawn
                        break;
                    case '1':
                        //Vector2 spawnPosition = new Vector2(screenDimensions.X, );
                        //gruntList.Add(new grunt(spawnPosition, screenDimensions, gruntTexture, eNBTexture));
                        break;
                }
            }
        }
    }
}
