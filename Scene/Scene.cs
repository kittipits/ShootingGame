namespace Shooting
{
    public abstract class Scene
    {
        protected Game game;

        public Scene(Game game)
        {
            this.game = game;
        }

        public abstract void Start();
        public abstract void Update();
        public abstract void Draw();
        public abstract void End();

        public virtual void ReceiveMessage(string message, object value)
        { 
        }
    }
}
