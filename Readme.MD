
In SQLCMD
CREATE DATABASE shoe_stores
GO
USE shoe_stores
GO
CREATE TABLE stores (id int identity(1,1), store_name varchar(255));
GO
CREATE TABLE brands (id int identity(1,1), brand_name varchar(255));
GO
CREATE table stores_brands (id int identity(1,1), store_id int, brand_id int);
GO

To implement the sql database files, please download the .sql files from the project folder and import them into Microsoft SQL Server Management Studio
Languages Used

    HTML
    C#
        Nancy
        Razor
    & coded in Atom, databases in SQL

(c) LawtonB 2016
