using System.Collections.Generic;

namespace Delegates.TreeTraversal
{
    public class ProductCategory
    {
        public List<Product> Products = new List<Product>();
        public List<ProductCategory> Categories = new List<ProductCategory>();
    }

    public class Product
    {
        public string Name;
    }

    public class Job
    {
        public string Name;
        public List<Job> Subjobs = new List<Job>();
    }

    public class BinaryTree<T>
    {
        public BinaryTree<T> Left;
        public BinaryTree<T> Right;
        public T Value;

        //public T Print(string padding)
        //{
        //    if ((Left is null) & (Right is null))                     //(!Equals(Value, default(T)))
        //        return Value;
        //    if (Left != null)
        //        Left.Print(padding);
        //    if (Right != null)
        //        Right.Print(padding);
        //    return default(T);
        //}

        //public IEnumerable<T> Print(BinaryTree<T> root)
        //{
        //    if (!Equals(root.Value, default(T)))
        //        yield return Value;
        //    if (root.Left != null)
        //        Print(root.Left);
        //    if (root.Right != null)
        //        Print(root.Right);
    }
}

