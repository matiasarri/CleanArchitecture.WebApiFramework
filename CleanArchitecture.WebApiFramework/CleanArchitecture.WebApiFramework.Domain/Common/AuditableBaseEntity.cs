using System;
using System.Collections.Generic;
using System.Text;


namespace CleanArchitecture.WebApiFramework.Domain.Common
{
    public abstract class AuditableBaseEntity : IAuditableEntity
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }

}
