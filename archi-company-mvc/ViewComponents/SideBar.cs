using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using archi_company_mvc.Models;

namespace archi_company_mvc.ViewComponents
{
    public class SideBar : ViewComponent
    {

        public SideBar(DatabaseContext context)
        {
        }

        public IViewComponentResult Invoke()
        {
            string currentController = RouteData.Values["controller"].ToString();
            List<ContextLink> items = GetItemsForController(currentController);
            return View(items);
        }

        private List<ContextLink> GetItemsForController(string controllerName)
        {   
            List<ContextLink> itemsList = new List<ContextLink>();
            switch (controllerName)
            {
                case "Equipments": case "EquipmentTypes": case "Rooms":
                    itemsList.Add(new ContextLink("./Equipments", "Equipments"));
                    itemsList.Add(new ContextLink("./Rooms", "Rooms"));
                    itemsList.Add(new ContextLink("./EquipmentTypes", "Equipment Types"));
                    break;
                case "Tickets": case "ConsumableTypes": case "Consumables":
                    itemsList.Add(new ContextLink("./Tickets", "Tickets"));
                    itemsList.Add(new ContextLink("./ConsumableTypes", "Consumables types"));
                    itemsList.Add(new ContextLink("./Consumables", "Consumables"));
                    break;
                case  "Caregivers": case  "Patients": case  "Secretaries":
                    itemsList.Add(new ContextLink("./Caregivers", "Caregivers"));
                    itemsList.Add(new ContextLink("./Secretaries", "Secretaries"));
                    itemsList.Add(new ContextLink("./Patients", "Patients"));
                    break;
                default:
                    break;
            }
            
            return itemsList;
        }
    }
}