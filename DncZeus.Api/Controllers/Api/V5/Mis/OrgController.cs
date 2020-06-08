using DncZeus.Api.Entities;
using DncZeus.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DncZeus.Api.Controllers.Mis
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("5.0")] //V5
    [Route("api/v5/[controller]/[action]")]
    public class OrgController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        public OrgController(DncZeusDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult List()
        {
            using (_dbContext)
            {
                var list = _dbContext.MisOrganization.ToList();
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(list);
                return Ok(response);
            }
        }
    }
}