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

    [Fact]
    public void Test_GetBrands_ReturnsAllStoreBrands()
    {
      Store testStore = new Store("Nike");
      testStore.Save();

      Brand firstBrand = new Brand("Puma");
      firstBrand.Save();

      Brand secondBrand = new Brand("Adidas");
      secondBrand.Save();

      testStore.AddBrand(firstBrand);
      List<Brand> savedBrands = testStore.GetBrands();
      List<Brand> testlist = new List<Brand>{firstBrand};

      Assert.Equal(testlist, savedBrands);
    }

    [Fact]
    public void Test_Save_AssignsIdToStoreObject()
    {
      //Arrange
      Store testStore = new Store("Nike");
      testStore.Save();

      //Act
      Store savedStore = Store.GetAll()[0];

      int result = savedStore.GetId();
      int testId = testStore.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Update_UpdatesStoreInDatabase()
    {
      //Arrange
      string StoreName = "Nike";
      Store testStore = new Store(StoreName);
      testStore.Save();
      string newStoreName = "Adidas";

      //Act
      testStore.Update(newStoreName);

      string result = testStore.GetStore_Name();

      //Assert
      Assert.Equal(newStoreName, result);
    }

    public void Dispose()
    {
      Store.DeleteAll();
      Brand.DeleteAll();
    }
  }
}
