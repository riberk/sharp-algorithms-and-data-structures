using AlgorithmsAndDataStructures.DataStructures.Trees;
using Xunit;

namespace AlgorithmsAndDataStructures.Tests.DataStructures.Trees
{
    public static class AvlTreeTests
    {
        [Fact]
        public static void Add_NoRotation()
        {
            var tree = new AvlTree<int>();
            tree.Add(30);
            tree.Add(20);
            tree.Add(40);
            tree.Add(10);
            tree.Add(50);
            tree.Add(25);
            tree.Add(35);

            Assert.Equal(30, tree.Root.Key);
            Assert.Equal(20, tree.Root.Left.Key);
            Assert.Equal(10, tree.Root.Left.Left.Key);
            Assert.Equal(25, tree.Root.Left.Right.Key);
            Assert.Equal(40, tree.Root.Right.Key);
            Assert.Equal(35, tree.Root.Right.Left.Key);
            Assert.Equal(50, tree.Root.Right.Right.Key);
        }

        [Fact]
        public static void Add_LeftRotation()
        {
            var tree = new AvlTree<int>();
            tree.Add(4);
            tree.Add(3);
            tree.Add(2);
            tree.Add(1);
            tree.Add(0);

            Assert.Equal(3, tree.Root?.Key);
            Assert.Equal(1, tree.Root?.Left?.Key);
            Assert.Equal(0, tree.Root?.Left?.Left?.Key);
            Assert.Equal(2, tree.Root?.Left?.Right?.Key);
            Assert.Equal(4, tree.Root?.Right?.Key);
        }

        [Fact]
        public static void Add_LeftRotationOnRoot()
        {
            var tree = new AvlTree<int>();
            tree.Add(30);
            tree.Add(20);
            tree.Add(40);
            tree.Add(10);
            tree.Add(25);
            tree.Add(5);

            Assert.Equal(20, tree.Root.Key);
            Assert.Equal(10, tree.Root.Left.Key);
            Assert.Equal(30, tree.Root.Right.Key);
            Assert.Equal(5, tree.Root.Left.Left.Key);
            Assert.Equal(25, tree.Root.Right.Left.Key);
            Assert.Equal(40, tree.Root.Right.Right.Key);
        }

        [Fact]
        public static void Add_LeftRotationOnLeftChild()
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

            Assert.Equal(50, tree.Root.Key);
            Assert.Equal(20, tree.Root.Left.Key);
            Assert.Equal(10, tree.Root.Left.Left.Key);
            Assert.Equal(5, tree.Root.Left.Left.Left.Key);
            Assert.Null(tree.Root.Left.Left.Right);
            Assert.Equal(30, tree.Root.Left.Right.Key);
            Assert.Equal(25, tree.Root.Left.Right.Left.Key);
            Assert.Equal(40, tree.Root.Left.Right.Right.Key);
            Assert.Equal(60, tree.Root.Right.Key);
            Assert.Equal(55, tree.Root.Right.Left.Key);
            Assert.Equal(65, tree.Root.Right.Right.Key);
        }

        [Fact]
        public static void Add_LeftRotationOnRight()
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

