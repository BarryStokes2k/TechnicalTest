using System.Linq;
using System.Text;
using ULaw.ApplicationProcessor.Enums;
using ULaw.ApplicationProcessor.Interfaces;

namespace ULaw.ApplicationProcessor
{
    public class ApplicationResultProcessor : IApplicationResultProcessor
    {
        private readonly IApplication _application;
        private readonly IApplicationResultSettings _settings;

        public ApplicationResultProcessor(IApplicationResultSettings settings, IApplication application)
        {
            _application = application;
            _settings = settings;
        }

        public string Process()
        {
            var result = new StringBuilder("<html><body><h1>Your Recent Application from the University of Law</h1>");
            result.Append($"<p> Dear {_application.Applicant.FirstName}, </p>");

            switch (_application.Applicant.DegreeResult.Grade)
            {
                case DegreeGrade.First:
                case DegreeGrade.TwoOne:
                    if (_settings.AcceptedDegrees.Contains(_application.Applicant.DegreeResult.Subject))
                    {
                        handleAccept(result);
                    }
                    else
                    {
                        handlePending(result);
                    }
                    break;
                case DegreeGrade.TwoTwo:
                    handlePending(result);
                    break;
                default:
                    handleReject(result);
                    break;
            }

            result.Append("<br/> Yours sincerely,");
            result.Append("<p/> The Admissions Team,");
            result.Append(string.Format("</body></html>"));

            return result.ToString();
        }

        private void handleAccept(StringBuilder result)
        {
            result.Append($"<p/> Further to your recent application, we are delighted to offer you a place on our course reference: {_application.Course.CourseCode} starting on {_application.Course.StartDate:dd MMMM yyyy}.");
            result.Append($"<br/> This offer will be subject to evidence of your qualifying {_application.Applicant.DegreeResult.Subject.ToDescription()} degree at grade: {_application.Applicant.DegreeResult.Grade.ToDescription()}.");
            result.Append($"<br/> Please contact us as soon as possible to confirm your acceptance of your place and arrange payment of the £{_settings.DepositRequired:0.00} deposit fee to secure your place.");
            result.Append($"<br/> We look forward to welcoming you to the University,");

        }

        private void handlePending(StringBuilder result)
        {
            result.Append($"<p/> Further to your recent application for our course reference: {_application.Course.CourseCode} starting on {_application.Course.StartDate:dd MMMM yyyy}, we are writing to inform you that we are currently assessing your information and will be in touch shortly.");
            result.Append("<br/> If you wish to discuss any aspect of your application, please contact us at AdmissionsTeam@Ulaw.co.uk.");
        }

        private void handleReject(StringBuilder result)
        {
            result.Append("<p/> Further to your recent application, we are sorry to inform you that you have not been successful on this occasion.");
            result.Append("<br/> If you wish to discuss the decision further, or discuss the possibility of applying for an alternative course with us, please contact us at AdmissionsTeam@Ulaw.co.uk.");
        }
    }
}

