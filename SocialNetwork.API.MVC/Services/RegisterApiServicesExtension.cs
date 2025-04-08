namespace SocialNetwork.API.MVC.Services;

public static class RegisterApiServicesExtension
{
    public static void RegisterApiServices(this IServiceCollection services)
    {
        services.AddSingleton<IPhotoService, PhotoService>();
    }
}
