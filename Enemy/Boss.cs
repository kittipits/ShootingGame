using DxLibDLL;
using MyLib;
using System;

namespace Shooting
{
    public class Boss : Enemy
    {
        enum State
        {
            Appear,
            Normal,
            Swoon,
            Angry,
            Dying,
        }

        State state = State.Appear;
        int swoonTime = 120;
        int dyingTime = 180;
        int normalTime = 0;
        int angryTime = 0;
        float intialLife;

        float bodyAngle = 0.0f;
        float centerX;
        float centerY;

        float vx;
        float vy;
        float restartX;
        float restartY;
        int restartTime = 40;
        int moveState;

        int animationCounter = 0;
        int[] enemyMotion = new int[5] { 1, 2, 1, 0, 1};
        int motionIndex = 1;

        public Boss(Game game, float x, float y) : base(game, x, y)
        {
            life = 400;
            intialLife = life;
            collisionRadius = 50;

            vx = 0f;
            vy = 0f;

            moveState = 0;
        }

        public override void Update()
        {
            if (state == State.Appear)
            {
                x -= 1;

                if (x <= 750)
                {
                    state = State.Normal;

                    restartX = x;
                    restartY = y;

                    vx = 0f;
                    vy = 0f;
                }
            }
            else if (state == State.Normal)
            {
                normalTime++;

                //星移動
                x += vx;
                y += vy;

                if (normalTime >= 30)
                {
                    if (moveState == 0)
                    {
                        vx = -3f;
                        vy = -1f;
                        if (normalTime == 120)
                        {
                            moveState = 1;
                            normalTime = 30;
                        }
                    }
                    else if (moveState == 1)
                    {
                        vx = +2f;
                        vy = +3f;
                        if (normalTime == 120)
                        {
                            moveState = 2;
                            normalTime = 30;
                        }
                    }
                    else if (moveState == 2)
                    {
                        vx = 0f;
                        vy = -4f;
                        if (normalTime == 120)
                        {
                            moveState = 3;
                            normalTime = 30;
                        }
                    }
                    else if (moveState == 3)
                    {
                        vx = -2f;
                        vy = 3f;
                        if (normalTime == 120)
                        {
                            moveState = 4;
                            normalTime = 30;
                        }
                    }
                    else if (moveState == 4)
                    {
                        vx = 3f;
                        vy = -1f;
                        if (normalTime == 120)
                        {
                            moveState = 0;
                            normalTime = 0;
                        }
                    }
                }
                else if (normalTime < 30)
                {
                    vx = 0f;
                    vy = 0f;
                }

                //ハート発射
                if (normalTime % 40 == 0)
                {
                    HeartBullet(180f + MyRandom.PlusMinus(15), 6f);
                    HeartBullet(210f + MyRandom.PlusMinus(15), 6f);
                    HeartBullet(150f + MyRandom.PlusMinus(15), 6f);
                }

                //コウモリ投げ
                if (normalTime < 30)
                {
                    if (normalTime % 12 == 0)
                    {
                        float angle = MyMath.PointToPointAngle(x, y, game.player.x, game.player.y);
                        game.destructibleBullets.Add(new DestructibleBatBullet(game, x + 90, y, angle, 10f));
                    }

                }
            }
            else if (state == State.Swoon)
            {
                if (swoonTime == 100)
                {
                    DropItem();
                }

                swoonTime--; ;


                if (swoonTime > restartTime)
                {
                    x += vx;
                    y += vy;

                    vx *= 0.995f;
                    vy *= 0.995f;
                }
                else
                {
                    const float Agility = 0.07f;
                    x = x + (restartX - x) * Agility;
                    y = y + (restartY - y) * Agility;
                }

                if (swoonTime <= 0)
                {
                    state = State.Angry;

                    x = restartX;
                    y = restartY;

                    centerX = x;
                    centerY = y;
                }
            }
            else if (state == State.Angry)
            {
                angryTime++;

                float rushMotionTheta = 1.0f * angryTime * MyMath.Deg2Rad;

                //x = centerX + 300f * ((float)Math.Cos(2f * rushMotionTheta) - 1f);
                //y = centerY + 150f * (float)Math.Sin(rushMotionTheta);

                float spiral = (float)(Math.Sin(rushMotionTheta / 10) * Math.Sin(rushMotionTheta / 10));
                x = centerX + 240f * ((float)Math.Cos(3f * rushMotionTheta) - 1f) + (50f * spiral);
                y = centerY + 120f * (float)Math.Sin(rushMotionTheta + Math.PI) + spiral;

                if (angryTime % 10 == 0)
                {
                    HeartBullet(180f + MyRandom.PlusMinus(180), 8f);
                    HeartBullet(0f + MyRandom.PlusMinus(180), 8f);
                }

                if (angryTime % 60 == 0)
                {
                    float angle = MyMath.PointToPointAngle(x, y, game.player.x, game.player.y);
                    game.destructibleBullets.Add(new DestructibleBatBullet(game, x + 64, y, angle, 10f));
                }
                while (game.bloods.Count == 0 && game.enemyTraps.Count == 0)
                {
                    SetTrap(15);
                }
            }
            else if (state == State.Dying)
            {
                dyingTime--;

                // ①  傾く
                bodyAngle += (0.25f * MyMath.Deg2Rad);

                // ② 震える
                // 「タイマー」を「震えるときの角度」に変換
                float vibrationTheta = 2.8f * dyingTime * MyMath.Deg2Rad;
                x = centerX + (float)(Math.Cos(vibrationTheta * 43) * 3f);
                y = centerY + (float)(Math.Sin(vibrationTheta * 37) * 3f);

                // ③  沈む
                centerY += 0.5f;

                // ④  爆発を出す
                if (dyingTime % 5 == 0)
                {
                    Explode();
                }

                if (dyingTime <= 0)
                {
                    // ⑤  消える瞬間に大爆発を起こす
                    for (int i = 0; i < 30; i++)
                    {
                        Explode();
                    }

                    for (int i = 0; i < 360; i += 10)
                    {
                        game.destructibleBullets.Add(new DestructibleBatBullet(game, x, y, (float)(i * MyMath.Deg2Rad), 12f));
                    }

                    isDead = true;
                    game.score += 300;
                    game.clearFlag = true;
                }
            }

        }



