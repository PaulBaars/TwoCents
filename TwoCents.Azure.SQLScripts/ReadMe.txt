Run the scripts in the following order:
1. WorkflowAdminDB -> Drop/Create Scripts 1 t/m 6
2. Case Specific -> Insert Scripts: 3a t/m 3c
3. Archiving -> Script: 7. sp_ArchiveData -> Only workflow tables, strip case specific archiving. Can be updated in live situation.
4. Monitoring -> Script 8 t/m 10 -> Monitoring is linked to setup Portal (not only workflow tables). Has to be corrected.