using BookStore.Authors;
using BookStore.Books;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BookStore.EntityFrameworkCore;

public interface IExampleDbContext : IEfCoreDbContext
{
	DbSet<Book>   Books   { get; }
	DbSet<Author> Authors { get; }
}