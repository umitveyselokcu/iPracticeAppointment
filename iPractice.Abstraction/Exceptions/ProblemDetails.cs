using System;

namespace iPractice.Abstraction.Exceptions
{
    public class ProblemDetails : BaseProblemDetails
    {
        public ProblemDetails(Exception ex)
        {
            Type = this.GetType();
            StackTrace = ex.StackTrace;
            Message = ex.Message;
        }
    }
}