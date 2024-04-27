namespace OpenGaming;

public class GameObject
{
    public bool IsActive { get; set; } = true;

    public GameObjectComponentsCollection Components { get; }

    public GameObject()
    {
        Components = new(this);
    }
}
