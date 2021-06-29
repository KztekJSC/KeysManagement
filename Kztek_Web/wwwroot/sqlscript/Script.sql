﻿
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Group')
BEGIN

CREATE TABLE [dbo].[Group](
	[Id] [VARCHAR](128) NOT NULL,
	[Code] [nvarchar](200) NULL,
	[Name] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Vehicle')
BEGIN

CREATE TABLE [dbo].[Vehicle](
	[Id] [VARCHAR](128) NOT NULL,
	[Phone] [varchar](200) NULL,
	[Name] [nvarchar](500) NULL,
	[Plate] [varchar](200) NULL,
	[GroupId] [varchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'VehicleEvent')
BEGIN

CREATE TABLE [dbo].[VehicleEvent](
	[Id] [VARCHAR](128) NOT NULL,
	[EventCode] [varchar](100) NULL,
	[PlateIn] [varchar](150) NULL,
	[PicVehicleIn] [varchar](max) NULL,
	[PicAllIn] [varchar](max) NULL,
	[GateIn] [varchar](150) NULL,
	[DateTimeIn] [datetime] NOT NULL,
	[PlateOut] [varchar](150) NULL,
	[PicVehicleOut] [varchar](max) NULL,
	[PicAllOut] [varchar](max) NULL,
	[GateOut] [varchar](150) NULL,
	[DateTimeOut] [datetime] NOT NULL,
	[GroupId] [varchar](150) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsDelete] bit NOT NULL default (0),
 CONSTRAINT [PK_VehicleEvent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END