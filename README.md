### Metigator Academy  :school:

##### + Courses  :book:
<div style="padding: 10px; font-size: 10px; font-weight: bold"> 
```sql
	CREATE TABLE [dbo].[Courses](
	[Id] [int]  PRIMARY KEY,
	[CourseName] [varchar](255) NOT NULL,
	[Price] [decimal](15, 2) NOT NULL,
	[HoursToComplete] [int] NOT NULL
	);
```
</div>

##### + Offices  :book:
<div style="padding: 10px; font-size: 10px; font-weight: bold">
```sql
	CREATE TABLE [dbo].[Offices](
	[Id] [int] NOT NULL PRIMARY KEY,
	[OfficeName] [varchar](50) NOT NULL,
	[OfficeLocation] [varchar](50) NOT NULL
	);
```
</div>

##### + Instructors Table :mens:
<div style="padding: 10px; font-size: 10px; font-weight: bold">
```sql
	CREATE TABLE [dbo].[Instructors](
	[Id] [int] PRIMARY KEY,
	[FName] [varchar](50) NOT NULL,
	[LName] [varchar](50) NOT NULL,
	[OfficeId] [int] REFERENCES Offices(Id),
 	)
```
</div>

##### + Schedules Table :department_store:
<div style="padding: 10px; font-size: 10px; font-weight: bold">
```sql
	CREATE TABLE [dbo].[Schedules](
	[Id] [int] PRIMARY KEY,
	[ScheduleType] [nvarchar](max) NOT NULL,
	[SUN] [bit] NOT NULL,
	[MON] [bit] NOT NULL,
	[TUE] [bit] NOT NULL,
	[WED] [bit] NOT NULL,
	[THU] [bit] NOT NULL,
	[FRI] [bit] NOT NULL,
	[SAT] [bit] NOT NULL
	);
```
</div>

##### + Sections Table :department_store:
<div style="padding: 10px; font-size: 10px; font-weight: bold">
```sql
	CREATE TABLE [dbo].[Sections](
	[Id] [int] NOT NULL PRIMARY KEY,
	[SectionName] [varchar](255) NOT NULL,
	[CourseId] [int] NOT NULL REFERENCES Courses(Id),
	[InstructorId] [int] NOT NULL REFERENCES Instructors(Id),
	[ScheduleId] [int] NOT NULL REFERENCES Schedules(Id),
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[StartTime] [time](0) NOT NULL,
	[EndTime] [time](0) NOT NULL
	);
```
</div> 

#####  + Participants Table
<div style="padding: 10px; font-size: 10px; font-weight: bold">
```sql
	CREATE TABLE [dbo].[Particpants](
	[Id] [int] PRIMARY KEY,
	[FName] [varchar](50) NOT NULL,
	[LName] [varchar](50) NOT NULL,
	); 
```A
</div>
  
#####  + Corporates Table
<div style="padding: 10px; font-size: 10px; font-weight: bold">
```sql
	CREATE TABLE [dbo].[Coporates](
	[Id] [int] PRIMARY KEY REFERENCES Participats(Id),
	[Company] [nvarchar](max) NOT NULL,
	[JobTitle] [nvarchar](max) NOT NULL
	);
```
</div>

##### Individuals Table
<div style="padding: 10px; font-size: 10px; font-weight: bold">
```sql
	CREATE TABLE [dbo].[Individuals](
	[Id] [int] PRIMARY KEY REFERENCES Participats(Id),
	[University] [nvarchar](max) NOT NULL,
	[YearOfGraduation] [int] NOT NULL,
	[IsIntern] [bit] NOT NULL
	);
```
</div>
 
#### + Enrollments :man: :girl:  
<div style="padding: 10px; font-size: 10px; font-weight: bold">
```sql
CREATE TABLE [dbo].[Enrollments](
	[SectionId] [int] NOT NULL REFERENCES Sections(Id),
	[ParticipantId] [int] NOT NULL REFERENCES Courses(Id),
	PRIMARYKEY(SectionId, ParticipantId)
	); 
```
</div>

##### + Stored Procedure GetSectionDetais
```sql
USE [EF015]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetSectionDetails]    Script Date: 2023-08-06 4:00:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetSectionDetails]
    @SectionId INT
AS
BEGIN
    SELECT
        S.Id,
        C.CourseName,
        C.HoursToComplete TotalHours,
        S.SectionName,
        (I.FName + ' ' + I.LName) As Instructor,
        FORMAT(S.[StartDate], 'yyyy-MM-dd') + ' - ' + FORMAT(S.[EndDate], 'yyyy-MM-dd') As Period,
           COALESCE(CONVERT(VARCHAR, S.[StartTime], 108), 'N/A') 
            + ' - ' + 
            COALESCE(CONVERT(VARCHAR, S.[EndTime], 108), 'N/A') 
            + ' (' + 
            CAST(DATEDIFF(HOUR, S.[StartTime], S.[EndTime]) AS VARCHAR(2)) + ' hrs)' As Timeslot,
        SC.SUN,
        SC.MON,
        SC.Tue,
        SC.WED,
        SC.THU,
        SC.FRI,
        SC.SAT,
            CAST((DATEDIFF(MINUTE, S.StartTime, S.EndTime)/60.0) * 
                CASE 
                    WHEN SC.ScheduleType = 'Daily' THEN 7
                    WHEN SC.ScheduleType = 'DayAfterDay' THEN 3
                    WHEN SC.ScheduleType = 'TwiceAWeek' THEN 2
                    WHEN SC.ScheduleType = 'Weekend' THEN 2
                    WHEN SC.ScheduleType = 'Compact' THEN 7
                    ELSE 0
                END AS INT) AS HoursPerWeek
    FROM Sections S
    JOIN Courses C ON S.CourseId = C.Id
    JOIN Schedules SC ON S.ScheduleId = SC.Id
    JOIN Instructors I ON S.InstructorId = I.Id
    WHERE S.Id = @SectionId
END;
```

```sql
AUSE [EF015]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[sp_GetSectionDetails]
		@SectionId = 22

SELECT	'Return Value' = @return_value

GO

```
<div style="padding: 10px; font-size: 10px; font-weight: bold">
![image](https://github.com/metigator/EF-015/assets/87314838/1f1ab081-857b-4e7f-a7f7-1d6195e2a279)
</div>

##### + Function InstructorAvailable

```sql
USE [EF015]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_InstructorAvailability]    Script Date: 2023-08-06 4:00:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_InstructorAvailability](
    @InstructorId INT,
    @StartDate DATE,
    @EndDate DATE,
    @StartTime TIME,
    @EndTime TIME
)
RETURNS NVARCHAR(50)
AS
BEGIN
    DECLARE @Result NVARCHAR(50)

    IF EXISTS(
        SELECT 1
        FROM Sections
        WHERE InstructorId = @InstructorId
        AND (
            (StartDate BETWEEN @StartDate AND @EndDate) OR
            (EndDate BETWEEN @StartDate AND @EndDate) OR
            (StartDate <= @StartDate AND EndDate >= @EndDate)
        )
        AND (
            (StartTime BETWEEN @StartTime AND @EndTime) OR
            (EndTime BETWEEN @StartTime AND @EndTime) OR
            (StartTime <= @StartTime AND EndTime >= @EndTime)
        )
    )
        SET @Result = 'Unavailable'
    ELSE
        SET @Result = 'Available'

    RETURN @Result
END
```

```sql
SELECT dbo.fn_InstructorAvailability(1, '2023-08-01', '2023-08-15', '10:00:00', '14:00:00') AS AvailabilityStatus;
```

