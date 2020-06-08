using System.Collections.Generic;

namespace AlgorithmsAndDataStructures.DataStructures.Trees
{
    public class AvlTree<T> 
    {
        public AvlTree(IComparer<T> comparer)
        {
            Comparer = comparer;
        }

        public AvlTree() : this(Comparer<T>.Default)
        {
        }

        public IComparer<T> Comparer { get; }
        public AvlTreeNode<T>? Root { get; private set; }

        public void Add(T key)
        {
            Root = AvlTreeNode<T>.Add(Root, key, Comparer);
        }

        public void Remove(T key)
        {
            Root = AvlTreeNode<T>.Remove(Root, key, Comparer, 0);
        }
    }
}