﻿using Contoso.Events.Data;
using Contoso.Events.Models;
using Contoso.Events.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Contoso.Events.Management.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("", Name = "Home")]
        public IActionResult Index([FromServices] EventsContext eventsContext, [FromServices] IOptions<ApplicationSettings> appSettings)
        {
            var upcomingEvents = Enumerable.Empty<Event>();

            UpcomingEventsViewModel viewModel = new UpcomingEventsViewModel
            {
                Events = upcomingEvents
            };

            return View(viewModel);
        }
    }
}