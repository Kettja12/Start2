CREATE TABLE [dbo].[Reservations] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [ReservationNodeId] INT NOT NULL,
    [CustomerId]     INT NULL,
    [ReservationStartDate]  DATETIME NOT NULL,
    [ReservationEndDate]  DATETIME NOT NULL,
    [ReservationTag] NVARCHAR(50) NOT NULL,
    CONSTRAINT [PK_Reservations] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Reservations_ReservationNodes_Id] FOREIGN KEY ([ReservationNodeId]) REFERENCES [dbo].[ReservationNodes] ([Id]) ON DELETE CASCADE
);


