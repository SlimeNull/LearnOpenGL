using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGaming;
using OpenGaming.Components;
using OpenGaming.Materials;
using OpenTK.Mathematics;

namespace LearnOpenGL.Components
{
    public class MyCube : GameComponent
    {
        public override void GameStart()
        {
            base.GameStart();

            if (Owner is GameObject selfGameObject)
            {
                var transform = selfGameObject.Components.Transform;
                var renderer = selfGameObject.Components.GetRequired<Renderer>();
                var material = (StandardMaterial)renderer.Material!;

                transform.Position = new Vector3(0, 0, -10);
                material.Color = new Vector3(0.44f, 0.32f, 0.65f);
            }
        }
    }
}
