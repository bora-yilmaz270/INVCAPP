using INVCAPP.Core.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace INVCAPP.API.Validations
{
    public class ValidateInvoiceCreateDtoAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.ContainsKey("invoiceCreateDto"))
            {
                var invoiceCreateDto = context.ActionArguments["invoiceCreateDto"] as InvoiceCreateDto;

                if (invoiceCreateDto != null)
                {
                    
                    if (string.IsNullOrWhiteSpace(invoiceCreateDto.InvoiceHeader?.InvoiceId))
                    {
                        context.Result = new BadRequestObjectResult(CustomResponseDto<NoContent>.Fail( 400,new List<string> { "Invoice header cannot be empty." }));
                        return;
                       
                    }

                    
                    if (!IsValidEmail(invoiceCreateDto.InvoiceHeader?.Email))
                    {
                        context.Result = new BadRequestObjectResult(CustomResponseDto<NoContent>.Fail(400,new List<string> { "Invalid email address." }));
                        return;
                    }
                }
            }

            await next();
        }
     
        private bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
            return regex.IsMatch(email);
        }
    }
}
