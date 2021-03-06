CREATE DATABASE Bookstore
USE Bookstore
-----------------------------------------------------------Users-------------------------------------------------------
CREATE TABLE Users (UserId int IDENTITY PRIMARY KEY NOT NULL, Name varchar(100) NOT NULL, Email varchar(100) NOT NULL, Mobile bigint NOT NULL,Password varchar(250) NOT NULL)
SELECT * FROM Users
delete Users where UserId=6
--------StoreProcedure---------
CREATE PROCEDURE CreateUserSP(
	@Name varchar(50),
	@Email varchar(50),
	@Mobile bigint,
	@Password varchar(50)
)
AS
BEGIN
SET NOCOUNT ON
INSERT INTO Users VALUES(@Name,@Email,@Mobile,@Password)
END
GO
-------------------------------------------------------------Books----------------------------------------------------
CREATE TABLE BookImages (ImageId int IDENTITY PRIMARY KEY NOT NULL, ImageUrl varchar(255), BookId int FOREIGN KEY REFERENCES Books(BookId));

CREATE TABLE Books (BookId int IDENTITY PRIMARY KEY NOT NULL, Title varchar(100),
Author varchar(100),Rating float(53), Reviews int, Quantity int, Price float(53), Details varchar(255));
SET IDENTITY_INSERT Bookstore.[dbo].Books ON
INSERT Books VALUES('Think And Grow Rich','Napoleon Hill',4.4,40,10,290.00,'Think and Grow Rich is a book written by Napoleon Hill in 1937 and promoted as a personal development and self-improvement book.')
select * from Books
select * from BookImages
select * from Users
select * from Reviews
update Books set Reviews = 0 where BookId = 2
ALTER TABLE Books ADD ImgPath varchar(100) 
ALTER TABLE Books ADD CONSTRAINT Quantity CHECK(Quantity > -1)
---------------------------------------------------------------BookImages--------------------
INSERT BookImages VALUES('https//:img4.com',8)
--delete from Books where BookId=4
--delete from BookImages where BookId=4
--delete from BookImages where ImageId=13
--delete from Users where UserId=7

--------------------------------------------Reviews Table-------------------
CREATE TABLE Reviews (ReviewId int IDENTITY PRIMARY KEY NOT NULL, Star float(53), Text varchar(255),
BookId int FOREIGN KEY REFERENCES Books(BookId),UserId int FOREIGN KEY REFERENCES Users(UserId))
INSERT Reviews VALUES(4.5,'Best book',1,2)
--------------------------------------------------------Added Cascade-----------------------------
alter table BookImages drop constraint FK__BookImage__BookI__3B75D760
alter table BookImages add constraint FK__BookImage__BookI__3B75D760
foreign key (BookId) references Books(BookId) on delete cascade
-------------------------------------------------------Cart Table------------
CREATE TABLE Cart (CartId int IDENTITY PRIMARY KEY NOT NULL,Quantity int default 1, BookId int FOREIGN KEY REFERENCES Books(BookId),UserId int FOREIGN KEY REFERENCES Users(UserId) on delete cascade)
select * from Cart
ALTER TABLE Cart ADD Price float(53)
truncate table Cart
----------------------------------------------Address Table----------------------
CREATE TABLE Address (AddressId int IDENTITY PRIMARY KEY NOT NULL, Address varchar(150), City varchar(30), State varchar(30), Type varchar(25),
UserId int FOREIGN KEY REFERENCES Users(UserId) on delete cascade)

insert into Address VALUES('Pratik Nagar','Pune','Maharashtra','Home',3)
select * from Address
SELECT Users.Name, Users.Mobile, Address.AddressId,Address.Address, Address.City, Address.State, Address.Type from Address
INNER JOIN Users ON(Address.UserId = Users.UserId)
WHERE Users.UserId = 3
--------------------------------------------Wishlist Table----------------
CREATE TABLE WishList (WishlistId int IDENTITY PRIMARY KEY NOT NULL,
BookId int FOREIGN KEY REFERENCES Books(BookId) on delete cascade,
UserId int FOREIGN KEY REFERENCES Users(UserId) on delete cascade)
select * from WishList
---------------------------------------------Order Table-------------------
CREATE TABLE Orders (OrderId int IDENTITY PRIMARY KEY NOT NULL,
BookId int FOREIGN KEY REFERENCES Books(BookId) on delete cascade,
AddressId int FOREIGN KEY REFERENCES Address(AddressId),
UserId int FOREIGN KEY REFERENCES Users(UserId) on delete cascade,
Quantity int,
TotalPrice decimal default 0,
Status int default 1,
OrderDate DATE default GETDATE()
)
select * from Orders
---------------------------------------------Reviews Table-------------------
CREATE TABLE Reviews (ReviewId int IDENTITY PRIMARY KEY NOT NULL,
Rating float(53), Comment varchar(150), CreatedAt DATE default GETDATE(),
BookId int FOREIGN KEY REFERENCES Books(BookId) on delete cascade,
UserId int FOREIGN KEY REFERENCES Users(UserId)
)
