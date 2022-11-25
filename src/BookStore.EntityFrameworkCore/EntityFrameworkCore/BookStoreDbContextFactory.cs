using Microsoft.EntityFrameworkCore;

namespace BookStore.EntityFrameworkCore;

public class BookStoreDbContextFactory :
    BookStoreDbContextFactoryBase<BookStoreDbContext>
{
    protected override BookStoreDbContext CreateDbContext(
        DbContextOptions<BookStoreDbContext> dbContextOptions)
    {
        return new BookStoreDbContext(dbContextOptions);
    }
}
