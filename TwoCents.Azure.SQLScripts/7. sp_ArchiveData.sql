IF OBJECT_ID('[dbo].sp_ArchiveData', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].sp_ArchiveData
GO

CREATE PROCEDURE [dbo].sp_ArchiveData
(
	@deleteAfterMonths		INT
)
AS
BEGIN

	-- Backup service orders	
	INSERT INTO ServiceOrdersBU 
	SELECT EntityId, Entity, SourceSystem, ServiceNumber, AddressCode, ServiceContractNumber, BronCodeService, OrderDate, 
	[Priority], RegistrationDate, ReferencePoint, [Status], StartDate, EndDate, AuditUser 
	FROM ServiceOrders
	WHERE EntityId IN
	(SELECT EntityId  
	 FROM ProcessFlow 
	 WHERE [Status]='Completed')

	-- Backup process flow
	INSERT INTO ProcessFlowBU
	SELECT * FROM ProcessFlow
	WHERE [Status]='Completed'   
	
	-- Cleanup service orders
	DELETE FROM ServiceOrders 
	WHERE EntityId IN
	(SELECT EntityId  
	 FROM ProcessFlow 
	 WHERE [Status]='Completed')

	-- Cleanup process flow
	DELETE FROM ProcessFlow
	WHERE [Status]='Completed'

	-- Hard delete ServiceOrderBU
	DELETE FROM ServiceOrdersBU 
	WHERE StartDate <= DATEADD(MONTH, @deleteAfterMonths*-1, StartDate)

	-- Hard delete ProcessFlowBU
	DELETE FROM ProcessFlowBU 
	WHERE StartDate <= DATEADD(MONTH, @deleteAfterMonths*-1, StartDate)

END