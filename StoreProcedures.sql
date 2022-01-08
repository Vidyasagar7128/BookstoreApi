CREATE PROCEDURE AddBookSP(
	@Title varchar(100),
	@Author varchar(100),
	@Rating bigint,
	@Reviews int,
	@Quantity int,
	@Price bigint,
	@Details varchar(255)
)
AS
BEGIN
SET NOCOUNT ON
INSERT INTO Books VALUES(@Title,@Author,@Rating,@Reviews,@Quantity,@Price,@Details)
END
GO
select * from Books
--------------------------------------------for Update Books-----------------------
CREATE PROCEDURE UpdateBookSP(
	@BookId int,
	@Title varchar(100),
	@Author varchar(100),
	@Quantity int,
	@Price bigint,
	@Details varchar(255)
)
AS
BEGIN
SET NOCOUNT ON
UPDATE Books SET Title= @Title, Author= @Author, Quantity= @Quantity, Price= @Price, Details = @Details WHERE BookId= @BookId
END
GO
-------------------------------------------for Cart table--------------------------------
ALTER PROCEDURE AddToCartSP(
	@BookId int,
	@UserId int,
	@Price int
)
AS
BEGIN
SET NOCOUNT ON
INSERT INTO Cart (BookId,UserId,Price) VALUES(@BookId,@UserId,@Price)
END
GO
------------------------------------Increament Decreament for cart------------------------------------
ALTER PROCEDURE CartIncreamentSP(
	@CartId int,
	@Quantity int,
	@Price float,
	@UserId int
)
AS
BEGIN
SET NOCOUNT ON
UPDATE Cart SET Price = @Price, Quantity= @Quantity WHERE CartId = @CartId AND UserId = @UserId
END
GO

ALTER PROCEDURE ShowCartItems(
	@UserId int
)
AS
BEGIN
SET NOCOUNT ON
SELECT Cart.CartId, Books.BookId, Cart.Quantity, Books.Title, Books.Author,
Books.Rating, Books.Reviews,Cart.Price, Books.Details FROM Cart
INNER JOIN Books ON(Cart.BookId = Books.BookId)
WHERE Cart.UserId = @UserId
END
GO
exec ShowCartItems 3
----------------------------------------Address---------------------------------
CREATE PROCEDURE SetAddressSP(
	@Address varchar(150),
	@City varchar(30),
	@State varchar(30),
	@Type varchar(25),
	@UserId int
)
AS
BEGIN
SET NOCOUNT ON
INSERT INTO Address (Address, City, State, Type, UserId) VALUES(@Address, @City, @State, @Type, @UserId)
END
GO

ALTER PROCEDURE UpdateAddressSP(
	@AddressId int,
	@Address varchar(150),
	@City varchar(30),
	@State varchar(30),
	@Type varchar(25),
	@UserId int
)
AS
BEGIN
SET NOCOUNT ON
UPDATE Address SET Address = @Address, City = @City, State = @State, Type = @Type
WHERE AddressId = @AddressId AND UserId = @UserId
END
GO

CREATE PROCEDURE ShowAddress(
	@UserId int
)
AS
BEGIN
SET NOCOUNT ON
SELECT Users.Name, Users.Mobile, Address.AddressId, Address.Address, Address.City, Address.State,Address.Type FROM Address
INNER JOIN Users ON(Address.UserId = Users.UserId)
WHERE Users.UserId = @UserId
END
GO

select * from Address
-------------------------------------------------Wishlist Table-------------------------
ALTER PROCEDURE AddToWishlistSP(
	@BookId int,
	@UserId int
)
AS
BEGIN
SET NOCOUNT ON
INSERT INTO WishList (BookId, UserId) VALUES(@BookId, @UserId)
END
GO

CREATE PROCEDURE RemoveFromWishlistSP(
	@BookId int,
	@UserId int
)
AS
BEGIN
SET NOCOUNT ON
DELETE FROM WishList WHERE BookId = @BookId AND UserId = @UserId
END
GO

CREATE PROCEDURE DataFromWishlistSP(
	@UserId int
)
AS
BEGIN
SET NOCOUNT ON
SELECT * FROM Books
INNER JOIN WishList ON(Books.BookId = WishList.BookId)
WHERE WishList.UserId = @UserId
END
GO
--------------------------------------Order Table--------------------------
ALTER PROCEDURE AddOrderSP(
	@BookId int,
	@Quantity int,
	@AddressId int,
	@UserId int
)
AS
BEGIN
SET NOCOUNT ON
INSERT INTO Orders (BookId, Quantity, AddressId, UserId) VALUES(@BookId, @Quantity, @AddressId, @UserId)
END
GO

CREATE PROCEDURE CancleOrderSP(
	@OrderId int,
	@UserId int
)
AS
BEGIN
SET NOCOUNT ON
UPDATE Orders SET Status = 2 WHERE OrderId = @OrderId AND UserId = @UserId
END
GO

ALTER PROCEDURE ShowOrderSP(
	@UserId int
)
AS
BEGIN
SET NOCOUNT ON
SELECT Orders.OrderId, Users.Name, Users.Mobile, Address.Address, Address.City,Address.State, Books.Title,
Cart.Quantity, Books.Price, Orders.Status, Orders.OrderDate FROM Books
INNER JOIN Orders ON(Orders.BookId = Books.BookId)
INNER JOIN Address ON(Address.AddressId = Orders.AddressId)
INNER JOIN Cart ON(Cart.BookId = Orders.BookId)
INNER JOIN Users ON(Users.UserId = Orders.UserId)
WHERE Users.UserId = @UserId
END
GO
exec ShowOrderSP 3

select * from Orders
select * from Users
select * from Address
select * from Books
select * from Cart