using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return string.Format("RetryPolicy (maxAttempts={0}, timeOutSeconds={1}", this.MaxAttempts, this.TimeOutSeconds);
        }
    }
}
