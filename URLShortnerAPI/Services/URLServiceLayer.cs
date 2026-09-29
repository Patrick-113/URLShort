using URLShortnerAPI.Database;
using URLShortnerAPI.Models;

namespace URLShortnerAPI.Services;

public class URLServiceLayer
{
  private readonly UrlDbContext _context;

  public URLServiceLayer(UrlDbContext context)
  {
    _context = context;
  }

  public URLViewModel? CreateNew(string url)
  {
    if(_context.shortUrls.Any(s => s.LongUrl.Equals(url)))
      return null;

    URLModel newEntry = new URLModel(url, GenerateShort());

    _context.shortUrls.Add(newEntry);
    _context.SaveChanges();

    URLViewModel returnData = new URLViewModel(newEntry);
    
    return returnData;
  }
  
  public URLViewModel? Retrieve(string shortUrl)
  {
    var entry = _context.shortUrls.SingleOrDefault(u => u.ShortCode.Equals(shortUrl));
    if(entry is null)
      return null;

    entry.AccessCount++;
    _context.shortUrls.Update(entry);
    _context.SaveChanges();

    URLViewModel returnData = new URLViewModel(entry);
    return returnData;
  }

  public URLViewModel? Update(string shortUrl, string longUrl)
  {
    var entry = _context.shortUrls.SingleOrDefault(u => u.ShortCode.Equals(shortUrl));
    if(entry is null)
      return null;

    entry.LongUrl = longUrl;
    entry.UpdatedAt = DateTime.UtcNow;
    _context.shortUrls.Update(entry);
    _context.SaveChanges();

    URLViewModel returnData = new URLViewModel(entry);
    return returnData;
  }

  public bool Delete(string shortUrl)
  {
    var entry = _context.shortUrls.SingleOrDefault(u => u.ShortCode.Equals(shortUrl));
    if (entry is null)
      return false;

    _context.shortUrls.Remove(entry);
    _context.SaveChanges();
    return true;
  }

  public URLModel? GetStats(string shortUrl)
  {
    return _context.shortUrls.SingleOrDefault(u => u.ShortCode.Equals(shortUrl));
  }

  private string GenerateShort()
  {
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    var shortUrl = new string(chars.ToCharArray().Shuffle().ToArray()[..6]);
    if (_context.shortUrls.Any(s => s.ShortCode == shortUrl))
      shortUrl = GenerateShort();

    return shortUrl;
  }
}