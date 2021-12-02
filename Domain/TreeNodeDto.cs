using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TreeNodeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<LeafDto> Leafs { get; set; }
        public List<Guid> ChildrenIds { get; set; }
        public Guid? ParentId { get; set; }
    }
}
