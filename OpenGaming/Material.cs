using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace OpenGaming;

public class Material
{
    private readonly Dictionary<string, object> _propertyValues = new();

    public Shader Shader { get; }

    protected Material(Shader shader)
    {
        Shader = shader;
    }

    public T? Get<T>(string name)
    {
        if (!_propertyValues.TryGetValue(name, out var value))
        {
            return default;
        }

        if (value is not T finalValue)
        {
            throw new InvalidOperationException($"Target value is not {typeof(T).Name}");
        }

        return finalValue;
    }

    public void Set(string name, Vector4 value)
        => _propertyValues[name] = value;

    public void Set(string name, Vector3 value)
        => _propertyValues[name] = value;

    public void Set(string name, Vector2 value)
        => _propertyValues[name] = value;

    public void Set(string name, Matrix4 value)
        => _propertyValues[name] = value;

    public void Set(string name, Matrix3 value)
        => _propertyValues[name] = value;

    public void Set(string name, Matrix2 value)
        => _propertyValues[name] = value;

    public void Set(string name, float value)
        => _propertyValues[name] = value;

    public bool Unset(string name)
        => _propertyValues.Remove(name);

    public virtual void Use()
    {
        Shader.Use();

        foreach (var propertyValue in _propertyValues)
        {
            switch (propertyValue.Value)
            {
                case Matrix4 matrix4:
                    GL.ProgramUniformMatrix4(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key), false, ref matrix4);
                    break;

                case Matrix3 matrix3:
                    GL.ProgramUniformMatrix3(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key), false, ref matrix3);
                    break;

                case Matrix2 matrix2:
                    GL.ProgramUniformMatrix2(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key), false, ref matrix2);
                    break;

                case Vector4 vector4:
                    GL.ProgramUniform4(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key), vector4);
                    break;

                case Vector3 vector3:
                    GL.ProgramUniform3(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key), vector3);
                    break;

                case Vector2 vector2:
                    GL.ProgramUniform2(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key), vector2);
                    break;

                case float floatNumber:
                    GL.ProgramUniform1(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key), floatNumber);
                    break;
            }
        }
    }

    public static Material Create(Shader shader)
        => new Material(shader);
}
