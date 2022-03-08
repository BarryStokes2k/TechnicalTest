using System.Collections.Generic;
using ULaw.ApplicationProcessor.Enums;

namespace ULaw.ApplicationProcessor
{
    public interface IApplicationResultSettings
    {
        IEnumerable<DegreeSubject> AcceptedDegrees { get; }
        decimal DepositRequired { get; }
    }
}