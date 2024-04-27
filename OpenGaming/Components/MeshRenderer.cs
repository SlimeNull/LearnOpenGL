namespace OpenGaming.Components
{
    public class MeshRenderer : Renderer
    {
        public Mesh? Mesh { get; set; }

        public override void GameUpdate()
        {
            base.GameUpdate();
        }

        public override void Render(Camera camera)
        {
            base.Render(camera);

            if (Mesh is not null)
            {
                Mesh.Draw();
            }
        }
    }
}
