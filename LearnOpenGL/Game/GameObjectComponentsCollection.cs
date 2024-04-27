using System.Collections;

namespace LearnOpenGL.Gaming;

public class GameObjectComponentsCollection : ICollection<GameComponent>
{
    private readonly List<GameComponent> _stroage = new();

    public GameObject Owner { get; }

    public int Count => ((ICollection<GameComponent>)_stroage).Count;

    public bool IsReadOnly => ((ICollection<GameComponent>)_stroage).IsReadOnly;

    public GameObjectComponentsCollection(GameObject owner)
    {
        Owner = owner;
    }

    public void Add(GameComponent item) => ((ICollection<GameComponent>)_stroage).Add(item);
    public void Clear() => ((ICollection<GameComponent>)_stroage).Clear();
    public bool Contains(GameComponent item) => ((ICollection<GameComponent>)_stroage).Contains(item);
    public void CopyTo(GameComponent[] array, int arrayIndex) => ((ICollection<GameComponent>)_stroage).CopyTo(array, arrayIndex);
    public bool Remove(GameComponent item) => ((ICollection<GameComponent>)_stroage).Remove(item);
    public IEnumerator<GameComponent> GetEnumerator() => ((IEnumerable<GameComponent>)_stroage).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_stroage).GetEnumerator();

    public TComponent? GetComponent<TComponent>() where TComponent : GameComponent
    {
        return _stroage
            .OfType<TComponent>()
            .FirstOrDefault();
    }

    public TComponent? RemoveComponent<TComponent>() where TComponent : GameComponent
    {
        int index = 0;
        while (index < _stroage.Count)
        {
            if (_stroage[index] is not TComponent component)
                continue;

            _stroage.RemoveAt(index);
            return component;
        }

        return null;
    }

    public IEnumerable<TComponent> RemoveComponents<TComponent>() where TComponent : GameComponent
    {
        int index = 0;
        while (index < _stroage.Count)
        {
            if (_stroage[index] is not TComponent component)
                continue;

            _stroage.RemoveAt(index);
            yield return component;
        }
    }
}