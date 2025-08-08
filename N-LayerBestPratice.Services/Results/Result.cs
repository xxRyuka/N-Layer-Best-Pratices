namespace N_LayerBestPratice.Services.Results;

public class Result
{
    public bool IsSuccess { get; set; }
    public bool IsFail => !IsSuccess;
    public IReadOnlyList<Error>? Errors { get;  }
    
    protected Result( bool isSuccess, List<Error>? errors = null)
    {
        IsSuccess = isSuccess;
        Errors = (errors ?? new List<Error>()).AsReadOnly();
    }

    public static Result Success()
    {
        return new Result(true, null);
    }
    
    public static Result Failure(params Error[] errors )
    {
        return new Result(false, errors.ToList());
    }
}

public class Result<T> : Result
{
    public T? Data { get;  }

    private Result(bool isSuccess, T? data, List<Error>? errors = null) : base(isSuccess:isSuccess,errors:errors)
    {
        Data = data;
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(true, data, null);
    }
    
        // uyari vermesin diye new keyword kullanildi 
        // sebebi Result ana classinin ayni isimde ve ayni parametreyi alan methodu var , amacimiz override etmek degil
    public new static Result<T> Failure(params Error[] errors )  
    {
        return new Result<T>(false, default, errors.ToList());
    }
}