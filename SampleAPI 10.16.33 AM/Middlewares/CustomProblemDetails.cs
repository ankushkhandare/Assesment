using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Middlewares;

public class CustomProblemDetails: ProblemDetails
{ 
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}