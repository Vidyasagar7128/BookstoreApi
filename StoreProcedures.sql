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
CREATE PROCEDURE AddToCartSP(
	@BookId int,
	@UserId int
)
AS
BEGIN
SET NOCOUNT ON
INSERT INTO Cart (BookId,UserId) VALUES(@BookId,@UserId)
END
GO
------------------------------------Increament Decreament for cart------------------------------------
CREATE PROCEDURE CartIncreamentSP(
	@BookId int,
	@UserId int
)
AS
BEGIN
SET NOCOUNT ON
UPDATE Cart SET Quantity= Quantity+1 WHERE BookId = @BookId AND UserId = @UserId
END
GO

CREATE PROCEDURE CartDecreamentSP(
	@BookId int,
	@UserId int
)
AS
BEGIN
SET NOCOUNT ON
UPDATE Cart SET Quantity= Quantity-1 WHERE BookId = @BookId AND UserId = @UserId
END
GO

ALTER PROCEDURE ShowCartItems(
	@UserId int
)
AS
BEGIN
SET NOCOUNT ON
SELECT Cart.CartId, Books.BookId, Cart.Quantity, Books.Title, Books.Author,
Books.Rating, Books.Reviews,Books.Price, Books.Details FROM Cart
INNER JOIN Books ON(Cart.BookId = Books.BookId)
WHERE Cart.UserId = @UserId
END
GO
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
