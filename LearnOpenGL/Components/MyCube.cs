using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenGaming;
using OpenGaming.Components;
using OpenGaming.Materials;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LearnOpenGL.Components
{

    public class MyCube : GameComponent
    {
        public float MoveSpeed { get; set; } = 10;
        public float RotateSpeed { get; set; } = MathF.PI / 100;

        public override void GameStart()
        {
            base.GameStart();

            if (Owner is not GameObject selfGameObject)
            {
                return;
            }

            var transform = selfGameObject.Components.Transform;
            var renderer = selfGameObject.Components.GetRequired<Renderer>();
            var material = (StandardMaterial)renderer.Material!;

            material.Color = new Vector3(40 / 255f, 174 / 255f, 255 / 255f);
        }

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

            var transform = selfGameObject.Components.Transform;

            var movement = Vector3.Zero;

            if (keyboardState.IsKeyDown(Keys.P))
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

                if (mouseState.ScrollDelta.Y > 0)
                {
                    transform.Scale *= 1.1f;
                }
                else if (mouseState.ScrollDelta.Y < 0)
                {
                    transform.Scale *= 0.9f;
                }
            }

            var rotationY = 0.0f;
            var rotationX = 0.0f;

            if (mouseState.IsButtonDown(MouseButton.Left))
            {
                rotationY = mouseState.Delta.X * RotateSpeed;
                rotationX = mouseState.Delta.Y * RotateSpeed;
            }

            var rotationEulerAngles = new Vector3(rotationX, rotationY, 0);

            transform.Translate(movement * deltaTime, Space.Self);
            transform.Rotate(Quaternion.FromEulerAngles(rotationEulerAngles), Space.World);


            Debug.WriteLine($"Cube Rotation: {transform.Rotation.ToEulerAngles()}");
        }
    }
}
