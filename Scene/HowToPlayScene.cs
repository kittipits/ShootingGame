using DxLibDLL;
using MyLib;

namespace Shooting
{
    enum Howto
    {
        Shoot,
        Caution,
        Objective
    }

    public class HowToPlayScene : Scene
    {
        Howto howto;

        public HowToPlayScene(Game game) : base(game)
        {
        }

        public override void Start()
        {
            DX.StopMusic();
            Sound.PlayMusic(Sound.bgmTitle);
            game.StartFadeIn(120, Image.black);
        }

        public override void Update()
        {
            if (Input.GetButtonDown(DX.PAD_INPUT_LEFT))
            {
                howto--;
                Sound.Play(Sound.seSelect);
            }

            if (Input.GetButtonDown(DX.PAD_INPUT_RIGHT))
            {
                howto++;
                Sound.Play(Sound.seSelect);
            }

            if ((int)howto > 2) howto = Howto.Shoot;
            if ((int)howto < 0) howto = Howto.Objective;

            if (Input.GetButtonDown(DX.PAD_INPUT_1))
            {
                Sound.Play(Sound.seSelect);

                game.RequestScene("TitleScene");
                game.StartFadeOut(120, Image.black);
            }
        }

        public override void Draw()
        {
            if (howto == Howto.Shoot)
            {
                DX.DrawGraph(0, 0, Image.howtoplay0);
            }
            else if (howto == Howto.Caution)
            {
                DX.DrawGraph(0, 0, Image.howtoplay1);
            }
            else if (howto == Howto.Objective)
            {
                DX.DrawGraph(0, 0, Image.howtoplay2);
            }

            DX.DrawString(580, 495, "←→：ページ変更　Ⓐ／Z：タイトルに戻る", DX.GetColor(0, 0, 0));
        }

        public override void End()
        {
            DX.StopMusic();
        }
    }
}
