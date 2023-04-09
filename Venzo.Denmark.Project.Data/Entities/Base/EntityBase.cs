using Microsoft.EntityFrameworkCore.Update;
using System.ComponentModel.DataAnnotations.Schema;

namespace Venzo.Denmark.Project.Data.Entities.Base
{
    public class EntityBase<T>
    {
        [Column(Order = 0)]
        public T Id { get; set; }
    }
}
