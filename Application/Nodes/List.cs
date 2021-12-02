using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Nodes
{
    public class List
    {
        public class Query : IRequest<Result<TreeNodeDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<TreeNodeDto>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<TreeNodeDto>> Handle(Query request, CancellationToken cancellationToken)
            {

                TreeNode node = await _context.Nodes
                    .Include(x => x.Children)
                    .Include(x => x.Leafs)
                    .Where(x => x.Id == request.Id)
                    .FirstOrDefaultAsync();

                return Result<TreeNodeDto>.Success(
                    new TreeNodeDto
                    {
                        Id = node.Id,
                        Name = node.Name,
                        ParentId = node.ParentId,
                        ChildrenIds = node.Children.Select(x => x.Id).ToList(),
                        Leafs = node.Leafs.Select(l => new LeafDto { Id = l.Id, Name = l.Name, ParentId = l.ParentId, Text = l.Text, Title = l.Title}).ToList()
                    }
                );
            }
        }
    }
}