USE [master]
GO
/****** Object:  Database [dbDoKhi]    Script Date: 5/18/2019 16:22:24 ******/
CREATE DATABASE [dbDoKhi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbDoKhi', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\dbDoKhi.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'dbDoKhi_log', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\dbDoKhi_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [dbDoKhi] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbDoKhi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbDoKhi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbDoKhi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbDoKhi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbDoKhi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbDoKhi] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbDoKhi] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbDoKhi] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [dbDoKhi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbDoKhi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbDoKhi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbDoKhi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbDoKhi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbDoKhi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbDoKhi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbDoKhi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbDoKhi] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbDoKhi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbDoKhi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbDoKhi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbDoKhi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbDoKhi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbDoKhi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbDoKhi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbDoKhi] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbDoKhi] SET  MULTI_USER 
GO
ALTER DATABASE [dbDoKhi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbDoKhi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbDoKhi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbDoKhi] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [dbDoKhi]
GO
/****** Object:  StoredProcedure [dbo].[SP_InitData]    Script Date: 5/18/2019 16:22:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_InitData]
AS BEGIN
DELETE dbo.DiemDo
DELETE dbo.DaThongSo

DECLARE @opc NVARCHAR(50) = 'Server_ThanMaoKhe.'

DECLARE @count INT = 7
DECLARE @type INT = 1
DECLARE @name NVARCHAR(50) = 'CH4'

SET @count = 7; SET @type = 1; SET @name = 'CH4';
WHILE @count > 0 BEGIN INSERT dbo.DiemDo ( Name, OPC_Address, Type, Active ) VALUES ( @name+'_' + CAST(@count AS NVARCHAR(50)), @opc + CAST((@count/3) AS NVARCHAR(50)) + @name +'_'+ CAST(@count AS NVARCHAR(50)) + '.Giatri', @type, 1 ) SET @count = @count - 1; end

SET @count = 7; SET @type = 2; SET @name = 'CO';
WHILE @count > 0 BEGIN INSERT dbo.DiemDo ( Name, OPC_Address, Type , Active) VALUES ( @name +'_'+ CAST(@count AS NVARCHAR(50)), @opc + @name +'_'+ CAST(@count AS NVARCHAR(50)) + '.Giatri', @type, 1 ) SET @count = @count - 1; end

SET @count = 2; SET @type = 3; SET @name = 'O2';
WHILE @count > 0 BEGIN INSERT dbo.DiemDo ( Name, OPC_Address, Type, Active ) VALUES ( @name+'_' + CAST(@count AS NVARCHAR(50)), @opc + @name +'_'+ CAST(@count AS NVARCHAR(50)) + '.Giatri', @type, 1 ) SET @count = @count - 1; end

SET @count = 2; SET @type = 4; SET @name = 'H2';
WHILE @count > 0 BEGIN INSERT dbo.DiemDo ( Name, OPC_Address, Type, Active ) VALUES ( @name+'_' + CAST(@count AS NVARCHAR(50)), @opc + @name +'_'+ CAST(@count AS NVARCHAR(50)) + '.Giatri', @type, 1 ) SET @count = @count - 1; end

SET @count = 5; SET @type = 5; SET @name = 'NhietDo';
WHILE @count > 0 BEGIN INSERT dbo.DiemDo ( Name, OPC_Address, Type, Active ) VALUES ( @name+'_' + CAST(@count AS NVARCHAR(50)), @opc + @name +'_'+ CAST(@count AS NVARCHAR(50)) + '.Giatri', @type, 1 ) SET @count = @count - 1; end

SET @count = 5; SET @type = 6; SET @name = 'Gio';
WHILE @count > 0 BEGIN INSERT dbo.DiemDo ( Name, OPC_Address, Type, Active ) VALUES ( @name+'_' + CAST(@count AS NVARCHAR(50)), @opc + @name +'_'+ CAST(@count AS NVARCHAR(50)) + '.Giatri', @type, 1 ) SET @count = @count - 1; end

SET @count = 5;  SET @name = 'dathongso.dtts';
WHILE @count > 0 BEGIN INSERT dbo.DaThongSo ( Name, OPC_Address, Active ) VALUES ( N'Đa thông số '+CAST(@count AS NVARCHAR(50)), @opc+@name+'_'+CAST(@count AS NVARCHAR(50)), 1 ) SET @count = @count - 1; END


SELECT * FROM dbo.DiemDo
SELECT * FROM dbo.DaThongSo

end
GO
/****** Object:  Table [dbo].[DaThongSo]    Script Date: 5/18/2019 16:22:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DaThongSo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[OPC_Address] [nvarchar](50) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_DaThongSo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DiemDo]    Script Date: 5/18/2019 16:22:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiemDo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[OPC_Address] [nvarchar](50) NULL,
	[Type] [int] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_DiemDo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[DaThongSo] ON 

INSERT [dbo].[DaThongSo] ([ID], [Name], [OPC_Address], [Active]) VALUES (9, N'Đa thông số 4', N'dathongso.dtts4', 1)
INSERT [dbo].[DaThongSo] ([ID], [Name], [OPC_Address], [Active]) VALUES (10, N'Đa thông số 3', N'dathongso.dtts3', 1)
INSERT [dbo].[DaThongSo] ([ID], [Name], [OPC_Address], [Active]) VALUES (11, N'Đa thông số 2', N'dathongso.dtts2', 1)
INSERT [dbo].[DaThongSo] ([ID], [Name], [OPC_Address], [Active]) VALUES (12, N'Đa thông số 1', N'dathongso.dtts1', 1)
INSERT [dbo].[DaThongSo] ([ID], [Name], [OPC_Address], [Active]) VALUES (1013, N'Đa thông số 5', N'dathongso.dtts5', 1)
SET IDENTITY_INSERT [dbo].[DaThongSo] OFF
SET IDENTITY_INSERT [dbo].[DiemDo] ON 

INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (131, N'CH4_7', N'Server_ThanMaoKhe2.CH4_7.Giatri', 1, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (132, N'CH4_6', N'Server_ThanMaoKhe2.CH4_6.Giatri', 1, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (133, N'CH4_5', N'Server_ThanMaoKhe2.CH4_5.Giatri', 1, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (134, N'CH4_4', N'Server_ThanMaoKhe1.CH4_4.Giatri', 1, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (135, N'CH4_3', N'Server_ThanMaoKhe1.CH4_3.Giatri', 1, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (136, N'CH4_2', N'Server_ThanMaoKhe.CH4_2.Giatri', 1, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (137, N'CH4_1', N'Server_ThanMaoKhe.CH4_1.Giatri', 1, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (138, N'CO_7', N'Server_ThanMaoKhe2.CO_7.Giatri', 2, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (139, N'CO_6', N'Server_ThanMaoKhe2.CO_6.Giatri', 2, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (140, N'CO_5', N'Server_ThanMaoKhe2.CO_5.Giatri', 2, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (141, N'CO_4', N'Server_ThanMaoKhe1.CO_4.Giatri', 2, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (142, N'CO_3', N'Server_ThanMaoKhe1.CO_3.Giatri', 2, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (143, N'CO_2', N'Server_ThanMaoKhe.CO_2.Giatri', 2, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (144, N'CO_1', N'Server_ThanMaoKhe.CO_1.Giatri', 2, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (145, N'O2_2', N'Server_ThanMaoKhe2.O2_2.Giatri', 3, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (146, N'O2_1', N'Server_ThanMaoKhe2.O2_1.Giatri', 3, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (147, N'H2_2', N'Server_ThanMaoKhe1.H2_2.Giatri', 4, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (148, N'H2_1', N'Server_ThanMaoKhe.H2_1.Giatri', 4, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (149, N'NhietDo_5', N'Server_ThanMaoKhe2.NhietDo_5.Giatri', 5, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (150, N'NhietDo_4', N'Server_ThanMaoKhe1.NhietDo_4.Giatri', 5, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (151, N'NhietDo_3', N'Server_ThanMaoKhe1.NhietDo_3.Giatri', 5, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (152, N'NhietDo_2', N'Server_ThanMaoKhe.NhietDo_2.Giatri', 5, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (153, N'NhietDo_1', N'Server_ThanMaoKhe.NhietDo_1.Giatri', 5, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (154, N'Gio_5', N'Server_ThanMaoKhe2.Gio_5.Giatri', 6, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (155, N'Gio_4', N'Server_ThanMaoKhe1.Gio_4.Giatri', 6, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (156, N'Gio_3', N'Server_ThanMaoKhe1.Gio_3.Giatri', 6, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (157, N'Gio_2', N'Server_ThanMaoKhe.Gio_2.Giatri', 6, 1)
INSERT [dbo].[DiemDo] ([ID], [Name], [OPC_Address], [Type], [Active]) VALUES (158, N'Gio_1', N'Server_ThanMaoKhe.Gio_1.Giatri', 6, 1)
SET IDENTITY_INSERT [dbo].[DiemDo] OFF
USE [master]
GO
ALTER DATABASE [dbDoKhi] SET  READ_WRITE 
GO
