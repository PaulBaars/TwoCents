/****** Object: View [dbo].[ViewResubmitProcess] Script Date: 12/3/2015 11:02:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW ViewResubmitProcess
AS
SELECT PF.SequenceId, P.Name AS Process, PS.NAME as Step, PF.Entity, PF.StartDate
	FROM ProcessFlow PF
	JOIN Processes P ON PF.ProcessId = P.ProcessId
	JOIN ProcessSteps PS ON PF.StepId = PS.StepId   
	WHERE Status='Pending' 
	AND StartDate IS NOT NULL
	AND DATEDIFF(MINUTE, StartDate, GetDate()) > 5  
	AND PF.EntityId NOT IN
	(SELECT DISTINCT EntityId FROM Faults);

GO
