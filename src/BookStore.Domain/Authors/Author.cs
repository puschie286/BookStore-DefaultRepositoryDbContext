using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace BookStore.Authors
{
    public class Author : FullAuditedAggregateRoot<Guid>,
		IMultiTenant
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string ShortBio { get; set; }
		public Guid? TenantId { get; set; }
	}
}
