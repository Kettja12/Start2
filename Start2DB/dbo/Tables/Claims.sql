CREATE TABLE [dbo].[Claims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     INT            NOT NULL,
    [ClaimType]  NVARCHAR (50) NOT NULL,
    [ClaimValue] NVARCHAR(50) NOT NULL,
    CONSTRAINT [PK_Claims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Claims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Claims_UserId]
    ON [dbo].[Claims]([UserId] ASC);