            Assert.Equal(15, tree.Root.Key);
            Assert.Equal(5, tree.Root.Left.Key);
            Assert.Equal(2, tree.Root.Left.Left.Key);
            Assert.Equal(3, tree.Root.Left.Left.Right.Key);
            Assert.Equal(10, tree.Root.Left.Right.Key);
            Assert.Equal(7, tree.Root.Left.Right.Left.Key);
            Assert.Equal(12, tree.Root.Left.Right.Right.Key);
            Assert.Equal(30, tree.Root.Right.Key);
            Assert.Equal(25, tree.Root.Right.Left.Key);
            Assert.Equal(20, tree.Root.Right.Left.Left.Key);
            Assert.Equal(50, tree.Root.Right.Right.Key);
            Assert.Equal(40, tree.Root.Right.Right.Left.Key);
            Assert.Equal(60, tree.Root.Right.Right.Right.Key);
        }

        [Fact]
        public static void Add_RightRotation()
        {
            var tree = new AvlTree<int>();

            tree.Add(2);
            tree.Add(3);
            tree.Add(4);

            Assert.Equal(3, tree.Root.Key);
            Assert.Equal(2, tree.Root.Left.Key);
            Assert.Equal(4, tree.Root.Right.Key);

            tree.Add(5);

            Assert.Equal(3, tree.Root.Key);
            Assert.Equal(2, tree.Root.Left.Key);
            Assert.Equal(4, tree.Root.Right.Key);
            Assert.Equal(5, tree.Root.Right.Right.Key);

            tree.Add(6);

            Assert.Equal(3, tree.Root.Key);
            Assert.Equal(2, tree.Root.Left.Key);
            Assert.Equal(5, tree.Root.Right.Key);
            Assert.Equal(4, tree.Root.Right.Left.Key);
            Assert.Equal(6, tree.Root.Right.Right.Key);
        }

        [Fact]
        public static void Add_RightRotationOnRoot()
        {
            var tree = new AvlTree<int>();

            tree.Add(30);
            tree.Add(20);
            tree.Add(40);
            tree.Add(50);

            Assert.Equal(30, tree.Root.Key);
            Assert.Equal(20, tree.Root.Left.Key);
            Assert.Equal(40, tree.Root.Right.Key);
            Assert.Equal(50, tree.Root.Right.Right.Key);

            tree.Add(35);
            Assert.Equal(30, tree.Root.Key);
            Assert.Equal(20, tree.Root.Left.Key);
            Assert.Equal(40, tree.Root.Right.Key);
            Assert.Equal(35, tree.Root.Right.Left.Key);
            Assert.Equal(50, tree.Root.Right.Right.Key);

            tree.Add(55);
            Assert.Equal(40, tree.Root.Key);
            Assert.Equal(30, tree.Root.Left.Key);
            Assert.Equal(20, tree.Root.Left.Left.Key);
            Assert.Equal(35, tree.Root.Left.Right.Key);
            Assert.Equal(50, tree.Root.Right.Key);
            Assert.Equal(55, tree.Root.Right.Right.Key);
        }

        [Fact]
        public static void Add_RightRotationOnLeftChild()
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

            Assert.Equal(60, tree.Root.Key);
            Assert.Equal(40, tree.Root.Left.Key);
            Assert.Equal(30, tree.Root.Left.Left.Key);
            Assert.Equal(20, tree.Root.Left.Left.Left.Key);
            Assert.Equal(35, tree.Root.Left.Left.Right.Key);
            Assert.Equal(50, tree.Root.Left.Right.Key);
            Assert.Equal(55, tree.Root.Left.Right.Right.Key);
            Assert.Equal(80, tree.Root.Right.Key);
            Assert.Equal(70, tree.Root.Right.Left.Key);
            Assert.Equal(65, tree.Root.Right.Left.Left.Key);
            Assert.Equal(90, tree.Root.Right.Right.Key);
            Assert.Equal(85, tree.Root.Right.Right.Left.Key);
        }

        [Fact]
        public static void Add_RightRotationOnLeftChild2()
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

            Assert.Equal(10, tree.Root.Key);
            Assert.Equal(5, tree.Root.Left.Key);
            Assert.Equal(2, tree.Root.Left.Left.Key);
            Assert.Equal(1, tree.Root.Left.Left.Left.Key);
            Assert.Equal(7, tree.Root.Left.Right.Key);
            Assert.Equal(6, tree.Root.Left.Right.Left.Key);
            Assert.Equal(40, tree.Root.Right.Key);
            Assert.Equal(30, tree.Root.Right.Left.Key);
            Assert.Equal(50, tree.Root.Right.Right.Key);
            Assert.Equal(20, tree.Root.Right.Left.Left.Key);
            Assert.Equal(35, tree.Root.Right.Left.Right.Key);
            Assert.Equal(50, tree.Root.Right.Right.Key);
            Assert.Equal(55, tree.Root.Right.Right.Right.Key);
        }

        [Fact]
        public static void LeftRightRotation1()
        {
            var tree = new AvlTree<int>();

            tree.Add(30);
            tree.Add(20);
            tree.Add(25);

            Assert.Equal(25, tree.Root.Key);
            Assert.Equal(20, tree.Root.Left.Key);
            Assert.Equal(30, tree.Root.Right.Key);
        }

        [Fact]
        public static void LeftRightRotation2()
        {
            var tree = new AvlTree<int>();

            tree.Add(30);
            tree.Add(20);
            tree.Add(40);
            tree.Add(10);
            tree.Add(25);
            tree.Add(22);

            Assert.Equal(25, tree.Root.Key);
            Assert.Equal(20, tree.Root.Left.Key);
            Assert.Equal(10, tree.Root.Left.Left.Key);
            Assert.Equal(22, tree.Root.Left.Right.Key);
            Assert.Equal(30, tree.Root.Right.Key);
            Assert.Equal(40, tree.Root.Right.Right.Key);
        }

        [Fact]
        public static void RightLeftRotation1()
        {
            var tree = new AvlTree<int>();

            tree.Add(30);
            tree.Add(40);
            tree.Add(35);

            Assert.Equal(35, tree.Root.Key);
            Assert.Equal(30, tree.Root.Left.Key);
            Assert.Equal(40, tree.Root.Right.Key);
        }

        [Fact]
        public static void RightLeftRotation2()
        {
            var tree = new AvlTree<int>();

            tree.Add(30);
            tree.Add(20);
            tree.Add(40);
            tree.Add(45);
            tree.Add(32);
            tree.Add(35);

            Assert.Equal(32, tree.Root.Key);
            Assert.Equal(30, tree.Root.Left.Key);
            Assert.Equal(40, tree.Root.Right.Key);
            Assert.Equal(35, tree.Root.Right.Left.Key);
            Assert.Equal(45, tree.Root.Right.Right.Key);
        }

        [Fact]
        public static void CreateBalancedTree1()
        {
            var tree = new AvlTree<int>();

            tree.Add(1);
            tree.Add(2);
            tree.Add(3);

            Assert.Equal(2, tree.Root.Key);

            tree.Add(6);

            Assert.Equal(2, tree.Root.Key);

            tree.Add(15);

            Assert.Equal(2, tree.Root.Key);

            tree.Add(-2);

            Assert.Equal(2, tree.Root.Key);

            tree.Add(-5);

            Assert.Equal(2, tree.Root.Key);

            tree.Add(-8);

            Assert.Equal(2, tree.Root.Key);
        }

        [Fact]
        public static void CreateBalancedTree2()
        {
            var tree = new AvlTree<int>();

            tree.Add(43);
            tree.Add(18);
            tree.Add(22);
            tree.Add(9);
            tree.Add(21);
            tree.Add(6);

            Assert.Equal(18, tree.Root.Key);

            tree.Add(8);

            Assert.Equal(18, tree.Root.Key);
        }
    }
}