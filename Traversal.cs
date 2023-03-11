using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.TreeTraversal
{
    public static class Traversal
    {
        public static void ProductTree(ProductCategory root, List<Product> lt)
        {
            if (root.Categories.Count != 0)
                foreach (var item in root.Categories)
                {
                    ProductTree(item, lt);
                }
            if (root.Products.Count != 0)
                foreach (var item in root.Products)
                {
                    lt.Add(item);
                }
        }
        public static IEnumerable<Product> GetProducts(ProductCategory root)
        {
            List<Product> lt = new List<Product>();
            ProductTree(root, lt);
            return lt;
        }

        public static void JobTree(Job root, List<Job> lt)
        {
            if (root.Subjobs.Count == 0)                     
                lt.Add(root);
            if (root.Subjobs.Count != 0)
                foreach (var item in root.Subjobs)
                {
                    JobTree(item, lt);
                }
        }

        public static IEnumerable<Job> GetEndJobs(Job root)
        {

            List<Job> lt = new List<Job>();
            JobTree(root, lt);
            return lt;
        }

        public static void TraversalTree<T>(BinaryTree<T> root, List<T> lt)
        {
            if ((root.Left is null) & (root.Right is null))                     
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
