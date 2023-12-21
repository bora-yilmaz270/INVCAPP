namespace INVCAPP.Core.Services;

public interface IEmailService
{
    Task ProcessAndSendEmailForUnprocessedInvoices();
}