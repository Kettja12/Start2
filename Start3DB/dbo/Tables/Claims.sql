CREATE TABLE [dbo].[Claims] (
    [Id]         VARCHAR(26) NOT NULL,
    [UserId]     VARCHAR(26) NOT NULL,
    [ClaimType]  NVARCHAR (50) NOT NULL,
    [ClaimValue] NVARCHAR(50) NOT NULL,
    [Modified] DATETIME NOT NULL, 
    CONSTRAINT [PK_Claims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Claims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) 
    ON UPDATE CASCADE ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Claims_UserId]
    ON [dbo].[Claims]([UserId] ASC);

