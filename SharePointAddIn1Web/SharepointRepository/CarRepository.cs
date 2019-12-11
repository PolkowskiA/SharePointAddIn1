using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using SharePointAddIn1Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace SharePointAddIn1Web.SharepointRepository
{
    public class CarRepository
    {
        private ClientContext _clientContext;

        public CarRepository(ClientContext clientContext)
        {
            _clientContext = clientContext;
        }

        public void AddNewCar(CarModel car)
        {
            Web web = _clientContext.Web;
            _clientContext.Load(web, x => x.Lists, w => w.ServerRelativeUrl);
            _clientContext.ExecuteQuery();
            List lista = _clientContext.Web.Lists.GetByTitle("Cars");
            _clientContext.Load(lista);
            _clientContext.ExecuteQuery();
            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem oListItem = lista.AddItem(itemCreateInfo);
            if (!string.IsNullOrEmpty(car.Brand) && !string.IsNullOrEmpty(car.Seria) && car.Seria != null)
            {
                oListItem["Brand"] = car.Brand;
                oListItem["Series"] = car.Seria;
                oListItem["Price"] = car.Price;
                oListItem["Title"] = "nowy samochodzik";
            }

            oListItem.Update();
            _clientContext.ExecuteQuery();


        }

        public List<CarModel> GetAllCars()
        {

            Web web = _clientContext.Web;
            _clientContext.Load(web, x => x.Lists, w => w.ServerRelativeUrl);
            _clientContext.ExecuteQuery();
            List<CarModel> carModelsList = new List<CarModel>();
            List lista = _clientContext.Web.Lists.GetByTitle("Cars");
            _clientContext.Load(lista, l => l.Fields);
            _clientContext.ExecuteQuery();
            CamlQuery query = new CamlQuery();

            ListItemCollection listItems = lista.GetItems(CamlQuery.CreateAllItemsQuery());
            _clientContext.Load(listItems);
            _clientContext.ExecuteQuery();
            foreach (ListItem item in listItems)
            {
                CarModel carModel = new CarModel();
                carModel.ID = item.Id;
                carModel.Brand = item["Brand"] == null ? string.Empty : item["Brand"].ToString();
                carModel.Seria = item["Series"] == null ? string.Empty : item["Series"].ToString();
                carModel.Title = item["Title"] == null ? string.Empty : item["Title"].ToString();
                //carModel.Price = (double?)item["Price"];
                carModelsList.Add(carModel);
            }
            return carModelsList;
        }

        public void AdAnotherChoice()
        {
            Web web = _clientContext.Web;
            _clientContext.Load(web, x => x.Lists, w => w.ServerRelativeUrl);
            _clientContext.ExecuteQuery();
            List<CarModel> carModelsList = new List<CarModel>();
            List lista = _clientContext.Web.Lists.GetByTitle("Cars");
            _clientContext.Load(lista, l => l.Fields);
            _clientContext.ExecuteQuery();

            //reference the SPField Collection
            FieldCollection tFields = lista.Fields;
            //referencing the title field
            Field titleField = tFields.GetByTitle("Choise");
            titleField.Required = false;
            FieldChoice fieldChoice = _clientContext.CastTo<FieldChoice>(titleField);

            //get the newly added choice field instance
            List<string> strdata = new List<string>();
            strdata.Add("Due Diligence");
            strdata.Add("Monitoring");
            strdata.Add("Conference");
            strdata.Add("Others");
            fieldChoice.Choices = strdata.ToArray();

            // Update the choice field  
            fieldChoice.Update();

            // Execute the query to the server  
            _clientContext.ExecuteQuery();

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