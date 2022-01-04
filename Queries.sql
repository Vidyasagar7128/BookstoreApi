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
INSERT Books(Title,Author,Rating, Price, Details) VALUES('The Alchemist','Steve krug',4.4,1200,'Best book for read.')
select * from Books
select * from BookImages
select * from Users
select * from Reviews
update Books set Reviews = 0 where BookId = 2

INSERT BookImages VALUES('https//:google.com',1)
--------------------------------------------Reviews Table-------------------
CREATE TABLE Reviews (ReviewId int IDENTITY PRIMARY KEY NOT NULL, Star float(53), Text varchar(255),
BookId int FOREIGN KEY REFERENCES Books(BookId),UserId int FOREIGN KEY REFERENCES Users(UserId))
INSERT Reviews VALUES(4.5,'Best book',1,2)
--------------------------------------------------------------------
drop table BookImages
drop table Books
drop table Reviews
