using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletDrizzle
{
    class mediShip : enemy
    {
        int healSpeed = 1;
        Texture2D beamTexture;
        //This next one is hilarious
        List<List<enemy>> ListOfEnemyLists = new List<List<enemy>>();
        List<enemy> alreadyHealing = new List<enemy>();
        List<enemy> impossible = new List<enemy>();
        Random randomGenerator = new Random();
        int maxHealsAtOneTime = 3;
        int healRange = 20;
        float deltaX;
        float deltaY;
        float hypotenuseSquared;
        private enemy temporaryClosestEnemy;
        private float temporarySmallestDistance;
        public mediShip(Vector2 spawnPosition, Vector2 screenDimensions, Texture2D inputTexture, Texture2D inputBeamTexture)
        {
            texture = inputTexture;
            beamTexture = inputBeamTexture;
            rectangle = new Rectangle((int)(screenDimensions.X), (int)(spawnPosition.Y), texture.Width, texture.Height);
            position.X = rectangle.X;
            position.Y = rectangle.Y;
            rectangle.Width = texture.Width;
            rectangle.Height = texture.Height;
            speed = 2;
            health = 50;
        }
        public void Heal(List<List<enemy>> totalList, List<medBeam> beamList)
        {
            ListOfEnemyLists = totalList;
            alreadyHealing.Clear();
           //Can't heal scouts, as they die after one hit, so healing them would not make them any sturdier (they'd die before being healed).
           for (int i = 0; i < maxHealsAtOneTime; i++)
           {
               int randomSection = randomGenerator.Next(0, ListOfEnemyLists.Count - 1);
               if (ListOfEnemyLists[randomSection].Count > 0)
               {
                   if (isHealAble(closestEnemyInList(ListOfEnemyLists[randomSection], alreadyHealing, impossible)))
                   {
                       addHealth(closestEnemyInList(ListOfEnemyLists[randomGenerator.Next(0, ListOfEnemyLists.Count - 1)], alreadyHealing, impossible));
                       alreadyHealing.Add(closestEnemyInList(ListOfEnemyLists[randomGenerator.Next(0, ListOfEnemyLists.Count - 1)], alreadyHealing, impossible));
                   }
                   else { i--; }
                   impossible.Add(closestEnemyInList(ListOfEnemyLists[randomGenerator.Next(0, ListOfEnemyLists.Count - 1)], alreadyHealing, impossible));
                   if (ListOfEnemyLists[randomGenerator.Next(0, ListOfEnemyLists.Count - 1)].Count == 0) { return; }
               }
               else
               {
                   i--;
               }
           }
           foreach (enemy wounded in alreadyHealing)
           {
               if (wounded != null)
               {
                   beamList.Add(new medBeam(beamTexture, position, rectangle, wounded.position, wounded.rectangle));
               }
           }
        }

        //Pythagoras etc. distance working out (having medi-ship heal closest enemy is easiest)
        public float workOutDistance(Vector2 medShip, Vector2 otherShip)
        {
            deltaX = otherShip.X - medShip.X;
            deltaY = otherShip.Y - medShip.Y;
            hypotenuseSquared = (deltaX * deltaX)+(deltaY * deltaY);
            return (float)(Math.Sqrt(hypotenuseSquared));
        }

        //Trying to make the Heal function as short as possible, because it won't be very simple.
        public bool isHealAble(enemy Enemy)
        {
            if (Enemy.health < Enemy.startingHealth) { return true; }
            else { return false; }
        }
        //Stops health overflow (although we may want that)
        public void addHealth(enemy Enemy)
        {
            if (Enemy != null)
            {
                if (Enemy.health > Enemy.startingHealth - healSpeed) { Enemy.health = Enemy.startingHealth; }
                else { Enemy.health += healSpeed; }
                Console.WriteLine("I healed Someone!");
            }
        }

        //Guts of the function: finds the enemy closest to the medship within a certain list that is not already being healed.
        public enemy closestEnemyInList(List<enemy> enemyList, List<enemy> alreadyHealing, List<enemy> impossibleList)
        {
            if (enemyList.Count > 0)
            {
                temporaryClosestEnemy = enemyList[0];
                temporarySmallestDistance = workOutDistance(this.position, enemyList[0].position);
                foreach (enemy enemyHandling in enemyList)
                {
                    if (!(alreadyHealing.Contains(enemyHandling) || impossibleList.Contains(enemyHandling)))
                    {
                        if (workOutDistance(this.position, enemyHandling.position) < temporarySmallestDistance)
                        {
                            temporaryClosestEnemy = enemyHandling;
                            temporarySmallestDistance = workOutDistance(this.position, enemyHandling.position);
                        }
                    }
                }
                return temporaryClosestEnemy;
            }
            else return null;
        }
    }
}