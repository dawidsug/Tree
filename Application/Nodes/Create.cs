using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Nodes
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public TreeNode Node { get; set; }
            public Guid? ParentId { get; set; }
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

                var newNode = new TreeNode
                {
                    Name = request.Node.Name,
                    ParentId = request.ParentId

                };

                if(newNode.ParentId is { })
                {
                    var parent = await _context.Nodes.FindAsync(request.Node.ParentId);
                    parent.Children.Add(newNode);
                }

                _context.Nodes.Add(newNode);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create node");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}