using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace ShoeStore
{
  public class Brand
  {
    private int _id;
    private string _brand_name;

    public Brand(string Brand_Name, int Id = 0)
    {
      _id = Id;
      _brand_name = Brand_Name;
    }

    public override bool Equals(System.Object otherBrand)
    {
      if (!(otherBrand is Brand))
      {
        return false;
      }
      else
      {
        Brand newBrand = (Brand) otherBrand;
        bool idEquality = this.GetId() == newBrand.GetId();
        bool brand_nameEquality = this.GetBrand_Name() == newBrand.GetBrand_Name();
        return (idEquality && brand_nameEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetBrand_Name()
    {
      return _brand_name;
    }
    public void SetBrand_Name(string newBrandName)
    {
      _brand_name = newBrandName;
    }

    public static List<Brand> GetAll()
    {
      List<Brand> allBrands = new List<Brand>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("select * from brands;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int brandId = rdr.GetInt32(0);
        string brandName = rdr.GetString(1);
        Brand newBrand = new Brand(brandName, brandId);
        allBrands.Add(newBrand);
      }
      if(rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }
     return allBrands;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO brands (brand_name) OUTPUT INSERTED.id VALUES (@BrandName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@BrandName";
      nameParameter.Value = this.GetBrand_Name();

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

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM brands;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
