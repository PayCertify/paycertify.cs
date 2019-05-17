using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PayCertify
{
  public class Plugin
  {
    public static Dictionary<string, object> parse(IFormCollection parameter)
    {
      Dictionary<string, object> response = new Dictionary<string, object>();
      if (parameter["kount_response"].Count > 0)
      {
        var base64EncodedBytes = System.Convert.FromBase64String(parameter["kount_response"][0]);
        var kountRawRespone = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        var kountObject = JObject.Parse(kountRawRespone);
        response.Add("score", kountObject["SCOR"]);
      }

      var success = Convert.ToBoolean(parameter["transaction[events][0][success]"][0]);
      var message = parameter["transaction[events][0][processor_message]"][0];

      response.Add("success", success);
      response.Add("message", message);

      return response;
    }
  }
}
