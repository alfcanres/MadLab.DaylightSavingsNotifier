namespace DSTN.Application.Helpers
{
    public class OperationResult<T> where T : class
    {
        private T _result;
        private ValidationResult _validatorResponse;

        public T Result { get => _result; set => _result = value; }
        public ValidationResult ValidatorResponse { get => _validatorResponse; set => _validatorResponse = value; }
    }
}
