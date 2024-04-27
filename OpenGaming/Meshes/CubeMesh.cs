using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGaming;

namespace OpenGaming.Meshes
{
    public class CubeMesh : Mesh
    {
        static readonly float[] _meshData = new float[]
        {
            // front
            -0.5f, -0.5f, 0.5f, 0, 0, 1, 0, 0,
            -0.5f, 0.5f, 0.5f, 0, 0, 1, 0, 1,
            0.5f, 0.5f, 0.5f, 0, 0, 1, 1, 1,

            -0.5f, -0.5f, 0.5f, 0, 0, 1, 0, 0,
            0.5f, 0.5f, 0.5f, 0, 0, 1, 1, 1,
            0.5f, -0.5f, 0.5f, 0, 0, 1, 1, 0,

            // top
            -0.5f, 0.5f, 0.5f, 0, 1, 0, 0, 0,
            -0.5f, 0.5f, -0.5f, 0, 1, 0, 0, 1,
            0.5f, 0.5f, -0.5f, 0, 1, 0, 1, 1,

            -0.5f, 0.5f, 0.5f, 0, 1, 0, 0, 0,
            0.5f, 0.5f, -0.5f, 0, 1, 0, 1, 1,
            0.5f, 0.5f, 0.5f, 0, 1, 0, 1, 0,
        
            // bottom
            -0.5f, -0.5f, -0.5f, 0, -1, 0, 0, 0,
            -0.5f, -0.5f, 0.5f, 0, -1, 0, 0, 1,
            0.5f, -0.5f, 0.5f, 0, -1, 0, 1, 1,

            -0.5f, -0.5f, -0.5f, 0, -1, 0, 0, 0,
            0.5f, -0.5f, 0.5f, 0, -1, 0, 1, 1,
            0.5f, -0.5f, -0.5f, 0, -1, 0, 1, 0,

            // right
            0.5f, -0.5f, 0.5f, 1, 0, 0, 0, 0,
            0.5f, 0.5f, 0.5f, 1, 0, 0, 0, 1,
            0.5f, 0.5f, -0.5f, 1, 0, 0, 1, 1,

            0.5f, -0.5f, 0.5f, 1, 0, 0, 0, 0,
            0.5f, 0.5f, -0.5f, 1, 0, 0, 1, 1,
            0.5f, -0.5f, -0.5f, 1, 0, 0, 1, 0,

            // left
            -0.5f, -0.5f, -0.5f, -1, 0, 0, 0, 0,
            -0.5f, 0.5f, -0.5f, -1, 0, 0, 0, 1,
            -0.5f, 0.5f, 0.5f, -1, 0, 0, 1, 1,

            -0.5f, -0.5f, -0.5f, -1, 0, 0, 0, 0,
            -0.5f, 0.5f, 0.5f, -1, 0, 0, 1, 1,
            -0.5f, -0.5f, 0.5f, -1, 0, 0, 1, 0,

            // back
            0.5f, -0.5f, -0.5f, 0, 0, -1, 0, 0,
            0.5f, 0.5f, -0.5f, 0, 0, -1, 0, 1,
            -0.5f, 0.5f, -0.5f, 0, 0, -1, 1, 1,

            0.5f, -0.5f, -0.5f, 0, 0, -1, 0, 0,
            -0.5f, 0.5f, -0.5f, 0, 0, -1, 1, 1,
            -0.5f, -0.5f, -0.5f, 0, 0, -1, 1, 0,
        };

        protected CubeMesh() : base(_meshData)
        {
        }

        public static CubeMesh Create()
            => new CubeMesh();
    }
}
