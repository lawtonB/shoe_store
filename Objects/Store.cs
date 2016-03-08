using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace ShoeStore
{
  public class Store
  {
    private int _id;
    private string _store_name;

    public Store(string Store_Name, int Id = 0)
    {
      _id = Id;
      _store_name = Store_Name;
    }

    public override bool Equals(System.Object otherStore)
    {
      if (!(otherStore is Store))
      {
        return false;
      }
      else
      {
        Store newStore = (Store) otherStore;
        bool idEquality = this.GetId() == newStore.GetId();
        bool store_nameEquality = this.GetStore_Name() == newStore.GetStore_Name();
        return (idEquality && store_nameEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetStore_Name()
    {
      return _store_name;
    }
    public void SetStore_Name(string newStoreName)
    {
      _store_name = newStoreName;
    }

    public static List<Store> GetAll()
    {
      List<Store> allStores = new List<Store>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("select * from stores;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int storeId = rdr.GetInt32(0);
        string storeName = rdr.GetString(1);
        Store newStore = new Store(storeName, storeId);
        allStores.Add(newStore);
      }
      if(rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }
     return allStores;
    }

    public void AddBrand(Brand newBrand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO brands_stores (store_id, brand_id) VALUES (@StoreId, @BrandId);", conn);

      SqlParameter brandIdParameter = new SqlParameter();
      brandIdParameter.ParameterName = "@BrandId";
      brandIdParameter.Value = newBrand.GetId();

      SqlParameter storeIdParameter = new SqlParameter();
      storeIdParameter.ParameterName = "@StoreId";
      storeIdParameter.Value = this.GetId();

      cmd.Parameters.Add(brandIdParameter);
      cmd.Parameters.Add(storeIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Brand> GetBrands()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      List<Brand> brands = new List<Brand>{};

      SqlCommand cmd = new SqlCommand("SELECT brands.* FROM stores JOIN brands_stores ON (stores.id = brands_stores.store_id) JOIN brands ON (brands_stores.brand_id = brands.id) WHERE stores.id = @StoreId;", conn);

       SqlParameter storeIdParameter = new SqlParameter();
       storeIdParameter.ParameterName = "@StoreId";
       storeIdParameter.Value = this.GetId();

       cmd.Parameters.Add(storeIdParameter);

       rdr = cmd.ExecuteReader();

       while (rdr.Read())
       {
         int brandId = rdr.GetInt32(0);
         string brandName = rdr.GetString(1);
         Brand newBrand = new Brand(brandName, brandId);
         brands.Add(newBrand);
       }
       if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return brands;
      }

      public static Store Find(int id)
     {
       SqlConnection conn = DB.Connection();
       SqlDataReader rdr = null;
       conn.Open();

       SqlCommand cmd = new SqlCommand("SELECT * FROM stores WHERE id = @StoreId;", conn);
       SqlParameter StoreIdParameter = new SqlParameter();
       StoreIdParameter.ParameterName = "@StoreId";
       StoreIdParameter.Value = id.ToString();

       cmd.Parameters.Add(StoreIdParameter);
       rdr = cmd.ExecuteReader();

       int foundStoreId = 0;
       string foundStoreName = null;

       while(rdr.Read())
       {
         foundStoreId = rdr.GetInt32(0);
         foundStoreName = rdr.GetString(1);
       }
       Store foundStore = new Store(foundStoreName, foundStoreId);

       if (rdr != null)
       {
         rdr.Close();
       }
       if (conn != null)
       {
         conn.Close();
       }
       return foundStore;
     }



    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stores;", conn);
      cmd.ExecuteNonQuery();
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO stores (store_name) OUTPUT INSERTED.id VALUES (@StoreName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@StoreName";
      nameParameter.Value = this.GetStore_Name();
      cmd.Parameters.Add(nameParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Update(string newStoreName)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE stores SET store_name = @newStoreName OUTPUT INSERTED.store_name WHERE id = @StoreId;", conn);

      SqlParameter newStoreParameter = new SqlParameter();
      newStoreParameter.ParameterName = "@NewStoreName";
      newStoreParameter.Value = newStoreName;

      SqlParameter StoreIdParameter = new SqlParameter();
      StoreIdParameter.ParameterName = "@StoreId";
      StoreIdParameter.Value = this.GetId();

      cmd.Parameters.Add(newStoreParameter);
      cmd.Parameters.Add(StoreIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._store_name = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }

      public void Delete()
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("DELETE from stores WHERE id = @StoreId;", conn);

        SqlParameter storeIdParameter = new SqlParameter();
        storeIdParameter.ParameterName = "@StoreId";
        storeIdParameter.Value = this.GetId();

        cmd.Parameters.Add(storeIdParameter);
        cmd.ExecuteNonQuery();
        if (conn != null)
        {
          conn.Close();
        }
      }
    }
  }
