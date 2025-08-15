namespace RunApp.Domain.PhotoAggregate
{
    public class Photo
    {
        private Photo() { }
        public string PhotoId { get; private set; }
        public string Url { get; private set; }
        public bool IsAvatar { get; private set;}
        public Guid ProductId { get; private set; }
        public Guid CustomerProfileId { get; private set; }

        public static Photo CreatePhotoEntity(string photoId, string url, Guid productId, Guid customerProfileId)
        {
            return new Photo
            {
                PhotoId = photoId,
                Url = url,
                ProductId = productId,
                CustomerProfileId = customerProfileId,
            };
        }
    }
}
