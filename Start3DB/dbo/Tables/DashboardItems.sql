CREATE TABLE [dbo].[DashboardItems]
(
	[Id] VARCHAR(26) NOT NULL, 
    [Control] NVARCHAR(50) NOT NULL, 
    [InUse] BIT NULL, 
    [UserGroups] NVARCHAR(50) NULL, 
    [Modified] DATETIME NOT NULL, 
    CONSTRAINT [PK_DashboardItems] PRIMARY KEY CLUSTERED ([Id] ASC),

)
