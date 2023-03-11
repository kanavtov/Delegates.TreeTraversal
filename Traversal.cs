using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.TreeTraversal
{
    public static class Traversal
    {
        public static IEnumerable<Product> GetProducts(ProductCategory root)
        {
            var right = root.Products.Select(a => a);
            var left = root.Categories.Select(a => a).Where(b => b.Products.Count != 0);
            var return_result = left is null;

            List<Product> r = new List<Product>();
            foreach (var item in root.Categories)
            {
                foreach (var item1 in item.Products)
                {
                        r.Add(item1);
                }
            }
            foreach (var item in root.Products)
            {
                r.Add(item);
            }
            return r;
            //            return GetProducts(root).Select(a => Selector<Product>(a));
        }

        public static IEnumerable<Job> GetEndJobs(Job root)
        {
            var right = root.Subjobs.Select(a => a).Where(b => b.Subjobs.Count == 0);
            var left = root.Subjobs.Select(a => a).Where(b => b.Subjobs.Count > 0);
            var return_result = left is null;

            List<Job> r = new List<Job>();
            foreach (var item in root.Subjobs)
            {
                if (item.Subjobs.Count == 0)
                    r.Add(item);
                else
                {
                    foreach (var item1 in item.Subjobs)
                    {
                        if (item1.Subjobs.Count == 0)
                            r.Add(item1);
                    }
                }
            }
            return r;
            //            return GetEndJobs(root).Select(a => Selector<Job>(a));
        }

        public static void TraversalTree<T>(BinaryTree<T> root, List<T> lt)
        {
            if ((root.Left is null) & (root.Right is null))                     //(!Equals(Value, default(T)))
                lt.Add(root.Value);
            if (root.Left != null)
                TraversalTree(root.Left, lt);
            if (root.Right != null)
                TraversalTree(root.Right, lt);
        }
        public static IEnumerable<T> GetBinaryTreeValues<T>(BinaryTree<T> root)
        {
            List<T> lt = new List<T>();
            TraversalTree(root, lt);
            return lt;
        }
    }
}
