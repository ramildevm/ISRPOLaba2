using ISRPOLaba2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ISRPOLaba2.Controllers
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
        //added:

        public IActionResult TaskSecond()
        {
            return View();
        }
        public IActionResult Taskthird()
        {
            if (BookSet.idCounter == 0)
            {
                BookSet.bookSet.Add(new Book() { Id = BookSet.idCounter, Name = "1984", Author = "Д.Оруэлл", Year = 1949 });
                BookSet.bookSet.Add(new Book() { Id = ++BookSet.idCounter, Name = "Идиот", Author = "Ф.Достоевский", Year = 1868 });
                BookSet.bookSet.Add(new Book() { Id = ++BookSet.idCounter, Name = "Горе от ума", Author = "А.Грибоедов", Year = 1824 });
            }
            ViewBag.list = BookSet.bookSet;
            return View();
        }
        public IActionResult AddBook()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBook(string name, string author, int year)
        {
            BookSet.bookSet.Add(new Book() { Id = ++BookSet.idCounter, Name = name, Author = author, Year = year });
            return View();
        }
        [HttpPost]
        public IActionResult Taskthird(string option, string txt, string action)
        {
            try
            {
                Book book = (from b in BookSet.bookSet
                             where b.Id == Convert.ToInt32(action)
                             select b).FirstOrDefault();
                BookSet.bookSet.Remove(book);
            }
            catch { }
            ViewBag.list = BookSet.bookSet;
            if (action == "find")
            {
                if (option == "num")
                {
                    try
                    {
                        ViewBag.list = (from b in BookSet.bookSet
                                        where b.Id == Convert.ToInt32(txt)
                                        select b).ToList<Book>();
                    }
                    catch { }
                }
                else if (option == "name")
                {
                    ViewBag.list = (from b in BookSet.bookSet
                                    where b.Name == txt
                                    select b).ToList<Book>();
                }
                else if (option == "author")
                {
                    ViewBag.list = (from b in BookSet.bookSet
                                    where b.Author == txt
                                    select b).ToList<Book>();
                }
                else if (option == "year")
                {
                    try
                    {
                        ViewBag.list = (from b in BookSet.bookSet
                                        where b.Year == Convert.ToInt32(txt)
                                        select b).ToList<Book>();
                    }
                    catch { }
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult TaskFirst()
        {
            int[] mas = new int[15];
            var rand = new Random();
            for (int i = 0; i < 15; i++)
            {
                mas[i] = rand.Next(-10, 30);
            }
            string array = "";
            foreach(int item in mas) { array += (item.ToString() + " "); }
            ViewBag.mass = array;
            int sred = 0;
            int count = 0;
            foreach (int i in mas)
            {
                if (i >= 0)
                {
                    sred += i;
                    count++;
                }
            }
            ViewBag.Sred = sred / count;
            return View();
        }
        [HttpPost]
        public IActionResult TaskSecond(string InputText)
        {
            if (InputText != null)
            {
                int plus = InputText.Split(new string[] { "+" }, StringSplitOptions.None).Count() - 1;
                int minus = InputText.Split(new string[] { "-" }, StringSplitOptions.None).Count() - 1;
                ViewBag.TExt = InputText;
                ViewBag.Quantity = plus + minus;
            }
            return View();
        }

    }
}
