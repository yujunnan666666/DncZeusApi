using DncZeus.Api.Entities;
using DncZeus.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DncZeus.Api.Controllers.api.v2
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    //[ApiVersion("2.0")] //增加V2版本
    [Route("api/v2/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        public UserController(DncZeusDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult List()
        {
            using (_dbContext)
            {
                var list = _dbContext.DncUser.ToList();
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(list);
                return Ok(response);
            }
        }
    }
}