Create Database shoe_stores
go
use shoe_stores
go
create table stores (id int identity(1,1), store_name varchar(255));
go
create table brands (id int identity(1,1), brand_name varchar(255));
go
create table stores_brands (id int identity(1,1), store_id int, brand_id int);
go
