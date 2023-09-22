using DxLibDLL;
using MyLib;
using System;

namespace Shooting
{
    public class Zako7 : Enemy //Slime
    {
        const int VisibleRadius = 32;
        public Zako7(Game game, float x, float y) : base(game, x, y)
        {
            life = 20;
        }

        public override void Update()
        {
            // プレイヤーに向かってくる
            float angleToPlayer = MyMath.PointToPointAngle(x, y, game.player.x, game.player.y);
            float speed = 3f;
            x += (float)Math.Cos(angleToPlayer) * speed;
            y += (float)Math.Sin(angleToPlayer) * speed;

            if (MyMath.CircleCircleIntersection(
                game.player.x, game.player.y, game.player.collisionRadius,
                x, y, collisionRadius))
            {
                isDead = true;
                game.explosions.Add(new Explosion(x, y));
                game.enemyTraps.Add(new EnemyTrapPoison(x, y));
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
            DX.DrawRotaGraphF(x, y, 1, 0, Image.zako7);
        }

        public override void DropItem()
        {
            game.enemyTraps.Add(new EnemyTrapPoison(x, y));
        }

        public override void OnCollisionPlayer(Player player)
        {
        }

        public override void ScoreUp()
        {
            game.score += 25;
        }
    }
}
