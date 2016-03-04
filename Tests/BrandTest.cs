using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStore
{
  public class BrandTest: IDisposable
  {
    public BrandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=shoe_stores_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_BrandsEmptyAtFirst()
    {
      int result = Brand.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      Brand firstBrand = new Brand("Puma");
      Brand secondBrand = new Brand("Puma");

      Assert.Equal(firstBrand, secondBrand);
    }

    [Fact]
    public void Test_Save_SavesBrandToDatabase()
    {
      Brand testBrand = new Brand("Puma");
      testBrand.Save();

      List<Brand> result = Brand.GetAll();
      List<Brand> testlist = new List<Brand>{testBrand};

      Assert.Equal(testlist, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToBrandObject()
    {
      //Arrange
      Brand testBrand = new Brand("Puma");
      testBrand.Save();

      //Act
      Brand savedBrand = Brand.GetAll()[0];

      int result = savedBrand.GetId();
      int testId = testBrand.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    public void Dispose()
    {
      // Store.DeleteAll();
      Brand.DeleteAll();
    }
  }
}
