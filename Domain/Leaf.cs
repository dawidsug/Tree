using System;

namespace Domain
{
    public class Leaf
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public TreeNode Parent { get; set; }
        public Guid ParentId { get; set; }
    }
}