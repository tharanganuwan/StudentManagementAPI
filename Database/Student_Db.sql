USE [master]
GO
/****** Object:  Database [Student_Db]    Script Date: 3/22/2023 9:28:02 AM ******/
CREATE DATABASE [Student_Db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Student_Db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Student_Db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Student_Db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Student_Db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Student_Db] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Student_Db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Student_Db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Student_Db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Student_Db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Student_Db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Student_Db] SET ARITHABORT OFF 
GO
ALTER DATABASE [Student_Db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Student_Db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Student_Db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Student_Db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Student_Db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Student_Db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Student_Db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Student_Db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Student_Db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Student_Db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Student_Db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Student_Db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Student_Db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Student_Db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Student_Db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Student_Db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Student_Db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Student_Db] SET RECOVERY FULL 
GO
ALTER DATABASE [Student_Db] SET  MULTI_USER 
GO
ALTER DATABASE [Student_Db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Student_Db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Student_Db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Student_Db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Student_Db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Student_Db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Student_Db', N'ON'
GO
ALTER DATABASE [Student_Db] SET QUERY_STORE = ON
GO
ALTER DATABASE [Student_Db] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Student_Db]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/22/2023 9:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 3/22/2023 9:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Dob] [datetime2](7) NOT NULL,
	[MotherName] [nvarchar](max) NULL,
	[FatherName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentTrigger]    Script Date: 3/22/2023 9:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentTrigger](
	[father] [varchar](50) NOT NULL,
	[mother] [varchar](50) NOT NULL,
	[student_Id] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Todo]    Script Date: 3/22/2023 9:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Todo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](300) NULL,
	[Created] [datetime2](7) NOT NULL,
	[End] [datetime2](7) NOT NULL,
	[Status] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
 CONSTRAINT [PK_Todo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Todo_StudentId]    Script Date: 3/22/2023 9:28:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_Todo_StudentId] ON [dbo].[Todo]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Todo]  WITH CHECK ADD  CONSTRAINT [FK_Todo_Student_StudentId] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Todo] CHECK CONSTRAINT [FK_Todo_Student_StudentId]
GO
/****** Object:  StoredProcedure [dbo].[CreateStudent]    Script Date: 3/22/2023 9:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateStudent] 
	
	
	@Id int OUTPUT,
	@FirstName nvarchar(50),
	@MiddleName nvarchar(50),
	@LastName nvarchar(50),
	@Dob date,
	@MotherName nvarchar(50),
	@FatherName nvarchar(50)


AS
BEGIN
	DECLARE @InsertedIds TABLE (Id int);
	
	SET NOCOUNT ON;

	INSERT INTO Student(FirstName, MiddleName,LastName,Dob,MotherName,FatherName)
    OUTPUT inserted.Id Into @InsertedIds
    VALUES (@FirstName, @MiddleName,@LastName,@Dob,@MotherName,@FatherName)

    SET @Id = SCOPE_IDENTITY();

    
END
GO
/****** Object:  StoredProcedure [dbo].[CreateTodo]    Script Date: 3/22/2023 9:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateTodo] 
	
	
	@Id int OUTPUT,
	@Title nvarchar(150),
	@Description nvarchar(300),
	@Created datetime2(7),
	@End datetime2(7),
	@Status int,
	@studentId int


AS
BEGIN
	DECLARE @InsertedIds TABLE (Id int);
	
	SET NOCOUNT ON;

	INSERT INTO Todo(Title,[Description],Created,[End],[Status],StudentId)
    OUTPUT inserted.Id Into @InsertedIds
    VALUES (@Title, @Description,@Created,@End,@Status,@studentId)

    SET @Id = SCOPE_IDENTITY();

    
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteStudent]    Script Date: 3/22/2023 9:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeleteStudent] 
    @Id int
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Student
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteTodo]    Script Date: 3/22/2023 9:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create PROCEDURE [dbo].[DeleteTodo] 
    @Id int
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Todo
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllStudents]    Script Date: 3/22/2023 9:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllStudents] 
	
AS
BEGIN
	SET NOCOUNT ON;


	SELECT *
    FROM Student s
    LEFT JOIN Todo t ON s.Id = t.StudentId
END
GO
/****** Object:  StoredProcedure [dbo].[GetStudentFromId]    Script Date: 3/22/2023 9:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetStudentFromId] 
	@Id int
AS
BEGIN

	SET NOCOUNT ON;


	
	SELECT *
    FROM Student s
    LEFT JOIN Todo t ON s.Id = t.StudentId
    WHERE s.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetTodoFromId]    Script Date: 3/22/2023 9:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTodoFromId] 
	@TodoId int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT * FROM Todo WHERE Id=@TodoId;
END


GO
/****** Object:  StoredProcedure [dbo].[GetTodos]    Script Date: 3/22/2023 9:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetTodos] 
    @InputStudentId int = NULL,
    @InputTodoId int = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @InputStudentId IS NOT NULL AND @InputTodoId IS NOT NULL
    BEGIN
        SELECT * FROM Todo WHERE StudentId=@InputStudentId AND Id=@InputTodoId;
    END
    ELSE IF @InputStudentId IS NOT NULL
    BEGIN
        SELECT * FROM Todo WHERE StudentId=@InputStudentId;
    END
    ELSE IF @InputTodoId IS NOT NULL
    BEGIN
        SELECT * FROM Todo WHERE Id=@InputTodoId;
    END
    ELSE
    BEGIN
        SELECT * FROM Todo;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[SearchStudentFromName]    Script Date: 3/22/2023 9:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SearchStudentFromName] 
	@Name varchar(50)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT * FROM Student WHERE FirstName LIKE '%'+@Name+'%' OR MiddleName LIKE '%'+@Name+'%' OR LastName LIKE '%'+@Name+'%';
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateStudent]    Script Date: 3/22/2023 9:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateStudent] 
    @Id int OUTPUT,
	@FirstName nvarchar(50),
	@MiddleName nvarchar(50),
	@LastName nvarchar(50),
	@Dob date,
	@MotherName nvarchar(50),
	@FatherName nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE STUDENT
    SET FirstName = @FirstName,
        MiddleName = @MiddleName,
        LastName = @LastName,
        Dob = @Dob,
        MotherName = @MotherName,
		FatherName = @FatherName
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTodo]    Script Date: 3/22/2023 9:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateTodo] 
    @Id int,
	@Title nvarchar(150),
	@Description nvarchar(300),
	@Created datetime2(7),
	@End datetime2(7),
	@Status int,
	@studentId int
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Todo
    SET Title = @Title,
        [Description] = @Description,
        Created = @Created,
        [End] = @End,
		[Status] = @Status,
		StudentId = @studentId
    WHERE Id = @Id;
END
GO
USE [master]
GO
ALTER DATABASE [Student_Db] SET  READ_WRITE 
GO
