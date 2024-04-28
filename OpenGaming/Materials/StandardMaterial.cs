using OpenGaming.Shaders;
using OpenGaming;
using OpenTK.Mathematics;
using OpenGaming.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenGaming.Textures;

namespace OpenGaming.Materials
{
    public class StandardMaterial : Material, IStandardMaterial
    {
        // material
        const string uniformNameColor = "color";
        const string uniformNameColorTexture = "colorTexture";

        // matrix
        const string uniformNameModelMatrix = "modelMatrix";
        const string uniformNameViewMatrix = "viewMatrix";
        const string uniformNameProjectionMatrix = "projectionMatrix";

        // lighting
        const string uniformNameViewPosition = "viewPosition";
        const string uniformNameSpotLightCount = "spotLightCount";
        const string uniformNameDirectionalLightCount = "directionalLightCount";
        const string uniformNamePointLightCount = "pointLightCount";
        const string uniformNamePointLights = "pointLights";

        static string GetUniformNamePointLightsPosition(int index) => $"pointLights[{index}].position";
        static string GetUniformNamePointLightsColor(int index) => $"pointLights[{index}].color";


        protected StandardMaterial() : base(StandardShader.Instance, [
            new MaterialPropertyKey(uniformNameColor, typeof(Vector3)),
            new MaterialPropertyKey(uniformNameColorTexture, typeof(Texture2D)),
            new MaterialPropertyKey(uniformNameModelMatrix, typeof(Matrix4)),
            new MaterialPropertyKey(uniformNameViewMatrix, typeof(Matrix4)),
            new MaterialPropertyKey(uniformNameModelMatrix, typeof(Matrix4)),
            new MaterialPropertyKey(uniformNameViewPosition, typeof(Vector3)),
            new MaterialPropertyKey(uniformNameSpotLightCount, typeof(int)),
            new MaterialPropertyKey(uniformNameDirectionalLightCount, typeof(int)),
            new MaterialPropertyKey(uniformNamePointLightCount, typeof(int)),
        ])
        {

        }

        public Matrix4 ModelMatrix { get => Get<Matrix4>(uniformNameModelMatrix); set => Set(uniformNameModelMatrix, value); }
        public Matrix4 ViewMatrix { get => Get<Matrix4>(uniformNameViewMatrix); set => Set(uniformNameViewMatrix, value); }
        public Matrix4 ProjectionMatrix { get => Get<Matrix4>(uniformNameProjectionMatrix); set => Set(uniformNameProjectionMatrix, value); }
        public Vector3 ViewPosition { get => Get<Vector3>(uniformNameViewPosition); set => Set(uniformNameViewPosition, value); }

        public List<PointLightData>? PointLights { get; set; }
        public List<SpotLightData>? SpotLights { get; set; }
        public List<DirectionalLightData>? DirectionalLights { get; set; }

        public Vector3 Color { get => Get<Vector3>(uniformNameColor); set => Set(uniformNameColor, value); }
        public Texture2D? ColorTexture
        {
            get => Get<Texture2D>(uniformNameColorTexture);
            set => Set(uniformNameColorTexture, value);
        }

        public override void Use()
        {
            base.Use();

            if (PointLights is null)
            {
                GL.Uniform1(GL.GetUniformLocation(Shader.ProgramHandle, uniformNamePointLightCount), 0);
            }
            else
            {
                GL.Uniform1(GL.GetUniformLocation(Shader.ProgramHandle, uniformNamePointLightCount), PointLights.Count);

                for (int i = 0; i < PointLights.Count; i++)
                {
                    PointLightData pointLight = PointLights[i];
                    GL.Uniform3(GL.GetUniformLocation(Shader.ProgramHandle, GetUniformNamePointLightsPosition(i)), pointLight.Position);
                    GL.Uniform3(GL.GetUniformLocation(Shader.ProgramHandle, GetUniformNamePointLightsColor(i)), pointLight.Color);
                }
            }
        }

        public static StandardMaterial Create()
            => new StandardMaterial();
    }
}
