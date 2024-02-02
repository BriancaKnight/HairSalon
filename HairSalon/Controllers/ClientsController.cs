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

        private async Task<List<Client>> SearchMethod (string query)
    {
      IQueryable<Client> result = _db.Set<Client>()
                                    .Include(client => client.Stylist);

      if (query != null)
      {
        return await result?.Where(client => client.ClientName.Contains(query) || client.Stylist.StylistName.Contains(query)).ToListAsync();
      }
      else
      {
        return await result.ToListAsync();
      }
    }

    public async Task<IActionResult> Index(string query)
    {
      List<Client> resultList = await SearchMethod(query);
      ViewBag.PageTitle = "Current Clients";
      return View(resultList);
    }

    // public ActionResult Index()
    // {
    //   List<Client> model = _db.Clients
    //                         .Include(client => client.Stylist)
    //                         .ToList();
    //   ViewBag.PageTitle = "Current Clients";
    //   return View(model);
    // }

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

    public ActionResult Details(int id)
    {
      Client selectedClient = _db.Clients 
                                  .Include(client => client.Stylist)
                                  .FirstOrDefault (client => client.ClientId ==id);
      ViewBag.PageTitle  = $"Details - {selectedClient.ClientName}";
      return View(selectedClient);
    }

    public ActionResult Edit (int id)
    {
      var selectedClient  = _db.Clients.FirstOrDefault (client => client.ClientId == id);
      ViewBag.PageTitle = $"Edit = {selectedClient.ClientName}";
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "StylistName");
      return View(selectedClient);
    }

    [HttpPost]
    public ActionResult Edit(Client client)
    {
      _db.Clients.Update(client);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Client selectedClient = _db.Clients.FirstOrDefault(client => client.ClientId == id);
      ViewBag.PageTitle = $"Delete = {selectedClient.ClientName}";
      return View(selectedClient);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Client selectedClient =  _db.Clients.FirstOrDefault (client => client.ClientId == id);
      _db.Clients.Remove(selectedClient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}