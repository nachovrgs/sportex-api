using System;
using System.Collections.Generic;
using System.Text;

namespace sportex.api.domain.EventClasses
{
    public class EventResult
    {
        public int ResultStatus { get; set; }
        public string ResultMessage { get; set; }

        public EventResult(int status, string message)
        {
            this.ResultStatus = status;
            this.ResultMessage = message;
        }
        public EventResult()
        {
            this.ResultStatus = 0;
            this.ResultMessage = "";
        }
    }
}
