using OpenGaming.Shaders;
using OpenGaming;
using OpenTK.Mathematics;

namespace OpenGaming.Materials
{
    public class StandardMaterial : Material, IStandardMaterial
    {
        const string uniformNameModelMatrix = "modelMatrix";
        const string uniformNameViewMatrix = "viewMatrix";
        const string uniformNameProjectionMatrix = "projectionMatrix";

        const string uniformNameColor = "color";

        protected StandardMaterial() : base(StandardShader.Instance)
        {

        }

        public Matrix4 ModelMatrix { get => Get<Matrix4>(uniformNameModelMatrix); set => Set(uniformNameModelMatrix, value); }
        public Matrix4 ViewMatrix { get => Get<Matrix4>(uniformNameViewMatrix); set => Set(uniformNameViewMatrix, value); }
        public Matrix4 ProjectionMatrix { get => Get<Matrix4>(uniformNameProjectionMatrix); set => Set(uniformNameProjectionMatrix, value); }
        public Vector3 Color { get => Get<Vector3>(uniformNameColor); set => Set(uniformNameColor, value); }

        public static StandardMaterial Create()
            => new StandardMaterial();
    }
}
