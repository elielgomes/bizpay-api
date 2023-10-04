using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bizpay_api.Controllers
{
    [ApiController]
    [Route("api/protected")]
    [Authorize]
    public class ProtectedController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProtectedData()
        {
            // Esta rota é protegida por autenticação JWT.
            // Somente solicitações com tokens JWT válidos têm acesso a esta ação.
            return Ok("Dados protegidos acessados com sucesso!");
        }
    }
}
