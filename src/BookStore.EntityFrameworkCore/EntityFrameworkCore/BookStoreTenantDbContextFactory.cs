using Microsoft.EntityFrameworkCore;

namespace BookStore.EntityFrameworkCore;

public class BookStoreTenantDbContextFactory :
    BookStoreDbContextFactoryBase<BookStoreTenantDbContext>
{
    protected override BookStoreTenantDbContext CreateDbContext(
        DbContextOptions<BookStoreTenantDbContext> dbContextOptions)
    {
        return new BookStoreTenantDbContext(dbContextOptions);
    }
}
