using System;
using System.Collections.Generic;

namespace Domain
{
    public class TreeNode
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Leaf> Leafs { get; set; } = new List<Leaf>();
        public ICollection<TreeNode> Children { get; set; } = new List<TreeNode>();
        public Guid? ParentId { get; set; }
        
    }
}