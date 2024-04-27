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
                var renderer = selfGameObject.Components.GetRequired<Renderer>();
                var material = (StandardMaterial)renderer.Material!;

                material.Color = new Vector3(0, 1, 1);
            }
        }
    }
}
