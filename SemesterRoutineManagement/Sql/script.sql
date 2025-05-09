USE [SemesterRoutineMSDB]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 6/30/2023 8:57:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[ShortName] [nvarchar](30) NOT NULL,
	[Code] [nchar](10) NULL,
	[Status] [bit] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifedBy] [int] NULL,
	[ModifiedAt] [datetime] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 6/30/2023 8:57:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Room]    Script Date: 6/30/2023 8:57:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[No] [nvarchar](50) NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Routine]    Script Date: 6/30/2023 8:57:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Routine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[TimeSpanId] [int] NOT NULL,
	[WeekDayId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
	[TeacherId] [int] NOT NULL,
 CONSTRAINT [PK_Routine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Session]    Script Date: 6/30/2023 8:57:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StudentCourseEnrollment]    Script Date: 6/30/2023 8:57:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentCourseEnrollment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[SessionId] [int] NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_StudentCourseEnrollment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TeacherAppointment]    Script Date: 6/30/2023 8:57:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeacherAppointment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeacherId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_TeacherAppointment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TimeSpan]    Script Date: 6/30/2023 8:57:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSpan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[startTime] [nvarchar](12) NOT NULL,
	[endTime] [nvarchar](12) NOT NULL,
	[sort] [int] NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_TimeSpan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 6/30/2023 8:57:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[Role] [nvarchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](20) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WeekDay]    Script Date: 6/30/2023 8:57:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeekDay](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](12) NOT NULL,
	[ShortName] [nvarchar](3) NOT NULL,
	[Sort] [int] NOT NULL,
	[Weekend] [bit] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_WeekDay] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([Id], [Name], [ShortName], [Code], [Status], [CreatedBy], [CreatedAt], [ModifedBy], [ModifiedAt]) VALUES (1, N'Algorithm', N'Algos', N'C-1       ', 1, 1, CAST(0x0000B02F00D84020 AS DateTime), 1, CAST(0x0000B02F00DA4079 AS DateTime))
INSERT [dbo].[Course] ([Id], [Name], [ShortName], [Code], [Status], [CreatedBy], [CreatedAt], [ModifedBy], [ModifiedAt]) VALUES (2, N'Data Structure', N'DS', N'C-2       ', 1, 1, CAST(0x0000B030007DD75B AS DateTime), NULL, NULL)
INSERT [dbo].[Course] ([Id], [Name], [ShortName], [Code], [Status], [CreatedBy], [CreatedAt], [ModifedBy], [ModifiedAt]) VALUES (3, N'C#', N'C#', N'C-3       ', 1, 1, CAST(0x0000B03100AD5162 AS DateTime), NULL, NULL)
INSERT [dbo].[Course] ([Id], [Name], [ShortName], [Code], [Status], [CreatedBy], [CreatedAt], [ModifedBy], [ModifiedAt]) VALUES (4, N'Java', N'JV', N'C-4       ', 1, 1, CAST(0x0000B03100AD5CA0 AS DateTime), NULL, NULL)
INSERT [dbo].[Course] ([Id], [Name], [ShortName], [Code], [Status], [CreatedBy], [CreatedAt], [ModifedBy], [ModifiedAt]) VALUES (5, N'Compiler', N'Com', N'C-5       ', 1, 1, CAST(0x0000B03100AD724C AS DateTime), NULL, NULL)
INSERT [dbo].[Course] ([Id], [Name], [ShortName], [Code], [Status], [CreatedBy], [CreatedAt], [ModifedBy], [ModifiedAt]) VALUES (6, N'C', N'C', N'C-6       ', 1, 1, CAST(0x0000B03100BA2E7B AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Course] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name], [Status]) VALUES (1, N'SuperAdmin', 1)
INSERT [dbo].[Role] ([Id], [Name], [Status]) VALUES (2, N'Student', 1)
INSERT [dbo].[Role] ([Id], [Name], [Status]) VALUES (3, N'Teacher', 1)
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[Room] ON 

