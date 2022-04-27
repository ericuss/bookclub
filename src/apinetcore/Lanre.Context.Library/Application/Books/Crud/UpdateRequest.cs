using MediatR;

namespace Lanre.Context.Library.Application.Books.Crud;

public class UpdateRequest : IRequest<Guid>
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
}
