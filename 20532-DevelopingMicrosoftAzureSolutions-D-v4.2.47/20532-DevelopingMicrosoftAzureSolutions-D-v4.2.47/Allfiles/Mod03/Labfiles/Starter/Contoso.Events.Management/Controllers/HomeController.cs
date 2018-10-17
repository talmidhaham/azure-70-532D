using Contoso.Events.Data;
using Contoso.Events.Models;
using Contoso.Events.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Events.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("", Name = "Home")]
        public async Task<IActionResult> Index([FromServices] EventsContext eventsContext)
        {
            var events = await eventsContext.Events.OrderBy(e => e.StartTime).Take(2).ToListAsync<Event>();

            EventsListViewModel viewModel = new EventsListViewModel
            {
                Events = events,
                LatestEventCount = 2
            };

            return View(viewModel);
        }
    }
}