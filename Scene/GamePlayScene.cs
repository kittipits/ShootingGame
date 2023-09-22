using DxLibDLL;
using MyLib;
using System;
using System.Collections.Generic;

namespace Shooting
{
    public class GamePlayScene : Scene
    {
        public int resultCountdown;

        //ボスのBGMを流す準備
        int appear;
        bool bossSoundCheck;

        public GamePlayScene(Game game) : base(game)
        { 
        }

        public override void Start()
        {
            Sound.PlayMusic(Sound.bgmTitle);
            game.StartFadeIn(120, Image.black);

            //自機の位置初期化
            game.player = new Player(100, Screen.Height / 2, game);
            game.playerBullets = new List<PlayerBullet>();

            game.enemies = new List<Enemy>();
            game.enemyBullets = new List<EnemyBullet>();
            game.explosions = new List<Explosion>();

            game.destructibleBullets = new List<DestructibleBullet>();
            game.items = new List<Item>();
            game.enemyTraps = new List<EnemyTrap>();
            game.bloods = new List<Blood>();

            //mapに応じて敵を生成
            //game.map = new Map(game, 0, "Map/StageTest.csv");
            //game.map = new Map(game, 0, "Map/stageBoss.csv");
            game.map = new Map(game, 0, "Map/stage1.csv");

            //ひとつひとつの敵を生成
            //game.enemies.Add(new Zako1(game, 1000, Screen.Height / 2)); 
            //game.enemyTraps.Add(new EnemyTrapSpike(1200, Screen.Height / 2));
            //game.bloods.Add(new Blood(game, 800, Screen.Height / 2));

            game.score = 0;
            resultCountdown = 180;
            game.clearFlag = false;

            appear = 0;
            bossSoundCheck = false;
        }

        public override void Update()
        {
            //画面の動き　//新しい敵をテストする時、これをコメントしとく。
            game.map.Scroll(game.scrollSpeed);

            //更新処理
            if (!game.player.isDead)
            {
                game.player.Update();
            }
            else if (game.player.isDead)
            {
                resultCountdown--;
            }
            foreach (PlayerBullet pb in game.playerBullets)
            {
                pb.Update();
            }
            foreach (EnemyBullet eb in game.enemyBullets)
            {
                eb.Update();
            }
            foreach (Enemy en in game.enemies)
            {
                en.Update();
            }
            foreach (Explosion ex in game.explosions)
            {
                ex.Update();
            }
            foreach (DestructibleBullet db in game.destructibleBullets)
            {
                db.Update();
            }
            foreach (Item it in game.items)
            {
                it.Update();
            }
            foreach (EnemyTrap et in game.enemyTraps)
            {
                et.Update();
            }
            foreach (Blood bl in game.bloods)
            {
                bl.Update();
            }

            OnCollisionCheck();     //衝突判定メソッド、下に↓

            game.playerBullets.RemoveAll(pb => pb.isDead);
            game.enemies.RemoveAll(en => en.isDead);
            game.enemyBullets.RemoveAll(eb => eb.isDead);
            game.explosions.RemoveAll(ex => ex.isDead);

            game.destructibleBullets.RemoveAll(db => db.isDead);
            game.items.RemoveAll(it => it.isDead);
            game.enemyTraps.RemoveAll(et => et.isDead);
            game.bloods.RemoveAll(bl => bl.isDead);

            if (game.clearFlag)
            {
                resultCountdown--;
            }

            if (resultCountdown <= 0 && game.clearFlag == true)
            {
                game.RequestScene("ResultScene");
                game.SendMessage("ResultScene", "Win", game.score);
            }
            else if (resultCountdown <= 0 && game.clearFlag == false)
            {
                game.RequestScene("ResultScene");
                game.SendMessage("ResultScene", "Lose", game.score);
            }

            //ボスの登場時、ボスのBGMを流す
            if (HasBoss() == true && bossSoundCheck == false)
            {
                appear++;
                if (appear == 240)
                {
                    DX.StopMusic();
                    Sound.PlayMusic(Sound.bgmBoss);
                    bossSoundCheck = true;
                }
            }
        }

        public override void Draw() 
        {
            //背景
            DX.DrawGraph(0, 0, Image.background);

            //描画処理
            foreach (Blood bl in game.bloods)
            {
                bl.Draw();
            }
            foreach (EnemyTrap et in game.enemyTraps)
            {
                et.Draw();
            }
            foreach (Enemy en in game.enemies)
            {
                en.Draw();
            }
            foreach (DestructibleBullet db in game.destructibleBullets)
            {
                db.Draw();
            }
            foreach (Explosion ex in game.explosions)
            {
                ex.Draw();
            }
            if (!game.player.isDead)
            {
                game.player.Draw();
            }
            foreach (PlayerBullet pb in game.playerBullets)
            {
                pb.Draw();
            }
            foreach (EnemyBullet eb in game.enemyBullets)
            {
                eb.Draw();
            }
            foreach (Item it in game.items)
            {
                it.Draw();
            }

            //確認のため
            //DX.DrawString(30, 470, "自機弾:" + game.playerBullets.Count, DX.GetColor(255, 255, 255));
            //DX.DrawString(130, 470, "敵:" + game.enemies.Count, DX.GetColor(255, 255, 255));
            //DX.DrawString(230, 470, "敵弾:" + game.enemyBullets.Count, DX.GetColor(255, 255, 255));
            //DX.DrawString(30, 500, "罠:" + game.enemyTraps.Count, DX.GetColor(255, 255, 255));
            //DX.DrawString(130, 500, "アイテム:" + game.items.Count, DX.GetColor(255, 255, 255));
            //DX.DrawString(230, 500, "敵から生成された敵:" + game.destructibleBullets.Count, DX.GetColor(255, 255, 255));

            DX.DrawString(820, 30, "SCORE : " + game.score, DX.GetColor(0, 0, 0));

            DX.DrawBoxAA(15, 15, 15 + game.player.life * 15, 30, DX.GetColor(191, 0, 0), DX.TRUE);
            DX.DrawRectGraphF(15, 15, 0, 0, 300, 15, Image.guageHP);

            DX.DrawBoxAA(15, 37.5f, 15 + game.player.mana * 1.8f, 52.5f, DX.GetColor(63, 63, 255), DX.TRUE);
            DX.DrawRectGraphF(15, 37.5f, 0, 0, 180, 15, Image.guageMP);

            DX.DrawBoxAA(210, 37.5f, 210 + game.player.ultimateGuage * 0.45f, 52.5f, DX.GetColor(255, 127, 63), DX.TRUE);
            DX.DrawRectGraphF(210, 37.5f, 0, 0, 90, 15, Image.guageUltimate);

            if (game.player.ultimateGuage >= 200)
            {
                DX.DrawStringF(232.5f, 37.5f, "Ready!", DX.GetColor(255, 255, 255));
            }
        }

