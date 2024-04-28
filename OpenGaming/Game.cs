using System.Collections;
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



    public class GameObjectsCollection : ICollection<GameObject>
    {
        private readonly List<GameObject> _storage = new();

        public Game Owner { get; }

        public int Count => ((ICollection<GameObject>)_storage).Count;

        public bool IsReadOnly => ((ICollection<GameObject>)_storage).IsReadOnly;

        public GameObjectsCollection(Game owner)
        {
            Owner = owner;
        }

        public void Add(GameObject gameObject)
        {
            if (gameObject.Owner is not null)
            {
                throw new ArgumentException("The GameObject already has an owner", nameof(gameObject));
            }

            gameObject.Owner = Owner;
            ((ICollection<GameObject>)_storage).Add(gameObject);
        }

        public void Clear()
        {
            foreach (var gameObject in _storage)
            {
                gameObject.Owner = null;
            }

            ((ICollection<GameObject>)_storage).Clear();
        }

        public bool Remove(GameObject item)
        {
            bool removed = ((ICollection<GameObject>)_storage).Remove(item);
            if (removed)
            {
                item.Owner = null;
            }

            return removed;
        }

        public bool Contains(GameObject item)
        {
            return ((ICollection<GameObject>)_storage).Contains(item);
        }

        public void CopyTo(GameObject[] array, int arrayIndex)
        {
            ((ICollection<GameObject>)_storage).CopyTo(array, arrayIndex);
        }

        public IEnumerator<GameObject> GetEnumerator() => ((IEnumerable<GameObject>)_storage).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_storage).GetEnumerator();
    }
}
