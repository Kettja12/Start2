CREATE TABLE [dbo].[ReservationNodes]
(
	[Id] VARCHAR(26) NOT NULL,
	[ParentId] VARCHAR(26),
	[Name] nvarchar(50),
    [Modified] DATETIME NOT NULL, 
	CONSTRAINT [PK_ReservationNodes] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_ReservationNodes_ReservationNodes] FOREIGN KEY([ParentId])
		REFERENCES [dbo].[ReservationNodes] ([Id])
)
