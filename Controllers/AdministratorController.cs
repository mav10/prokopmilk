using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using WebSite.Models;
using WebSite.Models.Concrete;
using WebSite.Models.Repository;

namespace WebSite.Controllers
{
    [Authorize(Roles="MainAdmin, Administrator")]
    public class AdministratorController : Controller
    {
        Repository repo = new Repository();
        // GET: Administrator
        public ActionResult Index()
        {
            Repository repo = new Repository();
            AllModels model = new AllModels()
            {
                Tovars = repo.Tovars,
                Categories = repo.Categories,
                Customers = repo.Customers,
                Orders = repo.Orders,
                News = repo.News.Distinct()
            };
            return View(model);
        }
        //Новости картинка
        [AllowAnonymous]
        public FileContentResult GetImageNew(int id)
        {
            New tovar = repo.News
                     .FirstOrDefault(g => g.id == id);

            if (tovar != null)
            {
                if (tovar.Picture != null)
                {
                    return File(tovar.Picture, "image/png");
                }
                else return null;
            }
            else
            {

                return null;
            }
        }
        public ActionResult Category()
        {
            
            return View(repo.Categories);
        }
        public ActionResult CategoryEdit(int Ca_ID)
        {
            Category categor = repo.Categories
                .FirstOrDefault(c => c.Ca_ID == Ca_ID);
            
            return View(categor);
        }
        public ActionResult Mail()
        {
            return View(repo.MailSettings);
        }
        
        [AllowAnonymous]
        public FileContentResult GetImage(int Ca_ID)
        {
           
                Category categor = repo.Categories
                    .FirstOrDefault(g => g.Ca_ID == Ca_ID);

                if (categor != null)
                {
                    if (categor.Pict != null)
                    {
                        return File(categor.Pict, "image/png");
                    }
                    else return null;
                }
                else
                {

                    return null;
                }
           
           
        }

        [AllowAnonymous]
        public FileContentResult GetImageTovar(int Tovar_ID)
        {
            Tovar tovar = repo.Tovars
                     .FirstOrDefault(g => g.Tovar_ID == Tovar_ID);

            if (tovar != null)
            {
                if (tovar.Pict != null)
                {
                    return File(tovar.Pict, "image/png");
                }
                else return null;
            }
            else
            {

                return null;
            }
        }

        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult CategoryEdit(Category category, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    
                    category.Pict = new byte[image.ContentLength];
                    image.InputStream.Read(category.Pict, 0, image.ContentLength);
                }

