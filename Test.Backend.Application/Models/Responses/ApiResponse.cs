
namespace Test.Backend.Application.Models.Responses
{
    using FluentValidation;
    using FluentValidation.Results;
    using System.Net;
    using Test.Backend.Application.Behaviours.Response;

    public class ApiResponse<T>
    {
        public int ResponseCode { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
        public ApiResponse()
        {
            ResponseCode = 500;
            Status = false;
        }

        public ApiResponse(int responseCode, string message, bool status, T data)
        {
            ResponseCode = responseCode;
            Message = message;
            Status = status;
            Data = data;
        }
        
        public ApiResponse CreateNotFoundResponse(int entityId, string message)
        {
            return new ApiResponse
            {
                ResponseCode = (int)HttpStatusCode.NoContent,
                Message = message,
                Status = false,
                Data = entityId
            };
        }

        public ApiResponse CreateInternalServerErrorResponse(string errorMessage, string traceExceptionMessage)
        {
            return new ApiResponse
            {
                ResponseCode = (int)HttpStatusCode.InternalServerError,
                Message = errorMessage,
                Status = false,
                Data = traceExceptionMessage
            };
        }

        public ApiResponse ValidateCommand<TCommand>(TCommand command, IEnumerable<IValidator<TCommand>> validators)
        {
            if (validators.Any())
            {
                ValidationContext<TCommand> context = new ValidationContext<TCommand>(command);
                List<ValidationResult> source = validators.Select((IValidator<TCommand> v) => v.Validate(context)).ToList();
                List<ValidationFailure> list = (from f in source.SelectMany((ValidationResult r) => r.Errors)
                                                where f != null
                                                select f).ToList();
                if (list.Count != 0)
                {
                    ValidationErrorResponse validationErrorResponse = new ValidationErrorResponse();
                    foreach (ValidationFailure item in list)
                    {
                        validationErrorResponse.Add(item.PropertyName, new List<string> { item.ErrorMessage });
                    }

                    return new ApiResponse
                    {
                        ResponseCode = (int)HttpStatusCode.BadRequest,
                        Message = "The request contains validation errors.",
                        Status = false,
                        Data = validationErrorResponse
                    };
                }
            }

            return null;
        }

        public ApiResponse CreateUpdateResponse(object entity, string message)
        {
            return new ApiResponse
            {
                ResponseCode = (int)HttpStatusCode.OK,
                Message = message,
                Status = true,
                Data = entity
            };
        }

        public ApiResponse GetResponse(object entity, string message)
        {
            return new ApiResponse
            {
                ResponseCode = (int)HttpStatusCode.OK,
                Message = message,
                Status = true,
                Data = entity
            };
        }

        public ApiResponse GetListAllResponse(IEnumerable<object> entityList, string message)
        {
            return new ApiResponse
            {
                ResponseCode = (int)HttpStatusCode.OK,
                Message = message,
                Status = true,
                Data = entityList.ToList()
            };
        }

    }
}
