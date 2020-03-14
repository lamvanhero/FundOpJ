using System;

namespace OppJar.Domain
{
    public interface IAudit
    {
        DateTime? CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
    }
}
