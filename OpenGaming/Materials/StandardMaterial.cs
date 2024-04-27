using LearnOpenGL.Game.Shaders;
using OpenGaming;

namespace OpenGaming.Materials
{
    public class StandardMaterial : Material
    {
        protected StandardMaterial() : base(StandardShader.Instance)
        {

        }

        public static StandardMaterial Create()
            => new StandardMaterial();
    }
}
