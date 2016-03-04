using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using ShoeStore;

namespace ShoeStore
{
  public class HomeModule: NancyModule
  {

    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

      Get["/stores"] = _ => {
        List<Store> allStores = Store.GetAll();
        return View["stores.cshtml", allStores];
      };
      Get["/brands"] = _ => {
        List<Brand> allBrands = Brand.GetAll();
        return View["brands.cshtml", allBrands];
      };
      Post["/stores/new"] = _ => {
        Store newStore = new Store(Request.Form["store-name"]);
        newStore.Save();
        List<Store> allStores = Store.GetAll();
        return View["stores.cshtml", allStores];
      };
      Post["/brands/new"] = _ => {
        Brand newBrand = new Brand(Request.Form["brand-name"]);
        newBrand.Save();
        List<Brand> allBrands = Brand.GetAll();
        return View["brands.cshtml"];
      };
      Get["/stores/delete"] = _ => {
        Store.DeleteAll();
        List<Store> allStores = Store.GetAll();
        return View["stores.cshtml"];
      };
    }
  }
}
