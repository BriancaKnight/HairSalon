using System.Collections.Generic;

namespace  HairSalon.Models
{
  public class Stylist
  {
    public int StylistId { get; set; }
    public string StylistName { get; set; }
    public List<Stylist> Stylists { get; set; }
  }
}