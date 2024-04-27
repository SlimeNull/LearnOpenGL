using OpenTK.Mathematics;

namespace OpenGaming;

public interface IStandardMaterial
{
    public Matrix4 ModelMatrix { get; set; }
    public Matrix4 ViewMatrix { get; set; }
    public Matrix4 ProjectionMatrix { get; set; }

    public Vector3 Color { get; set; }
}
