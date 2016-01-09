using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoCents.Azure.Library.Error
{

    public class ServiceBusException : Exception
    {

        public string ErrorType { get; set; }
        public DateTime ErrorDate { get; set; }
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }


        public ServiceBusException()
        {
        }

        public ServiceBusException(string errorType, string entity, string entityId, string errorMessage)
        {

            this.ErrorType = errorType;
            this.ErrorDate = DateTime.Now;
            this.Entity = entity;
            this.EntityId = entityId;
            this.ErrorMessage = errorMessage;
            this.ErrorDetails = "";

        }

        public ServiceBusException(string errorType, string entity, string entityId, string errorMessage, string errorDetails) 
        {

            this.ErrorType = errorType;
            this.ErrorDate = DateTime.Now;
            this.Entity = entity;
            this.EntityId = entityId;
            this.ErrorMessage = errorMessage;
            this.ErrorDetails = errorDetails;


        }

    }

}
