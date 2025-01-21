using ServiceContracts;

namespace Services;

public class CitiesService : ICitiesService, IDisposable
{
    private List<string> _cities;
    
    public Guid InstanceId { get; }

    public CitiesService()
    {
        InstanceId = Guid.NewGuid();
        _cities = new List<string>()
        {
            "Beijing",
            "Shanghai",
            "Guangzhou",
            "Shenzhen",
        };
    }

    public List<string> GetCities()
    {
        return _cities;
    }

    public void Dispose()
    {
        
    }
}
