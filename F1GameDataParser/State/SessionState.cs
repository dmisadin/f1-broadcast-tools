using F1GameDataParser.Models.Session;
using F1GameDataParser.Services;

namespace F1GameDataParser.State
{
    public class SessionState : StateBase<Session>
    {
        private readonly DriverDetailsBroadcastService driverDetailsBroadcastService;
        public SessionState(DriverDetailsBroadcastService driverDetailsBroadcastService)
        {
            this.driverDetailsBroadcastService = driverDetailsBroadcastService;
        }
        protected override void BeforeModelMerged(Session existingModel, Session newModel)
        {
            if (existingModel.Header.SessionUID != newModel.Header.SessionUID)
            {
                AfterModelAdded(newModel);
            }
        }

        protected override void AfterModelAdded(Session newModel)
        {
            this.driverDetailsBroadcastService.UpdateDrivers();
        }
    }
}
