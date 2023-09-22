using DxLibDLL;
using MyLib;
using System;

namespace Shooting
{
    enum State
    {
        StartGame,
        HowToPlay,
        Story,
        EndGame
    }

    public class TitleScene : Scene
    {
        State state;
        int bigFont;
        int smallFont;

        public TitleScene(Game game) : base(game)
        {
        }

        public override void Start()
        {
            Sound.PlayMusic(Sound.bgmTitle);
            game.StartFadeIn(120, Image.black);

            smallFont = DX.CreateFontToHandle(null, 20, -1);
            bigFont = DX.CreateFontToHandle(null, 36, -1);

            DX.SetWindowUserCloseEnableFlag(DX.TRUE);
            game.clearFlag = false;
        }

        public override void Update()
        {
            if (Input.GetButtonDown(DX.PAD_INPUT_UP))
            {
                state--;
                Sound.Play(Sound.seSelect);
            }

            if (Input.GetButtonDown(DX.PAD_INPUT_DOWN)) 
            {
                state++;
                Sound.Play(Sound.seSelect);
            } 

            if ((int)state >= 3) state = State.EndGame;
            if ((int)state <= 0) state = State.StartGame;

            if (state == State.StartGame)
            {
                if (Input.GetButtonDown(DX.PAD_INPUT_1))
                {
                    DX.StopMusic();
                    Sound.Play(Sound.seStart);

                    game.StartFadeOut(120, Image.black);
                    game.RequestScene("GamePlayScene");
                }
            }
            else if (state == State.HowToPlay)
            {
                if (Input.GetButtonDown(DX.PAD_INPUT_1))
                {
                    Sound.Play(Sound.seSelect);

                    game.StartFadeOut(120, Image.black);
                    game.RequestScene("HowToPlayScene");
                }
            }
            else if (state == State.Story)
            {
                if (Input.GetButtonDown(DX.PAD_INPUT_1))
                {
                    Sound.Play(Sound.seSelect);

                    game.StartFadeOut(120, Image.black);
                    game.RequestScene("StoryScene");
                }
            }
            else if (state == State.EndGame)
            {
                if (DX.ProcessMessage() == 0 && Input.GetButtonDown(DX.PAD_INPUT_1))
                {
                    DX.DxLib_End();
                    Environment.Exit(0);
                }
            }

        }

        public override void Draw() 
        {
            DX.DrawGraph(0, 0, Image.title);

            DrawStringRightScreen(275, "始まり", DX.GetColor(83, 0, 113),smallFont);
            DrawStringRightScreen(320, "プレイ方法", DX.GetColor(83, 0, 113), smallFont);
            DrawStringRightScreen(365, "ストーリー", DX.GetColor(83, 0, 113), smallFont);
            DrawStringRightScreen(410, "終わり", DX.GetColor(83, 0, 113), smallFont);
            DrawStringRightScreen(500, "↑↓：選択　Ⓐ／Z：決定", DX.GetColor(127, 127, 127), smallFont);

            if (state == State.StartGame)
            {
                DX.DrawBox(600, 270, 840, 300, DX.GetColor(0, 0, 0), DX.TRUE);
                DrawStringRightScreen(267, "始まり", DX.GetColor(186, 0, 123), bigFont);
            }
            else if (state == State.HowToPlay)
            {
                DX.DrawBox(600, 315, 840, 345, DX.GetColor(0, 0, 0), DX.TRUE);
                DrawStringRightScreen(312, "プレイ方法", DX.GetColor(186, 0, 123), bigFont);
            }
            else if (state == State.Story)
            {
                DX.DrawBox(600, 360, 840, 390, DX.GetColor(0, 0, 0), DX.TRUE);
                DrawStringRightScreen(357, "ストーリー", DX.GetColor(186, 0, 123), bigFont);
            }
            else if (state == State.EndGame)
            {
                DX.DrawBox(600, 405, 840, 435, DX.GetColor(0, 0, 0), DX.TRUE);
                DrawStringRightScreen(402, "終わり", DX.GetColor(186, 0, 123), bigFont);
            }
        }

        public override void End() 
        {
        }

        void DrawStringRightScreen(int y, string s, uint color, int fontHandle)
        {
            int width = DX.GetDrawStringWidthToHandle(s, s.Length, fontHandle);
            int screenWidth = Screen.Width / 2;
            int x = (Screen.Width / 2) + (screenWidth / 2 - width / 2);
            DX.DrawStringToHandle(x, y, s, color, fontHandle);
        }
    }
}
