using OpenGaming.Rendering;

namespace OpenGaming.Components
{
    public abstract class Renderer : GameComponent
    {
        public Material? Material { get; set; }

        public override void GameUpdate(float deltaTime)
        {
            base.GameUpdate(deltaTime);
        }

        public virtual void Render(Camera camera, LightingData lightingData)
        {
            if (Owner is not GameObject selfGameObject ||
                selfGameObject.Owner is not Game game)
            {
                return;
            }

            if (Material is IStandardMaterial standardMaterial)
            {
                var aspect = 1f;
                if (game.Output is not null)
                {
                    aspect = (float)game.Output.ClientSize.X / game.Output.ClientSize.Y;
                }

                standardMaterial.ModelMatrix = selfGameObject.Components.Transform.GetModelMatrix();
                standardMaterial.ViewMatrix = camera.GetViewMatrix();
                standardMaterial.ProjectionMatrix = camera.GetProjectionMatrix(aspect);

                standardMaterial.ViewPosition = camera.Owner.Components.Transform.WorldPosition;
                standardMaterial.PointLights = lightingData.PointLights;
            }

            if (Material is not null)
            {
                Material.Use();
            }
        }
    }
}
