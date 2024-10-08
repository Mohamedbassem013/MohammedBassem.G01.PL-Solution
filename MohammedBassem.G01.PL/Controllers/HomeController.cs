using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MohammedBassem.G01.PL.Models;
using System.Diagnostics;

namespace MohammedBassem.G01.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //private readonly IScopedServices scop02;
        //private readonly ITranientServices tranient02;
        //private readonly ISingletonServices singleton02;

        //public IScopedServices Scop01 { get; }
        //public ITranientServices Tranient01 { get; }
        //public ISingletonServices Singleton01 { get; }

        //public HomeController(ILogger<HomeController> logger,
        //    IScopedServices scop01,
        //    IScopedServices scop02,

        //    ITranientServices tranient01,
        //    ITranientServices tranient02,

        //    ISingletonServices singleton01,
        //    ISingletonServices singleton02
        //    )
        //{
        //    _logger = logger;
        //    Scop01 = scop01;
        //    this.scop02 = scop02;
        //    Tranient01 = tranient01;
        //    this.tranient02 = tranient02;
        //    Singleton01 = singleton01;
        //    this.singleton02 = singleton02;
        //}

        //public string TestLifeTest()
        //{
        //    StringBuilder builder = new StringBuilder();
        //    builder.Append($"Scop01 :: {Scop01.GetGuid()}/n");
        //    builder.Append($"Scop01 :: {scop02.GetGuid()}/n");

        //    builder.Append($"Tranient01 :: {Tranient01.GetGuid()}/n");
        //    builder.Append($"Tranient02 :: {tranient02.GetGuid()}/n");

        //    builder.Append($"Singleton01 :: {Singleton01.GetGuid()}/n");
        //    builder.Append($"Singleton02 :: {singleton02.GetGuid()}/n");
        //    return builder.ToString();
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
