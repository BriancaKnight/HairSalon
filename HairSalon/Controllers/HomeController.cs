using Microsoft.AspNetCore.Mvs;

namespace HairSalon.Controllers
{
  public class HomeController : Controllers
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      ViewBag.PageTitle = "Home - Eau Claire's Salon";
      return View();
    }
  }
}