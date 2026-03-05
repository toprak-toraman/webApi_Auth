using Microsoft.AspNetCore.Mvc;

namespace wepAPI_denemeler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<T> : ControllerBase
    {
        protected readonly ILogger<T> _logger;

        protected BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        // Buraya bütün API'lerde ortak olan metodları ilerde ekleyebilirsin
        // Örneğin: Mevcut giriş yapmış kullanıcı ID'sini dönen bir metod
    }
}