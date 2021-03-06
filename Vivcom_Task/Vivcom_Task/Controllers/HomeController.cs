﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vivcom_Task.Models;

namespace Vivcom_Task.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var dirs = GetDirectory();
            return View(dirs);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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

        private IEnumerable<string> GetDirectory()
        {
            var projectDirectory = Directory.GetCurrentDirectory();
            var logFilesDirectoryRelativePath = @"\Logs";

            var fullPath = projectDirectory + logFilesDirectoryRelativePath;

            IEnumerable<string>
                dirs = new List<string>
                    (Directory.EnumerateDirectories(fullPath));

            return dirs;
        }
    }
}
