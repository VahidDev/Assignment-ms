using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    public class User : IdentityUser<int>
    {
        [Column("id")]
        [JsonProperty("id")]
        public override int Id { get => base.Id; set => base.Id = value; }
        [Column("first_name")]
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [Column("picture")]
        [JsonProperty("picture")]
        public string Picture { get; set; }
        [Column("phone")]
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [Column("email")]
        [JsonProperty("email")]
        public override string Email {get;set;}
        [Column("nationality")]
        [JsonProperty("nationality")]
        public byte Nationality { get; set; }
        [Column("_country_issued")]
        [JsonProperty("_country_issued")]
        public byte CountryIssued { get; set; }
        [Column("gender_for_accreditation")]
        [JsonProperty("gender_for_accreditation")]
        public string GenderForAccreditation { get; set; }
        [Column("dob")]
        [JsonProperty("dob")]
        public DateTime Dob { get; set; }
        [Column("current_occupation")]
        [JsonProperty("current_occupation")]
        public string CurrentOccupation { get; set; }
        public IdDocumentType IdDocumentType { get; set; }
        [Column("qid_number")]
        [JsonProperty("qid_number")]
        public string QidNumber { get; set; }
        [Column("id_document_expiry_date_q22")]
        [JsonProperty("id_document_expiry_date_q22")]
        public DateTime IdDocumentExpiryDateQ22 { get; set; }
        [Column("passport_number")]
        [JsonProperty("passport_number")]
        public string PassportNumber { get; set; }
        [Column("id_document_expiry_date")]
        [JsonProperty("id_document_expiry_date")]
        public DateTime IdDocumentExpiryDate { get; set; }
        [Column("id_document_country_of_issue")]
        [JsonProperty("id_document_country_of_issue")]
        public DateTime IdDocumentCountryOfIssue { get; set; }
        [Column("passport_expiry_date")]
        [JsonProperty("passport_expiry_date")]
        public DateTime PassportExpiryDate { get; set; }
        public PassportType PassportType { get; set; }
        [Column("qatari_driving_license")]
        [JsonProperty("qatari_driving_license")]
        public bool QatariDrivingLicense { get; set; }
        public DrivingLicenseType DrivingLicenseType { get; set; }
        [Column("country")]
        [JsonProperty("country")]
        public byte Country { get; set; }

        public InternationalAccommodationPreference 
            InternationalAccommodationPreference { get; set; }
        [Column("medical_conditions")]
        [JsonProperty("medical_conditions")]
        public string MedicalConditions { get; set; }
        [Column("disability_yes_no")]
        [JsonProperty("disability_yes_no")]
        public bool DisabilityYesNo { get; set; }
        [Column("disability_type")]
        [JsonProperty("disability_type")]
        public string DisabilityType { get; set; }
        [Column("disability_others")]
        [JsonProperty("disability_others")]
        public string DisabilityOthers { get; set; }
        [Column("social_worker_caregiver_support")]
        [JsonProperty("social_worker_caregiver_support")]
        public string SocialWorkerCaregiverSupport { get; set; }
        [Column("dietary_requirement_identification")]
        [JsonProperty("dietary_requirement_identification")]
        public string DietaryRequirementIdentification { get; set; }
        [Column("special_dietary_options")]
        [JsonProperty("special_dietary_options")]
        public string SpecialDietaryOptions { get; set; }
        [Column("alergies_other")]
        [JsonProperty("alergies_other")]
        public string AlergiesOther { get; set; }
        [Column("covid-19_vaccinated")]
        [JsonProperty("covid-19_vaccinated")]
        public string Covid19Vaccinated { get; set; }
        [Column("vaccine_type_multi")]
        [JsonProperty("vaccine_type_multi-select")]
        public string VaccineTypeMultiSelect { get; set; }
        [Column("final_vaccine_dose_date")]
        [JsonProperty("final_vaccine_dose_date")]
        public string FinalVaccineDoseDate { get; set; }
        [Column("education_onechoice")]
        [JsonProperty("education_onechoice")]
        public string EducationOnechoice { get; set; }
        [Column("area_of_study")]
        [JsonProperty("area_of_study")]
        public string AreaOfStudy { get; set; }
        [Column("english_fluency_level")]
        [JsonProperty("english_fluency_level")]
        public string EnglishFluencyLevel { get; set; }
        [Column("arabic_fluency_level")]
        [JsonProperty("arabic_fluency_level")]
        public string ArabicFluencyLevel { get; set; }
        [Column("additional_language_1")]
        [JsonProperty("additional_language_1")]
        public string AdditionalLanguage1 { get; set; }
        [Column("additional_language_1_fluency_level")]
        [JsonProperty("additional_language_1_fluency_level")]
        public string AdditionalLanguage1FluencyLevel { get; set; }
        [Column("additional_language_2")]
        [JsonProperty("additional_language_2")]
        public string AdditionalLanguage2 { get; set; }
        [Column("additional_language_2_fluency_level")]
        [JsonProperty("additional_language_2_fluency_level")]
        public string AdditionalLanguage2FluencyLevel { get; set; }
        [Column("additional_language_3")]
        [JsonProperty("additional_language_3")]
        public string AdditionalLanguage3 { get; set; }
        [Column("additional_language_3_fluency_level")]
        [JsonProperty("additional_language_3_fluency_level")]
        public string AdditionalLanguage3FluencyLevel { get; set; }
        [Column("additional_language_4")]
        [JsonProperty("additional_language_4")]
        public string AdditionalLanguage4 { get; set; }
        [Column("additional_language_4_fluency_level")]
        [JsonProperty("additional_language_4_fluency_level")]
        public string AdditionalLanguage4FluencyLevel { get; set; }
        [Column("languages_other")]
        [JsonProperty("languages_other")]
        public string LanguagesOther { get; set; }
        [Column("interpretation_and_translation_experience")]
        [JsonProperty("interpretation_and_translation_experience")]
        public string InterpretationAndTranslationExperience { get; set; }
        [Column("certified_translator_language")]
        [JsonProperty("certified_translator_language")]
        public string CertifiedTranslatorLanguage { get; set; }
        [Column("describe_your_it_skills")]
        [JsonProperty("describe_your_it_skills")]
        public string DescribeYourItSkills { get; set; }
        [Column("skill_1")]
        [JsonProperty("skill_1")]
        public string Skill1 { get; set; }
        [Column("skill_2")]
        [JsonProperty("skill_2")]
        public string Skill2 { get; set; }
        [Column("skill_3")]
        [JsonProperty("skill_3")]
        public string Skill3 { get; set; }
        [Column("skill_4")]
        [JsonProperty("skill_4")]
        public string Skill4 { get; set; }
        [Column("skill_5")]
        [JsonProperty("skill_5")]
        public string Skill5 { get; set; }
        [Column("skill_6")]
        [JsonProperty("skill_6")]
        public string Skill6 { get; set; }
        [Column("previous_volunteering_experience")]
        [JsonProperty("previous_volunteering_experience")]
        public string PreviousVolunteeringExperience { get; set; }
        [Column("volunteer_experience")]
        [JsonProperty("volunteer_experience")]
        public string VolunteerExperience { get; set; }
        [Column("other_role")]
        [JsonProperty("other_role")]
        public string OtherRole { get; set; }
        [Column("events_type_participations")]
        [JsonProperty("events_type_participations")]
        public string EventsTypeParticipations { get; set; }
        [Column("other_event_type")]
        [JsonProperty("other_event_type")]
        public string OtherEventType { get; set; }
        [Column("volunteer_hours_yearly")]
        [JsonProperty("volunteer_hours_yearly")]
        public string VolunteerHoursYearly { get; set; }
        [Column("preferred_functional_area")]
        [JsonProperty("preferred_functional_area")]
        public string PreferredFunctionalArea { get; set; }
        [Column("fwc_are_you_interested_in_a_leadership_role")]
        [JsonProperty("fwc_are_you_interested_in_a_leadership_role")]
        public string FwcAreYouInterestedInALeadershipRole { get; set; }
        [Column("fwc_leadership_experience")]
        [JsonProperty("fwc_leadership_experience")]
        public string FwcLeadershipExperience { get; set; }
        [Column("work_experience")]
        [JsonProperty("work_experience")]
        public string WorkExperience { get; set; }
        [Column("ceremonies_yes_no")]
        [JsonProperty("ceremonies_yes_no")]
        public bool CeremoniesYesNo { get; set; }
        [Column("cast_yes_no")]
        [JsonProperty("cast_yes_no")]
        public bool CastYesNo { get; set; }
        [Column("cast_options")]
        [JsonProperty("cast_options")]
        public string CastOptions { get; set; }
        [Column("motivation_to_volunteer_at_fwc")]
        [JsonProperty("motivation_to_volunteer_at_fwc")]
        public string MotivationToVolunteerAtFwc { get; set; }
        [Column("leave_or_arrange")]
        [JsonProperty("leave_or_arrange")]
        public string LeaveOrArrange { get; set; }
        [Column("local__international_volunteer")]
        [JsonProperty("local__international_volunteer")]
        public string LocalInternationalVolunteer { get; set; }
        [Column("language_path_-_english__arabic")]
        [JsonProperty("language_path_-_english__arabic")]
        public string LanguagePathEnglishArabic { get; set; }
        [Column("fwc_availability_pre_tournament_stage_one")]
        [JsonProperty("fwc_availability_pre_tournament_stage_one")]
        public string FwcAvailabilityPreTournamentStageOne { get; set; }
        [Column("fwc_availability_pre_tournament_stage_two")]
        [JsonProperty("fwc_availability_pre_tournament_stage_two")]
        public string FwcAvailabilityPreTournamentStageTwo { get; set; }
        [Column("availability_during_tournament")]
        [JsonProperty("availability_during_tournament")]
        public string AvailabilityDuringTournament { get; set; }
        [Column("daily_availability_shift_morning")]
        [JsonProperty("daily_availability_shift_morning")]
        public string daily_availability_shift_morning { get; set; }
        [Column("daily_availability_shift_morning")]
        [JsonProperty("daily_availability_shift_morning")]
        public string DailyAvailabilityShiftAfternoon { get; set; }
        [Column("daily_availability_shift_night")]
        [JsonProperty("daily_availability_shift_night")]
        public string DailyAvailabilityShiftNight { get; set; }
        [Column("daily_availability_shift_overnight")]
        [JsonProperty("daily_availability_shift_overnight")]
        public string DailyAvailabilityShiftOvernight { get; set; }
        [Column("candidate_under_18")]
        [JsonProperty("candidate_under_18")]
        public string CandidateUnder18 { get; set; }
        [Column("special_groups_international")]
        [JsonProperty("special_groups_international")]
        public string SpecialGroupsInternational { get; set; }
        [Column("phone")]
        [JsonProperty("municipality_address")]
        public string MunicipalityAddress { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        [Column("deleted_date")]
        public DateTime DeletedDate { get; set; }
        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
