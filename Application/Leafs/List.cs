using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Leafs
{
    public class List
    {
        public class Query : IRequest<Result<List<Leaf>>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<Leaf>>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<Leaf>>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<Leaf> children = await _context.Leafs
                    .Where(x => x.ParentId == request.Id)
                    .ToListAsync();

                return Result<List<Leaf>>.Success(
                    children
                );
            }
        }
    }
}