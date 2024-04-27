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

        public Transform()
        {
            Scale = Vector3.One;
            Rotation = Quaternion.Identity;
        }

        public Matrix4 GetModelMatrix()
        {
            var rotationMatrix = Matrix4.CreateFromQuaternion(Rotation);
            var scaleMatrix = Matrix4.CreateScale(Scale);
            var translateMatrix = Matrix4.CreateTranslation(Position);

            var finalMatrix = rotationMatrix * scaleMatrix * translateMatrix;
            return finalMatrix;
        }

        public void Translate(Vector3 offset)
        {
            Position += offset;
        }

        public void Rotate(Quaternion rotation)
        {
            Rotation = rotation * Rotation;
        }

        public void Rotate(Vector3 eulerAngles)
        {
            Rotation = Quaternion.FromEulerAngles(eulerAngles) * Rotation;
        }
    }
}
