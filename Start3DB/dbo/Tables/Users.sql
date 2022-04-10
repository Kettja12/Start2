CREATE TABLE [dbo].[Users] (
    [Id]        VARCHAR(26) NOT NULL,
    [Firstname] NVARCHAR (50) NOT NULL,
    [Lastname]  NVARCHAR (50) NOT NULL,
    [Username]  NVARCHAR (50) NOT NULL,
    [Modified] DATETIME NOT NULL, 
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

CREATE UNIQUE INDEX [IX_Users_Column] ON [dbo].[Users] ([Username])
