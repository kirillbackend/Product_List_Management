
namespace ProductListManagement.Service.Mappers.Contracts
{
    public interface IMapperFactory
    {
        T GetMapper<T>() where T : IMapper;
    }
}
