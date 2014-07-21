CREATE TABLE [dbo].[Rider]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [Surname] NVARCHAR(50) NOT NULL, 
    [TeamId] INT NOT NULL, 
    CONSTRAINT [FK_Rider_Team] FOREIGN KEY ([TeamId]) REFERENCES [Team]([Id])
)
