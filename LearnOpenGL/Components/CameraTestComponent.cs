using System.Diagnostics;
using OpenGaming;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LearnOpenGL.Components
{
    public class CameraTestComponent : GameComponent
    {
        public float MoveSpeed { get; set; } = 10;

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

            transform.Translate(movement * deltaTime);

            Debug.WriteLine($"Camera Rotation: {transform.Rotation.ToEulerAngles()}");
        }
    }
}
