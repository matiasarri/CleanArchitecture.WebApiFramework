using System;
using CleanArchitecture.WebApiFramework.Domain.Entities;

namespace CleanArchitecture.WebApiFramework.Domain.Common
{
    public interface IAuditableEntity : IEntity
    {
        string CreatedBy { get; set; }

        DateTime CreatedOn { get; set; }

        string LastModifiedBy { get; set; }

        DateTime? LastModifiedOn { get; set; }
    }
}