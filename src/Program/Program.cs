using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using Ucu.Poo.Twitter;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"beer.jpg");

            IFilter filter = new FilterNegative();
            IFilter filter2 = new FilterGreyscale();            

            IPipe pipe0 = new PipeNull();

            // Guardo con la primera transformación 
            IFilter saveFilter1 = new SaveFilter("Filter1");
            IPipe pipe1 = new PipeSerial(saveFilter1, pipe0);

            IFilter twitterFilter1 = new TwitterFilter();
            IPipe pipePost1 = new PipeSerial(twitterFilter1, pipe1);
            IPipe pipe2 = new PipeSerial(filter2, pipePost1);

            // Guardo la imagen con la segunda transformación
            IFilter saveFilter2 = new SaveFilter("Filter2");
            IPipe pipe3 = new PipeSerial(saveFilter2, pipe2);

            IFilter twitterFilter2 = new TwitterFilter();
            IPipe pipePost2 = new PipeSerial(twitterFilter2, pipe3);
            IPipe pipe4 = new PipeSerial(filter, pipePost2);

            IPicture result = pipe4.Send(picture);
            provider.SavePicture(result, @"beer_with_both_filters.jpg");          
        }
    }
}
