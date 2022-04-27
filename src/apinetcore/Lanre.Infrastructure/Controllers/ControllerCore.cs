using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lanre.Infrastructure.Controllers;


interface IControllerCore { }

//[AllowAnonymous]
[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[Controller]")]
public abstract class ControllerCore : ControllerBase, IControllerCore
{
}
