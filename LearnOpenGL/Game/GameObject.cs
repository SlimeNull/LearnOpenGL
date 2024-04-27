namespace LearnOpenGL.Gaming;

public class GameObject
{
    public GameObjectComponentsCollection Components { get; }

    public GameObject()
    {
        Components = new(this);
    }
}
