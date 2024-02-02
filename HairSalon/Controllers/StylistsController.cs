using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    private readonly HairSalonContext _db;

    public StylistsController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Stylist> model = _db.Stylists.ToList();
      ViewBag.PageTitle = "Current Stylists";
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.PageTitle = "New Stylist";
      return View();
    }

    [HttpPost]
    public ActionResult Create(Stylist stylist)
    {
      _db.Stylists.Add(stylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Stylist selectedStylist  = _db.Stylists
                                    .Include(stylist => stylist.Clients)
                                    .FirstOrDefault (stylist => stylist.StylistId == id);
      ViewBag.PageTitle = $"Details - {selectedStylist.StylistName}";
      return View(selectedStylist);
    }

    public ActionResult Edit (int id)
    {
      Stylist selectedStylist = _db.Stylists.FirstOrDefault (stylist =>stylist.StylistId == id);
      ViewBag.PageTitle = $"Edit - {selectedStylist.StylistName}";
      return View(selectedStylist);
    }

    [HttpPost]
    public ActionResult Edit(Stylist stylist)
    {
      _db.Stylists.Update(stylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Stylist selectedStylist = _db.Stylists.FirstOrDefault (stylist => stylist.StylistId == id);
      ViewBag.PageTitle = $"Delete = {selectedStylist.StylistName}";
      return View(selectedStylist);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Stylist selectedStylist = _db.Stylists.FirstOrDefault (stylist=>  stylist.StylistId == id);
      _db.Stylists.Remove(selectedStylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}