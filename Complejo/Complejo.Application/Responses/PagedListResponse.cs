using Complejo.Application.Utils;

namespace Complejo.Application.Responses
{
    public class PagedListResponse<T>
    {
        public T Data { get; set; }

        public Metadata Metadata { get; set; }

        public PagedListResponse(T data, Metadata metadata)
        {
            Data = data;
            Metadata = metadata;
        }
    }
}
