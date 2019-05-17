using System;
using Xunit;
using PayCertify;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace test
{
  public class UnitTestPlugin
  {
    [Fact]
    public void ParseWithAllData()
    {
      Dictionary<string, Microsoft.Extensions.Primitives.StringValues> c = new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>();
      c.Add("kount_response", "e1NDT1I6IDM4fQ==");
      c.Add("transaction[events][0][success]", "true");
      c.Add("transaction[events][0][processor_message]", "Your Transaction Were Approved");

      var b = new FormCollection(c);
      var a = Plugin.parse(b);

      Assert.Equal("38", a["score"].ToString());
      Assert.Equal("Your Transaction Were Approved", a["message"].ToString());
      Assert.Equal(true, a["success"]);
    }

    [Fact]
    public void ParseWithoutKount()
    {
      Dictionary<string, Microsoft.Extensions.Primitives.StringValues> c = new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>();
      c.Add("transaction[events][0][success]", "false");
      c.Add("transaction[events][0][processor_message]", "Your Transaction Were Declined");

      var b = new FormCollection(c);
      var a = Plugin.parse(b);

      Assert.Equal("Your Transaction Were Declined", a["message"].ToString());
      Assert.Equal(false, a["success"]);
    }
  }
}
