namespace N_LayerBestPratice.Services.Results;

public class Result
{
    public bool IsSuccess => Status == ResultStatus.Success;
    public bool IsFail => !IsSuccess;
    public IReadOnlyList<Error> Errors { get; }

    public ResultStatus Status { get; }

    protected Result(ResultStatus status, List<Error>? errors = null)
    {
        // IsSuccess = isSuccess;
        Status = status;
        Errors = (errors ?? new List<Error>()).AsReadOnly();
    }

    public static Result Success()
    {
        return new Result(ResultStatus.Success);
    }

    public static Result Failure(ResultStatus status, params Error[] errors)
    {
        if (status == ResultStatus.Success)
            throw new ArgumentException("Failure cannot have Success status.", nameof(status));
        return new Result(status, errors.ToList());
    }
}

public class Result<T> : Result
{
    public T? Data { get; }

    private Result( ResultStatus status,T? data, List<Error>? errors = null) : base(status: status, errors: errors)
    {
        Data = data;
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(  ResultStatus.Success, data);
    }

    // uyari vermesin diye new keyword kullanildi 
    // sebebi Result ana classinin ayni isimde ve ayni parametreyi alan methodu var , amacimiz override etmek degil
    public new static Result<T> Failure(ResultStatus status, params Error[] errors)
    {
        if (status == ResultStatus.Success)
            throw new ArgumentException("Failure cannot have Success status.", nameof(status));
        return new Result<T>(status, default,errors.ToList());
    }
}