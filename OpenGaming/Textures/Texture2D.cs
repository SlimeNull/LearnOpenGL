using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace OpenGaming.Textures
{
    public class Texture2D : IDisposable
    {
        private int _texture;
        private bool _disposedValue;

        protected Texture2D(
            PixelInternalFormat pixelInternalFormat,
            PixelFormat pixelFormat,
            int width,
            int height,
            byte[] data)
        {
            _texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _texture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, pixelInternalFormat, width, height, 0, pixelFormat, PixelType.UnsignedByte, data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }

        ~Texture2D()
        {
            Dispose(disposing: false);
        }

        public static Texture2D Create(PixelInternalFormat pixelInternalFormat, PixelFormat pixelFormat, int width, int height, byte[] data)
            => new Texture2D(pixelInternalFormat, pixelFormat, width, height, data);

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                GL.DeleteTexture(_texture);
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
