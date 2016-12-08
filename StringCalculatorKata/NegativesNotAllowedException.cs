using System;
using System.Runtime.Serialization;

namespace StringCalculatorKata
{
    [Serializable]
    public class NegativesNotAllowedException : Exception
    {
        public NegativesNotAllowedException()
            : base()
        { }

        public NegativesNotAllowedException(string message)
            : base(message)
        { }
        public NegativesNotAllowedException(string format, params object[] agrs)
            : base(string.Format(format, agrs))
        { }
        public NegativesNotAllowedException(string message, Exception innerException)
            : base(message, innerException)
        { }
        public NegativesNotAllowedException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        { }
        public NegativesNotAllowedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
