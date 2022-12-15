using Recipes.Domain.Core;
using Recipes.Domain.Core.Errors;
using Recipes.Domain.Shared;

namespace Recipes.Domain.Enumerations
{
    public abstract class ConfirmationRequestCheckType : Enumeration<ConfirmationRequestCheckType>
    {
        public static ConfirmationRequestCheckType Automatic = new AutomaticCheck();

        public static ConfirmationRequestCheckType Manual = new ManualCheck();

        public static ConfirmationRequestCheckType Unknown = new UnknownCheck();


        protected ConfirmationRequestCheckType(byte value, string name) 
            : base(value, name)
        {
        }

        public abstract Result CanAccept(int? acceptBy);

        public abstract Result CanReject();



        private class AutomaticCheck : ConfirmationRequestCheckType
        {
            public AutomaticCheck() 
                : base(0, "Автоматическая")
            {
            }

            public override Result CanAccept(int? acceptBy)
            {
                if (acceptBy != null)
                    return Result.FromError(ConfirmationRequestErrors.InvalidActionInCheckType());

                return Result.Success();
            }
            public override Result CanReject() => 
                Result.FromError(ConfirmationRequestErrors.InvalidActionInCheckType());
           
        }

        private class ManualCheck : ConfirmationRequestCheckType
        {
            public ManualCheck()
                : base(1, "Ручная")
            {

            }

            public override Result CanAccept(int? acceptBy)
            {
                if (acceptBy == null)
                    return Result.FromError(ConfirmationRequestErrors.InvalidActionInCheckType());

                return Result.Success();
            }

            public override Result CanReject() =>
                Result.FromError(ConfirmationRequestErrors.InvalidActionInCheckType());
            
        }

        private class UnknownCheck : ConfirmationRequestCheckType
        {
            public UnknownCheck()
                : base(2, "Не определен")
            {

            }

            public override Result CanAccept(int? acceptBy) =>
                Result.FromError(ConfirmationRequestErrors.InvalidActionInCheckType());
            public override Result CanReject() =>
                Result.FromError(ConfirmationRequestErrors.InvalidActionInCheckType());

        }
    }
}
