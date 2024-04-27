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

    public void Add(GameObject item) => ((ICollection<GameObject>)_storage).Add(item);
    public void Clear() => ((ICollection<GameObject>)_storage).Clear();
    public bool Contains(GameObject item) => ((ICollection<GameObject>)_storage).Contains(item);
    public void CopyTo(GameObject[] array, int arrayIndex) => ((ICollection<GameObject>)_storage).CopyTo(array, arrayIndex);
    public bool Remove(GameObject item) => ((ICollection<GameObject>)_storage).Remove(item);
    public IEnumerator<GameObject> GetEnumerator() => ((IEnumerable<GameObject>)_storage).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_storage).GetEnumerator();
}
