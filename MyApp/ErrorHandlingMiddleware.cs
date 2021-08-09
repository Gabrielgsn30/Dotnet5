using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.Text.Json;

public class ErrorHandlingMiddleware{
    private readonly RequestDelegate Next;
    
    public ErrorHandlingMiddleware(RequestDelegate next){
        
        this.Next = next;
    }

    public async Task Invoke(HttpContext context){
        try
        {
            //verificacao para ver se esta pegando o middleware nosso
            //System.Console.WriteLine(context.GetEndpoint());
            await Next(context);
        } catch (Exception ex)
        { 
            await HandleExceptionAsync(context,ex);
            //return Conflict(ex.Message);
        }
    }

    private static  Task HandleExceptionAsync(HttpContext context, Exception ex){
        var code = HttpStatusCode.InternalServerError;
        if(ex is Exception){
            code = HttpStatusCode.NotFound;
        }
        //incluir mais opções NullReferenceException por exemplo criar swith cases
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(JsonSerializer.Serialize(new {error = ex.Message}));

    }
}