        public override void End() 
        {
            DX.StopMusic();
            bossSoundCheck = false;
        }

        public void OnCollisionCheck()
        {
            //自機弾と敵の衝突判定
            foreach (PlayerBullet playerBullet in game.playerBullets)
            {
                //自機弾が死んだらスキップする
                if (playerBullet.isDead)
                    continue;

                foreach (Enemy enemy in game.enemies)
                {
                    //敵が死んだらスキップする
                    if (enemy.isDead)
                        continue;

                    //自機弾と敵が重なっているか
                    if (MyMath.CircleCircleIntersection(
                        playerBullet.x, playerBullet.y, playerBullet.collisionRadius,
                        enemy.x, enemy.y, enemy.collisionRadius))
                    {
                        //重なっていたら、それぞれのぶつかったときの処理を呼び出す
                        enemy.OnCollisionPlayerBullet(playerBullet);
                        playerBullet.OnCollisionEnemy(enemy);

                        // 衝突の結果、自機弾が死んだら、この弾のループはおしまい
                        if (playerBullet.isDead)
                            break;
                    }
                }

                foreach (DestructibleBullet destructibleBullet in game.destructibleBullets)
                {
                    if (destructibleBullet.isDead)
                        continue;

                    if (MyMath.CircleCircleIntersection(
                        playerBullet.x, playerBullet.y, playerBullet.collisionRadius,
                        destructibleBullet.x, destructibleBullet.y, destructibleBullet.collisionRadius))
                    {
                        destructibleBullet.OnCollisionPlayerBullet(playerBullet);
                        playerBullet.OnCollisionDestructibleBullet(destructibleBullet);

                        if (playerBullet.isDead)
                            break;
                    }
                }
            }

            //自機と敵の衝突判定
            foreach (Enemy enemy in game.enemies)
            {
                if (game.player.isDead)
                    break;

                if (enemy.isDead)
                    continue;

                if (MyMath.CircleCircleIntersection(
                    game.player.x, game.player.y, game.player.collisionRadius,
                    enemy.x, enemy.y, enemy.collisionRadius))
                {
                    game.player.OnCollisionEnemy(enemy);
                }
            }

            //自機と敵弾の衝突判定
            foreach (EnemyBullet enemyBullet in game.enemyBullets)
            {
                if (game.player.isDead)
                    break;

                if (enemyBullet.isDead)
                    continue;

                if (MyMath.CircleCircleIntersection(
                    game.player.x, game.player.y, game.player.collisionRadius,
                    enemyBullet.x, enemyBullet.y, enemyBullet.collisionRadius))
                {
                    game.player.OnCollisionEnemyBullet(enemyBullet);
                }
            }

            foreach (DestructibleBullet destructibleBullet in game.destructibleBullets)
            {
                if (game.player.isDead)
                    break;

                if (destructibleBullet.isDead)
                    continue;

                if (MyMath.CircleCircleIntersection(
                    game.player.x, game.player.y, game.player.collisionRadius,
                    destructibleBullet.x, destructibleBullet.y, destructibleBullet.collisionRadius))
                {
                    game.player.OnCollisionDestructibleBullet(destructibleBullet);
                }
            }

            foreach (Item item in game.items)
            {
                if (game.player.isDead)
                    break;

                if (item.isDead)
                    continue;

                if (MyMath.CircleCircleIntersection(
                    game.player.x, game.player.y, game.player.collisionRadius,
                    item.x, item.y, item.collisionRadius))
                {
                    item.OnCollisionPlayer(game.player);
                    game.player.OnCollisionItem(item);
                }
            }

            foreach (EnemyTrap enemyTrap in game.enemyTraps)
            {
                if (game.player.isDead)
                    break;

                if (enemyTrap.isDead)
                    continue;

                if (MyMath.CircleCircleIntersection(
                    game.player.x, game.player.y, game.player.collisionRadius,
                    enemyTrap.x, enemyTrap.y, enemyTrap.collisionRadius))
                {
                    game.player.OnCollisionEnemyTrap(enemyTrap);
                }
            }
        }

        //ボスの存在確認
        bool HasBoss()
        {
            foreach (Enemy e in game.enemies)
            {
                if (e is Boss)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
