using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebApi
{
  public class DFResponse<T>
  {
    public static DFResponse<T> Ok(T aData)
    {
      return new() {ResultCode = 0, ResultText = string.Empty, Data = aData};
    }

    public static DFResponse<T> Fail(int code = 1, string text = "")
    {
      return new() {ResultCode = code, ResultText = text};
    }

    public int ResultCode { get; set; }

    public string ResultText { get; set; }

    public T Data { get; set; }
  }
}
