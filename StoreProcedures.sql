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
