namespace TaskrowSharp
{
    public class RetryPolicy
    {
        public int MaxAttempts { get; private set; }

        public int TimeOutSeconds { get; private set; }

        public RetryPolicy(int maxAttempts, int timeOutSeconds)
        {
            this.MaxAttempts = maxAttempts;
            this.TimeOutSeconds = timeOutSeconds;
        }

        public override string ToString()
        {
            return $"RetryPolicy (maxAttempts={this.MaxAttempts}, timeOutSeconds={this.TimeOutSeconds}";
        }
    }
}
