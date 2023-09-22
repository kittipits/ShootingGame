using System;
using System.Collections.Generic;
using System.Diagnostics;
using DxLibDLL;
using MyLib;

namespace Shooting
{
    public class Game
    {
        public Player player;                       //自機生成
        public List<PlayerBullet> playerBullets;    //自機弾配列
        public List<Enemy> enemies;                 //敵配列
        public List<EnemyBullet> enemyBullets;      //敵弾配列
        public List<Explosion> explosions;          //死亡エフェクト
        public float scrollSpeed = 1.5f;            //画面の速度
        public Map map;                             //map

        public List<DestructibleBullet> destructibleBullets;
        public List<Item> items;
        public List<EnemyTrap> enemyTraps;
        public List<Blood> bloods;

        //シーン管理用変数
        Scene currentScene = null;
        Dictionary<string, Scene> scenes;
        string nextSceneName = null;
        Fade fade;

        public int score;
        public bool clearFlag;

        public void Init()
        {
            Input.Init();
            MyRandom.Init();
            Image.Load();
            Sound.Load();

            //シーン管理
            fade = new Fade();

            scenes = new Dictionary<string, Scene>();
            scenes.Add("TitleScene", new TitleScene(this));
            scenes.Add("GamePlayScene", new GamePlayScene(this));
            scenes.Add("ResultScene", new ResultScene(this));
            scenes.Add("HowToPlayScene", new HowToPlayScene(this));
            scenes.Add("StoryScene", new StoryScene(this));

            ChangeScene("TitleScene");
        }

        public void Update()
        {
            Input.Update();

            //シーン管理
            fade.Update();
            if (fade.IsFading())
            {
                return;
            }

            if (nextSceneName != null)
            {
                ChangeScene(nextSceneName);
                nextSceneName = null;
            }

            currentScene.Update();
        }

        public void Draw()
        {
            currentScene.Draw();
            fade.Draw();
        }

        public void SendMessage(string sceneName, string message, object value = null)
        {
            Debug.Assert(scenes.ContainsKey(sceneName), "存在しないシーン名が指定された:" + sceneName);

            scenes[sceneName].ReceiveMessage(message, value);
        }

        public void RequestScene(string sceneName)
        {
            nextSceneName = sceneName;
        }

        public void ChangeScene(string sceneName)
        {
            Debug.Assert(scenes.ContainsKey(sceneName), "存在しないシーン名が指定された:" + sceneName);

            if (currentScene != null)
            {
                currentScene.End();
            }

            currentScene = scenes[sceneName];
            currentScene.Start();
        }

        public void DrawTargetScene(string sceneName)
        {
            Debug.Assert(scenes.ContainsKey(sceneName), "存在しないシーン名が指定された:" + sceneName);

            scenes[sceneName].Draw();
        }

        public void StartFadeIn(int time, int image)
        {
            fade.Start(time, 255, 0, image);
        }

        public void StartFadeOut(int time, int image)
        {
            fade.Start(time, 0, 255, image);
        }

    }
}
