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
    public static ushort TOk => 0;
    public static ushort TFail => 1;
    public static ushort TFailDB => 10;

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

    public static async Task<Tuple<ushort, string, Client>> CreateClient(Client aClient)
    {
      try
      {
        var result = await DB.Clients.AddAsync(aClient);
        await DB.SaveChangesAsync();
        return Tuple.Create(TOk, string.Empty, result.Entity);
      }
      catch (Exception e)
      {
        return Tuple.Create(TFail, e.ToString(), (Client)null);
      }
    }

    public static async Task<Tuple<ushort, string, Client>> UpdateClient(Client aClient)
    {
      Client dbClient = null;

      try
      {
        dbClient = await DB.Clients.SingleAsync(x => x.Id == aClient.Id);
      }
      catch (Exception eNotFound)
      {
        return Tuple.Create(TFailDB, eNotFound.ToString(), aClient);
      }

      try
      {
        dbClient.Firstname = aClient.Firstname;
        dbClient.Lastname = aClient.Lastname;
        dbClient.Birthdate = aClient.Birthdate;
        dbClient.Cellphone = aClient.Cellphone;
        dbClient.Email = aClient.Email;

        await DB.SaveChangesAsync();

        return Tuple.Create(TOk, string.Empty, dbClient);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return Tuple.Create(TFail, e.ToString(), (Client) null);
      }
    }
  }
}
