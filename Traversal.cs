using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.TreeTraversal
{
    public class Delegates<T1, T2>
    {
        public Predicate<T1> ConditionAdd;
        public Predicate<T1> LeftBrunch;
        public Predicate<T1> RightBrunch;
        public Func<T1, Delegates<T1, T2>, IEnumerable<T2>> LeftFunc;
        public Func<T1, Delegates<T1, T2>, IEnumerable<T2>> RightFunc;
        public Func<T1, List<T2>> NodeItems;
        public Func<T1, List<T1>> LeftItems;
        public Func<T1, List<T1>> RightItems;
    }

    public static class Traversal
    {
        static IEnumerable<T2> TraversalTree<T1, T2>(T1 node, Delegates<T1, T2> deleg)
        {
            if (deleg.ConditionAdd(node))
                foreach (var elem in deleg.NodeItems(node))
                    yield return elem;
            if (deleg.LeftBrunch(node))
                foreach (var item in deleg.LeftItems(node))
                    foreach (var elem in deleg.LeftFunc(item, deleg))
                        yield return elem;
            if (deleg.RightBrunch(node))
                foreach (var item in deleg.RightItems(node))
                    foreach (var elem in deleg.RightFunc(item, deleg))
                        yield return elem;
        }

        public static IEnumerable<Job> GetEndJobs(Job root)
        {
            var del = new Delegates<Job, Job>();
            del.ConditionAdd = (Job c) => (c.Subjobs.Count == 0);
            del.LeftBrunch = (Job c) => (c.Subjobs.Count != 0);
            del.RightBrunch = (Job c) => false;
            del.LeftFunc = (Job c, Delegates<Job, Job> d) => TraversalTree(c, d);
            del.RightFunc = (Job c, Delegates<Job, Job> d) => TraversalTree(c, d);
            del.NodeItems = (Job c) => new List<Job>() { c };
            del.LeftItems = (Job c) => c.Subjobs;
            del.RightItems = (Job c) => new List<Job>() { c };
            foreach (var e in TraversalTree(root, del))
                yield return e;
        }

        public static IEnumerable<Product> GetProducts(ProductCategory root)
        {
            var del = new Delegates<ProductCategory, Product>();
            del.ConditionAdd = (ProductCategory c) => (c.Products.Count != 0);
            del.LeftBrunch = (ProductCategory c) => (c.Categories.Count != 0);
            del.RightBrunch = (ProductCategory c) => false;
            del.LeftFunc = (ProductCategory c, Delegates<ProductCategory, Product> d) => TraversalTree(c, d);
            del.RightFunc = (ProductCategory c, Delegates<ProductCategory, Product> d) => TraversalTree(c, d);
            del.NodeItems = (ProductCategory c) => c.Products;
            del.LeftItems = (ProductCategory c) => c.Categories;
            del.RightItems = (ProductCategory c) => new List<ProductCategory>() { c };
            foreach (var e in TraversalTree(root, del))
                yield return e;
        }

        public static IEnumerable<T> GetBinaryTreeValues<T>(BinaryTree<T> root)
        {
            var del = new Delegates<BinaryTree<T>, BinaryTree<T>>();
            del.ConditionAdd = (BinaryTree<T> c) => (c.Left is null) & (c.Right is null);
            del.LeftBrunch = (BinaryTree<T> c) => (c.Left != null);
            del.RightBrunch = (BinaryTree<T> c) => (c.Right != null);
            del.LeftFunc = (BinaryTree<T> c, Delegates<BinaryTree<T>, BinaryTree<T>> d) => TraversalTree(c.Left, d);
            del.RightFunc = (BinaryTree<T> c, Delegates<BinaryTree<T>, BinaryTree<T>> d) => TraversalTree(c.Right, d);
            del.NodeItems = (BinaryTree<T> c) => new List<BinaryTree<T>>() { c };
            del.LeftItems = (BinaryTree<T> c) => new List<BinaryTree<T>>() { c };
            del.RightItems = (BinaryTree<T> c) => new List<BinaryTree<T>>() { c }; ;
            foreach (var e in TraversalTree(root, del))
                yield return e.Value;
        }
    }
}
