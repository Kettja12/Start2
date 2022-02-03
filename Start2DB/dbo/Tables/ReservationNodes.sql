CREATE TABLE [dbo].[ReservationNodes]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[ParentId] INT,
	[Name] nvarchar(50),
	CONSTRAINT [PK_[ReservationNodes] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_ReservationNodes_ReservationNodes] FOREIGN KEY([ParentId])
		REFERENCES [dbo].[ReservationNodes] ([Id])
)
