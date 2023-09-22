using DxLibDLL;
using MyLib;
using System;

namespace Shooting
{
    public class EnemyLaunchBullet : EnemyBullet
    {
        const float VisibleRadius = 8f;

        Game game;
        EnemyTrap enemyTrap;
        string type;

        //type = "poison" or "spike"
        public EnemyLaunchBullet(Game game, float x, float y, string type)
            : base(x, y)
        { 
            this.game = game;
            this.type = type;
        }
        public override void Update()
        {
            float angleToPlayer = MyMath.PointToPointAngle(x, y, game.player.x, game.player.y);
            float speed = 3f;
            x += (float)Math.Cos(angleToPlayer) * speed;
            y += (float)Math.Sin(angleToPlayer) * speed;

            TriggerTrap(game.player, type);

            if (y + VisibleRadius < 0 || y - VisibleRadius > Screen.Height ||
                x + VisibleRadius < 0 || x - VisibleRadius > Screen.Width)
            {
                isDead = true;
            }
        }

        public override void Draw()
        {
            DX.DrawRotaGraphF(x, y, 1, 0f, Image.enemyBullet16);
        }

        void TriggerTrap(Player player,string type)
        {
            if (MyMath.CircleCircleIntersection(
                player.x, player.y, player.collisionRadius * 5,
                x, y, collisionRadius * 5))
            {
                isDead = true;

                if (type == "poison")
                {
                    game.enemyTraps.Add(new EnemyTrapPoison(x, y));
                }

            }
        }
    }
}
