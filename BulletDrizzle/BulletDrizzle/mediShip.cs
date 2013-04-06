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
        float healSpeed = 0.1f;
        Texture2D beamTexture;
        //This next one is hilarious
        List<List<enemy>> ListOfEnemyLists = new List<List<enemy>>();
        List<enemy> ListOfAllEnemies = new List<enemy>();
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
        public void Heal(List<List<enemy>> totalList)
        {
            ListOfEnemyLists = totalList;
            ListOfAllEnemies.Clear();
            foreach (List<enemy> list in ListOfEnemyLists)
            {
                foreach (enemy item in list)
                {
                    ListOfAllEnemies.Add(item);
                }
            }
            
           alreadyHealing.Clear();
           impossible.Clear();
           for (int i = 0; i < maxHealsAtOneTime; i++)
           {
               if (ListOfAllEnemies.Count > 0)
               {
                   if (isHealAble(closestEnemyInList(ListOfAllEnemies, alreadyHealing, impossible)))
                   {
                       addHealth(closestEnemyInList(ListOfAllEnemies, alreadyHealing, impossible));
                       alreadyHealing.Add(closestEnemyInList(ListOfAllEnemies, alreadyHealing, impossible));
                   }
                   else { i--; impossible.Add(closestEnemyInList(ListOfAllEnemies, alreadyHealing, impossible)); }
                   if (impossible.Count + alreadyHealing.Count >= ListOfAllEnemies.Count) { return; }
               }
               else
               {
                   return;
               }
           }
       }
        public void manageBeams(List<medBeam> beamList)
        {
            foreach (enemy wounded in alreadyHealing)
            {
                beamList.Add(new medBeam(beamTexture, new Vector2(position.X + rectangle.Width / 2, position.Y + rectangle.Height / 2), rectangle, wounded.position, wounded.rectangle));
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