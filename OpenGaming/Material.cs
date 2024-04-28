using OpenGaming.Textures;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace OpenGaming;

public class Material
{
    private readonly Dictionary<MaterialPropertyKey, object?> _propertyValues = new();

    protected record struct MaterialPropertyKey(string UniformName, Type Type);

    public Shader Shader { get; }

    protected Material(
        Shader shader,
        IEnumerable<MaterialPropertyKey> propertyKeys)
    {
        Shader = shader;

        foreach (var propertyKey in propertyKeys)
        {
            object? defaultValue = null;
            if (propertyKey.Type.IsValueType)
            {
                defaultValue = Activator.CreateInstance(propertyKey.Type);
            }

            _propertyValues[propertyKey] = defaultValue;
        }
    }

    public T? Get<T>(string name)
    {
        var key = new MaterialPropertyKey(name, typeof(T));
        if (!_propertyValues.TryGetValue(key, out var value))
        {
            return default;
        }

        if (value is not T finalValue)
        {
            throw new InvalidOperationException($"Target value is not {typeof(T).Name}");
        }

        return finalValue;
    }

    public void Set<T>(string name, T? value)
        => _propertyValues[new MaterialPropertyKey(name, typeof(T))] = value;

    public virtual void Use()
    {
        Shader.Use();

        int texture2dCount = 0;
        
        foreach (var propertyValue in _propertyValues)
        {
            switch (propertyValue.Value)
            {
                case Texture2D texture2D:
                    GL.ProgramUniform1(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key.UniformName), texture2dCount);
                    texture2D.Use(texture2dCount);
                    texture2dCount++;
                    break;

                case Matrix4 matrix4:
                    GL.ProgramUniformMatrix4(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key.UniformName), false, ref matrix4);
                    break;

                case Matrix3 matrix3:
                    GL.ProgramUniformMatrix3(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key.UniformName), false, ref matrix3);
                    break;

                case Matrix2 matrix2:
                    GL.ProgramUniformMatrix2(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key.UniformName), false, ref matrix2);
                    break;

                case Vector4 vector4:
                    GL.ProgramUniform4(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key.UniformName), vector4);
                    break;

                case Vector3 vector3:
                    GL.ProgramUniform3(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key.UniformName), vector3);
                    break;

                case Vector2 vector2:
                    GL.ProgramUniform2(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key.UniformName), vector2);
                    break;

                case float floatNumber:
                    GL.ProgramUniform1(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key.UniformName), floatNumber);
                    break;

                default:
                    if (propertyValue.Key.Type == typeof(Texture2D))
                    {
                        GL.ProgramUniform1(Shader.ProgramHandle, GL.GetUniformLocation(Shader.ProgramHandle, propertyValue.Key.UniformName), 0);
                    }
                    break;
            }
        }

        if (texture2dCount == 0)
        {

        }
    }

    public static Material CreateEmpty(Shader shader)
        => new Material(shader, []);
}
