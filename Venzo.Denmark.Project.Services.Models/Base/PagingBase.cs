namespace Venzo.Denmark.Project.Services.Models.Base
{
    public class PagingBase<T> where T : class
    {
        public int Count { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
