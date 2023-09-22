using DxLibDLL;
using MyLib;

namespace Shooting
{
    public class Player
    {
        const int PlayerSize = 32;
        const float MoveSpeed = 6f;　//移動速度
        const int InvulCountdown = 90; //無敵時間
        
        const int MaxLife = 20;
        const float MaxMana = 100;
        const int MaxUltGuage = 200;

        //座標
        public float x;
        public float y;
        
        public float collisionRadius = 4f;　//当たり判定の大きさ
        public bool isDead = false;　//死亡フラグ

        public int life = MaxLife;　//ライフ
        public float mana = MaxMana;
        public int ultimateGuage = 0;

        int InvulTimer = 0; //残り無敵時間

        int animationCounter = 0;
        int[] playerMotion = new int[5] { 1, 2, 1, 0, 1};
        int motionIndex = 1;

        Game game;

        //初期位置
        public Player(float x, float y, Game game)
        { 
            this.x = x;
            this.y = y;
            this.game = game;
        }

        public void Update()
        {
            //加速・方向
            float vx = 0f;
            float vy = 0f;


            //移動処理　まっすぐ
            if (Input.GetButton(DX.PAD_INPUT_LEFT))
            {
                vx = -MoveSpeed;
            }
            else if (Input.GetButton(DX.PAD_INPUT_RIGHT))
            { 
                vx = MoveSpeed;
            }
            if (Input.GetButton(DX.PAD_INPUT_UP))
            {
                vy = -MoveSpeed;
            }
            else if (Input.GetButton(DX.PAD_INPUT_DOWN))
            {
                vy = MoveSpeed;
            }

            if (Input.GetButton(DX.PAD_INPUT_LEFT) || Input.GetButton(DX.PAD_INPUT_RIGHT) ||
                Input.GetButton(DX.PAD_INPUT_UP) || Input.GetButton(DX.PAD_INPUT_DOWN))
            {
                animationCounter++;
            }
            else 
            {
                animationCounter = 1;
            }

            //移動処理　斜め
            if (vx != 0 && vy != 0)
            {
                vx /= MyMath.Sqrt2;
                vy /= MyMath.Sqrt2;
            }

            //位置計算
            x += vx;
            y += vy;

            //自機弾を発射
            if (Input.GetButtonDown(DX.PAD_INPUT_1)) //Zキー
            {
                for (int i = -15; i <= 15; i += 15)
                {
                    game.playerBullets.Add(new PlayerStraightBullet(game, x, y, i * MyMath.Deg2Rad));
                    Sound.Play(Sound.seShoot1);
                }
            }

            //MPを消費て、より広い範囲で発射
            if (mana >= 15)
            {
                if (Input.GetButtonDown(DX.PAD_INPUT_2)) //Xキー
                {
                    for (int i = -45 ; i <= 45; i += 15)
                    {
                        game.playerBullets.Add(new PlayerStraightBullet(game, x, y, i * MyMath.Deg2Rad));
                        game.playerBullets.Add(new PlayerStraightBullet(game, x, y, (i + 180)  * MyMath.Deg2Rad));
                    }
                    mana -= 15f;
                    Sound.Play(Sound.seShoot2);
                }
            }
            if (mana < MaxMana)
            {
                mana += 0.05f;
            }

            //ゲージを消費して、アルチメイト使用
            if (ultimateGuage >= MaxUltGuage)
            {
                ultimateGuage = MaxUltGuage;

                if (Input.GetButtonDown(DX.PAD_INPUT_3)) //Cキー
                {
                    for (int i = 0; i < 360; i += 12)
                    {
                        game.playerBullets.Add(new PlayerReflectBullet(x, y, i * MyMath.Deg2Rad));
                    }
                    ultimateGuage = 0;
                    Sound.Play(Sound.seShoot2);
                }
            }

            InvulTimer--;

            //画面を出ないようにする
            if (x < PlayerSize) x = PlayerSize;
            if (x > Screen.Width - PlayerSize) x = Screen.Width - PlayerSize;
            if (y < PlayerSize) y = PlayerSize;
            if (y > Screen.Height - PlayerSize) y = Screen.Height - PlayerSize;
        }

        public void Draw()
        {
            motionIndex = motionIndex % 4;
            if (animationCounter % 10 == 0) motionIndex++;


            //無敵ではない場合は描画 //無敵中は点滅
            if (InvulTimer <= 0 || InvulTimer % 2 == 0)
                DX.DrawRotaGraphF(x, y, 1, 0, Image.player[playerMotion[motionIndex]]);
        }

        //敵にぶつかったら、ライフが減る
        public void OnCollisionEnemy(Enemy enemy)
        {
            if (InvulTimer <= 0)
            {
                TakeDamage();
            }
            
        }

        //敵の弾にぶつかったら、ライフが減る
        public void OnCollisionEnemyBullet(EnemyBullet enemyBullet)
        {
            if (InvulTimer <= 0)
            {
                TakeDamage();
            }
        }

        public void OnCollisionDestructibleBullet(DestructibleBullet destructibleBullet)
        {
            if (InvulTimer <= 0)
            {
                TakeDamage();
            }
        }

        public void OnCollisionEnemyTrap(EnemyTrap enemyTrap)
        {
            if (InvulTimer <= 0)
            {
                TakeDamage();
            }
        }

        //ライフが0になったら、死亡フラグ
        void TakeDamage() 
        {
            life -= 1;

            if (life <= 0)
            {
                isDead = true;
                game.explosions.Add(new Explosion(x, y));
            }
            else
            {
                InvulTimer = InvulCountdown;
            }
        }

        public void OnCollisionItem(Item item)
        { 
        }

        public void RecoverHP()
        {
            life++;
            if (life > MaxLife)
            { 
                life = MaxLife;
            }
        }

        public void RecoverMP()
        {
            mana += 30;
            if (mana > MaxMana)
            { 
                mana = MaxMana;
            }
        }
    }
}
