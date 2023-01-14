using passport.Dtos;

namespace passport.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPassport(PassportPublishedDto passportPublishedDto);
    }
}