using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace OpenGaming.Components
{

    public class Camera : GameComponent
    {
        public float FieldOfView { get; set; } = MathF.PI / 2;
        public float DepthNear { get; set; } = 0.1f;
        public float DepthFar { get; set; } = 100;

        public CameraClearType ClearType { get; set; }
        public Color4 ClearColor { get; set; } = new Color4(57, 112, 172, 255);
        public float ClearDepth { get; set; } = 0;

        private void Clear()
        {

            switch (ClearType)
            {
                case CameraClearType.None:
                    break;

                case CameraClearType.Color:
                    GL.ClearColor(ClearColor);
                    GL.Clear(ClearBufferMask.ColorBufferBit);
                    break;

                case CameraClearType.Depth:
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

                renderer.Render(this);
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
