SET IDENTITY_INSERT [dbo].[Games] ON
INSERT INTO [dbo].[Games] ([GameID], [Name], [Description], [Category], [Price]) VALUES (1, N'Mario', N'Cool game', N'TopGame', CAST(100.00 AS Decimal(16, 2)))
INSERT INTO [dbo].[Games] ([GameID], [Name], [Description], [Category], [Price]) VALUES (3, N'Minecraft', N'Улюблена гра Аліси', N'CoolGame', CAST(300.00 AS Decimal(16, 2)))
INSERT INTO [dbo].[Games] ([GameID], [Name], [Description], [Category], [Price]) VALUES (5, 'SimCity', 'Градостроительный симулятор снова с вами! Создайте город своей мечты', 'Симулятор', CAST(1000.00 AS Decimal(16, 2)))
INSERT INTO [dbo].[Games] ([GameID], [Name], [Description], [Category], [Price]) VALUES (4, 'Battlefield 4', 'Battlefield 4 – это определяющий для жанра, полный экшена боевик, известный своей разрушаемостью, равных которой нет', 'Шутер', CAST(1200.00 AS Decimal(16, 2)))
SET IDENTITY_INSERT [dbo].[Games] OFF
