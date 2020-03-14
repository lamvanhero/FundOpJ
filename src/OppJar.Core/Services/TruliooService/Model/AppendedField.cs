namespace OppJar.Core.Services.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AppendedField
    {
        internal AppendedField()
        { }

        /// <summary>
        /// 
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// can be string or Object(only if FieldName is WatchlistDetails)
        /// </summary>
        public dynamic Data { get; set; }
    }
}
