using System;

namespace Business
{
    public class ServiceResult<T>
    {
        //Result object to use in methods as return type(fail/success)
        public ServiceResultType ResultType { get; private set; }
        public Exception Exception { get; private set; }
        public T Data { get; set; }

        public ServiceResult(T data)
        {
            ResultType = ServiceResultType.Success;
            Data = data;
        }

        public ServiceResult(Exception exception)
        {
            ResultType = ServiceResultType.Fail;
            Exception = exception;
        }
    }
}
