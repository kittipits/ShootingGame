using System.Diagnostics;
using System.IO;

namespace Shooting
{
    public class Map
    {
        const int Width = 1000;
        const int Height = 17;
        const int CellSize = 32;

        Game game;
        int[,] enemyData;
        float position;

        public Map(Game game, float startPosition, string filePath) 
        { 
            this.game = game;
            position = startPosition;
            Load(filePath);
        }

        void Load(string filePath)
        {
            enemyData = new int[Width, Height];

            string[] lines = File.ReadAllLines(filePath);

            Debug.Assert(lines.Length == Height, "CSVファイルの高さが不正です：" + lines.Length);

            for (int y = 0; y < Height; y++)
            {
                string[] splitted = lines[y].Split(new char[] {','});

                Debug.Assert(splitted.Length == Width, "CSVファイルの" + y + "行目の列数が不正です:" + splitted.Length);

                for (int x = 0; x < Width; x++)
                {
                    enemyData[x, y] = int.Parse(splitted[x]);
                }
            }
        }

        public void Scroll(float delta)
        {
            int prevRightCell = (int)(position + Screen.Width) / CellSize;

            position += delta;

            int currentRightCell = (int)(position + Screen.Width) / CellSize;

            if (currentRightCell >= Width) 
                return;

            if (prevRightCell == currentRightCell)
                return;

            float x = currentRightCell * CellSize - position;

            //idを指定して、敵を生成
            //引数は x,y座標 + 画像のサイズ 
            for (int cellY = 0; cellY < Height; cellY++)
            {
                float y = cellY * CellSize;

                int id = enemyData[currentRightCell, cellY];
                if (id == -1) continue;
                else if (id == 0) game.enemies.Add(new Zako0(game, x + 16, y + 16));
                else if (id == 1) game.enemies.Add(new Zako1(game, x + 32, y + 32));
                else if (id == 2) game.enemies.Add(new Zako2(game, x + 16, y + 16));
                else if (id == 3) game.enemies.Add(new Zako3(game, x + 32, y + 32));
                else if (id == 4) game.enemies.Add(new Zako4(game, x + 32, y + 32));
                else if (id == 5) game.enemies.Add(new Zako5(game, x + 32, y + 32));
                else if (id == 6) game.enemies.Add(new Zako6(game, x + 32, y + 32));
                else if (id == 7) game.enemies.Add(new Zako7(game, x + 32, y + 32));
                else if (id == 11) game.items.Add(new LifeUpItem(game, x + 16, y + 16));
                else if (id == 10) game.enemyTraps.Add(new EnemyTrapSpike(x + 32, y + 32));
                else if (id == 100) game.enemies.Add(new Boss(game, x + 64, y + 32));
                else Debug.Assert(false, "敵ID" + id + "番の生成処理は未実装です。");
            }
        
        }

    }
}
