using System;
using System.Threading.Tasks;
using Application.Nodes;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class NodesController : BaseApiController
    {
        [HttpGet("withoutParents")]
        public async Task<IActionResult> GetFirstNode()
        {
            return HandleResult(await Mediator.Send(new First.Query { }));
        }

        [HttpGet("nodes/{id}")]
        public async Task<IActionResult> GetNodes(Guid id)
        {
            return HandleResult(await Mediator.Send(new List.Query{Id = id}));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNode(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query{Id = id}));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNode(TreeNode node)
        {
            return HandleResult(await Mediator.Send(new Create.Command{Node = node, ParentId = node.ParentId}));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> EditNode(Guid id, TreeNode node)
        {
            node.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command{Node = node}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNode(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command{Id = id}));
        }

        
    }
}