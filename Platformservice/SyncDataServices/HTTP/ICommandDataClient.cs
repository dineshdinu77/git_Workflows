using Platformservice.Dtos;

namespace Platformservice.SyncDataServices.HTTP
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto plat);

    }
}
