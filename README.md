# Инициализация базы данных

## Запустите скрипт который находится [тут](https://github.com/Sasha20101989/PrintManager/blob/master/PrintManager.Persistence/database-script.sql) или скопируйте код ниже

```sql
USE [master]
GO
/****** Object:  Database [PrintingManagement]    Script Date: 12.05.2024 22:38:59 ******/
CREATE DATABASE [PrintingManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PrintingManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PrintingManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PrintingManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PrintingManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PrintingManagement] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PrintingManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PrintingManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PrintingManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PrintingManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PrintingManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PrintingManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [PrintingManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PrintingManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PrintingManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PrintingManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PrintingManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PrintingManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PrintingManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PrintingManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PrintingManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PrintingManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PrintingManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PrintingManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PrintingManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PrintingManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PrintingManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PrintingManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PrintingManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PrintingManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PrintingManagement] SET  MULTI_USER 
GO
ALTER DATABASE [PrintingManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PrintingManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PrintingManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PrintingManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PrintingManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PrintingManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PrintingManagement] SET QUERY_STORE = ON
GO
ALTER DATABASE [PrintingManagement] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PrintingManagement]
GO
/****** Object:  Table [dbo].[tbd_Branches]    Script Date: 12.05.2024 22:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbd_Branches](
	[BranchId] [int] IDENTITY(1,1) NOT NULL,
	[BranchName] [nvarchar](100) NOT NULL,
	[Location] [nvarchar](100) NULL,
 CONSTRAINT [PK__tbd_Bran__A1682FA51A1DD186] PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbd_ConnectionTypes]    Script Date: 12.05.2024 22:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbd_ConnectionTypes](
	[ConnectionTypeId] [int] IDENTITY(1,1) NOT NULL,
	[ConnectionTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tbd_ConnectionTypes] PRIMARY KEY CLUSTERED 
(
	[ConnectionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbd_Employees]    Script Date: 12.05.2024 22:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbd_Employees](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [nvarchar](100) NOT NULL,
	[BranchId] [int] NOT NULL,
 CONSTRAINT [PK__tbd_Empl__7AD04FF13F486E80] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbd_Installations]    Script Date: 12.05.2024 22:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbd_Installations](
	[InstallationId] [int] IDENTITY(1,1) NOT NULL,
	[InstallationName] [nvarchar](50) NOT NULL,
	[BranchId] [int] NOT NULL,
	[PrinterId] [int] NOT NULL,
	[DefaultInstallation] [bit] NOT NULL,
	[PrinterOrder] [int] NULL,
 CONSTRAINT [PK__tbd_Inst__5F69B614F3601FB9] PRIMARY KEY CLUSTERED 
(
	[InstallationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbd_Jobs]    Script Date: 12.05.2024 22:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbd_Jobs](
	[JobId] [int] IDENTITY(1,1) NOT NULL,
	[PrintJobName] [nvarchar](100) NOT NULL,
	[PrinterId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[PagesPrinted] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
 CONSTRAINT [PK__tbd_Jobs__C9F492902B921EEB] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbd_Printers]    Script Date: 12.05.2024 22:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbd_Printers](
	[PrinterID] [int] IDENTITY(1,1) NOT NULL,
	[PrinterName] [nvarchar](100) NOT NULL,
	[ConnectionTypeId] [int] NOT NULL,
	[MACAddress] [nvarchar](17) NULL,
	[DefaultPrinter] [bit] NOT NULL,
 CONSTRAINT [PK__tmp_ms_x__D452AB2148AEC640] PRIMARY KEY CLUSTERED 
(
	[PrinterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbd_Statuses]    Script Date: 12.05.2024 22:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbd_Statuses](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_tbd_Statuses] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbd_Branches] ON 

INSERT [dbo].[tbd_Branches] ([BranchId], [BranchName], [Location]) VALUES (1, N'Тридевятое царство', NULL)
INSERT [dbo].[tbd_Branches] ([BranchId], [BranchName], [Location]) VALUES (2, N'Дремучий Лес', NULL)
INSERT [dbo].[tbd_Branches] ([BranchId], [BranchName], [Location]) VALUES (3, N'Луна', NULL)
SET IDENTITY_INSERT [dbo].[tbd_Branches] OFF
GO
SET IDENTITY_INSERT [dbo].[tbd_ConnectionTypes] ON 

INSERT [dbo].[tbd_ConnectionTypes] ([ConnectionTypeId], [ConnectionTypeName]) VALUES (1, N'Local')
INSERT [dbo].[tbd_ConnectionTypes] ([ConnectionTypeId], [ConnectionTypeName]) VALUES (2, N'Network')
SET IDENTITY_INSERT [dbo].[tbd_ConnectionTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[tbd_Employees] ON 

INSERT [dbo].[tbd_Employees] ([EmployeeId], [EmployeeName], [BranchId]) VALUES (1, N'Царь', 1)
INSERT [dbo].[tbd_Employees] ([EmployeeId], [EmployeeName], [BranchId]) VALUES (2, N'Яга', 2)
INSERT [dbo].[tbd_Employees] ([EmployeeId], [EmployeeName], [BranchId]) VALUES (3, N'Копатыч', 3)
INSERT [dbo].[tbd_Employees] ([EmployeeId], [EmployeeName], [BranchId]) VALUES (4, N'Добрыня', 1)
INSERT [dbo].[tbd_Employees] ([EmployeeId], [EmployeeName], [BranchId]) VALUES (5, N'Кощей', 3)
INSERT [dbo].[tbd_Employees] ([EmployeeId], [EmployeeName], [BranchId]) VALUES (6, N'Лосяш', 3)
SET IDENTITY_INSERT [dbo].[tbd_Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[tbd_Installations] ON 

INSERT [dbo].[tbd_Installations] ([InstallationId], [InstallationName], [BranchId], [PrinterId], [DefaultInstallation], [PrinterOrder]) VALUES (3, N'Конюшни', 1, 3, 0, 2)
INSERT [dbo].[tbd_Installations] ([InstallationId], [InstallationName], [BranchId], [PrinterId], [DefaultInstallation], [PrinterOrder]) VALUES (4, N'Оружейная', 1, 3, 0, 3)
INSERT [dbo].[tbd_Installations] ([InstallationId], [InstallationName], [BranchId], [PrinterId], [DefaultInstallation], [PrinterOrder]) VALUES (5, N'Кратер', 3, 4, 1, 1)
INSERT [dbo].[tbd_Installations] ([InstallationId], [InstallationName], [BranchId], [PrinterId], [DefaultInstallation], [PrinterOrder]) VALUES (6, N'Избушка', 2, 3, 0, 3)
INSERT [dbo].[tbd_Installations] ([InstallationId], [InstallationName], [BranchId], [PrinterId], [DefaultInstallation], [PrinterOrder]) VALUES (7, N'Топи', 2, 2, 1, 2)
SET IDENTITY_INSERT [dbo].[tbd_Installations] OFF
GO
SET IDENTITY_INSERT [dbo].[tbd_Jobs] ON 

INSERT [dbo].[tbd_Jobs] ([JobId], [PrintJobName], [PrinterId], [EmployeeId], [PagesPrinted], [StatusId]) VALUES (2, N'Отчёт', 2, 1, 20, 1)
INSERT [dbo].[tbd_Jobs] ([JobId], [PrintJobName], [PrinterId], [EmployeeId], [PagesPrinted], [StatusId]) VALUES (3, N'Заявление', 2, 2, 40, 1)
INSERT [dbo].[tbd_Jobs] ([JobId], [PrintJobName], [PrinterId], [EmployeeId], [PagesPrinted], [StatusId]) VALUES (4, N'Договор', 3, 3, 10, 2)
INSERT [dbo].[tbd_Jobs] ([JobId], [PrintJobName], [PrinterId], [EmployeeId], [PagesPrinted], [StatusId]) VALUES (5, N'Презентация', 3, 4, 15, 1)
SET IDENTITY_INSERT [dbo].[tbd_Jobs] OFF
GO
SET IDENTITY_INSERT [dbo].[tbd_Printers] ON 

INSERT [dbo].[tbd_Printers] ([PrinterID], [PrinterName], [ConnectionTypeId], [MACAddress], [DefaultPrinter]) VALUES (2, N'Папирус', 1, NULL, 1)
INSERT [dbo].[tbd_Printers] ([PrinterID], [PrinterName], [ConnectionTypeId], [MACAddress], [DefaultPrinter]) VALUES (3, N'Бумага', 1, NULL, 1)
INSERT [dbo].[tbd_Printers] ([PrinterID], [PrinterName], [ConnectionTypeId], [MACAddress], [DefaultPrinter]) VALUES (4, N'Камень', 2, NULL, 1)
SET IDENTITY_INSERT [dbo].[tbd_Printers] OFF
GO
SET IDENTITY_INSERT [dbo].[tbd_Statuses] ON 

INSERT [dbo].[tbd_Statuses] ([StatusId], [StatusName]) VALUES (1, N'Success')
INSERT [dbo].[tbd_Statuses] ([StatusId], [StatusName]) VALUES (2, N'Failure')
SET IDENTITY_INSERT [dbo].[tbd_Statuses] OFF
GO
ALTER TABLE [dbo].[tbd_Employees]  WITH CHECK ADD  CONSTRAINT [FK_BranchID_Emp] FOREIGN KEY([BranchId])
REFERENCES [dbo].[tbd_Branches] ([BranchId])
GO
ALTER TABLE [dbo].[tbd_Employees] CHECK CONSTRAINT [FK_BranchID_Emp]
GO
ALTER TABLE [dbo].[tbd_Installations]  WITH CHECK ADD  CONSTRAINT [FK_BranchID] FOREIGN KEY([BranchId])
REFERENCES [dbo].[tbd_Branches] ([BranchId])
GO
ALTER TABLE [dbo].[tbd_Installations] CHECK CONSTRAINT [FK_BranchID]
GO
ALTER TABLE [dbo].[tbd_Installations]  WITH CHECK ADD  CONSTRAINT [FK_PrinterID] FOREIGN KEY([PrinterId])
REFERENCES [dbo].[tbd_Printers] ([PrinterID])
GO
ALTER TABLE [dbo].[tbd_Installations] CHECK CONSTRAINT [FK_PrinterID]
GO
ALTER TABLE [dbo].[tbd_Jobs]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tbd_Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[tbd_Jobs] CHECK CONSTRAINT [FK_EmployeeId]
GO
ALTER TABLE [dbo].[tbd_Jobs]  WITH CHECK ADD  CONSTRAINT [FK_PrinterId_Session] FOREIGN KEY([PrinterId])
REFERENCES [dbo].[tbd_Printers] ([PrinterID])
GO
ALTER TABLE [dbo].[tbd_Jobs] CHECK CONSTRAINT [FK_PrinterId_Session]
GO
ALTER TABLE [dbo].[tbd_Jobs]  WITH CHECK ADD  CONSTRAINT [FK_tbd_Jobs_tbd_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[tbd_Statuses] ([StatusId])
GO
ALTER TABLE [dbo].[tbd_Jobs] CHECK CONSTRAINT [FK_tbd_Jobs_tbd_Statuses]
GO
ALTER TABLE [dbo].[tbd_Printers]  WITH CHECK ADD  CONSTRAINT [FK_tbd_Printers_tbd_ConnectionTypes] FOREIGN KEY([ConnectionTypeId])
REFERENCES [dbo].[tbd_ConnectionTypes] ([ConnectionTypeId])
GO
ALTER TABLE [dbo].[tbd_Printers] CHECK CONSTRAINT [FK_tbd_Printers_tbd_ConnectionTypes]
GO
ALTER TABLE [dbo].[tbd_Installations]  WITH CHECK ADD  CONSTRAINT [CHK_DefaultInstallation] CHECK  (([DefaultInstallation]=(1) OR [DefaultInstallation]=(0)))
GO
ALTER TABLE [dbo].[tbd_Installations] CHECK CONSTRAINT [CHK_DefaultInstallation]
GO
ALTER TABLE [dbo].[tbd_Printers]  WITH CHECK ADD  CONSTRAINT [CHK_DefaultPrinter] CHECK  (([DefaultPrinter]=(1) OR [DefaultPrinter]=(0)))
GO
ALTER TABLE [dbo].[tbd_Printers] CHECK CONSTRAINT [CHK_DefaultPrinter]
GO
USE [master]
GO
ALTER DATABASE [PrintingManagement] SET  READ_WRITE 
GO

```
