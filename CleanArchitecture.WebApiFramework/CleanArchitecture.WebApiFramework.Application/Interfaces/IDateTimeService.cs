using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApiFramework.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
