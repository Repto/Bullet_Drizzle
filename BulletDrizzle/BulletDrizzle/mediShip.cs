﻿using System;
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
        int maxHealsAtOneTime = 3;
        int healRange = 20;
        float deltaX;
        float deltaY;
        float hypotenuseSquared;
        private enemy temporaryClosestEnemy;
        private float temporarySmallestDistance;
        public mediShip(Vector2 spawnPosition, Vector2 screenDimensions, Texture2D inputTexture)
        {
            texture = inputTexture;
            rectangle = new Rectangle((int)(screenDimensions.X), (int)(spawnPosition.Y), texture.Width, texture.Height);
            position.X = rectangle.X;
            position.Y = rectangle.Y;
            rectangle.Width = texture.Width;
            rectangle.Height = texture.Height;
            speed = 2;
            health = 50;
            enemyType = 0;
        }
        public void Heal()
        {
           //Can't heal scouts, as they die after one hit, so healing them would not make them any sturdier (they'd die before being healed).
           
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
            if (Enemy.health > Enemy.startingHealth - healSpeed) { Enemy.health = Enemy.startingHealth; }
            else { Enemy.health += healSpeed; }
        }

        //Guts of the function: finds the enemy closest to the medship within a certain list.
        public enemy closestEnemyInList(List<enemy> enemyList)
        {
            temporaryClosestEnemy = enemyList[0];
            temporarySmallestDistance = workOutDistance(this.position, enemyList[0].position);
            foreach (enemy enemyHandling in enemyList)
            {
                if (workOutDistance(this.position, enemyHandling.position) < temporarySmallestDistance)
                {
                    temporaryClosestEnemy = enemyHandling;
                    temporarySmallestDistance = workOutDistance(this.position, enemyHandling.position);
                }
            }
            return temporaryClosestEnemy;
        }
    }
}

