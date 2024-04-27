﻿namespace OpenGaming.Components
{
    public abstract class Renderer : GameComponent
    {
        public Material? Material { get; set; }

        public override void GameUpdate()
        {
            base.GameUpdate();
        }

        public virtual void Render(Camera camera)
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
            }

            if (Material is not null)
            {
                Material.Use();
            }
        }
    }
}
