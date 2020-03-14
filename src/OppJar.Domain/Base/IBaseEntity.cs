using System.ComponentModel.DataAnnotations;

namespace OppJar.Domain
{
    public interface IBaseEntity<TKey>
    {
        [Key]
        TKey Id { get; set; }
    }

    public interface IBaseEntity : IBaseEntity<string> { }
}
