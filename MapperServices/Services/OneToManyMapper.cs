using System;
using System.Collections.Generic;
using System.Linq;

namespace MapperServices
{
    public class OneToManyMapper : IOneToManyMapper
    {
        // Using two hashTable to store data because it helps on the search methods.
        // Could be a problem if the set get too large
        private readonly Dictionary<int, int> _childrensMappings = new Dictionary<int, int>(131073);
        private readonly Dictionary<int, List<int>> _parentsMappings = new Dictionary<int, List<int>>(131073);

        private const int _minimumIdentifier = 1;
        private const int _maximumIdentifier = 131072;

        public void Add(int parent, int child)
        {
            if (ValidateIdentifierConstraints(parent))
            {
                throw new ArgumentException("Identifier outside the constraints", nameof(parent));
            }

            if (ValidateIdentifierConstraints(child))
            {
                throw new ArgumentException("Identifier outside the constraints", nameof(child));
            }

            if (_childrensMappings.ContainsKey(child))
            {
                throw new Exception($"Relationship already defined for child {child}");
            }

            _childrensMappings.Add(child, parent);

            if (!_parentsMappings.ContainsKey(parent))
            {
                _parentsMappings.Add(parent, new List<int> { child });
            }
            else
            {
                _parentsMappings[parent].Add(child);
            }
        }

        public IEnumerable<int> GetChildren(int parent)
        {
            return _parentsMappings.ContainsKey(parent) ? _parentsMappings[parent] : Enumerable.Empty<int>();
        }

        public int GetParent(int child)
        {
            return _childrensMappings.ContainsKey(child) ? _childrensMappings[child] : 0;
        }

        public void RemoveChild(int child)
        {
            ContinueIfChildrenExist(child);

            var parent = _childrensMappings[child];
            _childrensMappings.Remove(child);

            _parentsMappings[parent].Remove(child);
        }

        public void RemoveParent(int parent)
        {
            ContinueIfParentExist(parent);

            var childrens = _parentsMappings[parent];
            _parentsMappings.Remove(parent);

            foreach (var children in childrens)
            {
                _childrensMappings.Remove(children);
            }
        }

        public void UpdateChild(int oldChild, int newChild)
        {
            if (ValidateIdentifierConstraints(newChild))
            {
                throw new ArgumentException("Identifier outside the constraints", nameof(newChild));
            }

            ContinueIfChildrenExist(oldChild);

            var parent = _childrensMappings[oldChild];
            _childrensMappings.Remove(oldChild);
            _childrensMappings.Add(newChild, parent);

            _parentsMappings[parent].Remove(oldChild);
            _parentsMappings[parent].Add(newChild);
        }

        public void UpdateParent(int oldParent, int newParent)
        {
            if (ValidateIdentifierConstraints(newParent))
            {
                throw new ArgumentException("Identifier outside the constraints", nameof(newParent));
            }

            ContinueIfParentExist(oldParent);

            var childrens = _parentsMappings[oldParent];
            _parentsMappings.Remove(oldParent);
            _parentsMappings.Add(newParent, childrens);

            foreach (var children in childrens)
            {
                _childrensMappings[children] = newParent;
            }
        }

        /// <summary>
        /// Throws an exception if children is not found on the HashTable
        /// </summary>
        /// <param name="child"></param>
        private void ContinueIfChildrenExist(int child)
        {
            if (!_childrensMappings.ContainsKey(child))
            {
                throw new Exception($"Relationship doesn't exist for child {child}");
            }
        }
        /// <summary>
        /// Throws an exception if parent is cannot be found on the HashTable
        /// </summary>
        /// <param name="parent"></param>
        private void ContinueIfParentExist(int parent)
        {
            if (!_parentsMappings.ContainsKey(parent))
            {
                throw new Exception($"Relationship doesn't exist for parent {parent}");
            }
        }
        private bool ValidateIdentifierConstraints(int identifier)
        {
            return identifier < _minimumIdentifier || identifier > _maximumIdentifier;
        }
    }
}
