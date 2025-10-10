using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSTN.AdminApp.WinForms.ViewModels
{
    public class ServiceResult<T>
    {
        private ResultStatus _status = ResultStatus.Success;
        private List<string> _messages = new List<string>();
        private readonly T _defaultData = default(T);   

        public T Data => _defaultData;
        public ResultStatus Status => _status;
        public IReadOnlyList<string> Messages => _messages.AsReadOnly();

        public ServiceResult(APIResponse<T> apiResponse)
        {
            _defaultData = apiResponse.Data;
            _messages = new List<string>(apiResponse.ValidatorResponse.MessageList);
            if(!apiResponse.ValidatorResponse.IsValid)
            {
                _status = ResultStatus.ValidationError;
            }
            else
            {
                _status = ResultStatus.Success;
            }

        }

        public ServiceResult(string errorMessage)
        {
            _status = ResultStatus.ServerError;
            _messages.Add(errorMessage);
        }

    }

    public enum ResultStatus
    {
        Success,
        ValidationError,
        ServerError,
    }
}
