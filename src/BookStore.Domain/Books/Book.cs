using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace BookStore.Books
{
    public class Book : AuditedAggregateRoot<Guid>,
		IMultiTenant
    {
        public Guid AuthorId { get; set; }

        public string Name { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
		
		public Guid? TenantId { get; }
	}
}
