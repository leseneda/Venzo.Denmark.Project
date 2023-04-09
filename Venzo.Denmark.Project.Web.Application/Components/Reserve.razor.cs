using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Venzo.Denmark.Project.Web.Api.Client;

namespace Venzo.Denmark.Project.Web.Application.Components
{
    public partial class Reserve
    {
        [Inject] IReservationsClient ReservationsClient { get; set; }
        [Inject] ILogger<Reserve> logger { get; set; }
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] protected DialogService DialogService { get; set; }

        [Parameter] public RoomModel Room { get; set; }
        public DateTime? dateFrom { get; set; }
        public DateTime? dateTo { get; set; }
        public int numberOfPeople { get; set; } = 1;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            try
            {
                if (Room == default)
                {
                    NotificationService.Notify(NotificationSeverity.Warning, "Warning", "No room selected.", 5000);

                    return;
                }
            }
            catch (ApiException exception)
            {
                logger.LogError(exception, exception.Message);
            }
        }
        async Task OnButtonClick_ConfirmAsync(MouseEventArgs args)
        {
            bool? result = await DialogService.Confirm($"confirm reservation to room {Room.Name}?",
                options: new ConfirmOptions
                {
                    ShowTitle = false,
                    OkButtonText = "Confirm",
                    CancelButtonText = "Cancel",
                    ShowClose = false
                });

            if (!result.HasValue || !result.Value)
            {
                return;
            }

            var model = new ReservationModel()
            {
                ResourceId = Room.Id,
                DateFrom = new DateTimeOffset(dateFrom.Value),
                DateTo = new DateTimeOffset(dateTo.Value),
                ReservedPeople = numberOfPeople,
            };

            await ReservationsClient.AddReservationAsync(model);

            DialogService.Close();
        }

        void OnButtonClick_CancelAsync(MouseEventArgs args) => DialogService.Close();
    }
}
