using AutoMapper;


namespace Infrastructure.Configuration.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateCommandMaps();

        CreateQueryMaps();
    }

    private void CreateCommandMaps()
    {
    }

    private void CreateQueryMaps()
    {
    }
}
