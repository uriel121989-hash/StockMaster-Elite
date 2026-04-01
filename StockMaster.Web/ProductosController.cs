using Microsoft.AspNetCore.Mvc;
using StockMaster.Negocio;
using StockMaster.Entidades;

namespace StockMaster.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoService _service;
        public ProductosController(ProductoService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get() => await _service.ObtenerTodos();

        [HttpPost]
        public async Task<IActionResult> Post(Producto p) {
            await _service.Crear(p);
            return Ok();
        }

        [HttpDelete("{nombre}")]
        public async Task<IActionResult> Delete(string nombre) {
            await _service.Eliminar(nombre);
            return Ok();
        }

        [HttpPost("importar")]
        public async Task<IActionResult> Import(IFormFile archivo) {
            using var s = archivo.OpenReadStream();
            await _service.ImportarExcel(s);
            return Ok();
        }

        [HttpGet("exportar")]
        public async Task<IActionResult> Export() {
            var l = await _service.ObtenerTodos();
            return File(_service.ExportarExcel(l), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Inventario_Uriel.xlsx");
        }
    }
}
