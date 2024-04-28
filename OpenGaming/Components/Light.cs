using OpenTK.Mathematics;

namespace OpenGaming.Components
{
    public class Light : GameComponent
    {
        public LightType LightType { get; set; }

        public Vector3 Color { get; set; } = new Vector3(1, 1, 1);
        public float Range { get; set; } = MathF.PI / 4;
    }
}
