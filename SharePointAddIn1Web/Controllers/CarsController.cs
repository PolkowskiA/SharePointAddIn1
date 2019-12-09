using SharePointAddIn1Web.Models;
using SharePointAddIn1Web.SharepointRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SharePointAddIn1Web.Controllers
{
    public class CarsController : Controller
    {
        // GET: Cars
        public ActionResult CarList()
        {
            CarRepository repository = new CarRepository();
            List<CarModel> mainList = repository.GetAllCars();
            return View(mainList);
        }
        /// <summary>
        /// Adding new Car
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(CarModel car)
        {
            CarRepository carRepository = new CarRepository();

            if (!ModelState.IsValid)
                return View("CarList", car);
            else
            {
                carRepository.AddNewCar(car);
                return RedirectToAction("CarList");
            }
        }


        public ActionResult UpdateCar()
        {

            return View("UpdateCars");

        }
    }
}