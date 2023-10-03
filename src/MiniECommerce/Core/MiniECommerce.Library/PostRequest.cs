namespace MiniECommerce.Library
{
    public class PostRequest<TData>
    {
        public string RequestId { get; set; }
        public TData Data { get; set; }
    }
}