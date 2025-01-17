﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ULaw.ApplicationProcessor.Enums;

namespace ULaw.ApplicationProcessor.Tests
{

    [TestClass]
    public class ApplicationSubmissionTests
    {
        private const string OfferEmailForFirstLawDegreeResult = @"<html><body><h1>Your Recent Application from the University of Law</h1><p> Dear Test, </p><p/> Further to your recent application, we are delighted to offer you a place on our course reference: ABC123 starting on 22 September 2019.<br/> This offer will be subject to evidence of your qualifying Law degree at grade: 1st.<br/> Please contact us as soon as possible to confirm your acceptance of your place and arrange payment of the £350.00 deposit fee to secure your place.<br/> We look forward to welcoming you to the University,<br/> Yours sincerely,<p/> The Admissions Team,</body></html>";
        private const string OfferEmailForTwoOneLawDegreeResult = @"<html><body><h1>Your Recent Application from the University of Law</h1><p> Dear Test, </p><p/> Further to your recent application, we are delighted to offer you a place on our course reference: ABC123 starting on 22 September 2019.<br/> This offer will be subject to evidence of your qualifying Law degree at grade: 2:1.<br/> Please contact us as soon as possible to confirm your acceptance of your place and arrange payment of the £350.00 deposit fee to secure your place.<br/> We look forward to welcoming you to the University,<br/> Yours sincerely,<p/> The Admissions Team,</body></html>";
        private const string OfferEmailForFirstLawAndBusinessDegreeResult = @"<html><body><h1>Your Recent Application from the University of Law</h1><p> Dear Test, </p><p/> Further to your recent application, we are delighted to offer you a place on our course reference: ABC123 starting on 22 September 2019.<br/> This offer will be subject to evidence of your qualifying Law and Business degree at grade: 1st.<br/> Please contact us as soon as possible to confirm your acceptance of your place and arrange payment of the £350.00 deposit fee to secure your place.<br/> We look forward to welcoming you to the University,<br/> Yours sincerely,<p/> The Admissions Team,</body></html>";
        private const string OfferEmailForTwoOneLawAndBusinessDegreeResult = @"<html><body><h1>Your Recent Application from the University of Law</h1><p> Dear Test, </p><p/> Further to your recent application, we are delighted to offer you a place on our course reference: ABC123 starting on 22 September 2019.<br/> This offer will be subject to evidence of your qualifying Law and Business degree at grade: 2:1.<br/> Please contact us as soon as possible to confirm your acceptance of your place and arrange payment of the £350.00 deposit fee to secure your place.<br/> We look forward to welcoming you to the University,<br/> Yours sincerely,<p/> The Admissions Team,</body></html>";

        private const string FurtherInfoEmailResult = @"<html><body><h1>Your Recent Application from the University of Law</h1><p> Dear Test, </p><p/> Further to your recent application for our course reference: ABC123 starting on 22 September 2019, we are writing to inform you that we are currently assessing your information and will be in touch shortly.<br/> If you wish to discuss any aspect of your application, please contact us at AdmissionsTeam@Ulaw.co.uk.<br/> Yours sincerely,<p/> The Admissions Team,</body></html>";
        private const string RejectionEmailForAnyThirdDegreeResult = @"<html><body><h1>Your Recent Application from the University of Law</h1><p> Dear Test, </p><p/> Further to your recent application, we are sorry to inform you that you have not been successful on this occasion.<br/> If you wish to discuss the decision further, or discuss the possibility of applying for an alternative course with us, please contact us at AdmissionsTeam@Ulaw.co.uk.<br/> Yours sincerely,<p/> The Admissions Team,</body></html>";

        [TestMethod]
        public void ApplicationSubmissionWithFirstLawDegree()
        {
            Application thisSubmission = new Application("Law", "ABC123", new DateTime(2019, 9, 22), "Mr", "Test", "Tester", new DateTime(1991, 08, 14), false);

            thisSubmission.DegreeGrade = DegreeGrade.First;
            thisSubmission.DegreeSubject = DegreeSubject.Law;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(OfferEmailForFirstLawDegreeResult, emailHtml);
        }

