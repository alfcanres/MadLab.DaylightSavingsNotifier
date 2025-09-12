using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DSTN.Application.Helpers
{
    public class ValidationResult
    {
        bool isValid = true;
        List<string> messageList = new List<string>();

        public List<string> MessageList
        {
            get
            {
                return messageList;
            }
        }

        public ValidationResult()
        {
        }

        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }


        public void AddMessage(string error)
        {
            messageList.Add(error);
        }

        public void AddError(string error)
        {
            messageList.Add(error);
            if (IsValid)
                IsValid = false;
        }

        public void Clear()
        {
            messageList.Clear();
            isValid = true;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
