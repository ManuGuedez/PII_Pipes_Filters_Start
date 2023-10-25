using System;
using Ucu.Poo.Twitter;
namespace CompAndDel
{
    public class TwitterFilter : IFilter
    {
        private PictureProvider p = new PictureProvider();

        public IPicture Filter(IPicture image)
        {
            var twitter = new TwitterImage();
            p.SavePicture(image, @"TwitterFilter.jpg");
            Console.WriteLine(twitter.PublishToTwitter("New post!", @"TwitterFilter.jpg"));
            return image;
        }
    }
}