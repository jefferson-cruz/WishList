using Newtonsoft.Json;
using System;

namespace WishList.Shared.Result
{
    public class OperationResult
    { 
        public static Result OK() => new Result(true, OperationResultType.OK);
        public static Result<T> OK<T>() => new Result<T>(true, OperationResultType.OK);
        public static Result<T> OK<T>(T value) => new Result<T>(true, value, OperationResultType.OK);

        public static Result Created() => new Result(true, OperationResultType.Created);
        public static Result<T> Created<T>() => new Result<T>(true, OperationResultType.Created);
        public static Result<T> Created<T>(T value) => new Result<T>(true, value, OperationResultType.Created);

        public static Result NoContent() => new Result(true, OperationResultType.NoContent);
        public static Result<T> NoContent<T>() => new Result<T>(true, OperationResultType.NoContent);

        public static Result NotFound() => new Result(false, OperationResultType.NotFound);
        public static Result NotFound(string message) => new Result(false, OperationResultType.NotFound, message);
        public static Result<T> NotFound<T>(string message) => new Result<T>(false, OperationResultType.NotFound, message);
        public static Result<T> NotFound<T>(Result operationResult) => new Result<T>(OperationResultType.NotFound, operationResult);

        public static Result Conflict() => new Result(false, OperationResultType.Conflict);
        public static Result Conflict(string message) => new Result(false, OperationResultType.Conflict, message);
        public static Result<T> Conflict<T>(string message) => new Result<T>(false, OperationResultType.Conflict, message);
        public static Result<T> Conflict<T>(Result operationResult) => new Result<T>(OperationResultType.Conflict, operationResult);

        public static Result BadRequest() => new Result(false, OperationResultType.BadRequest);
        public static Result BadRequest(string message) => new Result(false, OperationResultType.BadRequest, message);
        public static Result<T> BadRequest<T>(string message) => new Result<T>(false, OperationResultType.BadRequest, message);
        public static Result<T> BadRequest<T>(Result operationResult) => new Result<T>(OperationResultType.BadRequest, operationResult);

        public static Result InternalServerError() => new Result(false, OperationResultType.InternalServerError);
        public static Result InternalServerError(string message) => new Result(false, OperationResultType.InternalServerError, message);
        public static Result InternalServerError(System.Exception exception) => new Result(OperationResultType.InternalServerError, exception);
        public static Result<T> InternalServerError<T>(string message) => new Result<T>(false, OperationResultType.InternalServerError, message);
        public static Result<T> InternalServerError<T>(System.Exception exception) => new Result<T>(OperationResultType.InternalServerError, exception);
        public static Result<T> InternalServerError<T>(Result operationResult) => new Result<T>(OperationResultType.InternalServerError, operationResult);
    }

    public class Result
    {
        protected Result() { }

        internal Result(bool success, OperationResultType operationResultType)
        {
            Success = success;
            Type = operationResultType;
        }

        internal Result(bool success, OperationResultType operationResultType, string message) : this(success, operationResultType)
        {
            Message = message;
        }

        internal Result(OperationResultType operationResultType, System.Exception exception) : this(false, operationResultType)
        {
            Exception = exception;
        }

        public OperationResultType Type { get; protected set; }

        public bool Success { get; protected set; }

        [JsonIgnore]
        public bool Failure => !Success;

        public string Message { get; protected set; }

        public System.Exception Exception { get; protected set; }
    }

    public class Result<T> : Result
    {
        internal Result(bool success, OperationResultType operationResultType) : base(success, operationResultType)
        {
        }

        internal Result(bool success, T value, OperationResultType operationResultType) : base(success, operationResultType)
        {
            Value = value;
        }

        internal Result(bool success, OperationResultType operationResultType, string message) : base(success, operationResultType, message)
        {

        }

        internal Result(OperationResultType operationResultType, System.Exception exception) : base(operationResultType, exception)
        {

        }

        internal Result(OperationResultType operationResultType, Result operationResult)
        {
            Type = operationResultType;
            Success = operationResult.Success;
            Message = operationResult.Message;
            this.Exception = operationResult.Exception;
        }

        public T Value { get; }
    }

    public enum OperationResultType
    {
        OK = 200,
        Created = 201,
        NoContent = 204,
        BadRequest = 400,
        NotFound = 404,
        Conflict = 409,
        InternalServerError = 500
    }
}
