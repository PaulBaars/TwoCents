using TwoCents.Azure.Edmx.Common;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using TwoCents.Azure.Library.Error;

namespace TwoCents.Azure.Library.Process
{
    public class ProcessResolver
    {
        public bool ResolveProcess(string messageType, string entityId, string sourceSystem)
        {

            try
            {

                using (EntityModelCommon contextCommon = new EntityModelCommon())
                {

                    //Not all fields of the ProcessDefinition are required
                    //Select PD (the full selection) is used in order to be able to return a typed list
                    List<ProcessDefinition> entries = (from PD in contextCommon.ProcessDefinitions
                                                       where PD.Process.SourceSystem == sourceSystem && PD.Process.MessageType == messageType && PD.Process.ResolveProcess == true
                                                       select PD).ToList<ProcessDefinition>();

                    if (entries.Count >= 1)
                    {

                        foreach (ProcessDefinition entry in entries)
                        {
                            ProcessFlow processFlow = new ProcessFlow();
                            processFlow.ProcessId = entry.ProcessId;
                            processFlow.StepId = entry.StepId;
                            processFlow.Sequence = entry.Sequence;
                            processFlow.Status = entry.Status;
                            processFlow.Notification = entry.Notification;
                            processFlow.IsLastStep = entry.IsLastStep;
                            processFlow.Entity = messageType;
                            processFlow.EntityId = entityId;

                            contextCommon.ProcessFlows.Add(processFlow);
                            SaveChanges(contextCommon);
                        }
                    }
                    else
                    {
                        GatewayException gatewayException = new GatewayException(String.Format("{0}Gateway", sourceSystem), messageType, entityId, "The process cannot be resolved. Please contact the administrator.");
                        ErrorHandler handler = new ErrorHandler();
                        handler.LogError(gatewayException);
                        gatewayException = null;
                        handler = null;
                        return false;
                    }
                }
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {

                string detailedException = "";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        detailedException = detailedException + validationError.ErrorMessage + "; ";
                    }
                }

                //Log exception
                GatewayException gatewayException = new GatewayException(String.Format("{0}Gateway", sourceSystem), messageType, entityId, dbEx.Message, detailedException);
                ErrorHandler handler = new ErrorHandler();
                handler.LogError(gatewayException);
                gatewayException = null;
                handler = null;
                return false;

            }
            catch (Exception ex)
            {
                //Log exception
                GatewayException gatewayException = new GatewayException(String.Format("{0}Gateway", sourceSystem), messageType, entityId, ex.Message, String.IsNullOrEmpty(ex.InnerException.ToString()) ? "" : ex.InnerException.ToString());
                ErrorHandler handler = new ErrorHandler();
                handler.LogError(gatewayException);
                gatewayException = null;
                handler = null;
                return false;
            }
        }

        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage, " * ");
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed: " +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
