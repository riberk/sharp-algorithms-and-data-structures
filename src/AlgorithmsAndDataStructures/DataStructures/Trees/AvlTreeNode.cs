using System;
using System.Collections.Generic;

namespace AlgorithmsAndDataStructures.DataStructures.Trees
{
    public class AvlTreeNode<T>
    {
        public AvlTreeNode(T value)
        {
            Value = value;
            Height = 1;
        }
        
        public T Value { get; }
        
        private int Height { get; set; }

        public AvlTreeNode<T>? Left { get; private set; } 
        
        public AvlTreeNode<T>? Right { get; private set; }

        private int BalanceFactor() => (Right?.Height ?? 0) - (Left?.Height ?? 0);

        private void FixHeight()
        {
            var hl = Left?.Height ?? 0;
            var hr = Right?.Height ?? 0;
            Height = (hl > hr ? hl : hr) + 1;
        }

        private AvlTreeNode<T> RotateRight()
        {
            var a = this;
            var b = a.Left!;
            
            a.Left = b.Right;
            b.Right = a;
            
            a.FixHeight();
            b.FixHeight();
            
            return b;
        }
        
        private AvlTreeNode<T> RotateLeft()
        {
            var a = this;
            var b = a.Right!;
            
            a.Right = b.Left;
            b.Left = a;
            
            a.FixHeight();
            b.FixHeight();
            
            return b;
        }

        private AvlTreeNode<T> Balance()
        {
            FixHeight();
            var balanceFactor = BalanceFactor();
            if (balanceFactor == 2)
            {
                if (Right!.BalanceFactor() < 0)
                {
                    Right = Right.RotateRight();
                }

                return RotateLeft();
            }

            if (balanceFactor == -2)
            {
                if (Left!.BalanceFactor() > 0)
                {
                    Left = Left.RotateLeft();
                }

                return RotateRight();
            }

            return this;
        }

        private AvlTreeNode<T> Min()
        {
            return Left != null ? Left.Min() : this;
        }

        private AvlTreeNode<T>? RemoveMin()
        {
            if (Left == null) return Right;
            Left = Left.RemoveMin();
            return Balance();
        }


        internal static AvlTreeNode<T>? Remove(AvlTreeNode<T>? root, T key, IComparer<T> comparer, int deep)
        {
            if (root == null) return null;
            if (comparer.Compare(key, root.Value) < 0)
            {
                root.Left = Remove(root.Left, key, comparer, deep + 1);
            }
            else if(comparer.Compare(key, root.Value) > 0)
            {
                root.Right = Remove(root.Right, key, comparer, deep + 1);
            }
            else
            {
                var beforeLeft = root.Left;
                var beforeRight = root.Right;
                if (beforeRight == null) return beforeLeft;
                var min = beforeRight.Min();
                min.Right = beforeRight.RemoveMin();
                min.Left = beforeLeft;
                return min.Balance();
            }

            return root.Balance();
        } 
        
        internal static AvlTreeNode<T> Add(AvlTreeNode<T>? root, T key, IComparer<T> comparer)
        {
            if (root == null) return new AvlTreeNode<T>(key);
            var compare = comparer.Compare(key, root.Value);
            if (compare < 0)
            {
                root.Left = Add(root.Left, key, comparer);
            }
            else if(compare > 0)
            {
                root.Right = Add(root.Right, key, comparer);
            }
            else
            {
                throw new InvalidOperationException("Key exists");
            }
            return root.Balance();
        }
    }
}