### Metigator Academy  :school:

##### + Courses  :book:
<div style="padding: 10px; font-size: 10px; font-weight: bold"> 
![image](https://github.com/metigator/EF-015/assets/87314838/c55689c0-78fa-4615-8b71-037e852ca082)
</div>

##### + Office  :book:
<div style="padding: 10px; font-size: 10px; font-weight: bold">
![image](https://github.com/metigator/EF-015/assets/87314838/27e51590-73d2-4512-8e52-cf7d5c50e84b)
</div>

##### + Instructors Table :mens:
<div style="padding: 10px; font-size: 10px; font-weight: bold">
![image](https://github.com/metigator/EF-015/assets/87314838/a34176fd-a237-4dd2-b7e1-665327085803)
</div>

##### + Schedules Table :department_store:
<div style="padding: 10px; font-size: 10px; font-weight: bold">
![image](https://github.com/metigator/EF-015/assets/87314838/8e2f65c4-ee3c-4573-8c9d-83f1ed0d3a84)
</div>

##### + Sections Table :department_store:
<div style="padding: 10px; font-size: 10px; font-weight: bold">
![image](https://github.com/metigator/EF-015/assets/87314838/0ccb36c4-6e46-4a00-a4a1-273768f4cd0b)
</div> 

#####  + Participants Table
<div style="padding: 10px; font-size: 10px; font-weight: bold">
 ![image](https://github.com/metigator/EF-015/assets/87314838/62181093-b405-44a1-9187-b40bf651c591)
</div>
  
#####  + Corporates Table
<div style="padding: 10px; font-size: 10px; font-weight: bold">
![image](https://github.com/metigator/EF-015/assets/87314838/f15be23a-570b-4dfc-84ca-345ce9376c6c)
</div>

##### Individuals Table
<div style="padding: 10px; font-size: 10px; font-weight: bold">
![image](https://github.com/metigator/EF-015/assets/87314838/42e220a1-df23-4393-932d-c55968fa41b5)
</div>
 
#### + Participant Enrollment :man: :girl:  
<div style="padding: 10px; font-size: 10px; font-weight: bold">
![image](https://github.com/metigator/EF-015/assets/87314838/c8f9db12-341e-44a8-ab45-fa2e14f77cdc)
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
![image](https://github.com/metigator/EF-015/assets/87314838/8b0d1fb1-e50f-4efc-a2af-95c746d03865)
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

![image](https://github.com/metigator/EF-015/assets/87314838/84f873a6-16dd-4680-bfc0-cdab1977e075)
