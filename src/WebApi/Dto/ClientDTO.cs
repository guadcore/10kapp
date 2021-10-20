using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dto
{
  public class ClientDTO
  {
    public int id { get; set; }
    public string firstname { get; set; }
    public string lastname { get; set; }
    public DateTime BirthDate { get; set; }
    public string cellphone { get; set; }
    public string email { get; set; }
  }
}
