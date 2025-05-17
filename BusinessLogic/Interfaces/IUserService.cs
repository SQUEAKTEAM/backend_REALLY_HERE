namespace BusinessLogic.Interfaces;

public interface IUserService
{
    Task Register(AuthUserDto authDto, CancellationToken cancellationToken = default);
    Task<string> Login(AuthUserDto authDto, CancellationToken cancellationToken = default);
}
