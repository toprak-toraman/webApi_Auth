using wepAPI_denemeler.DTOs;

namespace wepAPI_denemeler.Interfaces
{
    public interface IUserProfileService
    {
        Task<bool> UpdateMyProfile(int userId, ProfileUpdateDto dto);

    }
}