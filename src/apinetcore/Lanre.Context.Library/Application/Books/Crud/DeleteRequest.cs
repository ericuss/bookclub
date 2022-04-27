using MediatR;

namespace Lanre.Context.Library.Application.Books.Crud;

public class DeleteRequest : IRequest<Guid>
{
    public Guid? Id { get; set; }
}
