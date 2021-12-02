using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Nodes
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public TreeNode Node { get; set; }
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
                var node = await _context.Nodes.FindAsync(request.Node.Id);

                if(node == null) return null;

                node.Name = request.Node.Name;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to edit node");

                return Result<Unit>.Success(Unit.Value);
            }

        }
    }
}