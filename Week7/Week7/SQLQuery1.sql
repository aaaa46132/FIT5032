CREATE TABLE [dbo].[Students](
	[Id] int identity(1,1) Not Null,
	[FirstName] nvarchar(max) Not Null,
	[LastName] nvarchar(max) Not Null,
	[UserId] nvarchar(max) Not Null,
	PRIMARY KEY (Id)
	);
	GO
	
	CREATE TABLE [dbo].[Units](
	[Id] int identity(1,1) Not Null,
	[Name] nvarchar(max) Not Null,
	[Description] nvarchar(max) Not Null,
	[StudentId] int Not Null,
	PRIMARY KEY (Id),
	FOREIGN KEY (StudentId) REFERENCES Students (Id)
	);
	GO