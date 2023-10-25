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

            // Create filters for image processing
            IFilter greyFilter = new FilterGreyscale();  
            IFilter negativeFilter = new FilterNegative();

            // Create a null pipe to terminate the serial pipe route
            IPipe pipeNull = new PipeNull();

            IFilter saveNegativeFilter = new SaveFilter("Negative");
            IPipe saveNegativePipe = new PipeSerial(saveNegativeFilter, pipeNull);

            IPipe negativeFilterPipe = new PipeSerial(negativeFilter, saveNegativePipe);

            IFilter saveGreyscaleFilter = new SaveFilter("Greyscale");
            
            // In the case where the image doesn´t have a face
            IPipe serialPipeWithoutFace = new PipeSerial(saveGreyscaleFilter, negativeFilterPipe);

            // In the case where the image has a face, send it to Twitter
            IFilter twitterFilter = new TwitterFilter();
            IPipe twitterPipe = new PipeSerial(twitterFilter, pipeNull);
            
            // Conditional pipe --> route to the correct pipe based on the conditional filter
            IConditionalFilter conditionalFilter = new ConditionalFilter();
            IPipe conditionalPipe = new PipeConditionalFork(conditionalFilter, twitterPipe, serialPipeWithoutFace);

            IPipe finalPipe = new PipeSerial(greyFilter, conditionalPipe);

            IPicture finalResult = finalPipe.Send(picture);
            provider.SavePicture(finalResult, @"finalImage.jpg");          
        }
    }
}
