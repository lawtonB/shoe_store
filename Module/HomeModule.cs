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
      Get["stores/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Store newStore = Store.Find(parameters.id);
        List<Brand> storeBrands = newStore.GetBrands();
        model.Add("store", newStore);
        model.Add("brands", storeBrands);
        return View["brand_stores.cshtml", model];
      };
      Get["brands/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Brand newBrand = Brand.Find(parameters.id);
        List<Store> Brandstores = newBrand.GetStores();
        model.Add("store", newBrand);
        model.Add("brands", Brandstores);
        return View["stores_brands.cshtml", model];
      };


      Delete["/store/delete/{id}"] = parameters => {
        Store newStore = Store.Find(parameters.id);
        newStore.DeleteAll();
        List<Store> allStores = Store.GetAll();
        return View["stores.cshtml", allStores];
      };
    }
  }
}
