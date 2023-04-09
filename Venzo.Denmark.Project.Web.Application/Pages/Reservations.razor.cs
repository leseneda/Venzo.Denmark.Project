using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using System;
using System.Net;
using Venzo.Denmark.Project.Web.Api.Client;
using Venzo.Denmark.Project.Web.Application.Components;

namespace Venzo.Denmark.Project.Web.Application.Pages
{
    [Route("/Reservations")]
    public partial class Reservations
    {
        [Inject] IRoomsClient RoomsClient { get; set; }
        [Inject] ILogger<Reservations> Logger { get; set; }
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] IConfiguration Configuration { get; set; }
        [Inject] protected DialogService DialogService { get; set; }

        bool isLoading;
        int roomsCount;
        int pageSize = 0;
        ICollection<RoomModel> rooms;
        RadzenDataGrid<RoomModel> dataGrid;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            try
            {
                pageSize = Configuration.GetRequiredSection("Project:Application:GridPagesize").Get<int>();
            }
            catch (ApiException exception)
            {
                Logger.LogError(exception, exception.Message);
                NotificationService.Notify(NotificationSeverity.Error, "Error", "An error happened, try again or contact the tech support.", 5000);
            }
        }

        async Task DataGrid_OnLoadDataAsync(LoadDataArgs args)
        {
            isLoading = true;

            rooms = Enumerable.Empty<RoomModel>().ToList();
            roomsCount = 0;

            try
            {
                var result = await RoomsClient.GetAllAsync(args.Skip, args.Top);

                if (result != default)
                {
                    rooms = result.Items;
                    roomsCount = result.Count;
                }
            }
            catch (ApiException exception) when (exception.StatusCode == (int)HttpStatusCode.NoContent)
            {
                NotificationService.Notify(NotificationSeverity.Warning, "Warning!", "There is no data to be displayed.", 5000);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, exception.Message);

                NotificationService.Notify(NotificationSeverity.Error, "Error", "An error happened, try again or contact the tech support.", 5000);
            }
            finally
            {
                isLoading = false;
            }
        }

        #region Row Grid events

        async Task AddReservation_OnCLickRowAsync(RoomModel room)
        {

            try
            {
                dynamic integrateContractResult = await DialogService.OpenAsync<Reserve>("Reserve",
                    parameters: new Dictionary<string, object> { { "Room", room } },
                    options: new DialogOptions()
                    {
                        ShowTitle = false,
                        Style = "width: 965px; top: 10%; align:center",
                        CloseDialogOnEsc = true,
                        CloseDialogOnOverlayClick= true,
                        ShowClose = true,
                    });
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, exception.Message);
                NotificationService.Notify(NotificationSeverity.Error, "Error", "An error happened, try again or contact the tech support.", 5000);
            }
            finally
            {
                DialogService.Close();
                await dataGrid.Reload();
            }
        }

        #endregion

    }
}
