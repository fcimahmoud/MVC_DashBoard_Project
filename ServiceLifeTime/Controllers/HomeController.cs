using Microsoft.AspNetCore.Mvc;
using ServiceLifeTime.Models;
using ServiceLifeTime.Services;
using System.Diagnostics;
using System.Text;

namespace ServiceLifeTime.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        private readonly ISingleTonService singleTonService1;
        private readonly ISingleTonService singleTonService2;
        private readonly IScopedService scopedService1;
        private readonly IScopedService scopedService2;
        private readonly ITransientService transientService1;
        private readonly ITransientService transientService2;

        public HomeController(ISingleTonService singleTonService1, ISingleTonService singleTonService2, 
            IScopedService scopedService1, IScopedService scopedService2, 
            ITransientService transientService1, ITransientService transientService2)
        {
            this.singleTonService1 = singleTonService1;
            this.singleTonService2 = singleTonService2;
            this.scopedService1 = scopedService1;
            this.scopedService2 = scopedService2;
            this.transientService1 = transientService1;
            this.transientService2 = transientService2;
        }

        public string Index()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"singleTon1 :: {singleTonService1.GetGuid()}");
            sb.AppendLine($"singleTon2 :: {singleTonService2.GetGuid()}\n");
            sb.AppendLine($"scoped1 :: {scopedService1.GetGuid()}");
            sb.AppendLine($"scoped2 :: {scopedService2.GetGuid()}\n");
            sb.AppendLine($"transient1 :: {transientService1.GetGuid()}");
            sb.AppendLine($"transient2 :: {transientService2.GetGuid()}");
            return sb.ToString();
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
