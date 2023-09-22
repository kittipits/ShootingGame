using DxLibDLL;
using MyLib;

namespace Shooting
{
    enum Story
    {
        Azalea,
        Ellanoir
    }

    public class StoryScene : Scene
    {
        Story story;

        public StoryScene(Game game) : base(game)
        {
        }

        public override void Start()
        {
            DX.StopMusic();
            Sound.PlayMusic(Sound.bgmBoss);
            game.StartFadeIn(120, Image.black);
        }

        public override void Update()
        {
            if (Input.GetButtonDown(DX.PAD_INPUT_LEFT))
            {
                story--;
                Sound.Play(Sound.seSelect);
            }

            if (Input.GetButtonDown(DX.PAD_INPUT_RIGHT))
            {
                story++;
                Sound.Play(Sound.seSelect);
            }

            if ((int)story > 1) story = Story.Azalea;
            if ((int)story < 0) story = Story.Ellanoir;

            if (Input.GetButtonDown(DX.PAD_INPUT_1))
            {
                Sound.Play(Sound.seSelect);

                game.RequestScene("TitleScene");
                game.StartFadeOut(120, Image.black);
            }
        }

        public override void Draw()
        {
            if (story == Story.Azalea)
            {
                DX.DrawGraph(0, 0, Image.story0);
            }
            else if (story == Story.Ellanoir)
            {
                DX.DrawGraph(0, 0, Image.story1);
            }

            DX.DrawString(580, 510, "←→：ページ変更　Ⓐ／Z：タイトルに戻る", DX.GetColor(127, 127, 127));
        }

        public override void End()
        {
            DX.StopMusic();
        }
    }
}
