using StockMaster.Data;
using StockMaster.Entidades;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;

namespace StockMaster.Negocio
{
    public class ProductoService
    {
        private readonly ApplicationDbContext _context;
        public ProductoService(ApplicationDbContext context) => _context = context;

        public async Task<List<Producto>> ObtenerTodos() => await _context.Productos.ToListAsync();

        public async Task Crear(Producto p) {
            _context.Productos.Add(p);
            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(string nombre) {
            var p = await _context.Productos.FirstOrDefaultAsync(x => x.Nombre == nombre);
            if (p != null) { _context.Productos.Remove(p); await _context.SaveChangesAsync(); }
        }

        public async Task ImportarExcel(Stream s) {
            using var wb = new XLWorkbook(s);
            var rows = wb.Worksheet(1).RowsUsed().Skip(1);
            foreach (var r in rows) {
                var n = r.Cell(1).GetValue<string>();
                var p = await _context.Productos.FirstOrDefaultAsync(x => x.Nombre == n);
                if (p == null) { p = new Producto { Nombre = n }; _context.Productos.Add(p); }
                p.Categoria = r.Cell(2).GetValue<string>();
                p.Precio = r.Cell(3).GetValue<decimal>();
                p.Stock = r.Cell(4).GetValue<int>();
            }
            await _context.SaveChangesAsync();
        }

        public byte[] ExportarExcel(List<Producto> l) {
            using var b = new XLWorkbook();
            var h = b.Worksheets.Add("Inventario");
            h.Cell(1,1).Value="Nombre"; h.Cell(1,2).Value="Categoria"; h.Cell(1,3).Value="Precio"; h.Cell(1,4).Value="Stock";
            for(int i=0; i<l.Count; i++) {
                h.Cell(i+2,1).Value=l[i].Nombre; h.Cell(i+2,2).Value=l[i].Categoria;
                h.Cell(i+2,3).Value=l[i].Precio; h.Cell(i+2,4).Value=l[i].Stock;
            }
            using var ms = new MemoryStream(); b.SaveAs(ms); return ms.ToArray();
        }
    }
}
