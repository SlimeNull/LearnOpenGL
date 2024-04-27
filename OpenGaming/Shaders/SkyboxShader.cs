using OpenGaming.Properties;

namespace OpenGaming.Shaders
{
    public class SkyboxShader : Shader
    {
        protected SkyboxShader() : base(
            new StringReader(Resources.SkyboxVertexShader),
            new StringReader(Resources.SkyboxFragmentShader))
        {

        }

        public static SkyboxShader Create()
            => new SkyboxShader();

        public static SkyboxShader? _instance;

        public static SkyboxShader Instance
            => _instance ??= new();
    }
}
