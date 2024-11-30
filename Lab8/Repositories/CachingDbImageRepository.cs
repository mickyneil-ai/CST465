using Lab8.DataObjects;
using Microsoft.Extensions.Caching.Memory;

namespace Lab8.Repositories
{
    public class CachingDbImageRepository : DbImageRepository, IImageRepository
    {
        private readonly IMemoryCache _Cache;
        private const string _CacheKey = "ImageList";

        public CachingDbImageRepository(IMemoryCache cache, IConfiguration config) : base(config)
        {
            _Cache = cache;
        }
        public override List<ImageObject> GetImages()
        {
            if (!_Cache.TryGetValue(_CacheKey, out List<ImageObject> images))
            {
                images = base.GetImages();
                _Cache.Set(_CacheKey, images, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });
            }

            return images;
        }
        public override void SaveImage(ImageObject image)
        {
            base.SaveImage(image);
            _Cache.Remove(_CacheKey);
        }
    }
}
