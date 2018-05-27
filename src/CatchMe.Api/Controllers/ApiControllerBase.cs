using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CatchMe.Api.Controllers
{
    [Route("controlle]")]
    public class ApiControllerBase : Controller
    {
		protected Guid UserId => User?.Identity?.IsAuthenticated == true ?
			 Guid.Parse(User.Identity.Name) :
			 Guid.Empty;
	}
}
