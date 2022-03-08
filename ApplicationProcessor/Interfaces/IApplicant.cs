namespace ULaw.ApplicationProcessor.Interfaces
{
    public interface IApplicant
    {
        string FirstName { get; }
        IDegreeResult DegreeResult { get; }
    }
}