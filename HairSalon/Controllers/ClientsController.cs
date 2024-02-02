using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    private  readonly HairSalonContext _db;

    public ClientsController(HairSalonContext db)
    {
      _db  = db;
    }

    public ActionResult Index()
    {
      List<Client> model = _db.Clients
                            .Include(client => client.Stylist)
                            .ToList();
      ViewBag.PageTitle = "Current Clients";
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.PageTitle  = "New Client";
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "StylistName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Client client)
    {
      if (client.StylistId == 0)
      {
        return RedirectToAction("Create");
      }
      _db.Clients.Add(client);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}