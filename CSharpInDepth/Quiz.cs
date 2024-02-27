using System.Runtime.CompilerServices;

namespace CSharpInDepth;

using static Utils;

public static class Quiz
{
    #region Question 1

    // These two methods are equivalent
    
    public static Task<string> Question1_A_Async()
    {
        Log();
        DoThing();
        return DoOtherThingAsync();
    }

    public static async Task<string> Question1_B_Async()
    {
        Log();
        DoThing();
        return await DoOtherThingAsync();
    }

    #endregion

    #region Question 2

    // These methods are NOT equivalent
    
    public static Task<string> Question2_A_Async()
    {
        Log(msg: "Notice who catches the exception");
        try
        {
            return ThrowsExceptionAsync();
        }
        catch (Exception)
        {
            return Task.FromResult("Caught error inside of " + nameof(Question2_A_Async));
        }
    }

    public static async Task<string> Question2_B_Async()
    {
        Log();
        try
        {
            return await ThrowsExceptionAsync();
        }
        catch (Exception)
        {
            return "Caught error inside of " + nameof(Question2_B_Async);
        }
    }

    #endregion

    #region Question 3

    // These methods are NOT equivalent
    
    public static Task<string> Question3_A_Async()
    {
        Log(msg: $"Note that the resource is disposed before {nameof(DoOtherThingAsync)} finishes");
        using var resource = new SomeResource();
        return DoOtherThingAsync(resource);
    }

    public static async Task<string> Question3_B_Async()
    {
        Log();
        using var resource = new SomeResource();
        return await DoOtherThingAsync(resource);
    }

    #endregion

    #region Support

    static void DoThing([CallerMemberName] string callerName = "") =>
        Log(msg: "From " + callerName);

    static async Task<string> DoOtherThingAsync(
        IDisposable? resource = null,
        [CallerMemberName] string callerName = ""
    )
    {
        Log(msg: $"From {callerName}");
        await Task.Delay(TimeSpan.FromSeconds(1));
        Log(msg: $"From {callerName} - After delay");
        return "result";
    }

    static Task<string> ThrowsExceptionAsync() =>
        Task.FromException<string>(new Exception("Where do I get caught?"));

    class SomeResource : IDisposable
    {
        private readonly string _caller;

        public SomeResource([CallerMemberName] string callerName = "")
        {
            _caller = callerName;
            Log(msg: $"From {callerName}", callingMethod: nameof(SomeResource) + " Constructor");
        }

        public void Dispose() =>
            Log(
                msg: $"From {_caller}",
                callingMethod: nameof(SomeResource) + "." + nameof(IDisposable.Dispose)
            );
    }

    #endregion
}
