using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ecomFront.Models;
using MySql.Data.MySqlClient;

namespace ecomFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Console.WriteLine(Startup.ConnectionString);
            using (MySqlConnection con = new MySqlConnection(Startup.ConnectionString))
            {
                Console.WriteLine(con.State.ToString());
                con.Open();
                var cmd = new MySqlCommand("select * from criteria", con);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(Convert.ToInt32(reader["id_criteria"]));
                }
            }
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
