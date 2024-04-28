namespace OpenGaming.Rendering
{
    public class LightingData
    {
        public List<SpotLightData> SpotLights { get; } = new();
        public List<DirectionalLightData> DirectionalLights { get; } = new();
        public List<PointLightData> PointLights { get; } = new();

        public void Clear()
        {
            SpotLights.Clear();
            DirectionalLights.Clear();
            PointLights.Clear();
        }
    }
}
