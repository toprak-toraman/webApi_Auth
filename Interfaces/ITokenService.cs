using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Interfaces
{
    public interface ITokenService
{
    string CreateToken(User user);
}

}