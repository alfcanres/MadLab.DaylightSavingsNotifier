namespace DSTN.Application.Helpers
{
    public class OperationResult<T> 
    {
        private T _result;
        private ValidationResult _validatorResponse = new ValidationResult();

        public T Data { get => _result; set => _result = value; }
        public ValidationResult ValidatorResponse { get => _validatorResponse; set => _validatorResponse = value; }
    }
}
