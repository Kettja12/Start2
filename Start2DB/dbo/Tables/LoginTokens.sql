CREATE TABLE [dbo].[LoginTokens] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Value]  NVARCHAR (50) NOT NULL,
    [UserId] INT            NOT NULL,
    CONSTRAINT [PK_LoginTokens] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoginTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LoginTokens_UserId]
    ON [dbo].[LoginTokens]([UserId] ASC);

