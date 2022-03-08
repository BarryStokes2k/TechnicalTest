using System;
using Moq;
using ULaw.ApplicationProcessor.Enums;
using ULaw.ApplicationProcessor.Interfaces;

namespace ULaw.ApplicationProcessor.Tests
{
    internal class Application
    {

        public Application(string faculty, string courseCode, DateTime courseStartDate, string title, string firstName, string lastname, DateTime dateOfBirth, bool requiresVisa)
        {
            _courseCode = courseCode;
            _courseStartDate = courseStartDate;
            _firstName = firstName;
        }

        private readonly string _courseCode;
        private readonly DateTime _courseStartDate;
        private readonly string _firstName;

        public DegreeSubject DegreeSubject { get; internal set; }
        public DegreeGrade DegreeGrade { get; internal set; }

        internal string Process()
        {
            // Configure Mock for the applicant's degree result
            var degreeResult = new Mock<IDegreeResult>();
            degreeResult.Setup(x => x.Subject).Returns(DegreeSubject);
            degreeResult.Setup(x => x.Grade).Returns(DegreeGrade);
            
            // Configure Mock for the applicant
            var applicant = new Mock<IApplicant>();
            applicant.Setup(x => x.FirstName).Returns(_firstName);
            applicant.Setup(x => x.DegreeResult).Returns(degreeResult.Object);
            
            // Configure Mock for the course which is being applied to
            var course = new Mock<ICourse>();
            course.Setup(x => x.CourseCode).Returns(_courseCode);
            course.Setup(x=>x.StartDate).Returns(_courseStartDate);
            
            // Configure Mock for the Application
            var application = new Mock<IApplication>();
            application.Setup(x => x.Applicant).Returns(applicant.Object);
            application.Setup(x => x.Course).Returns(course.Object);

            // Configure Mock for the Settings
            var settings = new Mock<IApplicationResultSettings>();
            settings.Setup(x => x.AcceptedDegrees).Returns(new[] { DegreeSubject.Law, DegreeSubject.LawAndBusiness });
            settings.Setup(x => x.DepositRequired).Returns(350M);

            // Create instance of the processor
            var processor = new ApplicationResultProcessor(settings.Object, application.Object);

            // Run processor and return result
            return processor.Process();
        }
    }
}
