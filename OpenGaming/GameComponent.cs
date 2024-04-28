namespace OpenGaming;

public class GameComponent
{
    internal GameObject? _owner;

    public GameObject Owner 
    { 
        get => _owner ?? throw new InvalidOperationException("This game component is not added to any game object."); 
        internal set => _owner = value; 
    }

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