        [TestMethod]
        public void ApplicationSubmissionWithFirstLawAndBusinessDegree()
        {
            Application thisSubmission = new Application("Law", "ABC123", new DateTime(2019, 9, 22), "Mr", "Test", "Tester", new DateTime(1991, 08, 14), false);

            thisSubmission.DegreeGrade = DegreeGrade.First;
            thisSubmission.DegreeSubject = DegreeSubject.LawAndBusiness;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(OfferEmailForFirstLawAndBusinessDegreeResult, emailHtml);
        }

        [TestMethod]
        public void ApplicationSubmissionWithFirstEnglishDegree()
        {
            Application thisSubmission = new Application("Law", "ABC123", new DateTime(2019, 9, 22), "Mr", "Test", "Tester", new DateTime(1991, 08, 14), false);

            thisSubmission.DegreeGrade = DegreeGrade.First;
            thisSubmission.DegreeSubject = DegreeSubject.English;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(FurtherInfoEmailResult, emailHtml);
        }

        [TestMethod]
        public void ApplicationSubmissionWithTwoOneLawDegree()
        {
            Application thisSubmission = new Application("Law", "ABC123", new DateTime(2019, 9, 22), "Mr", "Test", "Tester", new DateTime(1991, 08, 14), false);

            thisSubmission.DegreeGrade = DegreeGrade.TwoOne;
            thisSubmission.DegreeSubject = DegreeSubject.Law;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(OfferEmailForTwoOneLawDegreeResult, emailHtml);
        }

        [TestMethod]
        public void ApplicationSubmissionWithTwoOneMathsDegree()
        {

            Application thisSubmission = new Application("Law", "ABC123", new DateTime(2019, 9, 22), "Mr", "Test", "Tester", new DateTime(1991, 08, 14), false);

            thisSubmission.DegreeGrade = DegreeGrade.TwoOne;
            thisSubmission.DegreeSubject = DegreeSubject.Maths;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(FurtherInfoEmailResult, emailHtml);
        }

        [TestMethod]
        public void ApplicationSubmissionWithTwoOneLawAndBusinessDegree()
        {
            Application thisSubmission = new Application("Law", "ABC123", new DateTime(2019, 9, 22), "Mr", "Test", "Tester", new DateTime(1991, 08, 14), false);

            thisSubmission.DegreeGrade = DegreeGrade.TwoOne;
            thisSubmission.DegreeSubject = DegreeSubject.LawAndBusiness;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(OfferEmailForTwoOneLawAndBusinessDegreeResult, emailHtml);
        }

        [TestMethod]
        public void ApplicationSubmissionWithTwoTwoEnglishDegree()
        {
            Application thisSubmission = new Application("Law", "ABC123", new DateTime(2019, 9, 22), "Mr", "Test", "Tester", new DateTime(1991, 08, 14), false);

            thisSubmission.DegreeGrade = DegreeGrade.TwoTwo;
            thisSubmission.DegreeSubject = DegreeSubject.English;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(FurtherInfoEmailResult, emailHtml);
        }

        [TestMethod]
        public void ApplicationSubmissionWithTwoTwoLawDegree()
        {
            Application thisSubmission = new Application("Law", "ABC123", new DateTime(2019, 9, 22), "Mr", "Test", "Tester", new DateTime(1991, 08, 14), false);

            thisSubmission.DegreeGrade = DegreeGrade.TwoTwo;
            thisSubmission.DegreeSubject = DegreeSubject.Law;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(FurtherInfoEmailResult, emailHtml);
        }

        [TestMethod]
        public void ApplicationSubmissionWithThirdDegree()
        {
            Application thisSubmission = new Application("Law", "ABC123", new DateTime(2019, 9, 22), "Mr", "Test", "Tester", new DateTime(1991, 08, 14), false);

            thisSubmission.DegreeGrade = DegreeGrade.Third;
            thisSubmission.DegreeSubject = DegreeSubject.Maths;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(emailHtml, RejectionEmailForAnyThirdDegreeResult);
        }
    }
  
}
