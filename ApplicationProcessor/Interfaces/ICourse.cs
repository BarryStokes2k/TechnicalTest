using System;

namespace ULaw.ApplicationProcessor.Interfaces
{
    public interface ICourse
    {
        string CourseCode { get; }
        DateTime StartDate { get; }
    }
}