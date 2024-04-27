using OpenTK.Mathematics;

namespace LearnOpenGL.Gaming
{
    public class MeshBuilder
    {
        private readonly List<float> _data = new();

        protected MeshBuilder()
        {

        }

        public MeshBuilder AddVertex(Vector3 position, Vector3 normal, Vector2 uv)
        {
            _data.Add(position.X);
            _data.Add(position.Y);
            _data.Add(position.Z);

            _data.Add(normal.X);
            _data.Add(normal.Y);
            _data.Add(normal.Z);

            _data.Add(uv.X);
            _data.Add(uv.Y);

            return this;
        }

        public Mesh Build()
        {
            return Mesh.Create(_data);
        }

        public static MeshBuilder Create()
        {
            return new MeshBuilder();
        }
    }
}
