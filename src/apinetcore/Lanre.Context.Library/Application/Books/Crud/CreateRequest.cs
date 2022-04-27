using MediatR;

namespace Lanre.Context.Library.Application.Books.Crud;

public class CreateRequest : IRequest<Guid>
{
    public string? Name { get; set; }
}
