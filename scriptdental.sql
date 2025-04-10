USE [master]
GO
/****** Object:  Database [DentalClinic]    Script Date: 09-04-2025 16:12:43 ******/
CREATE DATABASE [DentalClinic]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DentalClinic', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DentalClinic.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DentalClinic_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DentalClinic_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DentalClinic] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DentalClinic].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DentalClinic] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DentalClinic] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DentalClinic] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DentalClinic] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DentalClinic] SET ARITHABORT OFF 
GO
ALTER DATABASE [DentalClinic] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DentalClinic] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DentalClinic] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DentalClinic] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DentalClinic] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DentalClinic] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DentalClinic] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DentalClinic] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DentalClinic] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DentalClinic] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DentalClinic] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DentalClinic] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DentalClinic] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DentalClinic] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DentalClinic] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DentalClinic] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DentalClinic] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DentalClinic] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DentalClinic] SET  MULTI_USER 
GO
ALTER DATABASE [DentalClinic] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DentalClinic] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DentalClinic] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DentalClinic] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DentalClinic] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DentalClinic] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DentalClinic', N'ON'
GO
ALTER DATABASE [DentalClinic] SET QUERY_STORE = ON
GO
ALTER DATABASE [DentalClinic] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DentalClinic]
GO
/****** Object:  User [dent]    Script Date: 09-04-2025 16:12:43 ******/
CREATE USER [dent] FOR LOGIN [dent] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [dent]
GO
ALTER ROLE [db_datareader] ADD MEMBER [dent]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [dent]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 09-04-2025 16:12:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[PhoneNumber] [nvarchar](15) NOT NULL,
	[ReasonForAppointment] [nvarchar](255) NOT NULL,
	[PreferredDateTime] [datetime] NOT NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DentalNews]    Script Date: 09-04-2025 16:12:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DentalNews](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](300) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_DentalNews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dentist]    Script Date: 09-04-2025 16:12:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dentist](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[institute] [nvarchar](50) NOT NULL,
	[Contact] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[BloodGroup] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[isVerfied] [bit] NOT NULL,
	[isAvailable] [bit] NOT NULL,
 CONSTRAINT [PK__Dentist__3214EC07B3A4C7D2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FeedBack]    Script Date: 09-04-2025 16:12:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedBack](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PatientName] [nvarchar](255) NOT NULL,
	[FeedbackText] [nvarchar](1000) NOT NULL,
	[Rating] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_FeedBack] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientRegister]    Script Date: 09-04-2025 16:12:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientRegister](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PatientName] [nvarchar](255) NOT NULL,
	[Username] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[DOB] [date] NOT NULL,
	[Contact] [nvarchar](20) NOT NULL,
	[BloodGroup] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK__PatientR__3214EC07DC3FAB4C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiteStat]    Script Date: 09-04-2025 16:12:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteStat](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TotalVisitors] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[subscription]    Script Date: 09-04-2025 16:12:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subscription](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[PhoneNumber] [varchar](10) NOT NULL,
 CONSTRAINT [PK_subscription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Appointment] ON 

INSERT [dbo].[Appointment] ([Id], [PatientName], [Email], [PhoneNumber], [ReasonForAppointment], [PreferredDateTime], [CreatedAt]) VALUES (1, N'aravind', N'aravind.mano1991@gmail.com', N'9566270765', N'CLEAR ALIGNERS', CAST(N'2025-03-14T10:34:00.000' AS DateTime), CAST(N'2025-03-05T23:32:04.080' AS DateTime))
INSERT [dbo].[Appointment] ([Id], [PatientName], [Email], [PhoneNumber], [ReasonForAppointment], [PreferredDateTime], [CreatedAt]) VALUES (2, N'aravind', N'aravind.mano1991@gmail.com', N'9566270765', N'ROOT CANAL THERAPY', CAST(N'2025-03-14T10:38:00.000' AS DateTime), CAST(N'2025-03-05T23:33:01.823' AS DateTime))
INSERT [dbo].[Appointment] ([Id], [PatientName], [Email], [PhoneNumber], [ReasonForAppointment], [PreferredDateTime], [CreatedAt]) VALUES (1002, N'aravind', N'aravind.mano1991@gmail.com', N'09566270465', N'PEDIATRIC DENTISTRY', CAST(N'2025-03-14T18:40:00.000' AS DateTime), CAST(N'2025-03-09T18:14:21.607' AS DateTime))
INSERT [dbo].[Appointment] ([Id], [PatientName], [Email], [PhoneNumber], [ReasonForAppointment], [PreferredDateTime], [CreatedAt]) VALUES (1003, N'ajay', N'aravind.mano1991@gmail.com', N'9626327656', N'LASER TREATMENT', CAST(N'2025-03-09T18:19:00.000' AS DateTime), CAST(N'2025-03-09T18:19:06.587' AS DateTime))
INSERT [dbo].[Appointment] ([Id], [PatientName], [Email], [PhoneNumber], [ReasonForAppointment], [PreferredDateTime], [CreatedAt]) VALUES (1004, N'hema', N'aravind.mano1991@gmail.com', N'6269054380', N'DIGITAL SMILE DESIGNING', CAST(N'2025-03-09T18:20:00.000' AS DateTime), CAST(N'2025-03-09T18:20:14.520' AS DateTime))
SET IDENTITY_INSERT [dbo].[Appointment] OFF
GO
SET IDENTITY_INSERT [dbo].[DentalNews] ON 

INSERT [dbo].[DentalNews] ([Id], [Description], [IsActive]) VALUES (1, N'Your best life begins with a smile.', 1)
INSERT [dbo].[DentalNews] ([Id], [Description], [IsActive]) VALUES (2, N'A brighter smile awaits you.', 1)
INSERT [dbo].[DentalNews] ([Id], [Description], [IsActive]) VALUES (3, N'A dream smile is a reality.', 1)
INSERT [dbo].[DentalNews] ([Id], [Description], [IsActive]) VALUES (4, N'A gentle touch for sensitive smiles.', 1)
INSERT [dbo].[DentalNews] ([Id], [Description], [IsActive]) VALUES (5, N'A great smile to start the day!', 1)
INSERT [dbo].[DentalNews] ([Id], [Description], [IsActive]) VALUES (6, N'A perfect smile, guaranteed.', 1)
SET IDENTITY_INSERT [dbo].[DentalNews] OFF
GO
SET IDENTITY_INSERT [dbo].[Dentist] ON 

INSERT [dbo].[Dentist] ([Id], [Username], [Email], [institute], [Contact], [Address], [Name], [BloodGroup], [Password], [isVerfied], [isAvailable]) VALUES (1, N'test', N'test@email.com', N'Harvard Dental School', N'123-456-7890', N'123 Main St, NY', N'Dr. Jones', N'O+', N'Password*1', 1, 1)
SET IDENTITY_INSERT [dbo].[Dentist] OFF
GO
SET IDENTITY_INSERT [dbo].[FeedBack] ON 

INSERT [dbo].[FeedBack] ([Id], [PatientName], [FeedbackText], [Rating], [CreatedAt]) VALUES (1, N'aravind', N'If you''re looking for a dentist who is comfortable with close personal interaction, easy to talk to, trustworthy, detail-oriented, and passionate about providing care to those in need, you''ve come to the right place! Dentists are people too, and they care about the quality of their care.', 5, CAST(N'2025-03-21T22:06:22.903' AS DateTime))
INSERT [dbo].[FeedBack] ([Id], [PatientName], [FeedbackText], [Rating], [CreatedAt]) VALUES (9, N'Hema', N'I appreciate the clean and organized environment of this dental office. It makes me feel safe and at ease."', 5, CAST(N'2025-03-21T23:32:26.890' AS DateTime))
INSERT [dbo].[FeedBack] ([Id], [PatientName], [FeedbackText], [Rating], [CreatedAt]) VALUES (10, N'jayamoorthi', N'I am very happy with the work done on my teeth. The dentist and staff were very professional and kind. I felt very comfortable during my visit', 4, CAST(N'2025-03-21T23:37:21.260' AS DateTime))
INSERT [dbo].[FeedBack] ([Id], [PatientName], [FeedbackText], [Rating], [CreatedAt]) VALUES (11, N'Deepika', N'The dentist explained everything clearly and answered all my questions. I felt like I was in good hands', 3, CAST(N'2025-03-21T23:37:56.870' AS DateTime))
INSERT [dbo].[FeedBack] ([Id], [PatientName], [FeedbackText], [Rating], [CreatedAt]) VALUES (12, N'asds', N'asdsa', 5, CAST(N'2025-04-09T13:06:50.327' AS DateTime))
SET IDENTITY_INSERT [dbo].[FeedBack] OFF
GO
SET IDENTITY_INSERT [dbo].[PatientRegister] ON 

INSERT [dbo].[PatientRegister] ([Id], [PatientName], [Username], [Password], [Email], [Address], [DOB], [Contact], [BloodGroup]) VALUES (1, N'ARAVIND M', N'Aravind', N'aravind1991', N'aravind.mano1991@gmail.com', N'no.48,9th cross krishna nagar,Pondicherry', CAST(N'1991-08-03' AS Date), N'9566270765', N'O+ve')
SET IDENTITY_INSERT [dbo].[PatientRegister] OFF
GO
SET IDENTITY_INSERT [dbo].[SiteStat] ON 

INSERT [dbo].[SiteStat] ([Id], [TotalVisitors], [CreatedAt]) VALUES (1, 1, CAST(N'2025-04-09T10:30:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[SiteStat] OFF
GO
ALTER TABLE [dbo].[Appointment] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Dentist] ADD  CONSTRAINT [DF__Dentist__isVerfi__1CF15040]  DEFAULT ((0)) FOR [isVerfied]
GO
ALTER TABLE [dbo].[Dentist] ADD  CONSTRAINT [DF__Dentist__isAvail__1DE57479]  DEFAULT ((0)) FOR [isAvailable]
GO
ALTER TABLE [dbo].[FeedBack] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[FeedBack]  WITH CHECK ADD CHECK  (([Rating]>=(1) AND [Rating]<=(5)))
GO
USE [master]
GO
ALTER DATABASE [DentalClinic] SET  READ_WRITE 
GO
