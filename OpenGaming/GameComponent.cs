namespace OpenGaming;

public class GameComponent
{
    public GameObject? Owner { get; internal set; }

    public virtual void GameStart()
    {

    }

    public virtual void GameUpdate(float deltaTime)
    {

    }

    public virtual void GameLateUpdate(float deltaTime)
    {

    }
}
