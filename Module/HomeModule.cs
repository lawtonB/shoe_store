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
        return View["brands.cshtml", allBrands];
      };
      Get["stores/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Store newStore = Store.Find(parameters.id);
        List<Brand> storeBrands = newStore.GetBrands();
        model.Add("store", newStore);
        model.Add("brands", storeBrands);
        return View["store_brands.cshtml", model];
      };
      Get["brands/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Brand newBrand = Brand.Find(parameters.id);
        List<Store> Brandstores = newBrand.GetStores();
        model.Add("brand", newBrand);
        model.Add("stores", Brandstores);
        return View["brand_stores.cshtml", model];
      };
      Get["stores/add/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Store newStore = Store.Find(parameters.id);
        List<Brand> allBrands = Brand.GetAll();
        model.Add("store", newStore);
        model.Add("brands", allBrands);
        return View["add_store.cshtml", model];
      };
      Post["store/add/{id}"] = parameters => {
        Store newStore = Store.Find(parameters.id);
        Brand newBrand = Brand.Find(Request.Form["id"]);
        newBrand.addStore(newStore);
        List<Store> allStores = Store.GetAll();
        return View["stores.cshtml", allStores];
      };

      Get["brands/add/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Brand newBrand = Brand.Find(parameters.id);
        List<Store> allstores = Store.GetAll();
        model.Add("brand", newBrand);
        model.Add("stores", allstores);
        return View["add_brand.cshtml", model];
      };

      Post["brands/add/{id}"] = parameters => {
        Brand newBrand = Brand.Find(parameters.id);
        Store newStore = Store.Find(Request.Form["id"]);
        newStore.AddBrand(newBrand);
        List<Brand> allBrands = Brand.GetAll();
        return View["brands.cshtml", allBrands];
      };

      Get["stores/edit/{id}"] = parameters => {
        Store newStore = Store.Find(parameters.id);
        return View["store_edit.cshtml", newStore];
      };

      Patch["/stores/edit/{id}"] = parameters => {
        Store newStore = Store.Find(parameters.id);
        newStore.Update(Request.Form["store-name"]);
        List<Store> allStores = Store.GetAll();
        return View["stores.cshtml", allStores];
      };

      Delete["/stores/delete/{id}"] = parameters => {
        Store newStore = Store.Find(parameters.id);
        newStore.Delete();
        List<Store> allStores = Store.GetAll();
        return View["stores.cshtml", allStores];
      };
    }
  }
}
