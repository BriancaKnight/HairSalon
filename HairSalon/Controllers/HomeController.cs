using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using HairSalon.Models;
using System.Linq; 
using Microsoft.EntityFrameworkCore;

namespace HairSalon.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      ViewBag.PageTitle = "Home - Eau Claire's Salon";
      return View();
    }
  }
}