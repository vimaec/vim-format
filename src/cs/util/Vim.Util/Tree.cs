using System.Collections.Generic;

namespace Vim.Util
{
    /// <summary>
    /// A generic tree data structure.
    /// </summary>
    public class Tree<T>
    {
        /// <summary>
        /// The value of the node.
        /// </summary>
        public T Value = default;

        /// <summary>
        /// The parent of this node.
        /// </summary>
        public Tree<T> Parent { get; private set; } = null;

        /// <summary>
        /// The children of this node.
        /// </summary>
        public readonly List<Tree<T>> Children = new List<Tree<T>>();

        /// <summary>
        /// Adds a child to this node and sets its parent.
        /// Any previous parent-child relationship of the child node is removed.
        /// </summary>
        public void AddChild(Tree<T> childNode)
        {
            if (childNode == null) { return; }
            childNode.Parent?.Children.Remove(childNode);
            childNode.Parent = this;
            Children.Add(childNode);
        }

        /// <summary>
        /// Sets the parent of this node and inserts this node into the children of the given parent.
        /// Any previous parent-child relationship of this node is removed.
        /// The given parentNode can be null, indicating that this node has no parent.
        /// </summary>
        public void SetParent(Tree<T> parentNode)
        {
            Parent?.Children.Remove(this);
            Parent = parentNode;
            Parent?.Children.Add(this);
        }

        /// <summary>
        /// Returns all the nodes in the tree including this node.
        /// A parent node will always be returned before its children.
        /// </summary>
        public IEnumerable<Tree<T>> AllNodes()
        {
            yield return this;

            if (Children != null)
            {
                foreach (var child in Children)
                {
                    foreach (var d in child.AllNodes())
                    {
                        yield return d;
                    }
                }
            }
        }
    }
}
