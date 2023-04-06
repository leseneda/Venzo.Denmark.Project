using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using System.Net;
using Venzo.Denmark.Project.Web.Api.Client;

namespace Venzo.Denmark.Project.Web.Application.Pages
{
    [Route("/Customers")]
    public partial class Customers
    {
        [Inject] ICustomersClient CustomersClient { get; set; }
        [Inject] ILogger<Customers> Logger { get; set; }
        [Inject] NotificationService NotificationService { get; set; }

        bool isLoading;
        int customersCount;
        int pageSize = 0;
        ICollection<CustomerModel> customers;
        RadzenDataGrid<CustomerModel> dataGrid;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            try
            {
                pageSize = 15;

            }
            catch (ApiException exception)
            {
                Logger.LogError(exception, exception.Message);
                NotificationService.Notify(NotificationSeverity.Error, "Error", "A error happens, Try again or contact the tech support.", 5000);
            }
        }

        async Task DataGrid_OnLoadDataAsync(LoadDataArgs args)
        {
            isLoading = true;

            customers = Enumerable.Empty<CustomerModel>().ToList();
            customersCount = 0;

            try
            {
                var result = await CustomersClient.GetCustomersAsync(args.Skip, args.Top);

                if (result != null)
                {
                    customers = result.Items;
                    customersCount = result.Count;
                }
            }
            catch (ApiException exception) when (exception.StatusCode == (int)HttpStatusCode.NoContent)
            {
                NotificationService.Notify(NotificationSeverity.Warning, "Warning!", "There is no data to be displayed.", 5000);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, exception.Message);
                NotificationService.Notify(NotificationSeverity.Error, "Error", "A error happens, Try again or contact the tech support.", 5000);
            }
            finally
            {
                isLoading = false;
            }
        }
    }
}
