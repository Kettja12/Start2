CREATE TABLE [dbo].[DashboardItems]
(
	[Id] INT NOT NULL identity (1,1) PRIMARY KEY , 
    [Control] NVARCHAR(50) NOT NULL, 
    [InUse] BIT NULL, 
    [UserGroups] NVARCHAR(50) NULL 
)
