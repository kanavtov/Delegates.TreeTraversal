using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.TreeTraversal
{
    public class BinaryTreeParameters<T>
    {
        public List<T> list = new List<T>();
        public bool IsEndLeaf(BinaryTree<T> a) => (a.Left is null) & (a.Right is null);

        public void AppendElement(BinaryTree<T> a) => list.Add(a.Value);

        public void GetNextElement(BinaryTree<T> a, BinaryTreeParameters<T> parameters) 
        {
            if (a.Left != null)
                Traversal.TraversalTree(a.Left, parameters);
            if (a.Right != null)
                Traversal.TraversalTree(a.Right, parameters);
        }
    }

    public class JobParameters
    {
        public List<Job> list = new List<Job>();
        public bool IsEndLeaf(Job a) => (a.Subjobs.Count == 0);

        public void AppendElement(Job a) => list.Add(a);

        public void GetNextElement(Job a, JobParameters parameters)
        {
            foreach (var item in a.Subjobs)
            {
                Traversal.JobTree(item, parameters);
            }
        }
    }

    public class ProductParameters<T1>         // ProductCategory 
        where T1 : ProductCategory
    {
        public List<Product> list = new List<Product>();

        public bool IsEndLeaf(ProductCategory a) => (a.Products.Count != 0);

        public void AppendElement(ProductCategory a)
        {
            foreach (var item in a.Products)
            {
                list.Add(item);
            }
        }

        public void GetNextElement(ProductCategory a, ProductParameters<ProductCategory> parameters)
        {
            foreach (var item in a.Categories)
            {
                Traversal.CommonTraversal(item, parameters);
            }
        }
    }

    public static class Traversal
    {
        public static void CommonTraversal(ProductCategory root, ProductParameters<ProductCategory> parameters)
        {
            if (parameters.IsEndLeaf(root))
                parameters.AppendElement(root);
            parameters.GetNextElement(root, parameters);
        }

        public static void JobTree(Job root, JobParameters parameters)
        {
            if (parameters.IsEndLeaf(root))
                parameters.AppendElement(root);
            parameters.GetNextElement(root, parameters);
        }

        public static void TraversalTree<T>(BinaryTree<T> root, BinaryTreeParameters<T> parameters)
        {
            if (parameters.IsEndLeaf(root))
                parameters.AppendElement(root);
            parameters.GetNextElement(root, parameters);
        }

        public static IEnumerable<Product> GetProducts(ProductCategory root)
        {
            ProductParameters<ProductCategory> parameters = new ProductParameters<ProductCategory>();
            CommonTraversal(root, parameters);
            return parameters.list;
        }

        public static IEnumerable<Job> GetEndJobs(Job root)
        {
            JobParameters parameters = new JobParameters();
            JobTree(root, parameters);
            return parameters.list;
        }

        public static IEnumerable<T> GetBinaryTreeValues<T>(BinaryTree<T> root)
        {
            BinaryTreeParameters<T> parameters = new BinaryTreeParameters<T>();
            TraversalTree(root, parameters);
            return parameters.list;
        }
    }
}
