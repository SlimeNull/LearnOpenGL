using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OpenGaming;

public class Game
{
    public GameObjectsCollection Objects { get; }

    public KeyboardState? KeyboardState { get; set; }
    public MouseState? MouseState { get; set; }

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

    public void GameUpdate(float deltaTime)
    {
        foreach (var gameObject in Objects)
        {
            if (!gameObject.IsActive)
            {
                continue;
            }

            foreach (var gameObjectComponent in gameObject.Components)
            {
                gameObjectComponent.GameUpdate(deltaTime);
            }
        }
    }

    public void GameLateUpdate(float deltaTime)
    {
        foreach (var gameObject in Objects)
        {
            if (!gameObject.IsActive)
            {
                continue;
            }

            foreach (var gameObjectComponent in gameObject.Components)
            {
                gameObjectComponent.GameLateUpdate(deltaTime);
            }
        }
    }
}
