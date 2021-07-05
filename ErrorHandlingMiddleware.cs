using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class ErrorHandlingMiddleware{
    private readonly RequestDelegate Next;
    
    public ErrorHandlingMiddleware(RequestDelegate next){
        
        this.Next = next;
    }

    public async Task Invoke(HttpContext context){
        //verificacao para ver se esta pegando o middleware nosso
        System.Console.WriteLine("teste middleware");
        await Next(context);
    }
}