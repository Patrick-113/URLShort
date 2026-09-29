using URLShortnerAPI.Models;

namespace URLShortnerAPI.Memory;

public class MemoryDb
{
  public uint IdTrack {get; set;}
  public List<URLModel> Data {get; set;}

  public MemoryDb()
  {
    IdTrack = 0;
    Data = new List<URLModel>();
  }
}