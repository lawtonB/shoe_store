using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace ShoeStoreNameSpace
{
  public class Store
  {
    private int _id;
    private string _store_name;

    public Store(string Store_Name, int id = 0)
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
        bool store_nameEquality = this.GetStore_name() == newStore.GetStore_name();
        return (idEquality && store_nameEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetStore_name()
    {
      return store_name;
    }
    
  }
}
