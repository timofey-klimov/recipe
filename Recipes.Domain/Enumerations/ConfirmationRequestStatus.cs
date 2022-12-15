using Recipes.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain.Enumerations
{
    public class ConfirmationRequestStatus : Enumeration<ConfirmationRequestStatus>
    {

        public static ConfirmationRequestStatus Accepted = new AcceptedStatus();
        public static ConfirmationRequestStatus Pending = new PendingStatus();
        public static ConfirmationRequestStatus Rejected = new RejectedStatus();

        public ConfirmationRequestStatus(byte value, string name) 
            : base(value, name)
        {

            
        }

        private class AcceptedStatus : ConfirmationRequestStatus
        {
            public AcceptedStatus()
                : base(0, "Опубликовано")
            {

            }
        }

        private class PendingStatus: ConfirmationRequestStatus
        {
            public PendingStatus()
                : base(1, "В ожидании")
            {

            }
        }

        private class RejectedStatus : ConfirmationRequestStatus
        {
            public RejectedStatus()
                : base(2, "Отклонено")
            {

            }
        }
    }
}
