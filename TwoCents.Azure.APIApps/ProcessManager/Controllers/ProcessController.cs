using Microsoft.Azure.AppService.ApiApps.Service;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TRex.Metadata;
using TwoCents.Azure.Edmx.Common;
using TwoCents.Azure.Library.Error;

namespace ProcessManager.Controllers
{
    public class ProcessController : ApiController
    {

        HttpResponseMessage response;

        [HttpGet]
        [ActionName("TriggerProcessStep")]
        [Trigger(TriggerType.Poll, typeof(ProcessStepTrigger))]
        [Metadata("Trigger Process Step", "Trigger Process Step")]
        [Route("api/Process/TriggerProcessStep/{processStep}")]
        public HttpResponseMessage TriggerProcessStep(string triggerState, [Metadata("ProcessStep", "Name of the process step")] string processStep)
        {

            try
            {

                ProcessStepTrigger trigger;

                using (EntityModelCommon contextCommon = new EntityModelCommon())
                {

                    trigger = (from PF in contextCommon.ProcessFlows
                               join PS in contextCommon.ProcessSteps
                               on PF.StepId equals PS.StepId
                               where PF.Status == "Pending" && PS.Name == processStep && PF.StartDate == null
                               orderby PF.SequenceId ascending
                               select new ProcessStepTrigger { ProcessFlowId = PF.SequenceId, EntityId = PF.EntityId }).FirstOrDefault();

                    // Add startdate to the processstep
                    // Set polling action of trigger
                    if (trigger != null)
                    {

                        ProcessFlow currentStep = (from PF in contextCommon.ProcessFlows
                                                   where PF.SequenceId == trigger.ProcessFlowId
                                                   select PF).FirstOrDefault();

                        if (currentStep != null)
                        {
                            contextCommon.ProcessFlows.Attach(currentStep);
                            currentStep.StartDate = DateTime.Now;
                            contextCommon.SaveChanges();
                        }
                        else
                        {
                            throw new ApplicationException("Cannot set the startdate of the process step. Process step is not found based on the supplied ProcessFlowId.");
                        }

                        //If data -> Call EventTriggered -> triggerState = "ProcessStep is kicked off"
                        triggerState = "ProcessStep is kicked off";
                        return Request.EventTriggered(trigger, triggerState: triggerState, pollAgain: new TimeSpan(0, 1, 0));
                    }
                    else
                    {
                        //If no data -> Call EventWaitPoll -> triggerState = "ProcessStep is not kicked off"
                        triggerState = "ProcessStep is not kicked off";
                        return Request.EventWaitPoll(retryDelay: new TimeSpan(0, 1, 0), triggerState: triggerState);
                    }

                }
            }
            catch (Exception ex)
            {

                string error = String.Format("Exception occurred when triggering process step {0}", processStep);
                ServiceBusException serviceBusException = new ServiceBusException("ProcessManager", "ProcessFlow", "", error, ex.Message);
                ErrorHandler handler = new ErrorHandler();
                handler.LogError(serviceBusException);
                serviceBusException = null;
                handler = null;

                response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.InternalServerError;
                string errorMsg = ex.Message.Replace("\r\n", string.Empty);
                errorMsg = errorMsg.Replace("\n", string.Empty);
                response.ReasonPhrase = errorMsg;
                return response;

            }

        }

        [HttpPost]
        [Metadata("Advance Process", "Advance process after execution of process step")]
        [ActionName("AdvanceProcess")]
        [Route("api/Process/AdvanceProcess/{sequenceId}")]
        public HttpResponseMessage AdvanceProcess([Metadata("SequenceId", "Unique identifier of the processflow")]
                                        long sequenceId)
        {

            try
            {

                using (EntityModelCommon contextCommon = new EntityModelCommon())
                {
                    ProcessFlow currentStep = (from PF in contextCommon.ProcessFlows
                                               where PF.SequenceId == sequenceId
                                               select PF).FirstOrDefault();

                    if (currentStep != null)
                    {

                        if (currentStep.IsLastStep == false)
                        {

                            ProcessFlow nextStep = (from PF in contextCommon.ProcessFlows
                                                    where PF.EntityId == currentStep.EntityId && PF.ProcessId == currentStep.ProcessId && PF.Sequence == currentStep.Sequence + 1
                                                    select PF).FirstOrDefault();

                            if (nextStep != null)
                            {
                                contextCommon.ProcessFlows.Attach(nextStep);
                                nextStep.Status = "Pending";
                            }
                            else
                            {
                                throw new ApplicationException("Process step is indicated not to be the last step, but the next step is not found. Check the process definition in the database.");
                            }

                        }

                        contextCommon.ProcessFlows.Attach(currentStep);
                        currentStep.Status = "Completed";
                        currentStep.EndDate = DateTime.Now;
                        contextCommon.SaveChanges();

                    }
                    else
                    {
                        throw new ApplicationException("Process step is not found based on the supplied ProcessFlowId.");
                    }

                }

                response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.Accepted;
                return response;

            }
            catch (Exception ex)
            {

                string error = "Exception occurred when advancing the process.";
                ServiceBusException serviceBusException = new ServiceBusException("ProcessManager", "ProcessFlow", sequenceId.ToString(), error, ex.Message);
                ErrorHandler handler = new ErrorHandler();
                handler.LogError(serviceBusException);
                serviceBusException = null;
                handler = null;

                response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.InternalServerError;
                string errorMsg = ex.Message.Replace("\r\n", string.Empty);
                errorMsg = errorMsg.Replace("\n", string.Empty);
                response.ReasonPhrase = errorMsg;
                return response;

            }

        }

    }
}
