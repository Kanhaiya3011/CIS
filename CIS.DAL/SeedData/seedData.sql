USE [CIS]
GO
--Role Table
INSERT INTO [dbo].[Roles]
           ([RoleName]
           ,[IsActive]
           ,[RoleDescription])
     VALUES
           ('Admin', 1, 'Admin of the Application'
		   )

INSERT INTO [dbo].[Roles]
           ([RoleName]
           ,[IsActive]
           ,[RoleDescription])
     VALUES
           ('User', 1, 'User of the Application'
		   )
GO

--Category
USE [CIS]
GO

INSERT INTO [dbo].[Categories]
           ([CategoryName]
           ,[CategoryDescription]
           ,[isActive])
     VALUES
           ( 
			'Entertainment', 'This is Entertainment', 1
		   )
INSERT INTO [dbo].[Categories]
           ([CategoryName]
           ,[CategoryDescription]
           ,[isActive])
     VALUES
           ( 
			'Education', 'This is Education', 1
		   )
INSERT INTO [dbo].[Categories]
           ([CategoryName]
           ,[CategoryDescription]
           ,[isActive])
     VALUES
           ( 
			'Health', 'This is Health', 1
		   )

INSERT INTO [dbo].[Categories]
           ([CategoryName]
           ,[CategoryDescription]
           ,[isActive])
     VALUES
           ( 
			'Finance', 'This is Finance', 1
		   )
GO

--User
USE [CIS]
GO

INSERT INTO [dbo].[Users]
           ([FirstName]
           ,[LastName]
           ,[Email]
           ,[Password]
           ,[RoleId]
           ,[Status])
     VALUES
           ( 'Admin', 'Admin', 'admin@cis.com', 'A12345', 1, 'Approved')
GO
