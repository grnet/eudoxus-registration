using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Eudoxus.BusinessModel;
using Eudoxus.Portal.Controls;

namespace Eudoxus.Portal.Admin
{
    public partial class UpdateAcademics : BaseEntityPortalPage<object>
    {
        protected void btnUpdateInstitutions_Click(object sender, EventArgs e)
        {
            var institutions = new InstitutionRepository(UnitOfWork).LoadAll();
            var newInstitutions = new NewInstitutionRepository(UnitOfWork).LoadAll();

            foreach (var institution in newInstitutions)
            {
                var institutionToUpdate = institutions.Where(x => x.ID == institution.ID).FirstOrDefault();
                institutionToUpdate.Name = institution.NewName;
            }

            UnitOfWork.Commit();

            lblResult.Text = string.Format("Η ενημέρωση των ({0}) Ιδρυμάτων ολοκληρώθηκε επιτυχώς.", newInstitutions.Count());
        }

        protected void btnCloneAcademics_Click(object sender, EventArgs e)
        {
            var password = "password1!";
            var sRep = new SecretaryRepository(UnitOfWork);
            var academics = new AcademicRepository(UnitOfWork).LoadAll();
            var academicsToClone = new AcademicsToCloneRepository(UnitOfWork).LoadAll();

            foreach (var academicToClone in academicsToClone)
            {
                var academic = academics.Where(x => x.ID == academicToClone.ID).FirstOrDefault();
                var secretary = sRep.FindVerifiedSecretaryByAcademic(academic.ID);
                secretary.SecretaryDetailsReference.Load();
                var secretaryDetails = secretary.SecretaryDetails;

                var username = secretary.UserName.Replace("_old", "");
                var email = secretary.Email.Replace("_old", "");


                //Clone Secretary
                var newSecretary = new Secretary()
                {
                    ContactName = secretary.ContactName,
                    ContactPhone = secretary.ContactPhone,
                    ContactMobilePhone = secretary.ContactMobilePhone,
                    ContactEmail = secretary.ContactEmail,
                    MustChangePassword = true,
                    UserName = username,
                    Email = email,
                    IsActivated = secretary.IsActivated,
                    VerificationStatus = secretary.VerificationStatus,
                    VerificationDate = secretary.VerificationDate,
                    CertificationNumber = secretary.CertificationNumber,
                    CertificationDate = secretary.CertificationDate,
                    CreatedBy = username,
                    CreatedAt = DateTime.Now
                };

                Academic newAcademic;

                if (academicToClone.ID != academicToClone.NewAcademicID)
                {
                    newAcademic = new Academic()
                    {
                        ID = academicToClone.NewAcademicID,
                        InstitutionID = academicToClone.NewInstitutionID,
                        Institution = academicToClone.NewInstitution,
                        School = academicToClone.NewSchool,
                        Department = academicToClone.NewDepartment,
                        IsGeneralDepartment = academic.IsGeneralDepartment
                    };

                    newSecretary.AcademicID = newAcademic.ID;
                }
                else
                {
                    newAcademic = new Academic()
                    {
                        ID = academic.ID + 10000,
                        InstitutionID = academicToClone.InstitutionID,
                        Institution = academicToClone.Institution,
                        School = academicToClone.School,
                        Department = academicToClone.Department,
                        IsGeneralDepartment = academic.IsGeneralDepartment
                    };

                    academic.InstitutionID = academicToClone.NewInstitutionID;
                    academic.Institution = academicToClone.NewInstitution;
                    academic.School = academicToClone.NewSchool;
                    academic.Department = academicToClone.NewDepartment;

                    secretary.AcademicID = newAcademic.ID;
                    newSecretary.AcademicID = academic.ID;
                }

                UnitOfWork.MarkAsNew(newAcademic);


                var newSecretaryDetails = new SecretaryDetails()
                {
                    SecretaryPhone = secretaryDetails.SecretaryPhone,
                    SecretaryEmail = secretaryDetails.SecretaryEmail,
                    SecretaryAddress = secretaryDetails.SecretaryAddress,
                    SecretaryZipCode = secretaryDetails.SecretaryZipCode,
                    CityID = secretaryDetails.CityID,
                    PrefectureID = secretaryDetails.PrefectureID,
                    AlternateContactName = secretaryDetails.AlternateContactName,
                    AlternateContactPhone = secretaryDetails.AlternateContactPhone,
                    AlternateContactMobilePhone = secretaryDetails.AlternateContactMobilePhone,
                    AlternateContactEmail = secretaryDetails.AlternateContactEmail,
                    RepresentativeName = secretaryDetails.RepresentativeName,
                    RepresentativeType = secretaryDetails.RepresentativeType,
                    Semesters = secretaryDetails.Semesters
                };

                newSecretary.SecretaryDetails = newSecretaryDetails;

                UnitOfWork.MarkAsNew(newSecretary);


                //Create New User with predefined password
                MembershipCreateStatus status;
                MembershipUser mu = Membership.CreateUser(username, password, email, null, null, true, out status);

                if (mu == null)
                    throw new MembershipCreateUserException(status);

                Roles.AddUserToRole(mu.UserName, RoleNames.SecretaryUser);
                Membership.UpdateUser(mu);
            }

            UnitOfWork.Commit();

            lblResult.Text = string.Format("Η κλωνοποίηση των ({0}) Τμημάτων ολοκληρώθηκε επιτυχώς.", academicsToClone.Count());
        }

        protected void btnAddNewAcademics_Click(object sender, EventArgs e)
        {
            var academicsToAdd = new AcademicsToAddRepository(UnitOfWork).LoadAll();

            foreach (var academic in academicsToAdd)
            {
                var newAcademic = new Academic()
                {
                    ID = academic.ID,
                    InstitutionID = academic.InstitutionID,
                    Institution = academic.Institution,
                    School = academic.School,
                    Department = academic.Department
                };

                UnitOfWork.MarkAsNew(newAcademic);
            }

            UnitOfWork.Commit();

            lblResult.Text = string.Format("Η προσθήκη των ({0}) Τμημάτων ολοκληρώθηκε επιτυχώς.", academicsToAdd.Count());
        }
    }
}
