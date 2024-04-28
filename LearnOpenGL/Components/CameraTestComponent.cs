using System.Diagnostics;
using OpenGaming;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LearnOpenGL.Components
{
    public class CameraTestComponent : GameComponent
    {
        public float MoveSpeed { get; set; } = 10;
        public float RotateSpeed { get; set; } = MathF.PI / 100;

        public override void GameUpdate(float deltaTime)
        {
            base.GameUpdate(deltaTime);

            if (Owner is not GameObject selfGameObject ||
                selfGameObject.Owner is not Game game ||
                game.KeyboardState is not KeyboardState keyboardState ||
                game.MouseState is not MouseState mouseState)
            {
                return;
            }

            var transform = Owner.Components.Transform;

            var movement = Vector3.Zero;

            if (!keyboardState.IsKeyDown(Keys.P))
            {
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    movement.Z -= MoveSpeed;
                }
                if (keyboardState.IsKeyDown(Keys.S))
                {
                    movement.Z += MoveSpeed;
                }
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    movement.X -= MoveSpeed;
                }
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    movement.X += MoveSpeed;
                }
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    movement.Y += MoveSpeed;
                }
                if (keyboardState.IsKeyDown(Keys.LeftShift))
                {
                    movement.Y -= MoveSpeed;
                }
            }

            var rotationY = 0.0f;
            var rotationX = 0.0f;

            if (mouseState.IsButtonDown(MouseButton.Right))
            {
                rotationY = -mouseState.Delta.X * RotateSpeed;
                rotationX = -mouseState.Delta.Y * RotateSpeed;
            }

            var eulerAngles = transform.Rotation.ToEulerAngles();
            eulerAngles.X += rotationX;
            eulerAngles.Y += rotationY;

            if (eulerAngles.X > 60)
            {
                eulerAngles.X = 60;
            }
            else if (eulerAngles.X < -60)
            {
                eulerAngles.X = -60;
            }

            transform.Translate(movement * deltaTime, Space.Self);
            transform.Rotation = Quaternion.FromEulerAngles(eulerAngles);

            Debug.WriteLine($"Camera Rotation: {transform.Rotation.ToEulerAngles()}");
        }
    }
}
