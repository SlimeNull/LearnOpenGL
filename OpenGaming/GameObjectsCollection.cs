using System.Collections;

namespace OpenGaming;

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
