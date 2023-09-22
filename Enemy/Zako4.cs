using DxLibDLL;
using MyLib;

namespace Shooting
{
    public class Zako4 : Enemy　//Skeleton
    {
        int counter = 0;
        private int state;
        private int moveCount;
        const int VisibleRadius = 32;

        public Zako4(Game game, float x, float y) : base(game, x, y)
        {
            life = 15;
            state = 0;
            moveCount = 0;
        }

        public override void Update()
        {
            moveCount++;
            counter++;

            //三角運動
            x -= 1f;

            if (state == 0)
            {
                y += 3;

                if (moveCount == 60)
                {
                    state = 1;
                    moveCount = 0;
                }
            }
            else if (state == 1)
            {
                x -= 2;
                y -= 1;

                if (moveCount == 60)
                {
                    state = 2;
                    moveCount = 0;
                }
            }
            else if (state == 2)
            {
                x += 2;
                y -= 2;

                if (moveCount == 60)
                {
                    state = 0;
                    moveCount = 0;
                }
            }


            //3 ways            
            if (counter % 60 == 0)
            {
                float angle = MyMath.PointToPointAngle(x, y, game.player.x, game.player.y);
                ShootEnemyBulletsWays(3, angle, 10f, 8f);
            }

            if (x - VisibleRadius < 0 ||
                y + VisibleRadius < 0 ||
                y - VisibleRadius > Screen.Height)
            {
                isDead = true;
            }
        }

        public override void Draw()
        {
            DX.DrawRotaGraphF(x, y, 1, 0, Image.zako4);
        }

        public override void DropItem()
        {
            game.items.Add(new LifeUpItem(game, x, y));
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
            game.score += 25;
        }

    }
}
