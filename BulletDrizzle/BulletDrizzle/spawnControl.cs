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
        static Texture2D mediTexture;
        static Texture2D mediBeamTexture;
        //Breaks are needed after some enemies because they are slow. f00 and g0 are necessary. Info: 6 = max grunts. e = max scouts. f = medic. g = interceptor
        static string[] levelSpawns = {"123456abcd123456f00f00abcd123456g0g0g0abccd1234566f006f006a1b2c3d4d5e6e6e6e6e6f6ef6ef6ef6ef6ef6e"};
        public static int characterNo = 0;
        static int coolDown = 60;
        public static bool levelOver = false;

        static public void setup(Texture2D inputGruntTexture, Texture2D inputENBtexture, Texture2D inputScoutTexture, Texture2D inputInterceptorTexture, Texture2D inputMediTexture, Texture2D inputMediBeamTexture)
        {
            gruntTexture = inputGruntTexture;
            eNBTexture = inputENBtexture;
            scoutTexture = inputScoutTexture;
            interceptorTexture = inputInterceptorTexture;
            mediTexture = inputMediTexture;
            mediBeamTexture = inputMediBeamTexture;
        }
        static public void spawn(int level, Vector2 screenDimensions, List<grunt> gruntList, List<scout> scoutList, List<interceptor> interceptorList, List<mediShip> mediList, bool waitLetGo)
        {
            if (coolDown == 0 && characterNo < (levelSpawns[level].Length))
            {
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
                    case '4': //six grunts
                        noEnemies = 6;
                        for (int i = 1; i < noEnemies; i++)
                        {
                            Vector2 spawnPosition = new Vector2(screenDimensions.X, screenDimensions.Y / noEnemies * i);
                            gruntList.Add(new grunt(spawnPosition, screenDimensions, gruntTexture, eNBTexture));
                        }
                        break;
                    case '5': //eight grunts
                        noEnemies = 8;
                        for (int i = 1; i < noEnemies; i++)
                        {
                            Vector2 spawnPosition = new Vector2(screenDimensions.X, screenDimensions.Y / noEnemies * i);
                            gruntList.Add(new grunt(spawnPosition, screenDimensions, gruntTexture, eNBTexture));
                        }
                        break;
                    case '6': //twelve grunts
                        noEnemies = 12;
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
                    case 'd': // 8 scouts
                        noEnemies = 8;
                        for (int i = 1; i < noEnemies; i++)
                        {
                            Vector2 spawnPosition = new Vector2(screenDimensions.X, screenDimensions.Y / noEnemies * i);
                            scoutList.Add(new scout(spawnPosition, screenDimensions, scoutTexture, eNBTexture));
                        }
                        break;
                    case 'e': // 12 scouts
                        noEnemies = 12;
                        for (int i = 1; i < noEnemies; i++)
                        {
                            Vector2 spawnPosition = new Vector2(screenDimensions.X, screenDimensions.Y / noEnemies * i);
                            scoutList.Add(new scout(spawnPosition, screenDimensions, scoutTexture, eNBTexture));
                        }
                        break;
                    case 'f': //1 medic
                        noEnemies = 2;
                        for (int i = 1; i < noEnemies; i++)
                        {
                            Vector2 spawnPosition = new Vector2(screenDimensions.X, (screenDimensions.Y / noEnemies * i) - mediTexture.Height / 2);
                            mediList.Add(new mediShip(spawnPosition, screenDimensions, mediTexture, mediBeamTexture));
                        }
                        break;
                    case 'g': //1 interceptor
                        noEnemies = 2;
                        for (int i = 1; i < noEnemies; i++)
                        {
                            Vector2 spawnPosition = new Vector2(screenDimensions.X, (screenDimensions.Y / noEnemies * i) - interceptorTexture.Height / 2);
                            interceptorList.Add(new interceptor(spawnPosition, screenDimensions, interceptorTexture, eNBTexture));
                        }
                        break;
                }
                characterNo++;
                coolDown = 60;
                if (characterNo == levelSpawns[level].Length)
                {
                    levelOver = true;
                    waitLetGo = false;
                }
                if (level == levelSpawns.Length)
                {
                    return;
                }
            }
            coolDown--;
        }
    }
}

