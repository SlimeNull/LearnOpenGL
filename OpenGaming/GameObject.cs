using System.Collections;
using OpenGaming.Components;

namespace OpenGaming;

public class GameObject
{
    public Game? Owner { get; internal set; }

    public bool IsActive { get; set; } = true;

    public GameObjectComponentsCollection Components { get; }

    public GameObject()
    {
        Components = new(this);
    }



    public class GameObjectComponentsCollection : ICollection<GameComponent>
    {
        private readonly List<GameComponent> _storage = new();

        public GameObject Owner { get; }

        public Transform Transform => (Transform)_storage[0];

        public int Count => ((ICollection<GameComponent>)_storage).Count;

        public bool IsReadOnly => ((ICollection<GameComponent>)_storage).IsReadOnly;

        public GameObjectComponentsCollection(GameObject owner)
        {
            Owner = owner;

            // add transform component
            Add(new Transform());
        }

        public void Add(GameComponent component)
        {
            if (component.Owner is not null)
            {
                throw new ArgumentException("The component already has an owner", nameof(component));
            }

            component.Owner = Owner;
            ((ICollection<GameComponent>)_storage).Add(component);
        }

        public void Clear()
        {
            while (_storage.Count > 1)
            {
                var lastIndex = _storage.Count - 1;
                var component = _storage[lastIndex];
                component.Owner = null;

                _storage.RemoveAt(lastIndex);
            }

            ((ICollection<GameComponent>)_storage).Clear();
        }

        public bool Contains(GameComponent item)
        {
            return ((ICollection<GameComponent>)_storage).Contains(item);
        }
        public bool Remove(GameComponent item)
        {
            var removed = ((ICollection<GameComponent>)_storage).Remove(item);
            if (removed)
            {
                item.Owner = null;
            }

            return removed;
        }

        public void CopyTo(GameComponent[] array, int arrayIndex) => ((ICollection<GameComponent>)_storage).CopyTo(array, arrayIndex);
        public IEnumerator<GameComponent> GetEnumerator() => ((IEnumerable<GameComponent>)_storage).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_storage).GetEnumerator();

        public TComponent AddComponent<TComponent>() where TComponent : GameComponent, new()
        {
            var component = new TComponent();
            Add(component);

            return component;
        }

        public TComponent? Get<TComponent>() where TComponent : GameComponent
        {
            return _storage
                .OfType<TComponent>()
                .FirstOrDefault();
        }

        public TComponent GetRequired<TComponent>() where TComponent : GameComponent
        {
            var component = Get<TComponent>();
            if (component is null)
            {
                throw new InvalidOperationException("Component is not exist");
            }

            return component;
        }

        public TComponent? Remove<TComponent>() where TComponent : GameComponent
        {
            int index = 0;
            while (index < _storage.Count)
            {
                if (_storage[index] is not TComponent component)
                    continue;

                _storage.RemoveAt(index);
                component.Owner = null;
                return component;
            }

            return null;
        }

        public IEnumerable<TComponent> RemoveAll<TComponent>() where TComponent : GameComponent
        {
            int index = 0;
            while (index < _storage.Count)
            {
                if (_storage[index] is not TComponent component)
                    continue;

                _storage.RemoveAt(index);
                component.Owner = null;
                yield return component;
            }
        }
    }
}
