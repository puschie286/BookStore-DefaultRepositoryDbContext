using System;
using System.Threading.Tasks;
using BookStore.Authors;
using BookStore.Books;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace BookStore
{
    public class BookStoreDataSeederContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IAuthorRepository _authorRepository;
		private readonly ICurrentTenant _currentTenant;

		public BookStoreDataSeederContributor(
            IRepository<Book, Guid> bookRepository,
            IAuthorRepository authorRepository,
			ICurrentTenant currentTenant )
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
			_currentTenant = currentTenant;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
			using( _currentTenant.Change( context.TenantId ) )
			{
				 if (await _bookRepository.GetCountAsync() > 0)
				 {
					 return;
				 }

				 var orwell = await _authorRepository.InsertAsync( new Author
				 {
					 BirthDate = new DateTime( 1903, 06, 25 ),
					 Name      = "George Orwell",
					 TenantId  = _currentTenant.Id,
					 ShortBio = "Orwell produced literary criticism and poetry, fiction and polemical journalism; and is best known for the allegorical novella Animal Farm (1945) and the dystopian novel Nineteen Eighty-Four (1949).",
				 }, true );

				 var douglas = await _authorRepository.InsertAsync( new Author
				 {
					 BirthDate = new DateTime( 1952, 03, 11 ),
					 Name      = "Douglas Adams",
					 ShortBio  = "Douglas Adams was an English author, screenwriter, essayist, humorist, satirist and dramatist. Adams was an advocate for environmentalism and conservation, a lover of fast cars, technological innovation and the Apple Macintosh, and a self-proclaimed 'radical atheist'.",
					 TenantId  = _currentTenant.Id
				 }, true );
                
				 await _bookRepository.InsertAsync(
					 new Book
					 {
						 AuthorId    = orwell.Id, // SET THE AUTHOR
						 Name        = "1984",
						 PublishDate = new DateTime(1949, 6, 8),
						 Price       = 19.84f
					 },
					 autoSave: true
				 );
                
				 await _bookRepository.InsertAsync(
					 new Book
					 {
						 AuthorId    = douglas.Id, // SET THE AUTHOR
						 Name        = "The Hitchhiker's Guide to the Galaxy",
						 PublishDate = new DateTime(1995, 9, 27),
						 Price       = 42.0f
					 },
					 autoSave: true
				 );
			}
		}
    }
}
