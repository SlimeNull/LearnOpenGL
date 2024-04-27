using OpenTK.Windowing.Desktop;

namespace OpenGaming;

public class Game
{
    public GameObjectsCollection Objects { get; }

    public GameWindow? Output { get; set; }

    public Game()
    {
        Objects = new(this);
    }

    public void GameStart()
    {
        foreach (var gameObject in Objects)
        {
            if (!gameObject.IsActive)
            {
                continue;
            }

            foreach (var gameObjectComponent in gameObject.Components)
            {
                gameObjectComponent.GameStart();
            }
        }
    }

    public void GameUpdate()
    {
        foreach (var gameObject in Objects)
        {
            if (!gameObject.IsActive)
            {
                continue;
            }

            foreach (var gameObjectComponent in gameObject.Components)
            {
                gameObjectComponent.GameUpdate();
            }
        }
    }

    public void GameLateUpdate()
    {
        foreach (var gameObject in Objects)
        {
            if (!gameObject.IsActive)
            {
                continue;
            }

            foreach (var gameObjectComponent in gameObject.Components)
            {
                gameObjectComponent.GameLateUpdate();
            }
        }
    }
}
