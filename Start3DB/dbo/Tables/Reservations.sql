CREATE TABLE [dbo].[Reservations] (
    [Id]    VARCHAR(26) NOT NULL,
    [ReservationNodeId] VARCHAR(26) NOT NULL,
    [CustomerId]     VARCHAR(26) NULL,
    [ReservationStartDate]  DATETIME NOT NULL,
    [ReservationEndDate]  DATETIME NOT NULL,
    [ReservationTag] NVARCHAR(50) NOT NULL,
    [Modified] DATETIME NOT NULL, 
    CONSTRAINT [PK_Reservations] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Reservations_ReservationNodes_Id] FOREIGN KEY ([ReservationNodeId]) 
    REFERENCES [dbo].[ReservationNodes] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE 
);


