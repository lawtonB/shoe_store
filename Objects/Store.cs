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

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stores;", conn);
      cmd.ExecuteNonQuery();
    }

  }
}
