CREATE DATABASE SportOrders
GO

USE SportOrders
GO

CREATE TABLE Products(
    IDProduct INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(50) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL
);
GO

CREATE TABLE Orders(
    IDOrder INT PRIMARY KEY IDENTITY(1,1),
    ClientIdentification NVARCHAR(20) NOT NULL,
    ClientAddress NVARCHAR(100) NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    Total DECIMAL(10, 2) NOT NULL
);
GO

CREATE TABLE OrdersDetail(
    IDOrderDetail INT PRIMARY KEY IDENTITY(1,1),
    IDOrder INT,
    IDProduct INT,
    ProductsQuantity INT NOT NULL,
    CONSTRAINT FK_OrdersDetail_Orders FOREIGN KEY (IDOrder) 
        REFERENCES Orders(IDOrder)
        ON DELETE CASCADE,
    CONSTRAINT FK_OrdersDetail_Products FOREIGN KEY (IDProduct) 
        REFERENCES Products(IDProduct)
        ON DELETE CASCADE
);
GO

--- PRODUCTS

CREATE PROCEDURE SP_InsertProduct
    @ProductName NVARCHAR(50),
    @Price DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO Products (ProductName, Price)
    VALUES (@ProductName, @Price);
END;
GO

CREATE PROCEDURE SP_GetAllProducts
AS
BEGIN
    SELECT * FROM Products;
END;
GO

CREATE PROCEDURE SP_GetProductById
    @IDProduct INT
AS
BEGIN
    SELECT * FROM Products WHERE IDProduct = @IDProduct;
END;
GO

CREATE PROCEDURE SP_UpdateProduct
    @IDProduct INT,
    @ProductName NVARCHAR(50),
    @Price DECIMAL(10, 2)
AS
BEGIN
    UPDATE Products
    SET ProductName = @ProductName,
        Price = @Price
    WHERE IDProduct = @IDProduct;
END;
GO

CREATE PROCEDURE SP_DeleteProduct
    @IDProduct INT
AS
BEGIN
    DELETE FROM Products WHERE IDProduct = @IDProduct;
END;
GO

---ORDERS

CREATE PROCEDURE SP_InsertOrder
    @ClientIdentification NVARCHAR(20),
    @ClientAddress NVARCHAR(100),
    @Total DECIMAL(10, 2),
	@OrderId INT OUTPUT
AS
BEGIN
    INSERT INTO Orders (ClientIdentification, ClientAddress, Total)
    VALUES (@ClientIdentification, @ClientAddress, @Total);
	SET @OrderId = SCOPE_IDENTITY();
END;
GO

CREATE PROCEDURE SP_GetAllOrders
AS
BEGIN
    SELECT * FROM Orders;
END;
GO

CREATE PROCEDURE SP_GetOrderById
    @IDOrder INT
AS
BEGIN
    SELECT * FROM Orders WHERE IDOrder = @IDOrder;
END;
GO

CREATE PROCEDURE SP_DeleteOrder
    @IDOrder INT
AS
BEGIN
    DELETE FROM Orders WHERE IDOrder = @IDOrder;
END;
GO

---ORDER DETAILS

CREATE PROCEDURE SP_InsertOrderDetail
    @IDOrder INT,
    @IDProduct INT,
    @ProductsQuantity INT
AS
BEGIN
    INSERT INTO OrdersDetail (IDOrder, IDProduct, ProductsQuantity)
    VALUES (@IDOrder, @IDProduct, @ProductsQuantity);
END;
GO

CREATE PROCEDURE SP_GetOrderDetailsByOrderId
    @IDOrder INT
AS
BEGIN
    SELECT * FROM OrdersDetail WHERE IDOrder = @IDOrder;
END;
GO