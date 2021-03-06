USE [band_tracker]
GO
/****** Object:  Table [dbo].[bands]    Script Date: 6/16/2017 4:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[venues]    Script Date: 6/16/2017 4:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[venues_bands]    Script Date: 6/16/2017 4:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues_bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[venue_id] [int] NULL,
	[band_id] [int] NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[venues_bands] ON 

INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (1, 9, 32)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (2, 9, 33)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (3, 10, 34)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (4, 14, 39)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (5, 14, 40)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (6, 15, 41)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (7, 17, 43)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (8, 20, 47)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (9, 20, 48)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (10, 21, 49)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (11, 23, 51)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (12, 26, 55)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (13, 26, 56)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (15, 29, 59)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (16, 32, 63)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (17, 32, 64)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (18, 33, 65)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (19, 35, 67)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (20, 38, 71)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (21, 38, 72)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (23, 41, 75)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (24, 44, 79)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (25, 44, 80)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (27, 47, 83)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (29, 51, 88)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (30, 51, 89)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (32, 54, 92)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (35, 59, 98)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (14, 27, 57)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (22, 39, 73)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (26, 45, 81)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (31, 52, 90)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (36, 59, 99)
INSERT [dbo].[venues_bands] ([id], [venue_id], [band_id]) VALUES (37, 60, 100)
SET IDENTITY_INSERT [dbo].[venues_bands] OFF
