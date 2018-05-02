using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApp.DataAccess;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        IRepository<City> context;

        public WeatherController()
        {
            context = new InMemoryRepository<City>();
        }
        // GET: Weather
        public ActionResult Index()
        {
            List<City> cities = context.Collection().ToList();
            return View(cities);
        }

        public ActionResult Create()
        {
            City city = new City();
            return View(city);
        }
        [HttpPost]
        public ActionResult Create(City t)
        {
            if (!ModelState.IsValid)
            {
                return View(t);
            }
            else
            {
                context.Insert(t);
                context.Commit();

                return RedirectToAction("Index");
            }
        }


        public ActionResult Edit(string Id)
        {
            City c = context.Find(Id);
            if (c == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(c);
            }
        }
        [HttpPost]
        public ActionResult Edit(City city, string Id)
        {
            City cityToEdit = context.Find(Id);
            if (cityToEdit == null)
            {
                return HttpNotFound();
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    return View(city);
                }

                cityToEdit.Name= city.Name;
                cityToEdit.Province = city.Province;
                cityToEdit.Country = city.Country;
                cityToEdit.Code = city.Code;
                

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            City cityToDelete = context.Find(Id);
            if (cityToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(cityToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            City cityToDelete = context.Find(Id);
            if (cityToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(string Id)
        {
            City city = context.Find(Id);
            if (city == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(city);
            }
        }


        [HttpPost]
        public string MyData(UserInfo userInfo, ClassInfo classInfo)
        {
            return "hello my data" + userInfo.UserName;
        }




    }

   

    public class UserInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
    }

    public class ClassInfo
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
    }
}