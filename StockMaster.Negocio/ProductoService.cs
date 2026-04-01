using StockMaster.Data;
using StockMaster.Entidades;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;

namespace StockMaster.Negocio {
    public class ProductoService {
        private readonly ApplicationDbContext _context;
        public ProductoService(ApplicationDbContext context) => _context = context;

        public async Task<List<Producto>> ObtenerTodos() => await _context.Productos.ToListAsync();

        public async Task Guardar(Producto p) {
            var existente = await _context.Productos.AsNoTracking().FirstOrDefaultAsync(x => x.Nombre == p.Nombre);
            if (existente != null) _context.Productos.Update(p);
            else _context.Productos.Add(p);
            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(string nombre) {
            var p = await _context.Productos.FindAsync(nombre);
            if (p != null) { _context.Productos.Remove(p); await _context.SaveChangesAsync(); }
        }

        public async Task ImportarExcel(Stream stream) {
            using var workbook = new XLWorkbook(stream);
            var sheet = workbook.Worksheet(1);
            var rows = sheet.RangeUsed().RowsUsed().Skip(1);
            foreach (var row in rows) {
                var p = new Producto {
                    Nombre = row.Cell(1).GetString(),
                    Categoria = row.Cell(2).GetString(),
                    Precio = row.Cell(3).GetValue<decimal>(),
                    Stock = row.Cell(4).GetValue<int>()
                };
                await Guardar(p);
            }
        }

        public async Task<byte[]> ExportarExcel() {
            using var workbook = new XLWorkbook();
            var sheet = workbook.Worksheets.Add("Inventario");
            sheet.Cell(1, 1).Value = "Nombre";
            sheet.Cell(1, 2).Value = "Categoría";
            sheet.Cell(1, 3).Value = "Precio";
            sheet.Cell(1, 4).Value = "Stock";
            var productos = await ObtenerTodos();
            for (int i = 0; i < productos.Count; i++) {
                sheet.Cell(i + 2, 1).Value = productos[i].Nombre;
                sheet.Cell(i + 2, 2).Value = productos[i].Categoria;
                sheet.Cell(i + 2, 3).Value = productos[i].Precio;
                sheet.Cell(i + 2, 4).Value = productos[i].Stock;
            }
            using var ms = new MemoryStream();
            workbook.SaveAs(ms);
            return ms.ToArray();
        }
    }
}
