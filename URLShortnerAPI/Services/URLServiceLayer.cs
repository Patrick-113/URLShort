using URLShortnerAPI.Memory;
using URLShortnerAPI.Models;

namespace URLShortnerAPI.Services;

public class URLServiceLayer
{
  private readonly MemoryDb _memory;

  public URLServiceLayer(MemoryDb memory)
  {
    _memory = memory;
  }

  public URLViewModel? CreateNew(string url)
  {
    if (_memory.Data.Any(s => s.LongUrl.Equals(url)))
      return null;

    URLModel newEntry = new URLModel(url, GenerateShort())
    {
      Id = _memory.IdTrack++
    };

    _memory.Data.Add(newEntry);

    URLViewModel returnData = new URLViewModel(newEntry);

    return returnData;
  }
  
  public URLViewModel? Retrieve(string shortUrl)
  {
    var entry = _memory.Data.SingleOrDefault(u => u.ShortCode.Equals(shortUrl));
    if(entry is null)
      return null;
    
    entry.AccessCount++;
    URLViewModel returnData = new URLViewModel(entry);
    return returnData;
  }

  public URLViewModel? Update(string shortUrl, string longUrl)
  {
    var entry = _memory.Data.SingleOrDefault(u => u.ShortCode.Equals(shortUrl));
    if(entry is null)
      return null;

    entry.LongUrl = longUrl;
    entry.UpdatedAt = DateTime.UtcNow;
    URLViewModel returnData = new URLViewModel(entry);
    return returnData;
  }

  public bool Delete(string shortUrl)
  {
    var entry = _memory.Data.SingleOrDefault(u => u.ShortCode.Equals(shortUrl));
    if(entry is null)
      return false;
    
    return _memory.Data.Remove(entry);
  }

  public URLModel? GetStats(string shortUrl)
  {
    return _memory.Data.SingleOrDefault(u => u.ShortCode.Equals(shortUrl));
  }

  private string GenerateShort()
  {
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    var shortUrl = new string(chars.ToCharArray().Shuffle().ToArray()[..6]);
    if (_memory.Data.Any(s => s.ShortCode == shortUrl))
      shortUrl = GenerateShort();

    return shortUrl;
  }
}