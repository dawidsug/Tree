using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Nodes
{
    public class Details
    {
        public class Query : IRequest<Result<TreeNode>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<TreeNode>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<TreeNode>> Handle(Query request, CancellationToken cancellationToken)
            {
                var node = await _context.Nodes.FindAsync(request.Id);

                return Result<TreeNode>.Success(
                   node
                );
            }
        }
    }
}