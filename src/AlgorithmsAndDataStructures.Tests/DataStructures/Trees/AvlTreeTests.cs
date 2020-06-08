using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AlgorithmsAndDataStructures.DataStructures.Trees;
using Xunit;
using Xunit.Abstractions;

namespace AlgorithmsAndDataStructures.Tests.DataStructures.Trees
{
    public class AvlTreeTests
    {
        private readonly ITestOutputHelper _console;

        public AvlTreeTests(ITestOutputHelper console)
        {
            _console = console;
        }
        
        [Fact]
        public void Add_NoRotation()
        {
            var tree = new AvlTree<int>();
            tree.Add(30);
            tree.Add(20);
            tree.Add(40);
            tree.Add(10);
            tree.Add(50);
            tree.Add(25);
            tree.Add(35);

            Assert.Equal(30, tree.Root.Value);
            Assert.Equal(20, tree.Root.Left.Value);
            Assert.Equal(10, tree.Root.Left.Left.Value);
            Assert.Equal(25, tree.Root.Left.Right.Value);
            Assert.Equal(40, tree.Root.Right.Value);
            Assert.Equal(35, tree.Root.Right.Left.Value);
            Assert.Equal(50, tree.Root.Right.Right.Value);
            
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void Add_LeftRotation()
        {
            var tree = new AvlTree<int>();
            tree.Add(4);
            tree.Add(3);
            tree.Add(2);
            tree.Add(1);
            tree.Add(0);

            Assert.Equal(3, tree.Root?.Value);
            Assert.Equal(1, tree.Root?.Left?.Value);
            Assert.Equal(0, tree.Root?.Left?.Left?.Value);
            Assert.Equal(2, tree.Root?.Left?.Right?.Value);
            Assert.Equal(4, tree.Root?.Right?.Value);
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void Add_LeftRotationOnRoot()
        {
            var tree = new AvlTree<int>();
            tree.Add(30);
            tree.Add(20);
            tree.Add(40);
            tree.Add(10);
            tree.Add(25);
            tree.Add(5);

            Assert.Equal(20, tree.Root.Value);
            Assert.Equal(10, tree.Root.Left.Value);
            Assert.Equal(30, tree.Root.Right.Value);
            Assert.Equal(5, tree.Root.Left.Left.Value);
            Assert.Equal(25, tree.Root.Right.Left.Value);
            Assert.Equal(40, tree.Root.Right.Right.Value);
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void Add_LeftRotationOnLeftChild()
        {
            var tree = new AvlTree<int>();
            tree.Add(50);
            tree.Add(30);
            tree.Add(60);
            tree.Add(20);
            tree.Add(40);
            tree.Add(55);
            tree.Add(65);
            tree.Add(10);
            tree.Add(25);
            tree.Add(5);

            Assert.Equal(50, tree.Root.Value);
            Assert.Equal(20, tree.Root.Left.Value);
            Assert.Equal(10, tree.Root.Left.Left.Value);
            Assert.Equal(5, tree.Root.Left.Left.Left.Value);
            Assert.Null(tree.Root.Left.Left.Right);
            Assert.Equal(30, tree.Root.Left.Right.Value);
            Assert.Equal(25, tree.Root.Left.Right.Left.Value);
            Assert.Equal(40, tree.Root.Left.Right.Right.Value);
            Assert.Equal(60, tree.Root.Right.Value);
            Assert.Equal(55, tree.Root.Right.Left.Value);
            Assert.Equal(65, tree.Root.Right.Right.Value);
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void Add_LeftRotationOnRight()
        {
            var tree = new AvlTree<int>();

            tree.Add(15);
            tree.Add(5);
            tree.Add(50);
            tree.Add(2);
            tree.Add(10);
            tree.Add(30);
            tree.Add(60);
            tree.Add(3);
            tree.Add(7);
            tree.Add(12);
            tree.Add(25);
            tree.Add(40);
            tree.Add(20);

            Assert.Equal(15, tree.Root.Value);
            Assert.Equal(5, tree.Root.Left.Value);
            Assert.Equal(2, tree.Root.Left.Left.Value);
            Assert.Equal(3, tree.Root.Left.Left.Right.Value);
            Assert.Equal(10, tree.Root.Left.Right.Value);
            Assert.Equal(7, tree.Root.Left.Right.Left.Value);
            Assert.Equal(12, tree.Root.Left.Right.Right.Value);
            Assert.Equal(30, tree.Root.Right.Value);
            Assert.Equal(25, tree.Root.Right.Left.Value);
            Assert.Equal(20, tree.Root.Right.Left.Left.Value);
            Assert.Equal(50, tree.Root.Right.Right.Value);
            Assert.Equal(40, tree.Root.Right.Right.Left.Value);
            Assert.Equal(60, tree.Root.Right.Right.Right.Value);
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void Add_RightRotation()
        {
            var tree = new AvlTree<int>();

            tree.Add(2);
            tree.Add(3);
            tree.Add(4);

            Assert.Equal(3, tree.Root.Value);
            Assert.Equal(2, tree.Root.Left.Value);
            Assert.Equal(4, tree.Root.Right.Value);

            tree.Add(5);

            Assert.Equal(3, tree.Root.Value);
            Assert.Equal(2, tree.Root.Left.Value);
            Assert.Equal(4, tree.Root.Right.Value);
            Assert.Equal(5, tree.Root.Right.Right.Value);

            tree.Add(6);

            Assert.Equal(3, tree.Root.Value);
            Assert.Equal(2, tree.Root.Left.Value);
            Assert.Equal(5, tree.Root.Right.Value);
            Assert.Equal(4, tree.Root.Right.Left.Value);
            Assert.Equal(6, tree.Root.Right.Right.Value);
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void Add_RightRotationOnRoot()
        {
            var tree = new AvlTree<int>();

            tree.Add(30);
            tree.Add(20);
            tree.Add(40);
            tree.Add(50);

            Assert.Equal(30, tree.Root.Value);
            Assert.Equal(20, tree.Root.Left.Value);
            Assert.Equal(40, tree.Root.Right.Value);
            Assert.Equal(50, tree.Root.Right.Right.Value);

            tree.Add(35);
            Assert.Equal(30, tree.Root.Value);
            Assert.Equal(20, tree.Root.Left.Value);
            Assert.Equal(40, tree.Root.Right.Value);
            Assert.Equal(35, tree.Root.Right.Left.Value);
            Assert.Equal(50, tree.Root.Right.Right.Value);

            tree.Add(55);
            Assert.Equal(40, tree.Root.Value);
            Assert.Equal(30, tree.Root.Left.Value);
            Assert.Equal(20, tree.Root.Left.Left.Value);
            Assert.Equal(35, tree.Root.Left.Right.Value);
            Assert.Equal(50, tree.Root.Right.Value);
            Assert.Equal(55, tree.Root.Right.Right.Value);
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void Add_RightRotationOnLeftChild()
        {
            var tree = new AvlTree<int>();

            tree.Add(60);
            tree.Add(30);
            tree.Add(80);
            tree.Add(20);
            tree.Add(40);
            tree.Add(70);
            tree.Add(90);
            tree.Add(35);
            tree.Add(50);
            tree.Add(65);
            tree.Add(85);
            tree.Add(55);

            Assert.Equal(60, tree.Root.Value);
            Assert.Equal(40, tree.Root.Left.Value);
            Assert.Equal(30, tree.Root.Left.Left.Value);
            Assert.Equal(20, tree.Root.Left.Left.Left.Value);
            Assert.Equal(35, tree.Root.Left.Left.Right.Value);
            Assert.Equal(50, tree.Root.Left.Right.Value);
            Assert.Equal(55, tree.Root.Left.Right.Right.Value);
            Assert.Equal(80, tree.Root.Right.Value);
            Assert.Equal(70, tree.Root.Right.Left.Value);
            Assert.Equal(65, tree.Root.Right.Left.Left.Value);
            Assert.Equal(90, tree.Root.Right.Right.Value);
            Assert.Equal(85, tree.Root.Right.Right.Left.Value);
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void Add_RightRotationOnLeftChild2()
        {
            var tree = new AvlTree<int>();

            tree.Add(10);
            tree.Add(5);
            tree.Add(30);
            tree.Add(2);
            tree.Add(7);
            tree.Add(20);
            tree.Add(40);
            tree.Add(1);
            tree.Add(6);
            tree.Add(35);
            tree.Add(50);
            tree.Add(55);

            Assert.Equal(10, tree.Root.Value);
            Assert.Equal(5, tree.Root.Left.Value);
            Assert.Equal(2, tree.Root.Left.Left.Value);
            Assert.Equal(1, tree.Root.Left.Left.Left.Value);
            Assert.Equal(7, tree.Root.Left.Right.Value);
            Assert.Equal(6, tree.Root.Left.Right.Left.Value);
            Assert.Equal(40, tree.Root.Right.Value);
            Assert.Equal(30, tree.Root.Right.Left.Value);
            Assert.Equal(50, tree.Root.Right.Right.Value);
            Assert.Equal(20, tree.Root.Right.Left.Left.Value);
            Assert.Equal(35, tree.Root.Right.Left.Right.Value);
            Assert.Equal(50, tree.Root.Right.Right.Value);
            Assert.Equal(55, tree.Root.Right.Right.Right.Value);
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void LeftRightRotation1()
        {
            var tree = new AvlTree<int>();

            tree.Add(30);
            tree.Add(20);
            tree.Add(25);

            Assert.Equal(25, tree.Root.Value);
            Assert.Equal(20, tree.Root.Left.Value);
            Assert.Equal(30, tree.Root.Right.Value);
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void LeftRightRotation2()
        {
            var tree = new AvlTree<int>();

            tree.Add(30);
            tree.Add(20);
            tree.Add(40);
            tree.Add(10);
            tree.Add(25);
            tree.Add(22);

            Assert.Equal(25, tree.Root.Value);
            Assert.Equal(20, tree.Root.Left.Value);
            Assert.Equal(10, tree.Root.Left.Left.Value);
            Assert.Equal(22, tree.Root.Left.Right.Value);
            Assert.Equal(30, tree.Root.Right.Value);
            Assert.Equal(40, tree.Root.Right.Right.Value);
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void RightLeftRotation1()
        {
            var tree = new AvlTree<int>();

            tree.Add(30);
            tree.Add(40);
            tree.Add(35);

            Assert.Equal(35, tree.Root.Value);
            Assert.Equal(30, tree.Root.Left.Value);
            Assert.Equal(40, tree.Root.Right.Value);
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void RightLeftRotation2()
        {
            var tree = new AvlTree<int>();

            tree.Add(30);
            tree.Add(20);
            tree.Add(40);
            tree.Add(45);
            tree.Add(32);
            tree.Add(35);

            Assert.Equal(32, tree.Root.Value);
            Assert.Equal(30, tree.Root.Left.Value);
            Assert.Equal(40, tree.Root.Right.Value);
            Assert.Equal(35, tree.Root.Right.Left.Value);
            Assert.Equal(45, tree.Root.Right.Right.Value);
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void CreateBalancedTree1()
        {
            var tree = new AvlTree<int>();

            tree.Add(1);
            tree.Add(2);
            tree.Add(3);

            Assert.Equal(2, tree.Root.Value);

            tree.Add(6);

            Assert.Equal(2, tree.Root.Value);

            tree.Add(15);

            Assert.Equal(2, tree.Root.Value);

            tree.Add(-2);

            Assert.Equal(2, tree.Root.Value);

            tree.Add(-5);

            Assert.Equal(2, tree.Root.Value);

            tree.Add(-8);

            Assert.Equal(2, tree.Root.Value);
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void CreateBalancedTree2()
        {
            var tree = new AvlTree<int>();

            tree.Add(43);
            tree.Add(18);
            tree.Add(22);
            tree.Add(9);
            tree.Add(21);
            tree.Add(6);

            Assert.Equal(18, tree.Root.Value);

            tree.Add(8);

            Assert.Equal(18, tree.Root.Value);
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void RemoveTest()
        {
            var tree = new AvlTree<int>();
            
            tree.Add(30);
            tree.Add(20);
            tree.Add(40);
            tree.Add(10);
            tree.Add(25);
            tree.Add(35);
            tree.Add(50);
            tree.Add(24);
            tree.Add(26);
            
            tree.Remove(20);
            
            Assert.Equal(30, tree.Root.Value);
            Assert.Equal(40, tree.Root.Right.Value);
            Assert.Equal(35, tree.Root.Right.Left.Value);
            Assert.Equal(50, tree.Root.Right.Right.Value);
            Assert.Equal(24, tree.Root.Left.Value);
            Assert.Equal(10, tree.Root.Left.Left.Value);
            Assert.Equal(25, tree.Root.Left.Right.Value);
            Assert.Null(tree.Root.Left.Right.Left);
            Assert.Equal(26, tree.Root.Left.Right.Right.Value);
        }
        
        [Fact]
        public void RemoveTest_3()
        {
            var tree = new AvlTree<int>();
            
            tree.Add(9);
            tree.Add(5);
            tree.Add(4);
            tree.Add(2);
            tree.Add(10);
            tree.Add(8);
            tree.Add(3);
            tree.Add(1);
            tree.Add(6);
            tree.Add(7);
            
            tree.Remove(2);
            tree.Remove(10);
            tree.Remove(6);
            
            AssertBalanced(tree);
            AssertOrdered(tree);
        }
        
        [Fact]
        public void RemoveTest_4()
        {
            var tree = new AvlTree<int>();
            
            tree.Add(6);
            tree.Add(19);
            tree.Add(16);
            tree.Add(11);
            tree.Add(18);
            tree.Add(12);
            tree.Add(2);
            tree.Add(14);
            tree.Add(5);
            tree.Add(3);
            tree.Add(8);
            tree.Add(1);
            tree.Add(4);
            tree.Add(7);
            tree.Add(13);
            tree.Add(15);
            tree.Add(17);
            tree.Add(10);
            tree.Add(20);
            tree.Add(9);
            
            tree.Remove(9);
            AssertBalanced(tree);
            tree.Remove(1);
            AssertBalanced(tree);
            tree.Remove(11);
            AssertBalanced(tree);
            tree.Remove(15);
            AssertBalanced(tree);
            
            AssertOrdered(tree);
        }

        [Fact]
        public void RandomBalanceTest_AssertAfterEverIteration()
        {
            var tree = new AvlTree<int>();
            var rnd = new UniqRandom(new Random());
            for (int i = 0; i < 10000; i++)
            {
                tree.Add(rnd.Next());
                AssertBalanced(tree);
            }
            AssertOrdered(tree);
        }
        
        [Fact]
        public void RandomBalanceTest_AssertAfterAllIterations()
        {
            var tree = new AvlTree<int>();
            var rnd = new UniqRandom(new Random());
            for (var i = 0; i < 1_000_000; i++)
            {
                tree.Add(rnd.Next());
            }
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        [Fact]
        public void RandomDeleteTest()
        {
            var allValues = new HashSet<int>();
            var rnd = new UniqRandom(new Random());
            var tree = new AvlTree<int>();
            for (var i = 0; i < 1000000; i++)
            {
                var val = rnd.Next();
                tree.Add(val);
                allValues.Add(val);
            }

            var removing = allValues.OrderBy(r => rnd.Next()).Take(100000).ToArray();
            foreach (var val in removing)
            {
                tree.Remove(val);
            }
            AssertBalanced(tree);
            AssertOrdered(tree);
        }

        private static void AssertBalanced<T>(AvlTree<T> tree)
        {
            var nodeCount = Count(tree.Root);
            var maxHeight = 1.45*Math.Log2(nodeCount + 2);
            var treeHeight = Height(tree.Root, 0);
            Assert.True(treeHeight <= maxHeight);
        }
        
        private static void AssertOrdered<T>(AvlTree<T> tree)
        {
            var values = Iterate(tree.Root).ToArray();
            Assert.Equal(values.OrderBy(x => x), values);
        }

        private static int Height<T>(AvlTreeNode<T> node, int deep)
        {
            var leftHeight = node.Left != null ? Height(node.Left, deep + 1) : 0;
            var rightHeight = node.Right != null ? Height(node.Right, deep + 1) : 0;
            return (leftHeight > rightHeight ? leftHeight : rightHeight) + 1;
        }
        
        private static int Count<T>(AvlTreeNode<T>? node)
        {
            if (node == null) return 0;
            var count = 1 + Count(node.Left) + Count(node.Right);
            return count;
        }

        private static IEnumerable<T> Iterate<T>(AvlTreeNode<T>? node)
        {
            if(node == null) yield break;
            
            foreach (var res in Iterate(node.Left))
            {
                yield return res;
            }

            yield return node.Value;

            foreach (var res in Iterate(node.Right))
            {
                yield return res;
            }
        }
        
        
        public class UniqRandom
        {
            private readonly Random _random;
            private readonly HashSet<int> _exists;

            public UniqRandom(Random random)
            {
                _random = random ?? throw new ArgumentNullException(nameof(random));
                _exists = new HashSet<int>();
            }

            public int Next()
            {
                int res;
                while (!_exists.Add(res =_random.Next())) { }
                return res;
            }
        }
    }
}