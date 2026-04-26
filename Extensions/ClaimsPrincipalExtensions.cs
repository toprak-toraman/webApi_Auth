using System.Security.Claims;

namespace wepAPI_denemeler.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(value, out int userId) ? userId : 0;
        }
    }
}