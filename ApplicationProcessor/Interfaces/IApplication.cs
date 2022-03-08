namespace ULaw.ApplicationProcessor.Interfaces
{
    public interface IApplication
    {
        IApplicant Applicant { get; }
        ICourse Course { get; }
        string ApplicationId { get; }
    }
}