using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data
{
  using Models;

  public static class DH
  {
    private static ushort TOk => 0;
    private static ushort TFail => 1;

    public static string CS { get; set; }

    public static dfContext DB => new();

    public static async Task<Tuple<ushort, string, IEnumerable<Client>>> GetAllClients()
    {
      try
      {
        IEnumerable<Client> dbResult = await DB.Clients.ToListAsync();
        return Tuple.Create(TOk, string.Empty, dbResult);
      }
      catch (Exception e)
      {
        return Tuple.Create(TFail, e.ToString(), (IEnumerable<Client>) null);
      }
    }

    public static async Task<Tuple<ushort, string, IEnumerable<Client>>> GetClientPage(string field, int firstRecord, int recordCount)
    {
      try
      {
        IEnumerable<Client> dbResult =
          await DB.Clients.OrderBy(x => field).Skip(firstRecord - 1).Take(recordCount).ToListAsync();
        return Tuple.Create(TOk, string.Empty, dbResult);
      }
      catch (Exception e)
      {
        return Tuple.Create(TFail, e.ToString(), (IEnumerable<Client>) null);
      }
    }
  }
}