        public override void Draw()
        {
            animationCounter++;
            motionIndex = motionIndex % 4;
            if (animationCounter % 10 == 0) motionIndex++;

            if (state == State.Appear || state == State.Normal)
            {
                DX.DrawRotaGraphF(x, y, 1, 0f, Image.boss1[enemyMotion[motionIndex]]);
            }
            else if (state == State.Swoon)
            {
                DX.DrawRotaGraphF(x, y, 1, 0f, Image.boss2);
            }
            else if (state == State.Angry)
            {
                DX.DrawRotaGraphF(x, y, 1, 0f, Image.boss3[enemyMotion[motionIndex]]);
            }
            else if (state == State.Dying)
            {
                DX.DrawRotaGraphF(x, y, 1, bodyAngle, Image.boss2);
            }

            if (state >= State.Normal) 
            {
                if (!isDead)
                { 
                    DX.DrawBoxAA(900, 120, 930, 120 + life * 0.75f, DX.GetColor(191, 0, 0), DX.TRUE);
                    DX.DrawRectGraph(900, 120, 0, 0, 30, 300, Image.guageBoss);
                }
            }
        }

        public override void OnCollisionPlayerBullet(PlayerBullet playerBullet)
        {
            if (state == State.Appear || state == State.Swoon || state == State.Dying)
                return;

            life -= 1;

            if (life <= 0)
            {
                state = State.Dying;

                centerX = x;
                centerY = y;
            }
            else if (state == State.Normal && life <= intialLife / 2)
            {
                state = State.Swoon;

                vx = 1.2f;
                vy = 1.2f;
            }

        }

        private void Explode()
        {
            float explosionX = x + MyRandom.PlusMinus(75f);
            float explosionY = y + MyRandom.PlusMinus(75f);

            game.explosions.Add(new Explosion(explosionX, explosionY));

        }

        void HeartBullet(float angle, float speed)
        {
            game.enemyBullets.Add(new EnemyStraightBullet(x, y, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 8, y - 8, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 16, y - 16, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 24, y - 24, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 32, y - 16, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 40, y - 8, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 48, y - 16, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 56, y - 24, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 64, y - 16, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 72, y - 8, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 80, y, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 8, y + 8, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 16, y + 16, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 24, y + 24, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 32, y + 32, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 40, y + 40, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 48, y + 32, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 56, y + 24, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 64, y + 16, angle * MyMath.Deg2Rad, speed));
            game.enemyBullets.Add(new EnemyStraightBullet(x - 72, y + 8, angle * MyMath.Deg2Rad, speed));
        }

        void SetTrap(int num) 
        {
            for (int i = 0; i < num; i++)
                game.bloods.Add(new Blood (game, MyRandom.Range(32, Screen.Width - 32),
                    MyRandom.Range(32, Screen.Height - 32)));
        }

        public override void DropItem()
        {

            game.items.Add(new ManaUpItem(game, x + 16, y + 16));
            game.items.Add(new ManaUpItem(game, x - 16, y + 16));
            game.items.Add(new LifeUpItem(game, x, y - 16));
            game.items.Add(new LifeUpItem(game, x + 32, y - 16));
            game.items.Add(new LifeUpItem(game, x - 32, y - 16));
        }
    }
}
