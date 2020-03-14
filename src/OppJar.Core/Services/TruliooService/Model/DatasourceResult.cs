using System.Collections.Generic;
using OppJar.Core.Services.Model.Errors;

namespace OppJar.Core.Services.Model
{
    /// <summary>
    /// A result from a particular datasource
    /// </summary>
    public class DatasourceResult
    {
        public string DatasourceStatus { get; set; }

        public string DatasourceName { get; set; }

        public IEnumerable<DatasourceField> DatasourceFields { get; set; }

        public IEnumerable<AppendedField> AppendedFields { get; set; }

        public IEnumerable<ServiceError> Errors { get; set; }

        public IEnumerable<string> FieldGroups { get; set; }
    }
}
