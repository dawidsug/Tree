using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Leafs
{
    public class Edit
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
                var leaf = await _context.Leafs.FindAsync(request.Leaf.Id);

                if(leaf == null) return null;

                leaf.Name = request.Leaf.Name;
                leaf.Text = request.Leaf.Text;
                leaf.Title = request.Leaf.Title;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to edit leaf");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}