using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using SharePointAddIn1Web.Models;
using System;
using System.Collections.Generic;

namespace SharePointAddIn1Web.SharepointRepository
{
    public class CarRepository
    {

        public void AddNewCar(CarModel car)
        {
            AuthenticationManager authenticationManager = new AuthenticationManager();
            using (ClientContext cnx = authenticationManager.GetWebLoginClientContext("https://polcodex.sharepoint.com/sites/dev"))

            {
                Web web = cnx.Web;
                cnx.Load(web, x => x.Lists, w => w.ServerRelativeUrl);
                cnx.ExecuteQuery();
                List lista = cnx.Web.Lists.GetByTitle("Cars");
                cnx.Load(lista);
                cnx.ExecuteQuery();
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                ListItem oListItem = lista.AddItem(itemCreateInfo);
                if (!string.IsNullOrEmpty(car.Brand) && !string.IsNullOrEmpty(car.Seria) && car.Seria != null)
                {
                    oListItem["Brand"] = car.Seria;
                    oListItem["Model"] = car.Brand;
                    oListItem["Price"] = car.Price;
                    oListItem["Title"] = "nowy samochodzik";
                }

                oListItem.Update();
                cnx.ExecuteQuery();
            }
        }

        public List<CarModel> GetAllCars()
        {
            try
            {
                AuthenticationManager authenticationManager = new AuthenticationManager();
                using (ClientContext cnx = authenticationManager.GetWebLoginClientContext("https://polcodex.sharepoint.com/sites/dev"))

                {
                    Web web = cnx.Web;
                    cnx.Load(web, x => x.Lists, w => w.ServerRelativeUrl);
                    cnx.ExecuteQuery();
                    List<CarModel> carModelsList = new List<CarModel>();
                    List lista = cnx.Web.Lists.GetByTitle("Cars");
                    cnx.Load(lista, l => l.Fields);
                    cnx.ExecuteQuery();
                    CamlQuery query = new CamlQuery();
                    
                    ListItemCollection listItems = lista.GetItems(CamlQuery.CreateAllItemsQuery());
                    cnx.Load(listItems);
                    cnx.ExecuteQuery();
                    CarModel carModel = new CarModel();
                    foreach (ListItem item in listItems)
                    {
                        carModel.ID = item.Id;
                        carModel.Brand = item["Brand"].ToString();
                        carModel.Seria = item["Model"].ToString();
                        carModel.Title = item["Title"].ToString();
                        carModel.Price = (double?)item["Price"];
                        carModelsList.Add(carModel);
                    }
                    return carModelsList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public CarModel GetCarById(int id)
        //{
        //    try
        //    {
        //        AuthenticationManager authenticationManager = new AuthenticationManager();
        //        using (ClientContext cnx = authenticationManager.GetWebLoginClientContext("https://polcodex.sharepoint.com/sites/dev"))
        //        {
        //            Web web = cnx.Web;
        //            cnx.Load(web, x => x.Lists, w => w.ServerRelativeUrl);
        //            cnx.ExecuteQuery();
        //            List<CarModel> carModelsList = new List<CarModel>();
        //            List lista = cnx.Web.Lists.GetByTitle("Cars");
        //            cnx.Load(lista, l => l.Fields);
        //            cnx.ExecuteQuery();
        //            CamlQuery query = new CamlQuery();

        //            ListItemCollection listItems = lista.GetItems(CamlQuery.CreateAllItemsQuery());
        //            cnx.Load(listItems);
        //            cnx.ExecuteQuery();
        //            CarModel carModel = new CarModel();
        //            carModel.
        //        }



        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}