USE [master]
GO
/****** Object:  Database [SCB.Surkova.Credit_approval_system]    Script Date: 29.07.2021 12:48:28 ******/
CREATE DATABASE [SCB.Surkova.Credit_approval_system]
 CONTAINMENT = NONE

GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SCB.Surkova.Credit_approval_system].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET ARITHABORT OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET RECOVERY FULL 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET  MULTI_USER 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SCB.Surkova.Credit_approval_system', N'ON'
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET QUERY_STORE = OFF
GO
USE [SCB.Surkova.Credit_approval_system]
GO
/****** Object:  Table [dbo].[Loan]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Sum] [bigint] NOT NULL,
	[Date] [date] NOT NULL,
	[PassportID] [int] NOT NULL,
	[StatusID] [int] NOT NULL,
	[AdditionalScanID] [int] NOT NULL,
 CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Passport]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Passport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Series] [nchar](4) NOT NULL,
	[Number] [nchar](6) NOT NULL,
	[ScanID] [int] NULL,
 CONSTRAINT [PK_Passport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScanFile]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScanFile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Link] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_ScanFile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Patronic] [nvarchar](50) NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [varbinary](max) NOT NULL,
	[AdditionalScanID] [int] NULL,
	[PassportID] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Loan]  WITH CHECK ADD  CONSTRAINT [FK_Loan_Passport] FOREIGN KEY([PassportID])
REFERENCES [dbo].[Passport] ([Id])
GO
ALTER TABLE [dbo].[Loan] CHECK CONSTRAINT [FK_Loan_Passport]
GO
ALTER TABLE [dbo].[Loan]  WITH CHECK ADD  CONSTRAINT [FK_Loan_ScanFile] FOREIGN KEY([AdditionalScanID])
REFERENCES [dbo].[ScanFile] ([Id])
GO
ALTER TABLE [dbo].[Loan] CHECK CONSTRAINT [FK_Loan_ScanFile]
GO
ALTER TABLE [dbo].[Loan]  WITH CHECK ADD  CONSTRAINT [FK_Loan_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Loan] CHECK CONSTRAINT [FK_Loan_Status]
GO
ALTER TABLE [dbo].[Loan]  WITH CHECK ADD  CONSTRAINT [FK_Loan_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Loan] CHECK CONSTRAINT [FK_Loan_User]
GO
ALTER TABLE [dbo].[Passport]  WITH CHECK ADD  CONSTRAINT [FK_Passport_ScanFile] FOREIGN KEY([ScanID])
REFERENCES [dbo].[ScanFile] ([Id])
GO
ALTER TABLE [dbo].[Passport] CHECK CONSTRAINT [FK_Passport_ScanFile]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Passport] FOREIGN KEY([PassportID])
REFERENCES [dbo].[Passport] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Passport]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_ScanFile] FOREIGN KEY([AdditionalScanID])
REFERENCES [dbo].[ScanFile] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_ScanFile]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Role]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_User]
GO
/****** Object:  StoredProcedure [dbo].[AddApplication]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddApplication]
	@UserID INT,
	@Sum BIGINT,
	@Status int,
	@Date DATE,
	@PassportID INT,
	@AdditionalScanID INT
AS
BEGIN
	INSERT INTO Loan(UserID, Sum, PassportID, StatusID, Date, AdditionalScanID)
	VALUES(@UserID, @Sum, @PassportID, @Status, @Date, @AdditionalScanID)
END
GO
/****** Object:  StoredProcedure [dbo].[AddPassport]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddPassport]
	@Series NVARCHAR(50),
	@Number NVARCHAR(50),
	@ScanID INT NULL
AS
BEGIN
	INSERT INTo Passport(Series, Number, ScanID)
	VALUES(@Series, @Number, @ScanID)
END
GO
/****** Object:  StoredProcedure [dbo].[AddRole]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddRole]
	@Title NVARCHAR(50)
AS
BEGIN
	INSERT INTO Role(Title)
	VALUES(@Title)
END
GO
/****** Object:  StoredProcedure [dbo].[AddRoleForUser]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddRoleForUser]
@Id INT,
@RoleId INT
AS
BEGIN
	INSERT INTO UserRoles(UserID, RoleID)
	VALUES(@Id, @RoleId)
END
GO
/****** Object:  StoredProcedure [dbo].[AddScan]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddScan]
	@Name NVARCHAR(50),
	@Link VARBINARY(MAX)
AS
BEGIN
	INSERT INTO ScanFile(Name, Link)
	VALUES(@Name, @Link)
	SELECT sf.Id FROM ScanFile as sf WHERE sf.Name=@Name and sf.Link=@Link
END
GO
/****** Object:  StoredProcedure [dbo].[AddScanForPassport]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddScanForPassport]
	@Id INT,
	@ScanId INT
AS
BEGIN
	UPDATE Passport SET ScanID=@ScanId WHERE Id=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[AddScanForUser]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddScanForUser]
	@Id INT,
	@ScanId INT
AS
BEGIN
	UPDATE [User] SET AdditionalScanID=@ScanId WHERE Id=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[AddStatus]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddStatus]
	@Title NVARCHAR(50)
AS
BEGIN
	INSERT INTO Status(Title)
	VALUES(@Title)
END
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddUser]
	@Name NVARCHAR(50),
	@Surname NVARCHAR(50),
	@Patronic NVARCHAR(50) NULL,
	@PassportID INT,
	@Login  NVARCHAR(50),
	@Password VARBINARY(50),
	@AdditionalScanID INT NULL
AS
BEGIN
	INSERT INTO [User](Name, Surname, Patronic, PassportID, Login, Password, AdditionalScanID)
	VALUES(@Name, @Surname, @Patronic, @PassportID, @Login, @Password, @AdditionalScanID)
	SELECT u.Id FROM [User] as u WHERE u.Login=@Login and u.Password=@Password
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteUser]
	@Id INT
AS
BEGIN
	DELETE [User] FROM [User] as u
	JOIN Passport as p
	ON u.PassportID = p.Id
	LEFT JOIN ScanFile as sf
	ON p.ScanID = sf.Id
	LEFT JOIN ScanFile as asf
	ON u.AdditionalScanID = asf.Id
	JOIN UserRoles as ur
	ON ur.UserID = u.Id
	WHERE u.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetApplication]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetApplication]
	@Id INT,
	@Sum BIGINT,
	@Date DATE
AS
BEGIN
	SELECT	a.Id,
			a.UserID,
			a.StatusID,
			a.Sum,
			a.Date,
			a.AdditionalScanID,
			scan.Name as scanName,
			scan.Link,
			a.PassportID
	FROM Loan as a
	JOIN Status as s
	ON a.StatusID = s.Id
	JOIN ScanFile as scan
	ON a.AdditionalScanID = scan.Id
	JOIN [User] as u
	ON a.UserID = u.Id
	WHERE u.Id = @id and a.Sum=@Sum and a.Date=@Date
END
GO
/****** Object:  StoredProcedure [dbo].[GetApplicationsOfUser]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetApplicationsOfUser]
	@id INT 
AS
BEGIN
	SELECT	a.Id,
			a.UserID,
			a.StatusID,
			a.Sum,
			a.Date,
			a.AdditionalScanID,
			scan.Name as scanName,
			scan.Link,
			a.PassportID
	FROM Loan as a
	JOIN Status as s
	ON a.StatusID = s.Id
	JOIN ScanFile as scan
	ON a.AdditionalScanID = scan.Id
	JOIN [User] as u
	ON a.UserID = u.Id
	WHERE u.Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[GetCurrentApplications]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCurrentApplications]
AS
BEGIN
	SELECT	a.Id,
			a.UserID,
			a.StatusID,
			a.Sum,
			a.Date,
			a.AdditionalScanID,
			scan.Name as scanName,
			scan.Link,
			a.PassportID
	FROM Loan as a
	JOIN Status as s
	ON a.StatusID = s.Id
	JOIN ScanFile as scan
	ON a.AdditionalScanID = scan.Id
	JOIN [User] as u
	ON a.UserID = u.Id
	WHERE s.Title='In Waiting'
END
GO
/****** Object:  StoredProcedure [dbo].[GetHistoryOfApplications]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetHistoryOfApplications]
AS
BEGIN
	SELECT	a.Id,
			a.UserID,
			a.StatusID,
			a.Sum,
			a.Date,
			a.AdditionalScanID,
			scan.Name as scanName,
			scan.Link,
			a.PassportID
	FROM Loan as a
	JOIN Status as s
	ON a.StatusID = s.Id
	JOIN ScanFile as scan
	ON a.AdditionalScanID = scan.Id
	JOIN [User] as u
	ON a.UserID = u.Id
	WHERE s.Title !='In Waiting'
END
GO
/****** Object:  StoredProcedure [dbo].[GetPassport]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPassport]
	@Series NVARCHAR(50),
	@Number NVARCHAR(50)
AS
BEGIN
	SELECT	p.Id,
			p.Number,
			p.Series,
			p.ScanID,
			sf.Name,
			sf.Link
	FROM Passport as p
	LEFT JOIN ScanFile as sf
	ON p.ScanID = p.ScanID
	WHERE p.Series = @Series and p.Number = @Number
END
GO
/****** Object:  StoredProcedure [dbo].[GetPassportById]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPassportById]
@Id INT
AS
BEGIN
	SELECT	p.Id,
			p.Series,
			p.Number,
			p.ScanID,
			scan.Name,
			scan.Link
	FROM Passport as p
	LEFT JOIN ScanFile as scan
	ON p.ScanID = scan.Id
	WHERE p.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByID]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserByID]
	@Id INT
AS
BEGIN
	SELECT u.Id,
			u.Name,
			u.Surname,
			u.Patronic,
			u.Password,
			u.Login,
			u.PassportID,
			p.Series,
			p.Number,
			p.ScanID,
			sf.Name as ScanName,
			sf.Link,
			u.AdditionalScanID,
			asf.Name as additionalScanName,
			asf.Link as additionalLink,
			ur.RoleID
	FROM [User] as u 
	JOIN Passport as p
	ON u.PassportID = p.Id
	LEFT JOIN ScanFile as sf
	ON p.ScanID = sf.Id
	LEFT JOIN ScanFile as asf
	ON u.AdditionalScanID = asf.Id
	JOIN UserRoles as ur
	ON ur.UserID = u.Id
	WHERE u.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByLoginAndPassword]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserByLoginAndPassword]
	@login NVARCHAR(50),
	@Password VARBINARY(MAX)
AS
BEGIN
	SELECT	u.Id,
			u.Name,
			u.Surname,
			u.Patronic,
			u.Password,
			u.Login,
			u.PassportID,
			p.Series,
			p.Number,
			p.ScanID,
			sf.Name as ScanName,
			sf.Link,
			u.AdditionalScanID,
			asf.Name as additionalScanName,
			asf.Link as additionalLink,
			ur.RoleID
	FROM [User] as u 
	JOIN Passport as p
	ON u.PassportID = p.Id
	LEFT JOIN ScanFile as sf
	ON p.ScanID = sf.Id
	LEFT JOIN ScanFile as asf
	ON u.AdditionalScanID = asf.Id
	JOIN UserRoles as ur
	ON ur.UserID = u.Id
	WHERE u.Login=@login and u.Password=@Password
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByPassport]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserByPassport]
	@Id INT
AS
BEGIN
	SELECT	u.Id,
			u.Name,
			u.Surname,
			u.Patronic,
			u.Password,
			u.Login,
			u.PassportID,
			p.Series,
			p.Number,
			p.ScanID,
			sf.Name as ScanName,
			sf.Link,
			u.AdditionalScanID,
			asf.Name as additionalScanName,
			asf.Link as additionalLink,
			ur.RoleID
	FROM [User] as u 
	JOIN Passport as p
	ON u.PassportID = p.Id
	LEFT JOIN ScanFile as sf
	ON p.ScanID = sf.Id
	LEFT JOIN ScanFile as asf
	ON u.AdditionalScanID = asf.Id
	JOIN UserRoles as ur
	ON ur.UserID = u.Id
	WHERE p.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUsers]
AS
BEGIN
	SELECT	u.Id,
			u.Name,
			u.Surname,
			u.Patronic,
			u.Password,
			u.Login,
			u.PassportID,
			p.Series,
			p.Number,
			p.ScanID,
			sf.Name as ScanName,
			sf.Link,
			u.AdditionalScanID,
			asf.Name as additionalScanName,
			asf.Link as additionalLink,
			ur.RoleID
	FROM [User] as u 
	JOIN Passport as p
	ON u.PassportID = p.Id
	LEFT JOIN ScanFile as sf
	ON p.ScanID = sf.Id
	LEFT JOIN ScanFile as asf
	ON u.AdditionalScanID = asf.Id
	JOIN UserRoles as ur
	ON ur.UserID = u.Id
END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePassword]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdatePassword]
	@Id INT,
	@newPassword VARBINARY(MAX)
AS
BEGIN
	UPDATE [User]
	SET Password=@newPassword WHERE Id=@id
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRole]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateRole]
	@Id INT,
	@RoleId INT
AS
BEGIN
	UPDATE UserRoles SET RoleID=@RoleId WHERE UserID=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateStatus]    Script Date: 29.07.2021 12:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateStatus]
	@Id INT,
	@newStatus int
AS
BEGIN
	UPDATE Loan 
	SET StatusID = @newStatus
	WHERE StatusID!=0 and Id=@Id
END
GO
USE [master]
GO
ALTER DATABASE [SCB.Surkova.Credit_approval_system] SET  READ_WRITE 
GO
