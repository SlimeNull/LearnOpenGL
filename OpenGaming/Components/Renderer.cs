namespace OpenGaming.Components
{
    public abstract class Renderer : GameComponent
    {
        public Material? Material { get; set; }

        public override void GameUpdate()
        {
            base.GameUpdate();
        }
    }
}
