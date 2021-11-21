using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using archi_company_mvc.Data;
using archi_company_mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace archi_company_mvc.ViewComponents
{
    public class SideBar : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public SideBar(DatabaseContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser == null)
            {
                return View();
            }
            string currentController = RouteData.Values["controller"].ToString();
            List<ContextLink> items = GetItemsForController(currentController, currentUser);
            return await Task.FromResult<IViewComponentResult>(View(items));

        }

        private List<ContextLink> GetItemsForController(string controllerName, User currentUser)
        {
            List<ContextLink> itemsList = new List<ContextLink>();
            switch (currentUser.GetType().Name)
            {
                case "Secretary":
                    switch (controllerName)
                    {
                        case "Equipments":
                        case "EquipmentTypes":
                        case "Rooms":
                            itemsList.Add(new ContextLink("Equipments", "Index", "Equipments"));
                            itemsList.Add(new ContextLink("Rooms", "Index", "Rooms"));
                            itemsList.Add(new ContextLink("EquipmentTypes", "Index", "Equipment Types"));
                            break;
                        case "Caregivers":
                        case "Patients":
                        case "Secretaries":
                            itemsList.Add(new ContextLink("Caregivers", "Index", "Caregivers"));
                            itemsList.Add(new ContextLink("Secretaries", "Index", "Secretaries"));
                            itemsList.Add(new ContextLink("Patients", "Index", "Patients"));
                            break;
                    }
                    break;
            }

            return itemsList;
        }
    }
}