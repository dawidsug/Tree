using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Leafs
{
    public class Details
    {
        public class Query : IRequest<Result<Leaf>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Leaf>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Leaf>> Handle(Query request, CancellationToken cancellationToken)
            {
                var leaf = await _context.Leafs.FindAsync(request.Id);

                return Result<Leaf>.Success(
                   leaf
                );
            }
        }
    }
}