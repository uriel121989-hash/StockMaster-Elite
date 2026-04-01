using Microsoft.AspNetCore.Mvc;
using StockMaster.Negocio;
using StockMaster.Entidades;

namespace StockMaster.Web.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase {
        private readonly ProductoService _service;
        public ProductosController(ProductoService s) => _service = s;

        [HttpGet] public async Task<List<Producto>> Get() => await _service.ObtenerTodos();

        [HttpGet("exportar")]
        public async Task<IActionResult> Exportar() {
            var file = await _service.ExportarExcel();
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Inventario_Elite.xlsx");
        }

        [HttpPost] 
        public async Task<IActionResult> Post([FromBody] Producto p) {
            await _service.Guardar(p);
            return Ok();
        }

        [HttpPost("importar")]
        public async Task<IActionResult> Importar(IFormFile archivo) {
            if (archivo == null || archivo.Length == 0) return BadRequest();
            using var stream = archivo.OpenReadStream();
            await _service.ImportarExcel(stream);
            return Ok();
        }

        [HttpDelete("{nombre}")]
        public async Task<IActionResult> Delete(string nombre) {
            await _service.Eliminar(nombre);
            return Ok();
        }
    }
}
