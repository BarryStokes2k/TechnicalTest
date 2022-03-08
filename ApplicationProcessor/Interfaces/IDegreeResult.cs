using ULaw.ApplicationProcessor.Enums;

namespace ULaw.ApplicationProcessor.Interfaces
{
    public interface IDegreeResult
    {
        DegreeSubject Subject { get; }
        DegreeGrade Grade { get; }
    }
}