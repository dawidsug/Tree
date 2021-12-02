using MediatR;
using Domain;
using System.Threading.Tasks;
using System.Threading;
using Persistence;

namespace Application.Leafs
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Leaf Leaf { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var parentId = request.Leaf.ParentId;

                var parent = await _context.Nodes.FindAsync(parentId);
                
                _context.Leafs.Add(request.Leaf);

                parent.Leafs.Add(request.Leaf);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create leaf");

                return Result<Unit>.Success(Unit.Value);
            }
        }

    }
}