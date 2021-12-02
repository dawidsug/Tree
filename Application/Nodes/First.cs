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
    public class First
    {
        public class Query : IRequest<Result<List<Guid>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<Guid>>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<Guid>>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<Guid> nodesWithoutParents = await _context.Nodes.Where(x => x.ParentId == null).Select(x => x.Id).ToListAsync();

                return Result<List<Guid>>.Success(
                   nodesWithoutParents
                );
            }
        }
    }
}