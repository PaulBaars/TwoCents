/****** Object: SqlProcedure [dbo].[sp_MonitoringData] Script Date: 11/23/2015 10:06:44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].sp_MonitoringData', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[sp_MonitoringData]
GO

CREATE PROCEDURE [dbo].sp_MonitoringData
AS
BEGIN

    DECLARE @ComponentType  VARCHAR(30)
    DECLARE @Week           INT
    DECLARE @Occurrences    INT
    DECLARE @LastOccurrence DATETIME
    DECLARE @Status         VARCHAR(10) 
	DECLARE @CountOccurs	INT

	DECLARE		monitoringCursor CURSOR FOR  
	SELECT		P.Name, datepart(week, PF.StartDate), Count(*), MAX(PF.StartDate)
	FROM
	(SELECT		ProcessId, [Status], StartDate
	FROM		ProcessFlow
	UNION
	SELECT		ProcessId, [Status], StartDate
	FROM		ProcessFlowBU) PF
	JOIN		Processes P
	ON			PF.ProcessId = P.ProcessId
	WHERE		PF.[Status]='Completed'
	GROUP BY	P.Name, datepart(week, PF.StartDate)
	HAVING		datepart(week, PF.StartDate) = datepart(week, getdate())   

	OPEN monitoringCursor   
	FETCH NEXT FROM monitoringCursor INTO @ComponentType, @Week, @Occurrences, @LastOccurrence   

	WHILE @@FETCH_STATUS = 0   
	BEGIN
		
		SELECT	@CountOccurs=COUNT(*)
		FROM	Monitoring
		WHERE	ComponentType=@ComponentType AND [Week]=@Week

		IF @CountOccurs=0
		BEGIN
			INSERT INTO Monitoring(ComponentType, [Week], NumberOfOK, NumberOfError, LastOccurrence, [Status], LastCheck)
			VALUES (@ComponentType, @Week, @Occurrences, 0, @LastOccurrence, 'OK', getdate())
		END
		ELSE
		BEGIN
			UPDATE Monitoring 
			SET NumberOfOK = @Occurrences, [Status]= 'OK', LastCheck=getdate()
			WHERE ComponentType=@ComponentType and [Week]=@Week
			
			UPDATE Monitoring
			SET LastOccurrence = @LastOccurrence 
			WHERE ComponentType=@ComponentType and [Week]=@Week AND @LastOccurrence > LastOccurrence  
		END
	
		FETCH NEXT FROM monitoringCursor INTO @ComponentType, @Week, @Occurrences, @LastOccurrence

	END

	CLOSE monitoringCursor   
	DEALLOCATE monitoringCursor 
	
	DECLARE		monitoringCursor2 CURSOR FOR
	SELECT		P.Name, datepart(week, PF.StartDate), Count(*), MAX(PF.StartDate)
	FROM
	(SELECT		ProcessId, [Status], StartDate
	FROM		ProcessFlow
	UNION
	SELECT		ProcessId, [Status], StartDate
	FROM		ProcessFlowBU) PF
	JOIN		Processes P
	ON			PF.ProcessId = P.ProcessId
	WHERE		PF.[Status]='Pending'	
	GROUP BY	P.Name, datepart(week, PF.StartDate)
	HAVING		datepart(week, PF.StartDate) = datepart(week, getdate())

	OPEN monitoringCursor2   
	FETCH NEXT FROM monitoringCursor2 INTO @ComponentType, @Week, @Occurrences, @LastOccurrence   

	WHILE @@FETCH_STATUS = 0   
	BEGIN

	    SELECT	@CountOccurs=COUNT(*)
		FROM	Monitoring
		WHERE	ComponentType=@ComponentType AND [Week]=@Week

		IF @CountOccurs=0
		BEGIN
			INSERT INTO Monitoring(ComponentType, [Week], NumberOfOK, NumberOfError, LastOccurrence, [Status], LastCheck)
			VALUES (@ComponentType, @Week, 0, @Occurrences, @LastOccurrence, 'Error', getdate())
		END
		ELSE
		BEGIN
			UPDATE Monitoring 
			SET NumberOfError=@Occurrences, [Status] = 'Error', LastCheck=getdate() 
			WHERE ComponentType=@ComponentType and [Week]=@Week 
	
			UPDATE Monitoring
			SET LastOccurrence = @LastOccurrence 
			WHERE ComponentType=@ComponentType and [Week]=@Week AND @LastOccurrence > LastOccurrence
		END

		FETCH NEXT FROM monitoringCursor2 INTO @ComponentType, @Week, @Occurrences, @LastOccurrence 
	
	END
	
	CLOSE monitoringCursor2 
	DEALLOCATE monitoringCursor2 
	
	DECLARE		monitoringCursor3 CURSOR FOR  
	SELECT		SO.SourceSystem+'Gateway', datepart(week, SO.StartDate), Count(*), MAX(SO.StartDate)
	FROM
	(SELECT		SourceSystem, [Status], StartDate
	FROM		ServiceOrders
	UNION
	SELECT		SourceSystem, [Status], StartDate
	FROM		ServiceOrdersBU) SO
	WHERE		SO.[Status]='Created'
	GROUP BY	SO.SourceSystem+'Gateway', datepart(week, SO.StartDate)
	HAVING		datepart(week, SO.StartDate) = datepart(week, getdate())   

	OPEN monitoringCursor3   
	FETCH NEXT FROM monitoringCursor3 INTO @ComponentType, @Week, @Occurrences, @LastOccurrence   

	WHILE @@FETCH_STATUS = 0   
	BEGIN
		
		SELECT	@CountOccurs=COUNT(*)
		FROM	Monitoring
		WHERE	ComponentType=@ComponentType AND [Week]=@Week

		IF @CountOccurs=0
		BEGIN
			INSERT INTO Monitoring(ComponentType, [Week], NumberOfOK, NumberOfError, LastOccurrence, [Status], LastCheck)
			VALUES (@ComponentType, @Week, @Occurrences, 0, @LastOccurrence, 'OK', getdate())
		END
		ELSE
		BEGIN
			UPDATE Monitoring 
			SET NumberOfOK = @Occurrences, LastCheck=getdate()
			WHERE ComponentType=@ComponentType and [Week]=@Week
			
			UPDATE Monitoring
			SET LastOccurrence = @LastOccurrence 
			WHERE ComponentType=@ComponentType and [Week]=@Week AND @LastOccurrence > LastOccurrence  
		END
	
		FETCH NEXT FROM monitoringCursor3 INTO @ComponentType, @Week, @Occurrences, @LastOccurrence

	END

	CLOSE monitoringCursor3   
	DEALLOCATE monitoringCursor3 
	
	DECLARE		monitoringCursor4 CURSOR FOR
	SELECT		SO.SourceSystem+'Gateway', datepart(week, SO.StartDate), Count(*), CASE WHEN Count(*)>0 THEN 'ERROR' ELSE 'SUCCESS' END, MAX(SO.StartDate)
	FROM
	(SELECT		SourceSystem, [Status], StartDate
	FROM		ServiceOrders
	UNION
	SELECT		SourceSystem, [Status], StartDate
	FROM		ServiceOrdersBU) SO
	WHERE		SO.[Status]<>'Created'
	GROUP BY	SO.SourceSystem+'Gateway', datepart(week, SO.StartDate)
	HAVING		datepart(week, SO.StartDate) = datepart(week, getdate())   
	
	OPEN monitoringCursor4   
	FETCH NEXT FROM monitoringCursor4 INTO @ComponentType, @Week, @Occurrences, @Status, @LastOccurrence   

	WHILE @@FETCH_STATUS = 0   
	BEGIN

		SELECT	@CountOccurs=COUNT(*)
		FROM	Monitoring
		WHERE	ComponentType=@ComponentType AND [Week]=@Week

		IF @CountOccurs=0
		BEGIN
			INSERT INTO Monitoring(ComponentType, [Week], NumberOfOK, NumberOfError, LastOccurrence, [Status], LastCheck)
			VALUES (@ComponentType, @Week, 0, @Occurrences, @LastOccurrence, 'Error', getdate())
		END
		ELSE
		BEGIN
			UPDATE Monitoring 
			SET NumberOfError=@Occurrences, [Status] = 'Error', LastCheck=getdate() 
			WHERE ComponentType=@ComponentType and [Week]=@Week 
	
			UPDATE Monitoring
			SET LastOccurrence = @LastOccurrence 
			WHERE ComponentType=@ComponentType and [Week]=@Week AND @LastOccurrence > LastOccurrence
		END

		FETCH NEXT FROM monitoringCursor4 INTO @ComponentType, @Week, @Occurrences, @Status, @LastOccurrence 
	
	END

	CLOSE monitoringCursor4
	DEALLOCATE monitoringCursor4 

END

GO