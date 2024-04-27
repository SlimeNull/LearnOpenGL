using OpenTK.Mathematics;

namespace OpenGaming;

public interface IUniformMaterial
{
    public Matrix4 ModelMatrix { get; set; }
    public Matrix4 ViewMatrix { get; set; }
    public Matrix4 ProjectionMatrix { get; set; }
}
