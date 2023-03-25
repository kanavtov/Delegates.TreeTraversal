using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.TreeTraversal
{
    public class Delegates<T1, T2>
    {
        public Predicate<T1> EndLeaf;
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
        static IEnumerable<T2> TraversalTree<T1, T2>(T1 node, Delegates<T1, T2> dlg)
        {
            if (dlg.EndLeaf(node))
                foreach (var elem in dlg.NodeItems(node))
                    yield return elem;
            if (dlg.LeftBrunch(node))
                foreach (var item in dlg.LeftItems(node))
                    foreach (var elem in dlg.LeftFunc(item, dlg))
                        yield return elem;
            if (dlg.RightBrunch(node))
                foreach (var item in dlg.RightItems(node))
                    foreach (var elem in dlg.RightFunc(item, dlg))
                        yield return elem;
        }

        public static IEnumerable<Job> GetEndJobs(Job root)
        {
            var dlg = new Delegates<Job, Job>();
            dlg.EndLeaf = (Job j) => (j.Subjobs.Count == 0);
            dlg.LeftBrunch = (Job j) => (j.Subjobs.Count != 0);
            dlg.RightBrunch = (Job j) => false;
            dlg.LeftFunc = (Job j, Delegates<Job, Job> d) => TraversalTree(j, d);
            dlg.RightFunc = (Job j, Delegates<Job, Job> d) => TraversalTree(j, d);
            dlg.NodeItems = (Job j) => new List<Job>() { j };
            dlg.LeftItems = (Job j) => j.Subjobs;
            dlg.RightItems = (Job j) => new List<Job>();
            foreach (var e in TraversalTree(root, dlg))
                yield return e;
        }

        public static IEnumerable<Product> GetProducts(ProductCategory root)
        {
            var dlg = new Delegates<ProductCategory, Product>();
            dlg.EndLeaf = (ProductCategory p) => (p.Products.Count != 0);
            dlg.LeftBrunch = (ProductCategory p) => (p.Categories.Count != 0);
            dlg.RightBrunch = (ProductCategory p) => false;
            dlg.LeftFunc = (ProductCategory p, Delegates<ProductCategory, Product> d) => TraversalTree(p, d);
            dlg.RightFunc = (ProductCategory p, Delegates<ProductCategory, Product> d) => TraversalTree(p, d);
            dlg.NodeItems = (ProductCategory p) => p.Products;
            dlg.LeftItems = (ProductCategory p) => p.Categories;
            dlg.RightItems = (ProductCategory p) => new List<ProductCategory>();
            foreach (var e in TraversalTree(root, dlg))
                yield return e;
        }

        public static IEnumerable<T> GetBinaryTreeValues<T>(BinaryTree<T> root)
        {
            var dlg = new Delegates<BinaryTree<T>, BinaryTree<T>>();
            dlg.EndLeaf = (BinaryTree<T> b) => (b.Left is null) & (b.Right is null);
            dlg.LeftBrunch = (BinaryTree<T> b) => (b.Left != null);
            dlg.RightBrunch = (BinaryTree<T> b) => (b.Right != null);
            dlg.LeftFunc = (BinaryTree<T> b, Delegates<BinaryTree<T>, BinaryTree<T>> d) => TraversalTree(b.Left, d);
            dlg.RightFunc = (BinaryTree<T> b, Delegates<BinaryTree<T>, BinaryTree<T>> d) => TraversalTree(b.Right, d);
            dlg.NodeItems = (BinaryTree<T> b) => new List<BinaryTree<T>>() { b };
            dlg.LeftItems = (BinaryTree<T> b) => new List<BinaryTree<T>>() { b };
            dlg.RightItems = (BinaryTree<T> b) => new List<BinaryTree<T>>() { b }; ;
            foreach (var e in TraversalTree(root, dlg))
                yield return e.Value;
        }
    }
}
