USE [WebStoreDB]
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1ad0b4dd6fe245deb03e84eb68fad5d6', N'user@webstore.pl', N'USER@WEBSTORE.PL', N'user@webstore.pl', N'USER@WEBSTORE.PL', 0, N'AQAAAAEAACcQAAAAEKWMZclZM9tkn5sSmOFWzNGHQwiOmsY/okBn0Cv+MstIMhsyCQXlOnKkWVBSQvXa0A==', N'23YYM3HNSADVOZUMMCW7VJQP64H5XOHP', N'0acd93f876474395bf17a8bc043e2b9a', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'fdc7a4212ea54f719bd55155f9ed9a80', N'admin@webstore.pl', N'ADMIN@WEBSTORE.PL', N'admin@webstore.pl', N'ADMIN@WEBSTORE.PL', 0, N'AQAAAAEAACcQAAAAEC0kmMm3lKv7BAKqhWc/wbT9JgFz6cC/17FEtGSD4Z0YgxTeWlvAprGE8fRv0K/ihg==', N'E3TUQSLCW3ZHXBZ4EDD6HMRD2P4SNTQM', N'e1e6a6fd2bd74c4d8972351d2f544249', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[ApplicationUsers] ([ApplicationUser_Key], [FirstName], [LastName], [City], [PostalCode], [Street], [BuildingNumber], [ApartmentNumber]) VALUES (N'1ad0b4dd6fe245deb03e84eb68fad5d6', N'User', N'Ógułem', N'Białystok', N'15-640', N'Szkolna', N'17', NULL)
INSERT [dbo].[ApplicationUsers] ([ApplicationUser_Key], [FirstName], [LastName], [City], [PostalCode], [Street], [BuildingNumber], [ApartmentNumber]) VALUES (N'fdc7a4212ea54f719bd55155f9ed9a80', N'Admin', N'Psikuta', N'City', N'12-345', N'Street', N'1', N'2')
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [IsVisible], [ParentCategoryId]) VALUES (1, N'Dla dzieci', 1, NULL)
INSERT [dbo].[Categories] ([Id], [Name], [IsVisible], [ParentCategoryId]) VALUES (2, N'Zabawki', 1, 1)
INSERT [dbo].[Categories] ([Id], [Name], [IsVisible], [ParentCategoryId]) VALUES (3, N'Zdrowie i uroda', 1, NULL)
INSERT [dbo].[Categories] ([Id], [Name], [IsVisible], [ParentCategoryId]) VALUES (4, N'Elektronika', 1, NULL)
INSERT [dbo].[Categories] ([Id], [Name], [IsVisible], [ParentCategoryId]) VALUES (5, N'Komputery', 1, 4)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Items] ON 

INSERT [dbo].[Items] ([Id], [Name], [Description], [Price], [AddedDateTime], [StockQuantity], [IsVisible], [IsNew], [IsInRecycleBin], [CategoryId]) VALUES (1, N'Granatnik Szymczyka', N'Lekki i poręczny granatnik typu RGW-90. Możesz pochwalić się nim koledze. INSTRUKCJA UŻYCIA: 1. Upewnij się, że granatnik jest naładowany. 2. Wyceluj w drzwi i pociągni za spust. 3. Ciesz się sianym spustoszeniem', CAST(6.4000 AS Decimal(19, 4)), CAST(N'2023-01-19T16:22:48.307' AS DateTime), 1, 1, 1, 0, 2)
INSERT [dbo].[Items] ([Id], [Name], [Description], [Price], [AddedDateTime], [StockQuantity], [IsVisible], [IsNew], [IsInRecycleBin], [CategoryId]) VALUES (2, N'Perełka', N'Napój Bogów i studentów (debili). Przyjmować tylko w dużych dawkach!', CAST(2.7000 AS Decimal(19, 4)), CAST(N'2023-01-19T16:22:48.443' AS DateTime), 1000, 1, 1, 0, 3)
INSERT [dbo].[Items] ([Id], [Name], [Description], [Price], [AddedDateTime], [StockQuantity], [IsVisible], [IsNew], [IsInRecycleBin], [CategoryId]) VALUES (3, N'Cygański laptop czterordzeniowy', N'Wydajny i szybki, cieszący oko prostym ale gustownym designem', CAST(2700.0000 AS Decimal(19, 4)), CAST(N'2023-01-19T16:22:48.447' AS DateTime), 6, 1, 1, 0, 5)
SET IDENTITY_INSERT [dbo].[Items] OFF
GO
INSERT [dbo].[CartItems] ([UserId], [ItemId], [Quantity]) VALUES (N'1ad0b4dd6fe245deb03e84eb68fad5d6', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[ShippingMethods] ON 

INSERT [dbo].[ShippingMethods] ([Id], [Method], [Price]) VALUES (1, N'1', CAST(15.9900 AS Decimal(19, 4)))
SET IDENTITY_INSERT [dbo].[ShippingMethods] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [Status], [PaymentMethod], [OrderDateTime], [UserId], [ShippingMethodId]) VALUES (1, N'3', N'0', CAST(N'2023-01-19T16:22:49.030' AS DateTime), N'1ad0b4dd6fe245deb03e84eb68fad5d6', 1)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2da682ff575246d6bdd1ecad11e1cb22', N'Administrator', N'ADMINISTRATOR', N'fcd3a770-cdd1-4ea5-9f2a-3c522c7335c4')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'7781b0b884a8463897009e81037a7947', N'Regular User', N'REGULAR USER', N'6cd08ffc-3df7-4c2d-82bb-5d19dc72b5ba')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1ad0b4dd6fe245deb03e84eb68fad5d6', N'7781b0b884a8463897009e81037a7947')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fdc7a4212ea54f719bd55155f9ed9a80', N'2da682ff575246d6bdd1ecad11e1cb22')
GO
SET IDENTITY_INSERT [dbo].[ItemImages] ON 

INSERT [dbo].[ItemImages] ([Id], [URL], [ItemId]) VALUES (1, N'C:\Users\Artur\Source\Repos\WebStore\WebStore\wwwroot\images\item_images\granatnik.jpg', 1)
INSERT [dbo].[ItemImages] ([Id], [URL], [ItemId]) VALUES (2, N'C:\Users\Artur\Source\Repos\WebStore\WebStore\wwwroot\images\item_images\perelka.png', 2)
INSERT [dbo].[ItemImages] ([Id], [URL], [ItemId]) VALUES (3, N'C:\Users\Artur\Source\Repos\WebStore\WebStore\wwwroot\images\item_images\laptop.PNG', 3)
SET IDENTITY_INSERT [dbo].[ItemImages] OFF
GO
INSERT [dbo].[OrderedItems] ([OrderId], [ItemId], [Quantity]) VALUES (1, 2, 100)
INSERT [dbo].[OrderedItems] ([OrderId], [ItemId], [Quantity]) VALUES (1, 3, 1)
GO
