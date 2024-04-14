using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnceMi.AspNetCore.IdGenerator;
using Sample.Models;

namespace Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IIdGeneratorService _idGenerator;

        public HomeController(ILogger<HomeController> logger, IIdGeneratorService idGenerator)
        {
            _logger = logger;
            _idGenerator = idGenerator;
        }

        public IActionResult Index()
        {
            List<long> ids = new List<long>();
            for (int i = 0; i < 5; i++)
            {
                ids.Add(_idGenerator.CreateId());
            }
            ViewBag.Ids = ids;
            return View();
        }

        public IActionResult TimeTest(int count)
        {
            count = count == 0 ? 100000 : count;
            Stopwatch st = new Stopwatch();
            st.Start();
            var vals = _idGenerator.CreateIds(count).ToArray();
            st.Stop();
            ViewBag.Count = count;
            ViewBag.Time = st.ElapsedMilliseconds;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
