using System;
using Ucu.Poo.Cognitive;

namespace CompAndDel
{
    public class ConditionalFilter : IConditionalFilter
    {
        public bool Filter(IPicture image)
        {
            PictureProvider p = new PictureProvider();
            p.SavePicture(image, @"ConditionalFilter.jpg");

            CognitiveFace picture = new CognitiveFace();
            picture.Recognize(@"ConditionalFilter.jpg");

            return picture.FaceFound;
        }
    }
}