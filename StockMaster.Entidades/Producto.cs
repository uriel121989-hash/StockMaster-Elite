namespace StockMaster.Entidades
{
    public class Producto
    {
        // Eliminamos el Id porque en tu Supabase no existe esa columna
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; } = string.Empty;
    }
}