INSERT [dbo].[Room] ([Id], [No], [Status]) VALUES (1, N'A-101', 1)
INSERT [dbo].[Room] ([Id], [No], [Status]) VALUES (2, N'A-102', 1)
INSERT [dbo].[Room] ([Id], [No], [Status]) VALUES (3, N'A-103', 1)
INSERT [dbo].[Room] ([Id], [No], [Status]) VALUES (4, N'A-104', 1)
INSERT [dbo].[Room] ([Id], [No], [Status]) VALUES (5, N'A-105', 1)
SET IDENTITY_INSERT [dbo].[Room] OFF
SET IDENTITY_INSERT [dbo].[Routine] ON 

INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (4, 1, 4, 1, 7, 3, 7)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (6, 1, 2, 0, 3, 1, 6)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (7, 1, 1, 1, 3, 3, 7)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (8, 1, 6, 2, 3, 1, 8)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (9, 1, 2, 0, 4, 1, 2)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (10, 1, 5, 1, 4, 4, 8)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (11, 1, 6, 2, 4, 2, 8)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (12, 1, 2, 0, 5, 5, 2)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (13, 1, 5, 0, 5, 1, 8)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (14, 1, 4, 1, 5, 3, 7)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (15, 1, 3, 2, 5, 5, 6)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (16, 1, 1, 0, 6, 1, 7)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (17, 1, 6, 0, 6, 2, 8)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (18, 1, 3, 1, 6, 1, 6)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (19, 1, 4, 2, 6, 2, 7)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (20, 1, 1, 0, 7, 2, 7)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (21, 1, 3, 0, 7, 3, 6)
INSERT [dbo].[Routine] ([Id], [SessionId], [CourseId], [TimeSpanId], [WeekDayId], [RoomId], [TeacherId]) VALUES (22, 1, 4, 2, 7, 1, 7)
SET IDENTITY_INSERT [dbo].[Routine] OFF
SET IDENTITY_INSERT [dbo].[Session] ON 

INSERT [dbo].[Session] ([Id], [Name], [Status]) VALUES (1, N'Semester:23-24', 1)
SET IDENTITY_INSERT [dbo].[Session] OFF
SET IDENTITY_INSERT [dbo].[StudentCourseEnrollment] ON 

INSERT [dbo].[StudentCourseEnrollment] ([Id], [StudentId], [CourseId], [SessionId], [Status]) VALUES (5, 3, 3, 1, 1)
INSERT [dbo].[StudentCourseEnrollment] ([Id], [StudentId], [CourseId], [SessionId], [Status]) VALUES (6, 4, 3, 1, 1)
SET IDENTITY_INSERT [dbo].[StudentCourseEnrollment] OFF
SET IDENTITY_INSERT [dbo].[TeacherAppointment] ON 

INSERT [dbo].[TeacherAppointment] ([Id], [TeacherId], [CourseId], [Status]) VALUES (1, 2, 1, 1)
INSERT [dbo].[TeacherAppointment] ([Id], [TeacherId], [CourseId], [Status]) VALUES (2, 2, 2, 1)
INSERT [dbo].[TeacherAppointment] ([Id], [TeacherId], [CourseId], [Status]) VALUES (3, 6, 3, 1)
INSERT [dbo].[TeacherAppointment] ([Id], [TeacherId], [CourseId], [Status]) VALUES (4, 7, 4, 1)
INSERT [dbo].[TeacherAppointment] ([Id], [TeacherId], [CourseId], [Status]) VALUES (5, 8, 5, 1)
INSERT [dbo].[TeacherAppointment] ([Id], [TeacherId], [CourseId], [Status]) VALUES (6, 6, 2, 1)
INSERT [dbo].[TeacherAppointment] ([Id], [TeacherId], [CourseId], [Status]) VALUES (7, 7, 1, 1)
INSERT [dbo].[TeacherAppointment] ([Id], [TeacherId], [CourseId], [Status]) VALUES (8, 8, 6, 1)
SET IDENTITY_INSERT [dbo].[TeacherAppointment] OFF
SET IDENTITY_INSERT [dbo].[TimeSpan] ON 

