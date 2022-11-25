using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace BookStore.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class BookStoreTenantDbContext : BookStoreDbContextBase<BookStoreTenantDbContext>
{
    public BookStoreTenantDbContext(DbContextOptions<BookStoreTenantDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.SetMultiTenancySide(MultiTenancySides.Tenant);

        base.OnModelCreating(builder);
    }
}
