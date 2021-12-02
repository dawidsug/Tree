using System.Threading.Tasks;
using Application.Leafs;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    public class LeafsController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaf(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query{Id = id}));
        }
        [HttpGet("{id}/leafs")]
        public async Task<IActionResult> GetLeafs(Guid id)
        {
            return HandleResult(await Mediator.Send(new List.Query{Id = id}));
        }
        [HttpPost]
        public async Task<IActionResult> CreateLeaf(Leaf leaf)
        {
            return HandleResult(await Mediator.Send(new Create.Command{Leaf = leaf}));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> EditLeaf(Guid id, Leaf leaf)
        {
            leaf.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command{Leaf = leaf}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaf(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command{Id = id}));
        }
    }
}