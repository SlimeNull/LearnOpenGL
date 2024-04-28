using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace OpenGaming.Components
{
    public class Transform : GameComponent
    {
        public Transform? Parent { get; internal set; }

        public Vector3 Position { get; set; }
        public Vector3 Scale { get; set; }
        public Quaternion Rotation { get; set; }

        public Vector3 WorldPosition
        {
            get
            {
                // TODO: 允许 Transform 有父子关系

                var matrix = Matrix4.Identity;
                return Position;
            }
            set
            {
                Position = value;
            }
        }

        public Transform()
        {
            Scale = Vector3.One;
            Rotation = Quaternion.Identity;
        }

        internal Matrix4 GetMatrix()
        {
            var rotationMatrix = Matrix4.CreateFromQuaternion(Rotation);
            var scaleMatrix = Matrix4.CreateScale(Scale);
            var translateMatrix = Matrix4.CreateTranslation(Position);

            var finalMatrix = rotationMatrix * scaleMatrix * translateMatrix;
            return finalMatrix;
        }

        internal Vector3 ParentToLocalPosition(Vector3 parentPosition)
        {
            var matrix = GetMatrix();
            var invertedMatrix = matrix.Inverted();

            return new Vector3(invertedMatrix * new Vector4(parentPosition, 1));
        }

        internal Vector3 LocalToParentPosition(Vector3 localPosition)
        {
            var matrix = GetMatrix();

            return new Vector3(matrix * new Vector4(localPosition, 1));
        }

        internal Vector3 ParentToLocalVector(Vector3 parentPosition)
        {
            var matrix = GetMatrix();
            var invertedMatrix = matrix.Inverted();

            return new Vector3(invertedMatrix * new Vector4(parentPosition, 0));
        }

        internal Vector3 LocalToParentVector(Vector3 localPosition)
        {
            var matrix = GetMatrix();

            return new Vector3(matrix * new Vector4(localPosition, 0));
        }

        public Matrix4 GetModelMatrix()
        {
            Matrix4 modelMatrix = GetMatrix();

            var parent = Parent;
            while (parent is not null)
            {
                modelMatrix = parent.GetMatrix() * modelMatrix;
                parent = parent.Parent;
            }

            return modelMatrix;
        }

        public Vector3 WorldToLocalPosition(Vector3 worldPosition)
        {
            var modelMatrix = GetModelMatrix();
            var invertedModelMatrix = modelMatrix.Inverted();

            var result = new Vector3(invertedModelMatrix * new Vector4(worldPosition, 1));
            return result;
        }

        public Vector3 LocalToWorldPosition(Vector3 localPosition)
        {
            var modelMatrix = GetModelMatrix();

            var result = new Vector3(modelMatrix * new Vector4(localPosition, 1));
            return result;
        }

        public Vector3 WorldToLocalVector(Vector3 parentPosition)
        {
            var modelMatrix = GetModelMatrix();
            var invertedModelMatrix = modelMatrix.Inverted();

            return new Vector3(invertedModelMatrix * new Vector4(parentPosition, 0));
        }

        public Vector3 LocalToWorldVector(Vector3 localPosition)
        {
            var modelMatrix = GetModelMatrix();

            return new Vector3(modelMatrix * new Vector4(localPosition, 0));
        }

        public void Translate(Vector3 offset, Space space)
        {
            if (space == Space.World)
            {
                var localPositionOffset = WorldToLocalVector(offset);
                Position += localPositionOffset;
            }
            else
            {
                var localPositionOffset = ParentToLocalVector(offset);
                Position += localPositionOffset;
            }
        }

        public void Rotate(Quaternion rotation, Space space)
        {
            if (space == Space.World)
            {
                Rotation = rotation * Rotation;
            }
            else
            {
                Rotation = Rotation * rotation;
            }
        }

        public void Rotate(Vector3 eulerAngles, Space space)
        {
            if (space == Space.World)
            {
                Rotation = Quaternion.FromEulerAngles(eulerAngles) * Rotation;
            }
            else
            {
                Rotation = Rotation * Quaternion.FromEulerAngles(eulerAngles);
            }
        }


        public class TransformChildrenCollection : ICollection<Transform>
        {
            private readonly List<Transform> _storage = new();

            public Transform Owner { get; }

            public int Count => ((ICollection<Transform>)_storage).Count;

            public bool IsReadOnly => ((ICollection<Transform>)_storage).IsReadOnly;

            public TransformChildrenCollection(Transform owner)
            {
                Owner = owner;
            }

            public void Add(Transform transform)
            {
                if (transform.Parent is not null)
                {
                    throw new ArgumentException("The Transform already has a parent");
                }

                transform.Parent = Owner;
                ((ICollection<Transform>)_storage).Add(transform);
            }

            public void Clear()
            {
                foreach (var transform in _storage)
                {
                    transform._owner = null;
                }

                ((ICollection<Transform>)_storage).Clear();
            }
            public bool Remove(Transform transform)
            {
                bool removed = ((ICollection<Transform>)_storage).Remove(transform);
                if (removed)
                {
                    transform._owner = null;
                }

                return removed;
            }

            internal void Swap(int index1, int index2)
            {
                (_storage[index1], _storage[index2]) = (_storage[index2], _storage[index1]);
            }

            public bool Contains(Transform item) => ((ICollection<Transform>)_storage).Contains(item);
            public void CopyTo(Transform[] array, int arrayIndex) => ((ICollection<Transform>)_storage).CopyTo(array, arrayIndex);
            public IEnumerator<Transform> GetEnumerator() => ((IEnumerable<Transform>)_storage).GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_storage).GetEnumerator();
        }
    }
}
