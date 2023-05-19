using Microsoft.EntityFrameworkCore;
using PenjualanUMKM.Models;

namespace PenjualanUMKM.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Produk> Produks { get; set; }
        public DbSet<Pelanggan> Pelanggans { get; set; }
        public DbSet<LogUMKM> LogUMKMs { get; set; }
        public DbSet<Antrian> Antrians { get; set; }
        public DbSet<Penjualan> Penjualans { get; set; }
        public DbSet<Transaksi> Transaksis { get; set; }
        public DbSet<User> Users { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
