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
        private bool _disposedValue;

        public int TextureHandle { get; }

        protected Texture2D(
            PixelInternalFormat pixelInternalFormat,
            PixelFormat pixelFormat,
            int width,
            int height,
            byte[] data)
        {
            TextureHandle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, TextureHandle);
            GL.TexImage2D(TextureTarget.Texture2D, 0, pixelInternalFormat, width, height, 0, pixelFormat, PixelType.UnsignedByte, data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.GenerateTextureMipmap(TextureHandle);
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
                GL.DeleteTexture(TextureHandle);
                _disposedValue = true;
            }
        }

        public void Use(int index)
        {
            if (index < 0 || index >= 32)
                throw new ArgumentOutOfRangeException(nameof(index));

            GL.ActiveTexture(TextureUnit.Texture0 + index);
            GL.BindTexture(TextureTarget.Texture2D, TextureHandle);
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
