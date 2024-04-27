using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace OpenGaming
{
    public class Mesh : IDisposable
    {
        const int DataRowSize = 3 + 3 + 2;

        private readonly float[] _data;

        private readonly int _glBuffer;
        private readonly int _glVertexArray;

        private bool _disposedValue;

        /// <summary>
        /// data contains position(3float), normal(3float) vector and uv(2float),
        /// </summary>
        /// <param name="data"></param>
        protected Mesh(IList<float> data)
        {
            if (data.Count % DataRowSize != 0)
            {
                throw new ArgumentException("Invalid count of data", nameof(data));
            }

            _data = new float[data.Count];
            data.CopyTo(_data, 0);

            _glBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _glBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * _data.Length, _data, BufferUsageHint.StaticDraw);

            _glVertexArray = GL.GenVertexArray();
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * DataRowSize, 0);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, sizeof(float) * DataRowSize, 0);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, sizeof(float) * DataRowSize, 0);

            GL.EnableVertexArrayAttrib(_glVertexArray, 0);
            GL.EnableVertexArrayAttrib(_glVertexArray, 1);
            GL.EnableVertexArrayAttrib(_glVertexArray, 2);
        }

        ~Mesh()
        {
            Dispose(disposing: false);
        }

        public virtual void Use()
        {
            GL.BindVertexArray(_glVertexArray);
            GL.DrawArrays(PrimitiveType.Triangles, 0, _data.Length);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                GL.DeleteBuffer(_glBuffer);
                GL.DeleteVertexArray(_glVertexArray);

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public static Mesh Create(IList<float> data)
        {
            return new Mesh(data);
        }
    }
}
