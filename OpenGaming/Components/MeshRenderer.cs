using OpenGaming.Rendering;

namespace OpenGaming.Components
{
    public class MeshRenderer : Renderer
    {
        public Mesh? Mesh { get; set; }

        public override void GameUpdate(float deltaTime)
        {
            base.GameUpdate(deltaTime);
        }

        public override void Render(Camera camera, LightingData lightingData)
        {
            base.Render(camera, lightingData);

            if (Mesh is not null)
            {
                Mesh.Draw();
            }
        }
    }
}
