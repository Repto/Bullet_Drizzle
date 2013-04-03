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
        static Texture2D gruntTexture;
        static Texture2D eNBTexture;
        static Texture2D scoutTexture;
        static Texture2D interceptorTexture;
        static string[] levelSpawns = {"123454a532a412a","abcabc"};
        static int characterNo = 0;
        static int coolDown = 60;
        
        static public void setup(Texture2D inputGruntTexture, Texture2D inputENBtexture, Texture2D inputScoutTexture, Texture2D inputInterceptorTexture)
        {
            gruntTexture = inputGruntTexture;
            eNBTexture = inputENBtexture;
            scoutTexture = inputScoutTexture;
            interceptorTexture = inputInterceptorTexture;
        }
        static public void spawn(int level, Vector2 screenDimensions, List<grunt> gruntList, List<scout> scoutList, List<interceptor> interceptorList)
        {
            if (coolDown == 0)
            {
                Console.WriteLine(level);
                Console.WriteLine(characterNo);
                switch (levelSpawns[level][characterNo])
                {
                    case '0': //no spawn
                        break;
                    case '1': //two grunts
                        int noEnemies = 2;
                        for (int i = 1; i < noEnemies; i++)
                        {
                            Vector2 spawnPosition = new Vector2(screenDimensions.X, screenDimensions.Y / noEnemies * i);
                            gruntList.Add(new grunt(spawnPosition, screenDimensions, gruntTexture, eNBTexture));
                        }
                        break;
                    case '2': //three grunts
                        noEnemies = 3;
                        for (int i = 1; i < noEnemies; i++)
                        {
                            Vector2 spawnPosition = new Vector2(screenDimensions.X, screenDimensions.Y / noEnemies * i);
                            gruntList.Add(new grunt(spawnPosition, screenDimensions, gruntTexture, eNBTexture));
                        }
                        break;
                    case '3': //four grunts
                        noEnemies = 4;
                        for (int i = 1; i < noEnemies; i++)
                        {
                            Vector2 spawnPosition = new Vector2(screenDimensions.X, screenDimensions.Y / noEnemies * i);
                            gruntList.Add(new grunt(spawnPosition, screenDimensions, gruntTexture, eNBTexture));
                        }
                        break;
                    case '4': //four grunts
                        noEnemies = 6;
                        for (int i = 1; i < noEnemies; i++)
                        {
                            Vector2 spawnPosition = new Vector2(screenDimensions.X, screenDimensions.Y / noEnemies * i);
                            gruntList.Add(new grunt(spawnPosition, screenDimensions, gruntTexture, eNBTexture));
                        }
                        break;
                    case '5': //four grunts
                        noEnemies = 8;
                        for (int i = 1; i < noEnemies; i++)
                        {
                            Vector2 spawnPosition = new Vector2(screenDimensions.X, screenDimensions.Y / noEnemies * i);
                            gruntList.Add(new grunt(spawnPosition, screenDimensions, gruntTexture, eNBTexture));
                        }
                        break;
                    case 'a': //2 scouts
                        Vector2 spawnPosition2 = new Vector2(screenDimensions.X, screenDimensions.Y / 3);
                        scoutList.Add(new scout(spawnPosition2, screenDimensions, scoutTexture, eNBTexture));
                        spawnPosition2 = new Vector2(screenDimensions.X, screenDimensions.Y / 3 * 2);
                        scoutList.Add(new scout(spawnPosition2, screenDimensions, scoutTexture, eNBTexture));
                        break;
                    case 'b': //4 scouts                        
                        noEnemies = 4;
                        for (int i = 1; i < noEnemies; i++)
                        {
                            Vector2 spawnPosition = new Vector2(screenDimensions.X, screenDimensions.Y / noEnemies * i);
                            scoutList.Add(new scout(spawnPosition, screenDimensions, scoutTexture, eNBTexture));
                        }
                        break;
                    case 'c': //6 scouts                        
                        noEnemies = 6;
                        for (int i = 1; i < noEnemies; i++)
                        {
                            Vector2 spawnPosition = new Vector2(screenDimensions.X, screenDimensions.Y / noEnemies * i);
                            scoutList.Add(new scout(spawnPosition, screenDimensions, scoutTexture, eNBTexture));
                        }
                        break;
                }
                characterNo++;
                coolDown = 60;
                if (characterNo > levelSpawns[level].Length)
                {
                    characterNo = 0;
                    level++;
                }
                if (level > levelSpawns.Length)
                {
                    return;
                }
            }
            coolDown--;
        }
    }
}

