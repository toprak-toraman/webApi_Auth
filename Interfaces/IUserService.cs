using wepAPI_denemeler.DTOs;
using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Interfaces
{
    // IBaseService<User>'dan miras alarak temel CRUD işlemlerini de dahil ediyoruz
    public interface IUserService : IBaseService<User>
    {

        Task<bool> UpdateUserAsync(int id, UserUpdateDto dto);
        //Task<bool> ChangeNameAsync(int userId, string newName);
    }
}