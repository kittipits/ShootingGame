using DxLibDLL;
using MyLib;
using System;

namespace Shooting
{
    class Zako1 : Enemy //Spider
    {
        int counter = 0;
        float theta;
        float radius;
        const int VisibleRadius = 32;


        public Zako1 (Game game, float x, float y) : base (game, x, y)
        {
            life = 15;
            theta = 0;
            radius = 5;
        }

        public override void Update()
        {
            //ゆらゆら
            theta -= 0.05f;
            y = y + (float)(Math.Sin(theta / 3) * radius / 5);

            x = x + (float)(Math.Sin(3 * theta) * radius) -1f;

            //ばらまき
            counter++;
            if (counter % 10 == 0)
            {
                float angle = MyMath.PointToPointAngle(x, y, game.player.x, game.player.y);
                game.enemyBullets.Add(new EnemyStraightBullet(x, y, angle + (MyRandom.PlusMinus(10)* MyMath.Deg2Rad), 6f));
            }

            //毒発射
            if (counter % 300 == 0) 
            {
                game.enemyBullets.Add(new EnemyLaunchBullet(game, x, y, "poison"));
            }

            if (x - VisibleRadius < 0 ||
                y + VisibleRadius < 0 ||
                y - VisibleRadius > Screen.Height)
            {
                isDead = true;
            }
        }

        public override void DropItem()
        {
            float angle = MyMath.PointToPointAngle(x, y, game.player.x, game.player.y);

            game.destructibleBullets.Add(new DestructibleSpiderBullet(game, x, y, angle, 6f));
            game.destructibleBullets.Add(new DestructibleSpiderBullet(game, x - 16, y + 16, angle, 6f));
            game.destructibleBullets.Add(new DestructibleSpiderBullet(game, x + 16, y - 16, angle, 6f));
        }

        public override void Draw()
        {
            DX.DrawRotaGraphF(x, y, 1, 0, Image.zako1);
        }

        public override void ScoreUp()
        {
            game.score += 25;
        }
    }
}
