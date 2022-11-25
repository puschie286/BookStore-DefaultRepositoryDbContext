using BookStore.Authors;
using BookStore.Books;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Gdpr;
using Volo.Abp.OpenIddict.EntityFrameworkCore;

namespace BookStore.EntityFrameworkCore;

[ReplaceDbContext( typeof( IExampleDbContext ) )]
public abstract class BookStoreDbContextBase<TDbContext> : AbpDbContext<TDbContext>,
	IExampleDbContext
    where TDbContext : DbContext
{
	public DbSet<Book> Books { get; set; }
	public DbSet<Author> Authors { get; set; }

    public BookStoreDbContextBase(DbContextOptions<TDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureGdpr();

        /* Configure your own tables/entities inside here */

		builder.Entity<Book>(b =>
		{
			b.ToTable(BookStoreConsts.DbTablePrefix + "Books",
					  BookStoreConsts.DbSchema);
			b.ConfigureByConvention(); //auto configure for the base class props
			b.Property(x => x.Name).IsRequired().HasMaxLength(128);

			b.HasOne<Author>().WithMany().HasForeignKey(x => x.AuthorId).IsRequired();
		});

		builder.Entity<Author>(b =>
		{
			b.ToTable(BookStoreConsts.DbTablePrefix + "Authors",
					  BookStoreConsts.DbSchema);

			b.ConfigureByConvention();

			b.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(128);

			b.HasIndex(x => x.Name);
		});
    }
}
