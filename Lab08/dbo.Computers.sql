use COMPUTERS
CREATE TABLE [dbo].[Table]
(
	[ID] int identity(1,1),
	[Producer] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [Model] NVARCHAR(50) NULL, 
    [Count] INT NULL, 
)
