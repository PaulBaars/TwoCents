using TwoCents.Azure.Edmx.Common;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace TwoCents.Azure.Library.Error
{
    public class ErrorHandler
    {

        public void LogError(GatewayException gatewayException)
        {

            using (EntityModelCommon contextCommon = new EntityModelCommon())
            {

                Fault fault = new Fault();
                fault.Entity = gatewayException.Entity;
                fault.EntityId = gatewayException.EntityId;
                fault.ErrorMessage = gatewayException.ErrorMessage;
                fault.ErrorDetails = gatewayException.ErrorDetails;
                fault.ErrorDate = gatewayException.ErrorDate;
                fault.ErrorType = gatewayException.ErrorType;

                contextCommon.Faults.Add(fault);
                contextCommon.SaveChanges();
            }

        }

        public void LogError(ServiceBusException serviceBusException)
        {

            using (EntityModelCommon contextCommon = new EntityModelCommon())
            {

                Fault fault = new Fault();
                fault.Entity = serviceBusException.Entity;
                fault.EntityId = serviceBusException.EntityId;
                fault.ErrorMessage = serviceBusException.ErrorMessage;
                fault.ErrorDetails = serviceBusException.ErrorDetails;
                fault.ErrorDate = serviceBusException.ErrorDate;
                fault.ErrorType = serviceBusException.ErrorType;

                contextCommon.Faults.Add(fault);
                contextCommon.SaveChanges();
            }

        }

    }
}
