using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace OpenGaming.Components
{
    public class Transform : GameComponent
    {
        public Vector3 Position { get; set; }
        public Vector3 Scale { get; set; }
        public Quaternion Rotation { get; set; }

        public Matrix4 GetMatrix()
        {
            var scaleMatrix = Matrix4.CreateScale(Scale);
            var rotationMatrix = Matrix4.CreateFromQuaternion(Rotation);
            var translateMatrix = Matrix4.CreateTranslation(Position);

            return translateMatrix * rotationMatrix * scaleMatrix;
        }
    }
}
