using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OppJar.Core.Services.Model
{
    /// <summary>
    /// Field info for a datasource
    /// </summary>
    public class NormalizedDatasourceField
    {
        /// <summary>
        /// Field Name
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }
    }
}
