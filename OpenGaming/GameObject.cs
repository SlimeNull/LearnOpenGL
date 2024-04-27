namespace OpenGaming;

public class GameObject
{
    public Game? Owner { get; internal set; }

    public bool IsActive { get; set; } = true;

    public GameObjectComponentsCollection Components { get; }

    public GameObject()
    {
        Components = new(this);
    }
}
