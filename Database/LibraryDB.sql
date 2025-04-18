USE [master]
GO
/****** Object:  Database [LibraryDB]    Script Date: 2025-02-02 3:49:29 PM ******/
CREATE DATABASE [LibraryDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibraryDB', FILENAME = N'C:\Users\antig\Desktop\MSSQL16.MSSQLSERVER\MSSQL\DATA\LibraryDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibraryDB_log', FILENAME = N'C:\Users\antig\Desktop\MSSQL16.MSSQLSERVER\MSSQL\DATA\LibraryDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [LibraryDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LibraryDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LibraryDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LibraryDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LibraryDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LibraryDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LibraryDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [LibraryDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LibraryDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LibraryDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LibraryDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LibraryDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LibraryDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LibraryDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LibraryDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LibraryDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LibraryDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LibraryDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LibraryDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LibraryDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LibraryDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LibraryDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LibraryDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LibraryDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LibraryDB] SET RECOVERY FULL 
GO
ALTER DATABASE [LibraryDB] SET  MULTI_USER 
GO
ALTER DATABASE [LibraryDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LibraryDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LibraryDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LibraryDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LibraryDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LibraryDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'LibraryDB', N'ON'
GO
ALTER DATABASE [LibraryDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [LibraryDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [LibraryDB]
GO
/****** Object:  UserDefinedFunction [dbo].[CalculatePenaltyFee]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Funksion për të llogaritur dënimin për një huazim të vonuar
CREATE FUNCTION [dbo].[CalculatePenaltyFee] (
    @DaysLate INT,
    @DailyRate DECIMAL(10, 2)
)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    RETURN @DaysLate * @DailyRate;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[CalculateTotalLateFees]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Funksion për të llogaritur tarifat totale për huazimet e vonuara
CREATE FUNCTION [dbo].[CalculateTotalLateFees] (
    @ClientID INT
)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    RETURN (
        SELECT SUM(PenaltyFee)
        FROM Loans
        WHERE ClientID = @ClientID AND ActualReturnDate > ReturnDate
    );
END;
GO
/****** Object:  UserDefinedFunction [dbo].[GetAverageLoansPerClient]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetAverageLoansPerClient] (
    @ClientID INT
)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    RETURN (
        SELECT CAST(COUNT(LoanID) AS DECIMAL) / COUNT(DISTINCT ClientID)
        FROM Loans
        WHERE ClientID = @ClientID
    );
END;
GO
/****** Object:  UserDefinedFunction [dbo].[GetLateLoanCount]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Funksion për të llogaritur numrin total të huazimeve të vonuara për një klient
CREATE FUNCTION [dbo].[GetLateLoanCount] (
    @ClientID INT
)
RETURNS INT
AS
BEGIN
    RETURN (
        SELECT COUNT(*)
        FROM Loans
        WHERE ClientID = @ClientID AND ActualReturnDate > ReturnDate
    );
END;
GO
/****** Object:  UserDefinedFunction [dbo].[GetPaymentBalance]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Funksion për të marrë balancin e pagesave për klientët
CREATE FUNCTION [dbo].[GetPaymentBalance] (
    @ClientID INT
)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    RETURN (
        SELECT ISNULL(SUM(Amount), 0) - ISNULL(SUM(PenaltyFee), 0)
        FROM Payments p
        LEFT JOIN Loans l ON p.ClientID = l.ClientID
        WHERE p.ClientID = @ClientID
    );
END;
GO
/****** Object:  UserDefinedFunction [dbo].[HasOutstandingPenalty]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Funksion për të kontrolluar nëse një klient ka dënime të papaguara
CREATE FUNCTION [dbo].[HasOutstandingPenalty] (
    @ClientID INT
)
RETURNS BIT
AS
BEGIN
    RETURN CASE
        WHEN EXISTS (
            SELECT 1
            FROM Loans
            WHERE ClientID = @ClientID AND ActualReturnDate > ReturnDate
              AND LoanID NOT IN (SELECT LoanID FROM Payments)
        )
        THEN 1
        ELSE 0
    END;
END;
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[ClientID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[DateOfBirth] [date] NULL,
	[Email] [nvarchar](255) NULL,
	[Phone] [nvarchar](20) NULL,
	[Address] [nvarchar](500) NULL,
	[RegistrationDate] [date] NULL,
	[MembershipActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[ClientID] [int] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[PaymentDate] [date] NULL,
	[PaymentType] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TotalPaymentsClient]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[TotalPaymentsClient] AS
SELECT
    c.ClientID,
    c.FirstName,
    c.LastName,
    SUM(p.Amount) AS ShumaTotale,
    MONTH(p.PaymentDate) AS Muaji,
    YEAR(p.PaymentDate) AS Viti
FROM
    Clients c
JOIN 
    Payments p ON c.ClientID = p.ClientID
GROUP BY
    c.ClientID, c.FirstName, c.LastName, MONTH(p.PaymentDate), YEAR(p.PaymentDate);
GO
/****** Object:  Table [dbo].[Bibliographic_Materials]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bibliographic_Materials](
	[MaterialID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Author] [nvarchar](255) NULL,
	[CoAuthors] [nvarchar](255) NULL,
	[Publisher] [nvarchar](255) NULL,
	[PublicationDate] [date] NULL,
	[ISBN] [nvarchar](20) NULL,
	[DOI] [nvarchar](50) NULL,
	[MaterialType] [nvarchar](50) NOT NULL,
	[Abstract] [text] NULL,
	[AvailableCopies] [int] NULL,
	[DateAdded] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaterialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Loans]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loans](
	[LoanID] [int] IDENTITY(1,1) NOT NULL,
	[ClientID] [int] NOT NULL,
	[MaterialID] [int] NOT NULL,
	[LoanDate] [date] NOT NULL,
	[ReturnDate] [date] NOT NULL,
	[ActualReturnDate] [date] NULL,
	[PenaltyFee] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[LoanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[OverdueLoans]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	 CREATE VIEW [dbo].[OverdueLoans] AS
SELECT
    l.LoanID,
    c.ClientID,
    c.FirstName,
    c.LastName,
    bm.Title,
    l.ReturnDate,
    l.ActualReturnDate,
    l.PenaltyFee
FROM
    Loans l
JOIN 
    Clients c ON l.ClientID = c.ClientID
JOIN 
    Bibliographic_Materials bm ON l.MaterialID = bm.MaterialID
WHERE 
    l.ActualReturnDate > l.ReturnDate;  -- Huazime të vonuara
GO
/****** Object:  View [dbo].[ActiveClients]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* Vetëm klientët aktivë*/
CREATE VIEW [dbo].[ActiveClients]
AS
SELECT ClientID, FirstName, LastName, DateOfBirth, Email, Phone, Address, RegistrationDate, MembershipActive
FROM     dbo.Clients
WHERE  (MembershipActive = 1)
GO
/****** Object:  View [dbo].[Materialet_Sipas_Llojit]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Materialet_Sipas_Llojit] AS
SELECT
    bm.MaterialType,
    bm.Title,
    COUNT(*) AS Numri
FROM
    Bibliographic_Materials bm
GROUP BY
    bm.MaterialType, bm.Title;  -- Grupi sipas materialeve dhe llojit
GO
/****** Object:  View [dbo].[Klientet_me_Borxhe]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Klientet_me_Borxhe] AS
SELECT
    c.ClientID,
    c.FirstName,
    c.LastName,
    SUM(l.PenaltyFee) AS BorxhiTotale
FROM
    Clients c
JOIN
    Loans l ON c.ClientID = l.ClientID
WHERE
    l.ActualReturnDate > l.ReturnDate  -- Borxhe për huazimet e vonuara
GROUP BY
    c.ClientID, c.FirstName, c.LastName;  -- Klientët me borxhe
GO
/****** Object:  View [dbo].[Materialet_Huazuar]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE VIEW [dbo].[Materialet_Huazuar] AS
SELECT
    bm.MaterialType,
    COUNT(l.LoanID) AS Numri_Huazimeve
FROM
    Bibliographic_Materials bm
JOIN
    Loans l ON bm.MaterialID = l.MaterialID
GROUP BY
    bm.MaterialType;
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bibliographic_Materials] ON 

INSERT [dbo].[Bibliographic_Materials] ([MaterialID], [Title], [Author], [CoAuthors], [Publisher], [PublicationDate], [ISBN], [DOI], [MaterialType], [Abstract], [AvailableCopies], [DateAdded]) VALUES (1, N'Librat e Universit', N'Albert Ajnshtajn', N'Stephen Hawking', N'Botimet Shkencore', CAST(N'2001-03-15' AS Date), N'9781234567890', N'10.1001/journal.ajnshtajn', N'Libër', N'Një eksplorim i thellë i universit.', 10, CAST(N'2025-01-26' AS Date))
INSERT [dbo].[Bibliographic_Materials] ([MaterialID], [Title], [Author], [CoAuthors], [Publisher], [PublicationDate], [ISBN], [DOI], [MaterialType], [Abstract], [AvailableCopies], [DateAdded]) VALUES (2, N'Rruga drejt Marsit', N'Elon Musk', N'Richard Branson', N'KosovaPress', CAST(N'2020-07-20' AS Date), N'9789876543210', N'10.1234/9789876543210', N'Artikull', N'Hapësira dhe teknologjia.', 10, CAST(N'2025-01-26' AS Date))
INSERT [dbo].[Bibliographic_Materials] ([MaterialID], [Title], [Author], [CoAuthors], [Publisher], [PublicationDate], [ISBN], [DOI], [MaterialType], [Abstract], [AvailableCopies], [DateAdded]) VALUES (3, N'Historia e Evropës', N'Veton Surroi', N'Fatos Lubonja', N'Dukagjini', CAST(N'2015-05-18' AS Date), N'9781122334455', N'10.1234/9781122334455', N'Libër', N'Evolucioni politik i Evropës.', 8, CAST(N'2025-01-26' AS Date))
INSERT [dbo].[Bibliographic_Materials] ([MaterialID], [Title], [Author], [CoAuthors], [Publisher], [PublicationDate], [ISBN], [DOI], [MaterialType], [Abstract], [AvailableCopies], [DateAdded]) VALUES (4, N'Programim i Avancuar', N'Mark Zuckerberg', N'Bill Gates', N'TechBooks', CAST(N'2019-09-10' AS Date), N'9780098765432', N'10.1123/code.advanced', N'Libër', N'Mësoni kodimin e avancuar.', 6, CAST(N'2025-01-26' AS Date))
INSERT [dbo].[Bibliographic_Materials] ([MaterialID], [Title], [Author], [CoAuthors], [Publisher], [PublicationDate], [ISBN], [DOI], [MaterialType], [Abstract], [AvailableCopies], [DateAdded]) VALUES (5, N'Fizika për të Gjithë', N'Isaac Newton', N'Galileo Galilei', N'Newtonian Publishing', CAST(N'2005-12-24' AS Date), N'9782345678901', N'10.1001/physics.everyone', N'Libër', N'Një hyrje e lehtë në fizikë.', 12, CAST(N'2025-01-26' AS Date))
INSERT [dbo].[Bibliographic_Materials] ([MaterialID], [Title], [Author], [CoAuthors], [Publisher], [PublicationDate], [ISBN], [DOI], [MaterialType], [Abstract], [AvailableCopies], [DateAdded]) VALUES (6, N'Kimi për Fillestarët', N'Marie Curie', N'Linus Pauling', N'ChemistryWorks', CAST(N'2008-03-12' AS Date), N'9785566778899', N'10.1234/9785566778899', N'Libër', N'Bazat e kimisë për studentët.', 7, CAST(N'2025-01-26' AS Date))
INSERT [dbo].[Bibliographic_Materials] ([MaterialID], [Title], [Author], [CoAuthors], [Publisher], [PublicationDate], [ISBN], [DOI], [MaterialType], [Abstract], [AvailableCopies], [DateAdded]) VALUES (7, N'Njohuri mbi IT', N'Steve Jobs', N'Wozniak', N'AppleBooks', CAST(N'2010-06-22' AS Date), N'9786677889900', N'10.1234/9786677889900', N'Libër', N'Bazat e teknologjisë informatike.', 15, CAST(N'2025-01-26' AS Date))
INSERT [dbo].[Bibliographic_Materials] ([MaterialID], [Title], [Author], [CoAuthors], [Publisher], [PublicationDate], [ISBN], [DOI], [MaterialType], [Abstract], [AvailableCopies], [DateAdded]) VALUES (8, N'Ekonomia e Tregut', N'Adam Smith', N'John Maynard Keynes', N'EconomyPress', CAST(N'1995-11-11' AS Date), N'9789988776655', N'10.1234/9789988776655', N'Libër', N'Zhvillimi ekonomik global.', 4, CAST(N'2025-01-26' AS Date))
INSERT [dbo].[Bibliographic_Materials] ([MaterialID], [Title], [Author], [CoAuthors], [Publisher], [PublicationDate], [ISBN], [DOI], [MaterialType], [Abstract], [AvailableCopies], [DateAdded]) VALUES (9, N'Revista e Shkencës dhe Teknologjisë', N'Diana Kelmendi', N'Arbër Quni', N'Shkenca Press', CAST(N'2021-06-10' AS Date), N'9787766554433', N'10.5555/science.technology', N'Revistë', N'Një përmbledhje e zhvillimeve më të fundit në shkencë.', 12, CAST(N'2025-01-26' AS Date))
INSERT [dbo].[Bibliographic_Materials] ([MaterialID], [Title], [Author], [CoAuthors], [Publisher], [PublicationDate], [ISBN], [DOI], [MaterialType], [Abstract], [AvailableCopies], [DateAdded]) VALUES (10, N'Planifikimi Urban', N'Jane Jacobs', N'David Harvey', N'UrbanPublish', CAST(N'2000-04-30' AS Date), N'9784433221100', N'10.5555/urban.planning', N'Artikull', N'Dizajni i qyteteve moderne.', 2, CAST(N'2025-01-26' AS Date))
INSERT [dbo].[Bibliographic_Materials] ([MaterialID], [Title], [Author], [CoAuthors], [Publisher], [PublicationDate], [ISBN], [DOI], [MaterialType], [Abstract], [AvailableCopies], [DateAdded]) VALUES (11, N'Misteret e Kohës', N'Kip Thorne', N'Roger Penrose', N'Dukagjini', CAST(N'1999-08-13' AS Date), N'9789876543210', N'10.1016/journal.kohathorne', N'Libër', NULL, 5, CAST(N'2025-02-02' AS Date))
SET IDENTITY_INSERT [dbo].[Bibliographic_Materials] OFF
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([ClientID], [FirstName], [LastName], [DateOfBirth], [Email], [Phone], [Address], [RegistrationDate], [MembershipActive]) VALUES (1, N'Ardit', N'Muriqi', CAST(N'1985-06-15' AS Date), N'arditmuriqi@email.com', N'044123456', N'Rr. Dajti, Prishtinë', CAST(N'2023-03-15' AS Date), 1)
INSERT [dbo].[Clients] ([ClientID], [FirstName], [LastName], [DateOfBirth], [Email], [Phone], [Address], [RegistrationDate], [MembershipActive]) VALUES (2, N'Mirela', N'Jaka', CAST(N'1990-04-10' AS Date), N'mirelajaka@email.com', N'067654321', N'Rr. Dajti, Prishtinë', CAST(N'2022-07-10' AS Date), 0)
INSERT [dbo].[Clients] ([ClientID], [FirstName], [LastName], [DateOfBirth], [Email], [Phone], [Address], [RegistrationDate], [MembershipActive]) VALUES (3, N'Luan', N'Bajrami', CAST(N'1987-11-22' AS Date), N'luanbajrami@email.com', N'069876543', N'Rr. Dajti, Prishtinë', CAST(N'2022-07-10' AS Date), 0)
INSERT [dbo].[Clients] ([ClientID], [FirstName], [LastName], [DateOfBirth], [Email], [Phone], [Address], [RegistrationDate], [MembershipActive]) VALUES (4, N'Drita', N'Fshazi', CAST(N'1982-01-09' AS Date), N'dritafshazi@email.com', N'065432109', N'Rr. Tiranës, Prishtinë', CAST(N'2024-09-25' AS Date), 0)
INSERT [dbo].[Clients] ([ClientID], [FirstName], [LastName], [DateOfBirth], [Email], [Phone], [Address], [RegistrationDate], [MembershipActive]) VALUES (5, N'Erion', N'Meksi', CAST(N'1990-05-17' AS Date), N'erionmeksi@email.com', N'0698765432', N'Rr. Shkumbinit, Durrës', CAST(N'2023-09-25' AS Date), 0)
INSERT [dbo].[Clients] ([ClientID], [FirstName], [LastName], [DateOfBirth], [Email], [Phone], [Address], [RegistrationDate], [MembershipActive]) VALUES (6, N'Vera', N'Saliu', CAST(N'1978-02-25' AS Date), N'verasaliu@email.com', N'0671234567', N'Rr. Kombinatit, Prishtine', CAST(N'2025-01-31' AS Date), 1)
INSERT [dbo].[Clients] ([ClientID], [FirstName], [LastName], [DateOfBirth], [Email], [Phone], [Address], [RegistrationDate], [MembershipActive]) VALUES (7, N'Alma', N'Krasniqi', CAST(N'1980-07-14' AS Date), N'almakrasniqi@email.com', N'0654321098', N'Rr. Shkodra, Shkodër', CAST(N'2024-01-01' AS Date), 1)
INSERT [dbo].[Clients] ([ClientID], [FirstName], [LastName], [DateOfBirth], [Email], [Phone], [Address], [RegistrationDate], [MembershipActive]) VALUES (8, N'Alban', N'Basha', CAST(N'1995-03-23' AS Date), N'albanbasha@email.com', N'067854321', N'Rr. Vlorës, Vlorë', CAST(N'2024-01-01' AS Date), 1)
INSERT [dbo].[Clients] ([ClientID], [FirstName], [LastName], [DateOfBirth], [Email], [Phone], [Address], [RegistrationDate], [MembershipActive]) VALUES (9, N'Lina', N'Gjoni', CAST(N'1986-12-10' AS Date), N'linagjoni@email.com', N'0691234321', N'Rr. Bogas, Elbasan', CAST(N'2024-01-01' AS Date), 1)
INSERT [dbo].[Clients] ([ClientID], [FirstName], [LastName], [DateOfBirth], [Email], [Phone], [Address], [RegistrationDate], [MembershipActive]) VALUES (10, N'Blerina', N'Marku', CAST(N'1992-06-20' AS Date), N'blerinamarku@email.com', N'0676543321', N'Rr. Fushës, Durrës', CAST(N'2024-09-25' AS Date), 0)
INSERT [dbo].[Clients] ([ClientID], [FirstName], [LastName], [DateOfBirth], [Email], [Phone], [Address], [RegistrationDate], [MembershipActive]) VALUES (11, N'Laura', N'Sejdiu', CAST(N'1999-07-15' AS Date), N'sejdiulaura@live.com', N'045368962', N'Rr.Sami Gashi, Mitrovice', CAST(N'2025-02-02' AS Date), 0)
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Loans] ON 

INSERT [dbo].[Loans] ([LoanID], [ClientID], [MaterialID], [LoanDate], [ReturnDate], [ActualReturnDate], [PenaltyFee]) VALUES (1, 1, 1, CAST(N'2024-12-01' AS Date), CAST(N'2025-01-15' AS Date), CAST(N'2025-12-16' AS Date), CAST(5.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([LoanID], [ClientID], [MaterialID], [LoanDate], [ReturnDate], [ActualReturnDate], [PenaltyFee]) VALUES (2, 1, 1, CAST(N'2024-12-05' AS Date), CAST(N'2025-12-20' AS Date), CAST(N'2025-12-25' AS Date), CAST(5.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([LoanID], [ClientID], [MaterialID], [LoanDate], [ReturnDate], [ActualReturnDate], [PenaltyFee]) VALUES (3, 1, 7, CAST(N'2024-12-10' AS Date), CAST(N'2024-12-24' AS Date), CAST(N'2025-12-26' AS Date), CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([LoanID], [ClientID], [MaterialID], [LoanDate], [ReturnDate], [ActualReturnDate], [PenaltyFee]) VALUES (4, 1, 7, CAST(N'2024-12-03' AS Date), CAST(N'2025-12-18' AS Date), CAST(N'2025-12-19' AS Date), CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([LoanID], [ClientID], [MaterialID], [LoanDate], [ReturnDate], [ActualReturnDate], [PenaltyFee]) VALUES (5, 6, 7, CAST(N'2024-12-01' AS Date), CAST(N'2025-01-15' AS Date), CAST(N'2025-12-18' AS Date), CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([LoanID], [ClientID], [MaterialID], [LoanDate], [ReturnDate], [ActualReturnDate], [PenaltyFee]) VALUES (6, 6, 9, CAST(N'2024-12-07' AS Date), CAST(N'2024-12-21' AS Date), CAST(N'2024-12-23' AS Date), CAST(7.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([LoanID], [ClientID], [MaterialID], [LoanDate], [ReturnDate], [ActualReturnDate], [PenaltyFee]) VALUES (7, 6, 10, CAST(N'2024-12-04' AS Date), CAST(N'2024-12-19' AS Date), CAST(N'2024-12-22' AS Date), CAST(6.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([LoanID], [ClientID], [MaterialID], [LoanDate], [ReturnDate], [ActualReturnDate], [PenaltyFee]) VALUES (8, 6, 10, CAST(N'2024-12-02' AS Date), CAST(N'2025-12-16' AS Date), CAST(N'2025-12-18' AS Date), CAST(6.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([LoanID], [ClientID], [MaterialID], [LoanDate], [ReturnDate], [ActualReturnDate], [PenaltyFee]) VALUES (9, 7, 10, CAST(N'2024-12-06' AS Date), CAST(N'2025-12-20' AS Date), CAST(N'2025-12-22' AS Date), CAST(7.00 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([LoanID], [ClientID], [MaterialID], [LoanDate], [ReturnDate], [ActualReturnDate], [PenaltyFee]) VALUES (10, 8, 10, CAST(N'2024-12-09' AS Date), CAST(N'2025-01-23' AS Date), CAST(N'2025-12-25' AS Date), CAST(7.50 AS Decimal(10, 2)))
INSERT [dbo].[Loans] ([LoanID], [ClientID], [MaterialID], [LoanDate], [ReturnDate], [ActualReturnDate], [PenaltyFee]) VALUES (11, 6, 7, CAST(N'2025-01-02' AS Date), CAST(N'2025-01-15' AS Date), CAST(N'2025-01-17' AS Date), CAST(1.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Loans] OFF
GO
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([PaymentID], [ClientID], [Amount], [PaymentDate], [PaymentType]) VALUES (1, 1, CAST(50.00 AS Decimal(10, 2)), CAST(N'2024-01-15' AS Date), N'Pagesa Mujore')
INSERT [dbo].[Payments] ([PaymentID], [ClientID], [Amount], [PaymentDate], [PaymentType]) VALUES (2, 2, CAST(40.00 AS Decimal(10, 2)), CAST(N'2024-01-12' AS Date), N'Tarifa Vonese')
INSERT [dbo].[Payments] ([PaymentID], [ClientID], [Amount], [PaymentDate], [PaymentType]) VALUES (3, 3, CAST(30.00 AS Decimal(10, 2)), CAST(N'2024-02-05' AS Date), N'Pagesa Mujore')
INSERT [dbo].[Payments] ([PaymentID], [ClientID], [Amount], [PaymentDate], [PaymentType]) VALUES (4, 4, CAST(35.00 AS Decimal(10, 2)), CAST(N'2024-02-10' AS Date), N'Tarifa Vonese')
INSERT [dbo].[Payments] ([PaymentID], [ClientID], [Amount], [PaymentDate], [PaymentType]) VALUES (5, 5, CAST(45.00 AS Decimal(10, 2)), CAST(N'2025-01-09' AS Date), N'Pagesa Mujore')
INSERT [dbo].[Payments] ([PaymentID], [ClientID], [Amount], [PaymentDate], [PaymentType]) VALUES (6, 6, CAST(55.00 AS Decimal(10, 2)), CAST(N'2025-01-07' AS Date), N'Tarifa Vonese')
INSERT [dbo].[Payments] ([PaymentID], [ClientID], [Amount], [PaymentDate], [PaymentType]) VALUES (7, 7, CAST(60.00 AS Decimal(10, 2)), CAST(N'2024-03-11' AS Date), N'Pagesa Mujore')
INSERT [dbo].[Payments] ([PaymentID], [ClientID], [Amount], [PaymentDate], [PaymentType]) VALUES (8, 8, CAST(20.00 AS Decimal(10, 2)), CAST(N'2024-03-13' AS Date), N'Pagesa Mujore')
INSERT [dbo].[Payments] ([PaymentID], [ClientID], [Amount], [PaymentDate], [PaymentType]) VALUES (9, 9, CAST(70.00 AS Decimal(10, 2)), CAST(N'2024-03-14' AS Date), N'Tarifa Vonese')
INSERT [dbo].[Payments] ([PaymentID], [ClientID], [Amount], [PaymentDate], [PaymentType]) VALUES (10, 10, CAST(80.00 AS Decimal(10, 2)), CAST(N'2024-01-12' AS Date), N'Pagesa Mujore')
INSERT [dbo].[Payments] ([PaymentID], [ClientID], [Amount], [PaymentDate], [PaymentType]) VALUES (11, 5, CAST(30.00 AS Decimal(10, 2)), CAST(N'2025-01-07' AS Date), N'Pagesa Mujore')
SET IDENTITY_INSERT [dbo].[Payments] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (1, N'erza.peci', N'password1', N'admin')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (2, N'erona.maxhuni', N'password2', N'admin')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (3, N'antigona.rrahmani', N'password3', N'admin')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (4, N'agnesa.baliu', N'password4', N'admin')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (5, N'arber.hoti', N'password5', N'user')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (6, N'valmira.shala', N'password6', N'user')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (7, N'blerim.krasniqi', N'password7', N'user')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (8, N'leonard.morina', N'password8', N'user')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (9, N'kaltrina.zeqiri', N'password9', N'user')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (10, N'artan.berisha', N'password10', N'user')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (13, N'laura.sejdeiu', N'password11', N'user')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (14, N'tina.salihu', N'password12', N'user')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Clients__A9D105341854DBA7]    Script Date: 2025-02-02 3:49:30 PM ******/
ALTER TABLE [dbo].[Clients] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__536C85E4AA6D9D84]    Script Date: 2025-02-02 3:49:30 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bibliographic_Materials] ADD  DEFAULT ((0)) FOR [AvailableCopies]
GO
ALTER TABLE [dbo].[Bibliographic_Materials] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((0)) FOR [MembershipActive]
GO
ALTER TABLE [dbo].[Loans] ADD  DEFAULT ((0)) FOR [PenaltyFee]
GO
ALTER TABLE [dbo].[Payments] ADD  DEFAULT (getdate()) FOR [PaymentDate]
GO
ALTER TABLE [dbo].[Loans]  WITH CHECK ADD FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[Loans]  WITH CHECK ADD FOREIGN KEY([MaterialID])
REFERENCES [dbo].[Bibliographic_Materials] ([MaterialID])
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
/****** Object:  StoredProcedure [dbo].[CalculateLateFee]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedura 2: Llogaritja e pagesës për vonesën
CREATE PROCEDURE [dbo].[CalculateLateFee]
    @LoanID INT,
    @DailyPenalty DECIMAL(10, 2),
    @LateFee DECIMAL(10, 2) OUTPUT
AS
BEGIN
    BEGIN TRY
        DECLARE @ReturnDate DATE, @ActualReturnDate DATE, @DaysLate INT;

        SELECT @ReturnDate = ReturnDate, @ActualReturnDate = ActualReturnDate
        FROM Loans
        WHERE LoanID = @LoanID;

        IF @ReturnDate IS NULL OR @ActualReturnDate IS NULL
        BEGIN
            THROW 50002, 'Huazimi nuk ekziston ose datat janë të pavlefshme.', 1;
        END;

        IF @ActualReturnDate > @ReturnDate
        BEGIN
            SET @DaysLate = DATEDIFF(DAY, @ReturnDate, @ActualReturnDate);
            SET @LateFee = @DaysLate * @DailyPenalty;
        END
        ELSE
        BEGIN
            SET @LateFee = 0;
        END;
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        SET @LateFee = NULL;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[CheckMaterialAvailability]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedura 5: Verifikimi i disponueshmërisë së materialit
CREATE PROCEDURE [dbo].[CheckMaterialAvailability]
    @MaterialID INT,
    @IsAvailable BIT OUTPUT
AS
BEGIN
    BEGIN TRY
        DECLARE @AvailableCopies INT;

        SELECT @AvailableCopies = AvailableCopies
        FROM Bibliographic_Materials
        WHERE MaterialID = @MaterialID;

        IF @AvailableCopies IS NULL
        BEGIN
            SET @IsAvailable = NULL;
            RETURN;
        END;

        SET @IsAvailable = CASE WHEN @AvailableCopies > 0 THEN 1 ELSE 0 END;
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        SET @IsAvailable = NULL;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetMaterialByISBN]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--3 Procedurë për të marrë materialin sipas ISBN.
CREATE PROCEDURE [dbo].[GetMaterialByISBN]
    @ISBN NVARCHAR(20)
AS
BEGIN
    SELECT MaterialID, Title, Author, Publisher, AvailableCopies
    FROM Bibliographic_Materials
    WHERE ISBN = @ISBN;
END;
GO
/****** Object:  StoredProcedure [dbo].[RegisterLoan]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedura 4: Regjistrimi i një huazimi
CREATE PROCEDURE [dbo].[RegisterLoan]
    @ClientID INT,
    @MaterialID INT,
    @LoanDate DATE,
    @ReturnDate DATE
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @AvailableCopies INT;

        SELECT @AvailableCopies = AvailableCopies
        FROM Bibliographic_Materials
        WHERE MaterialID = @MaterialID;

        IF @AvailableCopies IS NULL OR @AvailableCopies <= 0
        BEGIN
            ROLLBACK TRANSACTION;
            THROW 50003, 'Materiali nuk është i disponueshëm.', 1;
        END;

        INSERT INTO Loans (ClientID, MaterialID, LoanDate, ReturnDate)
        VALUES (@ClientID, @MaterialID, @LoanDate, @ReturnDate);

        UPDATE Bibliographic_Materials
        SET AvailableCopies = AvailableCopies - 1
        WHERE MaterialID = @MaterialID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT ERROR_MESSAGE();
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[RegisterNewClient]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Procedura 1: Regjistrimi i një klienti të ri
CREATE PROCEDURE [dbo].[RegisterNewClient]
    @FirstName NVARCHAR(255),
    @LastName NVARCHAR(255),
    @DateOfBirth DATE,
    @Email NVARCHAR(255),
    @Phone NVARCHAR(20),
    @Address NVARCHAR(500)
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM Clients WHERE Email = @Email OR Phone = @Phone)
        BEGIN
            THROW 50001, 'Klienti tashmë ekziston.', 1;
        END;

        INSERT INTO Clients (FirstName, LastName, DateOfBirth, Email, Phone, Address)
        VALUES (@FirstName, @LastName, @DateOfBirth, @Email, @Phone, @Address);
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateClientStatus]    Script Date: 2025-02-02 3:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedura 6: Përditësimi i statusit të një klienti
CREATE PROCEDURE [dbo].[UpdateClientStatus]
    @ClientID INT,
    @MembershipActive BIT
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Clients WHERE ClientID = @ClientID)
        BEGIN
            THROW 50004, 'Klienti nuk ekziston.', 1;
        END;

        UPDATE Clients
        SET MembershipActive = @MembershipActive
        WHERE ClientID = @ClientID;
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
    END CATCH;
END;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Clients"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 267
            End
            DisplayFlags = 280
            TopColumn = 5
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ActiveClients'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ActiveClients'
GO
USE [master]
GO
ALTER DATABASE [LibraryDB] SET  READ_WRITE 
GO
