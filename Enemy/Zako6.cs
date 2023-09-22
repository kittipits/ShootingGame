using DxLibDLL;
using MyLib;
using System;

namespace Shooting
{
    public class Zako6 : Enemy //Screamer
    {
        int counter = 0;
        private int state;
        private int moveCount;

        float theta;
        float radius;
        const int VisibleRadius = 32;

        public Zako6(Game game, float x, float y) : base(game, x, y)
        {
            life = 25;

            theta = 0;
            radius = 2;

            state = 0;
            moveCount = 0;
        }

        public override void Update()
        {
            moveCount++;
            counter++;
            theta += 0.05f;

            //なんらかの運動!! そのつもりではないが、面白そう。
            if (moveCount >= 30)
                x -= 1f;

            if (state == 0)
            {
                if (moveCount >= 30)
                    x = x + (float)(Math.Cos(theta) * radius);
                    y = y + (float)(Math.Sin(theta) * radius);

                if (moveCount == 90)
                {
                    state = 1;
                    moveCount = 0;
                }
            }
            else if (state == 1)
            {
                if (moveCount >= 30)
                    x = x - (float)(Math.Cos(theta) * radius);
                    y = y - (float)(Math.Sin(theta) * radius);

                if (moveCount == 90)
                {
                    state = 0;
                    moveCount = 0;
                }

                if (x - VisibleRadius < 0 ||
                    y + VisibleRadius < 0 ||
                    y - VisibleRadius > Screen.Height)
                {
                    isDead = true;
                }
            }

            //止まったら、悲鳴発射
            if (moveCount < 30)
            {
                if (counter % 10 == 0)
                {
                    ShootEnemyBulletsWays(4, 0f, 10f, 4f);
                    ShootEnemyBulletsWays(4, 180f * MyMath.Deg2Rad, 10f, 4f);
                }
            }

            //動いたら, 渦巻き発射
            else if (moveCount >= 30)
            {
                if (counter % 3 == 0)
                {
                    game.enemyBullets.Add(new EnemyStraightBullet(x, y, theta, 6f));
                    game.enemyBullets.Add(new EnemyStraightBullet(x, y, theta + (180f * MyMath.Deg2Rad), 6f));

                }
  
            }

        }

        public override void Draw()
        {
            DX.DrawRotaGraphF(x, y, 1, 0, Image.zako6);
        }

        /// <summary>
        /// N Ways 弾を発射する
        /// </summary>
        /// <param name="numWays">同時発射数</param>
        /// <param name="standardAngle">基本となる角度</param>
        /// <param name="deltaAngle">弾ごとの角度差</param>
        /// <param name="speed">弾の速さ</param>
        void ShootEnemyBulletsWays(int numWays, float standardAngle, float deltaAngle, float speed)
        {
            float startAngle = (numWays - 1) * deltaAngle / 2.0f;

            for (int i = 0; i < numWays; i++)
            {
                float firingAngle = (startAngle - deltaAngle * i) * MyMath.Deg2Rad;
                game.enemyBullets.Add(new EnemyStraightBullet(x, y, standardAngle + firingAngle, speed));
            }
        }

        public override void ScoreUp()
        {
            game.score += 50;
        }
    }
}
