namespace CompAndDel
{
    public class PipeConditional : IPipe
    {
        private IPipe nextFalsePipe;
        private IPipe nextTruePipe;
        private IConditionalFilter conditionalFilter;


        public PipeConditional(IConditionalFilter filter, IPipe pipetrueCondition, IPipe pipeFalseCondition)
        {
            this.conditionalFilter = filter;
            this.nextTruePipe = pipetrueCondition;
            this.nextFalsePipe = pipeFalseCondition;
        }
        
        public IPicture Send(IPicture picture)
        {
            if (this.conditionalFilter.Filter(picture))
            {
                return this.nextTruePipe.Send(picture);
            }
            else
            {
                return this.nextFalsePipe.Send(picture);
            }
        }
    }
}