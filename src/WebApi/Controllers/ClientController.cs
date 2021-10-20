using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebApi.Dto;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("[controller]")]

  public class ClientController : ControllerBase
  {
    private ILogger _logger;

    public ClientController(ILogger<ClientController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<Data.Models.Client>> Get()
    {
      var result = await Data.DH.GetAllClients();
      if (result.Item1 != 0)
        return null; 
      
      return result.Item3;
    }

    [HttpGet("GetClientPage")]
    [ProducesResponseType(typeof(IEnumerable<ClientDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IEnumerable<Data.Models.Client>> GetClientPage(string sortField, int firstRecord, int recordCount)
    {
      var result = await Data.DH.GetClientPage(sortField, firstRecord, recordCount);
      if (result.Item1 == 0)
      {
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        return null;
      }

      Response.StatusCode = StatusCodes.Status200OK;
      return result.Item3;
    }
  }
}
