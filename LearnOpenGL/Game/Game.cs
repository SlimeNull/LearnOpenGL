namespace LearnOpenGL.Gaming;

public class Game
{
    public GameObjectsCollection Objects { get; }

    public Game()
    {
        Objects = new(this);
    }

    public void Start()
    {
        foreach (var gameObject in Objects)
        {
            foreach (var gameObjectComponent in gameObject.Components)
            {
                gameObjectComponent.Start();
            }
        }
    }

    public void Update()
    {
        foreach (var gameObject in Objects)
        {
            foreach (var gameObjectComponent in gameObject.Components)
            {
                gameObjectComponent.Update();
            }
        }
    }

    public void LateUpdate()
    {
        foreach (var gameObject in Objects)
        {
            foreach (var gameObjectComponent in gameObject.Components)
            {
                gameObjectComponent.LateUpdate();
            }
        }
    }
}
