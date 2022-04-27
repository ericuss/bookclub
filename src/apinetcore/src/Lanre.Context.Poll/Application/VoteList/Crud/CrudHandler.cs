using Lanre.Context.Poll.Infrastructure.Database;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Lanre.Context.Poll.Application.VoteList.Crud;

public class CrudHandler : 
    IRequestHandler<CreateRequest, Guid>,
    IRequestHandler<GetAllRequest, IEnumerable<VoteListDto>>,
    IRequestHandler<GetByIdRequest, VoteListDto>
{
    private readonly PollContext pollContext;

    public CrudHandler(PollContext pollContext)
    {
        this.pollContext = pollContext;
    }

    public async Task<Guid> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        var entity = new Domain.VoteList.Builder()
            .SetName(request.Name)
            .SetUserId(request.UserId)
            .Build();

        this.pollContext.VoteLists.Add(entity);

        await this.pollContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<VoteListDto> Handle(GetByIdRequest request, CancellationToken cancellationToken)
    {
        var entity = await this.pollContext.VoteLists.FirstAsync(x => x.Id == request.Id);

        return MapTo(entity);
    }

    public async Task<IEnumerable<VoteListDto>> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        var entities = await this.pollContext.VoteLists.ToListAsync();
        return entities.Select(MapTo);
    }

    private VoteListDto MapTo(Domain.VoteList entity)
    {
        return new VoteListDto
        {
            Id = entity.Id,
            Name = entity.Name,
            UserId = entity.UserId,
        };
    }
}
