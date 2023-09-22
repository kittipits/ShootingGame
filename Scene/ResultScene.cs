using DxLibDLL;
using MyLib;

namespace Shooting
{
    public class ResultScene : Scene
    {
        int score;
        int image;
        string sound;
        int bigFont;
        int smallFont;
        int counter = 0;

        public ResultScene(Game game) : base(game)
        {
        }

        public override void Start()
        {
            Sound.PlayMusic(sound);

            smallFont = DX.CreateFontToHandle(null, 25, -1);
            bigFont = DX.CreateFontToHandle(null, 50, -1);
        }

        public override void Update()
        {
            counter++;

            if (Input.GetButtonDown(DX.PAD_INPUT_1))
            {
                game.RequestScene("TitleScene");
                game.StartFadeOut(120, Image.black);
            }
        }

        public override void Draw() 
        {
            DX.DrawGraph(0, 0, image);
            //DX.DrawString(0, 0, "リザルトシーン実行中", DX.GetColor(255, 255, 255));
            DrawStringCenterToHandle(320, "SCORE : " + score, DX.GetColor(255, 255, 255), bigFont);
            if (counter % 60 > 20)
            { 
                DrawStringCenterToHandle(475, "Aボタン（Zキー）でタイトルに戻る", DX.GetColor(127, 127, 127), smallFont);
            }
            //DX.DrawString(0, 20, "Aボタン（Zキー）でタイトルに戻る", DX.GetColor(255, 255, 255));
            //DX.DrawString(0, 60, "得点：" + score, DX.GetColor(255, 255, 255));
        }

        public override void End() 
        {
            DX.StopMusic();
        }

        public override void ReceiveMessage(string message, object value)
        {
            if (message == "Win")
            {
                score = (int)value;
                image = Image.winResult;
                sound = Sound.bgmWin;
                
            }
            else if (message == "Lose")
            { 
                score = (int)value;
                image = Image.loseResult;
                sound = Sound.bgmLose;
            }
        }

        void DrawStringCenterToHandle(int y, string s, uint color, int fontHandle)
        {
            int width = DX.GetDrawStringWidthToHandle(s, s.Length, fontHandle);
            int screenWidth = Screen.Width;
            int x = screenWidth / 2 - width / 2;
            DX.DrawStringToHandle(x, y, s, color, fontHandle);
        }
    }
}
