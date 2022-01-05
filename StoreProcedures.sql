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