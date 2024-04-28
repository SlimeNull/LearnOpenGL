using OpenGaming.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace OpenGaming.Components
{
    public class Camera : GameComponent
    {
        private readonly LightingData _lightingData = new();

        public float FieldOfView { get; set; } = MathF.PI / 2;
        public float DepthNear { get; set; } = 0.1f;
        public float DepthFar { get; set; } = 100;

        public CameraClearType ClearType { get; set; }
        public Color4 ClearColor { get; set; } = new Color4(57, 112, 172, 255);
        public float ClearDepth { get; set; } = 100;

        private void Clear()
        {

            switch (ClearType)
            {
                case CameraClearType.None:
                    break;

                case CameraClearType.SolidColor:
                    GL.ClearColor(ClearColor);
                    GL.ClearDepth(ClearDepth);
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    break;

                case CameraClearType.DepthOnly:
                    GL.ClearDepth(ClearDepth);
                    GL.Clear(ClearBufferMask.DepthBufferBit);
                    break;
            }
        }

        public override void GameUpdate(float deltaTime)
        {
            base.GameUpdate(deltaTime);

            if (Owner is not GameObject selfGameObject ||
                selfGameObject.Owner is not Game game)
            {
                return;
            }

            Clear();

            _lightingData.Clear();
            foreach (var gameObject in game.Objects)
            {
                if (!gameObject.IsActive)
                {
                    continue;
                }

                if (gameObject.Components.Get<Light>() is not Light light)
                {
                    continue;
                }

                var transform = gameObject.Components.Transform;

                if (light.LightType == LightType.Spot)
                {

                }
                else if (light.LightType == LightType.Directional)
                {

                }
                else
                {
                    _lightingData.PointLights.Add(
                        new PointLightData()
                        {
                            Position = transform.WorldPosition,
                            Color = light.Color,
                        });
                }
            }

            foreach (var gameObject in game.Objects)
            {
                if (!gameObject.IsActive)
                {
                    continue;
                }

                if (gameObject.Components.Get<Renderer>() is not Renderer renderer)
                {
                    continue;
                }

                renderer.Render(this, _lightingData);
            }
        }

        public override void GameLateUpdate(float deltaTime)
        {
            base.GameLateUpdate(deltaTime);
        }

        public Matrix4 GetViewMatrix()
        {
            if (Owner is null)
            {
                return default;
            }

            var transform = Owner.Components.Transform;

            return
                Matrix4.CreateTranslation(-transform.Position) *
                Matrix4.CreateFromQuaternion(transform.Rotation.Inverted());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspect">Aspect ratio of the view (width / height).</param>
        /// <returns></returns>
        public Matrix4 GetProjectionMatrix(float aspect)
        {
            return Matrix4.CreatePerspectiveFieldOfView(FieldOfView, aspect, DepthNear, DepthFar);
        }
    }
}
