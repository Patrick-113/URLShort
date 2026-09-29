using Microsoft.AspNetCore.Mvc;
using URLShortnerAPI.Models;
using URLShortnerAPI.Services;

namespace URLShortnerAPI.Controllers;

[ApiController]
[Route("shorten")]
public class URLController : ControllerBase
{
  private URLServiceLayer _service;
  public URLController(URLServiceLayer service)
  {
    _service = service;
  }

  [HttpPost]
  public IActionResult Post([FromBody] URLInputModel body)
  {
    if(body is not null)
    {
      var data = _service.CreateNew(body.LongUrl);
      if(data is not null)
        return Created(data.Id.ToString(), data);
    } 
    
    return BadRequest();
  }

  [HttpGet("{shortUrl}")]
  public IActionResult RetrieveOriginal(string shortUrl)
  {
    var data = _service.Retrieve(shortUrl);
    if (data is not null)
      return Ok(data);
    else
      return NotFound();
  }

  [HttpPut("{shortUrl}")]
  public IActionResult UpdateUrl(string shortUrl, [FromBody] URLInputModel body)
  {
    var data = _service.Update(shortUrl, body.LongUrl);
    if(data is not null)
      return Ok(data);
    else
      return NotFound();
  }

  [HttpDelete("{shortUrl}")]
  public IActionResult DeleteUrl(string shortUrl)
  {
    var result = _service.Delete(shortUrl);

    if(result)
      return NoContent();
    else
      return NotFound();
  }

  [HttpGet("{shortUrl}/stats")]
  public IActionResult GetUrlStats(string shortUrl)
  {
    var result = _service.GetStats(shortUrl);

    if(result is not null)
      return Ok(result);
    else
      return NotFound();
  }
}