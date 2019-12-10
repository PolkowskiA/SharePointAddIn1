using Microsoft.SharePoint.Client;
using SharePointAddIn1Web.Models;
using SharePointAddIn1Web.SharepointRepository;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            SharePointContext spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (ClientContext clientContext = spContext.CreateUserClientContextForSPHost())
            {
                CarRepository repository = new CarRepository(clientContext);
                List<CarModel> mainList = repository.GetAllCars();
                return View(mainList);
            }
        }
        /// <summary>
        /// Adding new Car
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(CarModel car)
        {
            if (!ModelState.IsValid)
                return View("CarList", car);
            else
            {
                string url = car.SPHostUrl.Substring(10);
                UriBuilder uri = new UriBuilder(url);
                NameValueCollection nameURL = new NameValueCollection();
                nameURL["SPHostUrl"] = uri.ToString();
                HttpContext.Request.QueryString.Add(nameURL);
                //HttpContext.Request.QueryString = url;

                SharePointContext spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

                using (ClientContext clientContext = spContext.CreateUserClientContextForSPHost())
                {
                    CarRepository carRepository = new CarRepository(clientContext);
                    carRepository.AddNewCar(car);
                    return RedirectToAction("CarList");
                }
            }
        }


        public ActionResult UpdateCar()
        {
            return View("UpdateCars");
        }
    }
}