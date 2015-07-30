using Xen.Entity;

namespace Xen.SOA.Base
{
    public class BaseServiceOperation 
    {
        public IOperationResult PreProcessing(UserContext userContext)
        {
            IOperationResult operationResult = new OperationResult();
            operationResult.CrudStatus = CrudStatusType.ValidUserContext;
            operationResult.OperationStatus = OperationStatusType.Successful;

            return operationResult;
        }

        public void PostProcessing()
        {

        }
    }
}
