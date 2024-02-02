using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HairSalon.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class SearchController : Controller
{
    private readonly HairSalonContext _db;

    public SearchController(HairSalonContext db)
    {
        _db = db;
    }

    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Results(string query, string searchType)
    {
        if (searchType == "Stylist")
        {
            var stylists = await SearchStylists(query);
            return View("Results", stylists);
        }
        else if (searchType == "Client")
        {
            var clients = await SearchClients(query);
            return View("Results", clients);
        }

        // Handle other cases or return an error view
        return View("Results");
    }

    private async Task<List<Stylist>> SearchStylists(string query)
    {
        IQueryable<Stylist> result = _db.Set<Stylist>()
                                     .Include(stylist => stylist.Clients);

        if (query != null)
        {
            return await result?.Where(stylist => stylist.StylistName.Contains(query)).ToListAsync();
        }
        else
        {
            return await result.ToListAsync();
        }
    }

    private async Task<List<Client>> SearchClients(string query)
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
}