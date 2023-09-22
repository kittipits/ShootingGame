using DxLibDLL;
using MyLib;
using System;

namespace Shooting
{
    class Zako2 : Enemy //miniSpider
    {
        const int VisibleRadius = 16;

        float angleToPlayer;
        int stepbackTime;

        public Zako2 (Game game, float x, float y) : base (game, x, y)
        {
            life = 5;
            collisionRadius = 16;
            angleToPlayer = MyMath.PointToPointAngle(x, y, game.player.x, game.player.y);
            stepbackTime = 0;
        }

        public override void Update()
        {
            stepbackTime++;

            float speed = 6f;
            x += (float)Math.Cos(angleToPlayer) * speed;
            y += (float)Math.Sin(angleToPlayer) * speed;

            
            if (MyMath.CircleCircleIntersection(
                game.player.x, game.player.y, game.player.collisionRadius,
                x, y, collisionRadius))
            {
                angleToPlayer = MyMath.PointToPointAngle(x, y, Screen.Width, MyRandom.Range(0, Screen.Height));
                stepbackTime = 0;

            }
            else if (stepbackTime > 45)
            {
                angleToPlayer = MyMath.PointToPointAngle(x, y, game.player.x, game.player.y);
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
            DX.DrawRotaGraphF(x, y, 1, 0, Image.zako2);
        }

        public override void ScoreUp()
        {
            game.score += 10;
        }
    }
}
