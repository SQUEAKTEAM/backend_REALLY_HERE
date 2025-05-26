namespace BusinessLogic.Interfaces;

public interface IUserService
{
    Task Register(AuthUserDto authDto, CancellationToken cancellationToken = default);
    Task<TokenDto> Login(AuthUserDto authDto, CancellationToken cancellationToken = default);
    Task<TokenDto> RefreshTokens(string accessToken, string refreshToken);
}
