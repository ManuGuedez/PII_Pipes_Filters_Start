namespace CompAndDel
{
    public class PipeConditionalFork : IPipe
    {
        private IPipe falseConditionPipe;
        private IPipe trueConditionPipe;
        private IConditionalFilter conditionalFilter;


        public PipeConditionalFork(IConditionalFilter filter, IPipe whenTrue, IPipe whenFalse)
        {
            this.conditionalFilter = filter;
            this.trueConditionPipe = whenTrue;
            this.falseConditionPipe = whenFalse;
        }
        
        public IPicture Send(IPicture picture)
        {
            if (this.conditionalFilter.Filter(picture))
            {
                return this.trueConditionPipe.Send(picture);
            }
            else
            {
                return this.falseConditionPipe.Send(picture);
            }
        }
    }
}