namespace URLShortnerAPI.Models;

public class URLModel
{
  public int Id {get; set;}
  public string LongUrl {get; set;}
  public string ShortCode {get; set;}
  public DateTime CreatedAt {get; set;}
  public DateTime UpdatedAt {get; set;}
  public int AccessCount {get; set;}
  
  public URLModel(string longUrl, string shortCode)
  {
    LongUrl = longUrl;
    ShortCode = shortCode;
    CreatedAt = UpdatedAt = DateTime.UtcNow;
    AccessCount = 0;
  }
}

public class URLViewModel
{
  public int Id { get; set; }
  public string LongUrl { get; set; }
  public string ShortCode { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public URLViewModel(URLModel baseModel)
  {
    LongUrl = baseModel.LongUrl;
    ShortCode = baseModel.ShortCode;
    CreatedAt = baseModel.CreatedAt;
    UpdatedAt = baseModel.UpdatedAt;
  }
}

public class URLInputModel
{
  public string LongUrl {get; set;}

  public URLInputModel(string longUrl)
  {
    LongUrl = longUrl;
  }
}