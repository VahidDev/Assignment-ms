using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;

namespace DomainModels.Models.Entities
{
    public class User : IdentityUser<int>
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("picture")]
        public string Picture { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("email")]
        public override string Email {get;set;}
        [JsonProperty("nationality")]
        public byte Nationality { get; set; }
        [JsonProperty("_country_issued")]
        public byte CountryIssued { get; set; }
        [JsonProperty("gender_for_accreditation")]
        public string GenderForAccreditation { get; set; }
        [JsonProperty("dob")]
        public DateTime Dob { get; set; }
        [JsonProperty("current_occupation")]
        public string CurrentOccupation { get; set; }
         [JsonProperty("id_document_type")]
        public string IdDocumentType { get; set; }
         [JsonProperty("qid_number")]
        public string QidNumber { get; set; }
         [JsonProperty("id_document_expiry_date_q22")]
        public DateTime IdDocumentExpiryDateQ22 { get; set; }
         [JsonProperty("passport_number")]
        public string PassportNumber { get; set; }
         [JsonProperty("id_document_expiry_date")]
        public DateTime IdDocumentExpiryDate { get; set; }
         [JsonProperty("id_document_country_of_issue")]
        public DateTime IdDocumentCountryOfIssue { get; set; }
         [JsonProperty("passport_expiry_date")]
        public DateTime PassportExpiryDate { get; set; }
         [JsonProperty("passport_type")]
        public string PassportType { get; set; }
         [JsonProperty("qatari_driving_license")]
        public bool QatariDrivingLicense { get; set; }
         [JsonProperty("driving_license_type")]
        public string DrivingLicenseType { get; set; }
         [JsonProperty("country")]
        public byte Country { get; set; }
         [JsonProperty("international_accommodation_preference")]
        public string InternationalAccommodationPreference { get; set; }
         [JsonProperty("medical_conditions")]
        public string MedicalConditions { get; set; }
         [JsonProperty("disability_yes_no")]
        public bool DisabilityYesNo { get; set; }
         [JsonProperty("disability_type")]
        public string DisabilityType { get; set; }
         [JsonProperty("disability_others")]
        public string DisabilityOthers { get; set; }
         [JsonProperty("social_worker_caregiver_support")]
        public string SocialWorkerCaregiverSupport { get; set; }
         [JsonProperty("dietary_requirement_identification")]
        public string DietaryRequirementIdentification { get; set; }
         [JsonProperty("special_dietary_options")]
        public string SpecialDietaryOptions { get; set; }
         [JsonProperty("alergies_other")]
        public string AlergiesOther { get; set; }
         [JsonProperty("covid-19_vaccinated")]
        public string Covid19Vaccinated { get; set; }
         [JsonProperty("vaccine_type_multi-select")]
        public string VaccineTypeMultiSelect { get; set; }
         [JsonProperty("final_vaccine_dose_date")]
        public string FinalVaccineDoseDate { get; set; }
         [JsonProperty("education_onechoice")]
        public string EducationOnechoice { get; set; }
         [JsonProperty("area_of_study")]
        public string AreaOfStudy { get; set; }
         [JsonProperty("english_fluency_level")]
        public string EnglishFluencyLevel { get; set; }
         [JsonProperty("arabic_fluency_level")]
        public string ArabicFluencyLevel { get; set; }
         [JsonProperty("additional_language_1")]
        public string AdditionalLanguage1 { get; set; }
         [JsonProperty("additional_language_1_fluency_level")]
        public string AdditionalLanguage1FluencyLevel { get; set; }
         [JsonProperty("additional_language_2")]
        public string AdditionalLanguage2 { get; set; }
         [JsonProperty("additional_language_2_fluency_level")]
        public string AdditionalLanguage2FluencyLevel { get; set; }
         [JsonProperty("additional_language_3")]
        public string AdditionalLanguage3 { get; set; }
         [JsonProperty("additional_language_3_fluency_level")]
        public string AdditionalLanguage3FluencyLevel { get; set; }
         [JsonProperty("additional_language_4")]
        public string AdditionalLanguage4 { get; set; }
         [JsonProperty("additional_language_4_fluency_level")]
        public string AdditionalLanguage4FluencyLevel { get; set; }
         [JsonProperty("languages_other")]
        public string LanguagesOther { get; set; }
         [JsonProperty("interpretation_and_translation_experience")]
        public string InterpretationAndTranslationExperience { get; set; }
         [JsonProperty("certified_translator_language")]
        public string CertifiedTranslatorLanguage { get; set; }
         [JsonProperty("describe_your_it_skills")]
        public string DescribeYourItSkills { get; set; }
         [JsonProperty("skill_1")]
        public string Skill1 { get; set; }
         [JsonProperty("skill_2")]
        public string Skill2 { get; set; }
         [JsonProperty("skill_3")]
        public string Skill3 { get; set; }
         [JsonProperty("skill_4")]
        public string Skill4 { get; set; }
         [JsonProperty("skill_5")]
        public string Skill5 { get; set; }
         [JsonProperty("skill_6")]
        public string Skill6 { get; set; }
         [JsonProperty("previous_volunteering_experience")]
        public string PreviousVolunteeringExperience { get; set; }
         [JsonProperty("volunteer_experience")]
        public string VolunteerExperience { get; set; }
         [JsonProperty("other_role")]
        public string OtherRole { get; set; }
         [JsonProperty("events_type_participations")]
        public string EventsTypeParticipations { get; set; }
         [JsonProperty("other_event_type")]
        public string OtherEventType { get; set; }
         [JsonProperty("volunteer_hours_yearly")]
        public string VolunteerHoursYearly { get; set; }
         [JsonProperty("preferred_functional_area")]
        public string PreferredFunctionalArea { get; set; }
         [JsonProperty("fwc_are_you_interested_in_a_leadership_role")]
        public string FwcAreYouInterestedInALeadershipRole { get; set; }
         [JsonProperty("fwc_leadership_experience")]
        public string FwcLeadershipExperience { get; set; }
         [JsonProperty("work_experience")]
        public string WorkExperience { get; set; }
         [JsonProperty("ceremonies_yes_no")]
        public bool CeremoniesYesNo { get; set; }
         [JsonProperty("cast_yes_no")]
        public bool CastYesNo { get; set; }
         [JsonProperty("cast_options")]
        public string CastOptions { get; set; }
         [JsonProperty("motivation_to_volunteer_at_fwc")]
        public string MotivationToVolunteerAtFwc { get; set; }
         [JsonProperty("leave_or_arrange")]
        public string LeaveOrArrange { get; set; }
         [JsonProperty("local__international_volunteer")]
        public string LocalInternationalVolunteer { get; set; }
         [JsonProperty("language_path_-_english__arabic")]
        public string LanguagePathEnglishArabic { get; set; }
         [JsonProperty("fwc_availability_pre_tournament_stage_one")]
        public string FwcAvailabilityPreTournamentStageOne { get; set; }
         [JsonProperty("fwc_availability_pre_tournament_stage_two")]
        public string FwcAvailabilityPreTournamentStageTwo { get; set; }
         [JsonProperty("availability_during_tournament")]
        public string AvailabilityDuringTournament { get; set; }
         [JsonProperty("daily_availability_shift_morning")]
        public string daily_availability_shift_morning { get; set; }
         [JsonProperty("daily_availability_shift_morning")]
        public string DailyAvailabilityShiftAfternoon { get; set; }
         [JsonProperty("daily_availability_shift_night")]
        public string DailyAvailabilityShiftNight { get; set; }
         [JsonProperty("daily_availability_shift_overnight")]
        public string DailyAvailabilityShiftOvernight { get; set; }
         [JsonProperty("candidate_under_18")]
        public string CandidateUnder18 { get; set; }
         [JsonProperty("special_groups_international")]
        public string SpecialGroupsInternational { get; set; }
         [JsonProperty("municipality_address")]
        public string MunicipalityAddress { get; set; }
    }
}
