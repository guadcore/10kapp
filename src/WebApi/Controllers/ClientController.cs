using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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

    [HttpPut]
    [ProducesResponseType(typeof(Data.Models.Client), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Data.Models.Client> Put(Data.Models.Client aClient)
    {
      var result = await Data.DH.UpdateClient(aClient);
      if (result.Item1 == 0)
      {
        Response.StatusCode = StatusCodes.Status200OK;
        return result.Item3;
      }
      if (result.Item1 == Data.DH.TFailDB)
      {
        Response.StatusCode = StatusCodes.Status404NotFound;
        return result.Item3;
      }

      Response.StatusCode = StatusCodes.Status500InternalServerError;
      return null;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Data.Models.Client), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Data.Models.Client> Post(Data.Models.Client aClient)
    {
      var result = await Data.DH.CreateClient(aClient);
      if (result.Item1 != 0)
      {
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        return null;
      }

      Response.StatusCode = StatusCodes.Status200OK;
      return result.Item3;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Data.Models.Client>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IEnumerable<Data.Models.Client>> Get()
    {
      var result = await Data.DH.GetAllClients();
      if (result.Item1 != 0)
      {
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        return null;
      }

      Response.StatusCode = StatusCodes.Status200OK;
      return result.Item3;
    }

    [HttpGet("GetClientPage")]
    [ProducesResponseType(typeof(IEnumerable<Data.Models.Client>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IEnumerable<Data.Models.Client>> GetClientPage(string sortField, int firstRecord, int recordCount)
    {
      if (!new[] {"lastname", "firstname", "birthdate", "email", "callphone", "id"}.Contains(
        sortField.ToLowerInvariant()) || firstRecord < 0 || recordCount <= 0)
      {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        return null;
      }

      var result = await Data.DH.GetClientPage(sortField, firstRecord, recordCount);
      if (result.Item1 == 0)
      {
        var clientPage = result.Item3.ToList();
        if (clientPage.Any())
        {
          Response.StatusCode = StatusCodes.Status200OK;
          return clientPage;
        }

        Response.StatusCode = StatusCodes.Status404NotFound;
        return clientPage;
      }

      Response.StatusCode = StatusCodes.Status500InternalServerError;
      return null;
    }
  }
}
