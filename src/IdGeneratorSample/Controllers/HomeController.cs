using IdGeneratorSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnceMi.AspNetCore.IdGenerator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdGeneratorSample.Controllers
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
                ids.Add(_idGenerator.NewId());
            }
            ViewBag.Ids = ids;
            return View();
        }

        public IActionResult TimeTest(int count)
        {
            count = count == 0 ? 100000 : count;
            Stopwatch st = new Stopwatch();
            st.Start();
            _idGenerator.NewIds(count);
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
