using DealDouble.Entities;
using DealDouble.Web.ViewModels;
using DealDoubleServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class CategoriesController : Controller
    {
        CategoriesService categoriesService = new CategoriesService();
        public ActionResult Index()
        {

            CategoriesListingViewModel model = new CategoriesListingViewModel();
            model.PageTitle = "Categories";
            model.PageDescription = "Categories listing page";

            return View(model);

        }

        public ActionResult Listing()
        {
            CategoriesListingViewModel model = new CategoriesListingViewModel();
            model.Categories = categoriesService.GetAllCategories();

            return PartialView(model);

        }
        public ActionResult Create()
        {
            CategoryViewModel model = new CategoryViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(CreateAuctionViewModel model)
        {
            Category category = new Category();
            category.Name = model.Title;
            category.Description = model.Description;

            categoriesService.SaveCategory(category);
            return RedirectToAction("Listing");
        }

        public ActionResult Edit(int ID)
        {
            CategoryViewModel model = new CategoryViewModel();
            var category = categoriesService.GetCategoryByID(ID);
            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(CategoryViewModel model)
        {
            Category category = new Category();
            category.ID = model.ID;
            category.Name = model.Name;
            category.Description = model.Description;

            categoriesService.UpdateCategory(category);
            return RedirectToAction("Listing");
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            categoriesService.DeleteCategory(category);
            return RedirectToAction("Listing");
        }

        public ActionResult Details(int ID)
        {
            CategoryDetailsViewModel model = new CategoryDetailsViewModel();

            model.Category = categoriesService.GetCategoryByID(ID);

            model.PageTitle = "Auction Details:" + model.Category.Name;
            model.PageDescription = model.Category.Description.Substring(0, 10);
            return View(model);
        }
    }
}