# paycertify.cs

## Prerequisites
* [DotNet](https://dotnet.microsoft.com/download) Command Line Tool

## Build
* `cd src/`
* `dotnet restore`

## Run tests
* `cd ../test`
* `dotnet test`

## How to install
* `dotnet add package PayCertify`

## How to use
```csharp
using PayCertify;

public IActionResult OnPost(IFormCollection formData)
{
  var response = Plugin.parse(formData);
  var success = response["success"];
  if (success.ToString() == "True")
  {
    return Page();
  }
  return RedirectToPage("/error", new { message = response["message"] });
}
```