INSERT [dbo].[TimeSpan] ([Id], [startTime], [endTime], [sort], [Status]) VALUES (0, N'09:30 AM', N'10:00 AM', 1, 1)
INSERT [dbo].[TimeSpan] ([Id], [startTime], [endTime], [sort], [Status]) VALUES (1, N'10:05 AM', N'10:35 AM', 2, 1)
INSERT [dbo].[TimeSpan] ([Id], [startTime], [endTime], [sort], [Status]) VALUES (2, N'10:40 AM', N'11:10 AM', 3, 1)
SET IDENTITY_INSERT [dbo].[TimeSpan] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Name], [UserName], [Password], [Role], [Status], [Email], [Phone]) VALUES (1, N'Shihab Saadi', N'saadi', N'123', N'SuperAdmin', 1, N's.saadi2047@gmail.com', NULL)
INSERT [dbo].[User] ([Id], [Name], [UserName], [Password], [Role], [Status], [Email], [Phone]) VALUES (2, N'Shofiul Basher', N'basher', N'c7c7f75a', N'Teacher', 1, N'basher@gmail.com', NULL)
INSERT [dbo].[User] ([Id], [Name], [UserName], [Password], [Role], [Status], [Email], [Phone]) VALUES (3, N'Easir Mahmud Emon', N'easir', N'8703b839', N'Student', 1, N'easir@gmail.com', NULL)
INSERT [dbo].[User] ([Id], [Name], [UserName], [Password], [Role], [Status], [Email], [Phone]) VALUES (4, N'Sujan Barua', N'sujan', N'a98969b3', N'Student', 1, N'sujan@gmail.com', NULL)
INSERT [dbo].[User] ([Id], [Name], [UserName], [Password], [Role], [Status], [Email], [Phone]) VALUES (5, N'Readh Md. Saifullah', N'readh', N'efc55139', N'Student', 1, N'readh@gmail.com', NULL)
INSERT [dbo].[User] ([Id], [Name], [UserName], [Password], [Role], [Status], [Email], [Phone]) VALUES (6, N'Alam Mohammad Rizvi', N'rizvi', N'59ede927', N'Teacher', 1, N'rizvi@gmail.com', NULL)
INSERT [dbo].[User] ([Id], [Name], [UserName], [Password], [Role], [Status], [Email], [Phone]) VALUES (7, N'Shantanu Chowdhury', N'shan', N'35864e43', N'Teacher', 1, N'shan@gmail.com', NULL)
INSERT [dbo].[User] ([Id], [Name], [UserName], [Password], [Role], [Status], [Email], [Phone]) VALUES (8, N'Nandan Datta', N'nandan', N'684b89f6', N'Teacher', 1, N'nandan@gmail.com', NULL)
INSERT [dbo].[User] ([Id], [Name], [UserName], [Password], [Role], [Status], [Email], [Phone]) VALUES (10, N'Shuvro', N'shuvro', N'ff0f5f7d', N'Student', 1, N'stone.shuvro@gmail.com', NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[WeekDay] ON 

INSERT [dbo].[WeekDay] ([Id], [Name], [ShortName], [Sort], [Weekend], [Status]) VALUES (1, N'Satureday', N'SAT', 1, 1, 1)
INSERT [dbo].[WeekDay] ([Id], [Name], [ShortName], [Sort], [Weekend], [Status]) VALUES (3, N'Sunday', N'SUN', 2, 0, 1)
INSERT [dbo].[WeekDay] ([Id], [Name], [ShortName], [Sort], [Weekend], [Status]) VALUES (4, N'Monday', N'MON', 3, 0, 1)
INSERT [dbo].[WeekDay] ([Id], [Name], [ShortName], [Sort], [Weekend], [Status]) VALUES (5, N'Tuesday', N'TUE', 4, 0, 1)
INSERT [dbo].[WeekDay] ([Id], [Name], [ShortName], [Sort], [Weekend], [Status]) VALUES (6, N'Wednesday', N'WED', 5, 0, 1)
INSERT [dbo].[WeekDay] ([Id], [Name], [ShortName], [Sort], [Weekend], [Status]) VALUES (7, N'Thursday', N'THU', 6, 0, 1)
INSERT [dbo].[WeekDay] ([Id], [Name], [ShortName], [Sort], [Weekend], [Status]) VALUES (8, N'Friday', N'FRI', 7, 1, 1)
SET IDENTITY_INSERT [dbo].[WeekDay] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [Unique_No]    Script Date: 6/30/2023 8:57:21 PM ******/
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [Unique_No] UNIQUE NONCLUSTERED 
(
	[No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Unique_Name]    Script Date: 6/30/2023 8:57:21 PM ******/
ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [Unique_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Unique_End]    Script Date: 6/30/2023 8:57:21 PM ******/
ALTER TABLE [dbo].[TimeSpan] ADD  CONSTRAINT [Unique_End] UNIQUE NONCLUSTERED 
(
	[endTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Unique_Start]    Script Date: 6/30/2023 8:57:21 PM ******/
ALTER TABLE [dbo].[TimeSpan] ADD  CONSTRAINT [Unique_Start] UNIQUE NONCLUSTERED 
(
	[startTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Unique_Email]    Script Date: 6/30/2023 8:57:21 PM ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [Unique_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Unique_UserName]    Script Date: 6/30/2023 8:57:21 PM ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [Unique_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_User]
GO
ALTER TABLE [dbo].[Routine]  WITH CHECK ADD  CONSTRAINT [FK_Routine_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
GO
ALTER TABLE [dbo].[Routine] CHECK CONSTRAINT [FK_Routine_Course]
GO
ALTER TABLE [dbo].[Routine]  WITH CHECK ADD  CONSTRAINT [FK_Routine_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[Routine] CHECK CONSTRAINT [FK_Routine_Room]
GO
ALTER TABLE [dbo].[Routine]  WITH CHECK ADD  CONSTRAINT [FK_Routine_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[Routine] CHECK CONSTRAINT [FK_Routine_Session]
GO
ALTER TABLE [dbo].[Routine]  WITH CHECK ADD  CONSTRAINT [FK_Routine_TimeSpan] FOREIGN KEY([TimeSpanId])
REFERENCES [dbo].[TimeSpan] ([Id])
GO
ALTER TABLE [dbo].[Routine] CHECK CONSTRAINT [FK_Routine_TimeSpan]
GO
ALTER TABLE [dbo].[Routine]  WITH CHECK ADD  CONSTRAINT [FK_Routine_User] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Routine] CHECK CONSTRAINT [FK_Routine_User]
GO
ALTER TABLE [dbo].[Routine]  WITH CHECK ADD  CONSTRAINT [FK_Routine_WeekDay] FOREIGN KEY([WeekDayId])
REFERENCES [dbo].[WeekDay] ([Id])
GO
ALTER TABLE [dbo].[Routine] CHECK CONSTRAINT [FK_Routine_WeekDay]
GO
ALTER TABLE [dbo].[StudentCourseEnrollment]  WITH CHECK ADD  CONSTRAINT [FK_StudentCourseEnrollment_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
GO
ALTER TABLE [dbo].[StudentCourseEnrollment] CHECK CONSTRAINT [FK_StudentCourseEnrollment_Course]
GO
ALTER TABLE [dbo].[StudentCourseEnrollment]  WITH CHECK ADD  CONSTRAINT [FK_StudentCourseEnrollment_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[StudentCourseEnrollment] CHECK CONSTRAINT [FK_StudentCourseEnrollment_Session]
GO
ALTER TABLE [dbo].[StudentCourseEnrollment]  WITH CHECK ADD  CONSTRAINT [FK_StudentCourseEnrollment_User] FOREIGN KEY([StudentId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[StudentCourseEnrollment] CHECK CONSTRAINT [FK_StudentCourseEnrollment_User]
GO
ALTER TABLE [dbo].[TeacherAppointment]  WITH CHECK ADD  CONSTRAINT [FK_TeacherAppointment_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
GO
ALTER TABLE [dbo].[TeacherAppointment] CHECK CONSTRAINT [FK_TeacherAppointment_Course]
GO
ALTER TABLE [dbo].[TeacherAppointment]  WITH CHECK ADD  CONSTRAINT [FK_TeacherAppointment_User] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[TeacherAppointment] CHECK CONSTRAINT [FK_TeacherAppointment_User]
GO
