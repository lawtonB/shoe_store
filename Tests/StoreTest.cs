using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStore
{
  public class StoreTest: IDisposable
  {
    public StoreTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=shoe_stores_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_StoresEmptyAtFirst()
    {
      int result = Store.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      Store firstStore = new Store("Nike");
      Store secondStore = new Store("Nike");

      Assert.Equal(firstStore, secondStore);
    }

    [Fact]
    public void Test_Save_SavesStoreToDatabase()
    {
      Store testStore = new Store("Nike");
      testStore.Save();

      List<Store> result = Store.GetAll();
      List<Store> testlist = new List<Store>{testStore};

      Assert.Equal(testlist, result);
    }

    public void Dispose()
    {
      Store.DeleteAll();
      // Brand.DeleteAll();
    }
  }
}
