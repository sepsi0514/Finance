using Microsoft.EntityFrameworkCore;

namespace DAO.DBModels;

public partial class FinanceDatasContext : DbContext
{
    public FinanceDatasContext()
    {
    }

    public FinanceDatasContext(DbContextOptions<FinanceDatasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        try
        {
            Database.Migrate();
        }
        catch (Exception)
        {
            Console.WriteLine("Database already exist!");
        }        

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.Property(e => e.Uid).HasColumnName("UID");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.Property(e => e.Uid).HasColumnName("UID");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.Property(e => e.Uid).HasColumnName("UID");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.StateId).HasColumnName("StateID");
            entity.Property(e => e.Tscreation).HasColumnName("TSCreation");
            entity.Property(e => e.Tstransaction).HasColumnName("TSTransaction");
            entity.Property(e => e.WalletId).HasColumnName("WalletID");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.Property(e => e.Uid).HasColumnName("UID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
