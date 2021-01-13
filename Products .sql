Create Table Products(
ProductId Int Identity(1,1) Primary Key,
Name Varchar(50) Not Null,
Category Varchar(30),
Color Varchar(15),
UnitPrice Decimal Not Null,
AvailableQuantity Int Not Null,
CreatedDate DateTime Default(GetDate()) Not null)
GO