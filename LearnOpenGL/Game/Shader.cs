using OpenTK.Graphics.OpenGL4;

namespace LearnOpenGL.Gaming;

public class Shader : IDisposable
{
    private bool _disposedValue;

    public int ProgramHandle { get; set; }

    protected Shader(
        TextReader vertexShaderSource,
        TextReader fragmentShaderSource)
    {
        string vertexShaderString = vertexShaderSource.ReadToEnd();
        string fragmentShaderString = fragmentShaderSource.ReadToEnd();

        var vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderString);
        GL.CompileShader(vertexShader);
        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int vertexShaderCompileSuccess);
        if (vertexShaderCompileSuccess != (int)All.True)
        {
            string info = GL.GetShaderInfoLog(vertexShader);
            throw new Exception($"Failed to compile vertex shader. {info}");
        }

        var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderString);
        GL.CompileShader(fragmentShader);
        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out int fragmentShaderCompileSuccess);
        if (fragmentShaderCompileSuccess != (int)All.True)
        {
            string info = GL.GetShaderInfoLog(fragmentShader);
            throw new Exception($"Failed to compile fragment shader. {info}");
        }

        ProgramHandle = GL.CreateProgram();
        GL.AttachShader(ProgramHandle, vertexShader);
        GL.AttachShader(ProgramHandle, fragmentShader);

        GL.LinkProgram(ProgramHandle);
        GL.GetProgram(ProgramHandle, GetProgramParameterName.LinkStatus, out var programLinkSuccess);
        if (programLinkSuccess != (int)All.True)
        {
            string info = GL.GetProgramInfoLog(ProgramHandle);
            throw new Exception($"Failed to link program. {info}");
        }

        GL.DetachShader(ProgramHandle, vertexShader);
        GL.DetachShader(ProgramHandle, fragmentShader);
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }
    ~Shader()
    {
        // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        Dispose(disposing: false);
    }

    public void Use()
    {
        GL.UseProgram(ProgramHandle);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                GL.DeleteProgram(ProgramHandle);
            }

            // TODO: 释放未托管的资源(未托管的对象)并重写终结器
            // TODO: 将大型字段设置为 null
            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public static Shader Create(TextReader vertexShaderSource, TextReader fragmentShaderSource)
    {
        return new Shader(vertexShaderSource, fragmentShaderSource);
    }

    public static Shader Create(string vertexShaderSource, string fragmentShaderSource)
    {
        return new Shader(
            new StringReader(vertexShaderSource),
            new StringReader(fragmentShaderSource));
    }
}