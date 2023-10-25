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

            IFilter filter = new FilterGreyscale();  
            IFilter filter2 = new FilterNegative();

            IPipe pipe0 = new PipeNull();

            // Guardo con la primera transformación 
            IFilter saveFilter1 = new SaveFilter("Filter1");
            IPipe pipe1 = new PipeSerial(saveFilter1, pipe0);
            IPipe pipe2 = new PipeSerial(filter2, pipe1);

            // Guardo la imagen con la segunda transformación
            IFilter saveFilter2 = new SaveFilter("Filter2");
            IPipe pipe3 = new PipeSerial(saveFilter2, pipe2);

            IFilter twitterFilter2 = new TwitterFilter();
            IPipe twitterPipe = new PipeSerial(twitterFilter2, pipe0);

            IConditionalFilter conditionalFilter = new CondiotionalFilter();
            IPipe conditionalPipe = new PipeConditional(conditionalFilter, twitterPipe, pipe3);
            IPipe pipe4 = new PipeSerial(filter, conditionalPipe);

            IPicture result = pipe4.Send(picture);
            provider.SavePicture(result, @"beer_with_both_filters.jpg");          
        }
    }
}