                repo.SaveCategory(category);
                TempData["message"] = string.Format("Изменения в категории \"{0}\" были сохранены", category.Category_Name);
                return RedirectToAction("/Category");
            }
            else
            {
                // Что-то не так со значениями данных
                TempData["message"] = string.Format("Изменения в категории \"{0}\" не удались", category.Category_Name);
                return View(category);
            }
        }
        public ActionResult Order()
        {
           
            return View(repo.Orders);
        }
        public ActionResult OrderItem()
        {

            return View(repo.OrderItems);
        }
        public ActionResult OrderEdit(int OrderId)
        {
            Order order = repo.Orders.FirstOrDefault(c => c.OrderId == OrderId);
            return View(order);
        }
        public ActionResult Client()
        {
            return View(repo.Customers);
        }
        public ActionResult ClientEdit(int CustomerId)
        {
            Customer custom = repo.Customers
                .FirstOrDefault(c => c.CustomerId == CustomerId);
            return View(custom);
        }
        public ActionResult MailEdit(string MailFromAddress)
        {
            EmailSettings custom = repo.MailSettings
                .FirstOrDefault(c => c.MailFromAddress == MailFromAddress);
            return View(custom);
        }
        [HttpPost]
        public ActionResult MailEdit(EmailSettings mailset)
        {
            if (ModelState.IsValid)
            {



                repo.SaveMail(mailset);
                
                TempData["message"] = string.Format("Изменения в учетной записи почтового ящика \"{0}\" были сохранены", mailset.MailFromAddress);
                return RedirectToAction("/Mail");

            }
            else
            {
                // Что-то не так со значениями данных
                TempData["message"] = string.Format("Изменения в учетной записи почтового ящика\"{0}\" не удались", mailset.MailFromAddress);

                return View(mailset);
            }
        }
        public ViewResult MailCreate()
        {
            return View("mailEdit", new EmailSettings());
        }
        public ViewResult ClientCreate()
        {
            return View("ClientEdit", new Customer());
        }
        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult ClientEdit(Customer client)
        {
            if (ModelState.IsValid)
            {

               

                    repo.SaveClient(client);
                client.CreateUserAsCustomer();
                    TempData["message"] = string.Format("Изменения в учетной записи клиента \"{0}\" были сохранены", client.Name );
                    return RedirectToAction("/Tovar");
               
            }
            else
            {
                // Что-то не так со значениями данных
                TempData["message"] = string.Format("Изменения в учетной записи клиента\"{0}\" не удались", client.Name);

                return View(client);
            }
        }
        public ActionResult Tovar()
        {
            Repository repo = new Repository();
            AllModels model = new AllModels()
            {
                Tovars = repo.Tovars,
                Categories = repo.Categories,
                Customers = repo.Customers,
                Orders = repo.Orders,
                News=repo.News.Distinct()
            };
            return View(model);
        }
        public ActionResult TovarEdit(int Tovar_ID)
        {
            Tovar tovar = repo.Tovars
                    .FirstOrDefault(g => g.Tovar_ID == Tovar_ID);
            return View(tovar);
        }
        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult TovarEdit(Tovar tovar, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (tovar.Tovar_ID == 0)
                {
                    tovar.OrderItems = new List<OrderItem>();
                }
               
                    if (image != null)
                    {

                       tovar.Pict = new byte[image.ContentLength];
                        image.InputStream.Read(tovar.Pict, 0, image.ContentLength);
                    }
                if (tovar.Category != null)
                {

                    repo.SaveTovar(tovar);
                
                TempData["message"] = string.Format("Изменения в товаре \"{0}\"\"{1}\" были сохранены", tovar.Category, tovar.Volume);
                return RedirectToAction("/Tovar");
                }else
                {
                    // Что-то не так со значениями данных
                    TempData["message"] = string.Format("Изменения в товаре \"{0}\"\"{1}\" не удались", tovar.Category, tovar.Volume);

                    return View(tovar);
                }
            }
            else
            {
                // Что-то не так со значениями данных
                TempData["message"] = string.Format("Изменения в товаре \"{0}\"\"{1}\" не удались", tovar.Category, tovar.Volume);

                return View(tovar);
            }
        }


        public ViewResult TovarCreate()
        {
            return View("TovarEdit", new Tovar());
        }
        public ViewResult CategoryCreate()
        {
            return View("CategoryEdit", new Category());
        }

        public ActionResult OrderCancel(int OrderId)
        {
            Order order = repo.Orders.FirstOrDefault(x => x.OrderId == OrderId);
            order.SolveOrder = false;
            order.StatusOrder = true;
                    repo.SaveOrder(order);

                    TempData["message"] = string.Format("Изменения в заказе №\"{0}\" были сохранены - заказ отменен.", order.OrderId);
                    return RedirectToAction("/Order");
               
           
        }
        public ActionResult OrderGet(int OrderId)
        {
            Order order = repo.Orders.FirstOrDefault(x => x.OrderId == OrderId);
            order.SolveOrder = true;
            order.StatusOrder = false;
                    repo.SaveOrder(order);

                    TempData["message"] = string.Format("Изменения в заказе №\"{0}\" были сохранены - заказ сохранен.", order.OrderId);
                    return RedirectToAction("/Order");
               
           
        }
        public ActionResult OrderFin(int OrderId)
        {
            Order order = repo.Orders.FirstOrDefault(x => x.OrderId == OrderId);
            order.SolveOrder = true;
            order.StatusOrder = true;
            repo.SaveOrder(order);

            TempData["message"] = string.Format("Изменения в заказе №\"{0}\" были сохранены - заказ сохранен.", order.OrderId);
            return RedirectToAction("/Order");


        }
        public ActionResult TovarDelete(int Tovar_ID)
        {
            Tovar deleted = repo.DeleteTovar(Tovar_ID);
            if (deleted != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удален",
                    deleted.Category);
            }
            return RedirectToAction("Tovar");
        }
        public ActionResult MailDelete(string MailFromAddress)
        {
            EmailSettings deleted = repo.DeleteMailSettings(MailFromAddress);
            if (deleted != null)
            {
                TempData["message"] = string.Format("Почтовый ящик \"{0}\" был удален",
                    deleted.MailFromAddress);
            }
            return RedirectToAction("Mail");
        }
        public ActionResult ClientDelete(int CustomerId)
        {
            Customer deleted = repo.DeleteClient(CustomerId);
            if (deleted != null)
            {
                TempData["message"] = string.Format("Клиент \"{0}\" был удален",
                    deleted.Name);
            }
            return RedirectToAction("Client");
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public PartialViewResult _UserPartial()
        {
            var userId = User.Identity.GetUserId();
            var CurUser = UserManager.Users.FirstOrDefault(x => x.Id == userId);
            var model = new NavBarViewModel();
            model.name = CurUser.name;
            return PartialView(model);
        }

        public ActionResult CategoryDelete(int Ca_ID)
        {
            Category deleted = repo.DeleteCategory(Ca_ID);
            if (deleted != null)
            {
                TempData["message"] = string.Format("Категория \"{0}\" была удалена",
                    deleted.Category_Name);
            }
            return RedirectToAction("Category");
        }


    }
}