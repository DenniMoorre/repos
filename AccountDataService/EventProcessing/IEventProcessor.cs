namespace AccountDataService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}