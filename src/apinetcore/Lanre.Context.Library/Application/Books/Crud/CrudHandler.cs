using Lanre.Context.Library.Domain;
using Lanre.Context.Library.Infrastructure.Database;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Lanre.Context.Library.Application.Books.Crud;

public class CrudHandler :
    IRequestHandler<GetAllRequest, IEnumerable<BookDto>>,
    IRequestHandler<GetByIdRequest, BookDto>,
    IRequestHandler<CreateRequest, Guid>,
    IRequestHandler<UpdateRequest, Guid>,
    IRequestHandler<DeleteRequest, Guid>
{
    private readonly LibraryContext libraryContext;

    public CrudHandler(LibraryContext libraryContext)
    {
        this.libraryContext = libraryContext;
    }

    public async Task<Guid> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        var entity = Book.Create(request.Name);

        this.libraryContext.Books.Add(entity);

        await this.libraryContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<Guid> Handle(DeleteRequest request, CancellationToken cancellationToken)
    {
        var entity = await this.libraryContext.Books.FirstAsync(x => x.Id == request.Id);
        this.libraryContext.Books.Remove(entity);

        await this.libraryContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<Guid> Handle(UpdateRequest request, CancellationToken cancellationToken)
    {
        var entity = await this.libraryContext.Books.FirstAsync(x => x.Id == request.Id);
        entity.SetName(request.Name);
        this.libraryContext.Books.Update(entity);

        await this.libraryContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<BookDto> Handle(GetByIdRequest request, CancellationToken cancellationToken)
    {
        var entity = await this.libraryContext.Books.FirstAsync(x => x.Id == request.Id);

        return MapTo(entity);
    }

    public async Task<IEnumerable<BookDto>> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        var entities = await this.libraryContext.Books.ToListAsync();
        return entities.Select(MapTo);
    }

    private BookDto MapTo(Book entity)
    {
        return new BookDto
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}
