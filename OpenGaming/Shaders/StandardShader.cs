using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGaming;
using OpenGaming.Properties;
using OpenTK.Mathematics;

namespace LearnOpenGL.Game.Shaders
{
    public class StandardShader : Shader
    {
        protected StandardShader() : base(
            new StringReader(Resources.StandardVertexShader),
            new StringReader(Resources.StandardFragmentShader))
        {

        }

        public static StandardShader Create()
            => new StandardShader();

        private static StandardShader? _instance;

        public static StandardShader Instance
            => _instance ??= new();
    }
}
