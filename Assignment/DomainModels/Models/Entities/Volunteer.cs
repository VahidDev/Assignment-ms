using DomainModels.Models.Entities.Base;
using DomainModels.Models.Enums;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models.Entities
{
    [Table("volunteers")]
    public class Volunteer:Entity
    {
        [ForeignKey("role_offer_id")]
        public int? RoleOfferId { get; set; }
        [Column("status", TypeName = "character varying")]
        public Status Status { get; set; }
        [Column("first_name", TypeName = "character varying")]
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [Column("last_name", TypeName = "character varying")]
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [Column("picture", TypeName = "character varying")]
        [JsonProperty("picture")]
        public string Picture { get; set; }
        [Column("phone", TypeName = "character varying")]
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [Column("email", TypeName = "character varying")]
        [JsonProperty("email")]
        public string Email {get;set;}

        [Column("nationality", TypeName = "character varying")]
        [JsonProperty("nationality")]
        public string Nationality { get; set; }
        [Column("_country_issued", TypeName = "character varying")]
        [JsonProperty("_country_issued")]
        public string CountryIssued { get; set; }
        [Column("gender_for_accreditation", TypeName = "character varying")]
        [JsonProperty("gender_for_accreditation")]
        public string GenderForAccreditation { get; set; }
        [Column("dob", TypeName = "date")]
        [JsonProperty("dob")]
        public DateTime? Dob { get; set; }
        [Column("current_occupation", TypeName = "character varying")]
        [JsonProperty("current_occupation")]
        public string CurrentOccupation { get; set; }
        [Column("id_document_type", TypeName = "character varying")]
        [JsonProperty("id_document_type")]
        public string IdDocumentType { get; set; }
        [Column("qid_number", TypeName = "character varying")]
        [JsonProperty("qid_number")]
        public string QidNumber { get; set; }
        [Column("id_document_expiry_date_q22",TypeName ="date")]
        [JsonProperty("id_document_expiry_date_q22")]
        public DateTime? IdDocumentExpiryDateQ22 { get; set; }
        [Column("passport_number", TypeName = "character varying")]
        [JsonProperty("passport_number")]
        public string PassportNumber { get; set; }
        [Column("id_document_expiry_date", TypeName = "date")]
        [JsonProperty("id_document_expiry_date")]
        public DateTime? IdDocumentExpiryDate { get; set; }
        [Column("id_document_country_of_issue", TypeName = "character varying")]
        [JsonProperty("id_document_country_of_issue")]
        public string IdDocumentCountryOfIssue { get; set; }
        [Column("passport_expiry_date", TypeName = "date")]
        [JsonProperty("passport_expiry_date")]
        public DateTime? PassportExpiryDate { get; set; }
        [Column("passport_type", TypeName = "character varying")]
        [JsonProperty("passport_type")]
        public string PassportType { get; set; }
        [Column("qatari_driving_license")]
        [JsonProperty("qatari_driving_license")]
        public bool? QatariDrivingLicense { get; set; }
        [Column("driving_license_type",TypeName = "character varying")]
        [JsonProperty("driving_license_type")]
        public string DrivingLicenseType { get; set; }
        [Column("country", TypeName = "character varying")]
        [JsonProperty("country")]
        public string Country { get; set; }
        [Column("international_accommodation_preference", TypeName = "character varying")]
        [JsonProperty("international_accommodation_preference")]
        public string  InternationalAccommodationPreference { get; set; }
        [Column("medical_conditions", TypeName = "character varying")]
        [JsonProperty("medical_conditions")]
        public string MedicalConditions { get; set; }
        [Column("disability_yes_no")]
        [JsonProperty("disability_yes_no")]
        public bool? DisabilityYesNo { get; set; }
        [Column("disability_type", TypeName = "character varying")]
        [JsonProperty("disability_type")]
        public string DisabilityType { get; set; }
        [Column("disability_others", TypeName = "character varying")]
        [JsonProperty("disability_others")]
        public string DisabilityOthers { get; set; }
        [Column("social_worker_caregiver_support", TypeName = "character varying")]
        [JsonProperty("social_worker_caregiver_support")]
        public string SocialWorkerCaregiverSupport { get; set; }
        [Column("dietary_requirement_identification", TypeName = "character varying")]
        [JsonProperty("dietary_requirement_identification")]
        public string DietaryRequirementIdentification { get; set; }
        [Column("special_dietary_options", TypeName = "character varying")]
        [JsonProperty("special_dietary_options")]
        public string SpecialDietaryOptions { get; set; }
        [Column("alergies_other", TypeName = "character varying")]
        [JsonProperty("alergies_other")]
        public string AlergiesOther { get; set; }
        [Column("covid-19_vaccinated", TypeName = "character varying")]
        [JsonProperty("covid-19_vaccinated")]
        public string Covid19Vaccinated { get; set; }
        [Column("vaccine_type_multi", TypeName = "character varying")]
        [JsonProperty("vaccine_type_multi-select")]
        public string VaccineTypeMultiSelect { get; set; }
        [Column("final_vaccine_dose_date", TypeName = "character varying")]
        [JsonProperty("final_vaccine_dose_date")]
        public string FinalVaccineDoseDate { get; set; }
        [Column("education_onechoice", TypeName = "character varying")]
        [JsonProperty("education_onechoice")]
        public string EducationOnechoice { get; set; }
        [Column("area_of_study", TypeName = "character varying")]
        [JsonProperty("area_of_study")]
        public string AreaOfStudy { get; set; }
        [Column("english_fluency_level", TypeName = "character varying")]
        [JsonProperty("english_fluency_level")]
        public string EnglishFluencyLevel { get; set; }
        [Column("arabic_fluency_level", TypeName = "character varying")]
        [JsonProperty("arabic_fluency_level")]
        public string ArabicFluencyLevel { get; set; }
        [Column("additional_language_1", TypeName = "character varying")]
        [JsonProperty("additional_language_1")]
        public string AdditionalLanguage1 { get; set; }
        [Column("additional_language_1_fluency_level", TypeName = "character varying")]
        [JsonProperty("additional_language_1_fluency_level")]
        public string AdditionalLanguage1FluencyLevel { get; set; }
        [Column("additional_language_2", TypeName = "character varying")]
        [JsonProperty("additional_language_2")]
        public string AdditionalLanguage2 { get; set; }
        [Column("additional_language_2_fluency_level", TypeName = "character varying")]
        [JsonProperty("additional_language_2_fluency_level")]
        public string AdditionalLanguage2FluencyLevel { get; set; }
        [Column("additional_language_3", TypeName = "character varying")]
        [JsonProperty("additional_language_3")]
        public string AdditionalLanguage3 { get; set; }
        [Column("additional_language_3_fluency_level", TypeName = "character varying")]
        [JsonProperty("additional_language_3_fluency_level")]
        public string AdditionalLanguage3FluencyLevel { get; set; }
        [Column("additional_language_4", TypeName = "character varying")]
        [JsonProperty("additional_language_4")]
        public string AdditionalLanguage4 { get; set; }
        [Column("additional_language_4_fluency_level", TypeName = "character varying")]
        [JsonProperty("additional_language_4_fluency_level")]
        public string AdditionalLanguage4FluencyLevel { get; set; }
        [Column("languages_other", TypeName = "character varying")]
        [JsonProperty("languages_other")]
        public string LanguagesOther { get; set; }
        [Column("interpretation_and_translation_experience", TypeName = "character varying")]
        [JsonProperty("interpretation_and_translation_experience")]
        public string InterpretationAndTranslationExperience { get; set; }
        [Column("certified_translator_language", TypeName = "character varying")]
        [JsonProperty("certified_translator_language")]
        public string CertifiedTranslatorLanguage { get; set; }
        [Column("describe_your_it_skills", TypeName = "character varying")]
        [JsonProperty("describe_your_it_skills")]
        public string DescribeYourItSkills { get; set; }
        [Column("skill_1", TypeName = "character varying")]
        [JsonProperty("skill_1")]
        public string Skill1 { get; set; }
        [Column("skill_2", TypeName = "character varying")]
        [JsonProperty("skill_2")]
        public string Skill2 { get; set; }
        [Column("skill_3", TypeName = "character varying")]
        [JsonProperty("skill_3")]
        public string Skill3 { get; set; }
        [Column("skill_4", TypeName = "character varying")]
        [JsonProperty("skill_4")]
        public string Skill4 { get; set; }
        [Column("skill_5", TypeName = "character varying")]
        [JsonProperty("skill_5")]
        public string Skill5 { get; set; }
        [Column("skill_6", TypeName = "character varying")]
        [JsonProperty("skill_6")]
        public string Skill6 { get; set; }
        [Column("previous_volunteering_experience", TypeName = "character varying")]
        [JsonProperty("previous_volunteering_experience")]
        public string PreviousVolunteeringExperience { get; set; }
        [Column("volunteer_experience", TypeName = "character varying")]
        [JsonProperty("volunteer_experience")]
        public string VolunteerExperience { get; set; }
        [Column("other_role", TypeName = "character varying")]
        [JsonProperty("other_role")]
        public string OtherRole { get; set; }
        [Column("events_type_participations", TypeName = "character varying")]
        [JsonProperty("events_type_participations")]
        public string EventsTypeParticipations { get; set; }
        [Column("other_event_type", TypeName = "character varying")]
        [JsonProperty("other_event_type")]
        public string OtherEventType { get; set; }
        [Column("volunteer_hours_yearly", TypeName = "character varying")]
        [JsonProperty("volunteer_hours_yearly")]
        public string VolunteerHoursYearly { get; set; }
        [Column("preferred_functional_area", TypeName = "character varying")]
        [JsonProperty("preferred_functional_area")]
        public string PreferredFunctionalArea { get; set; }
        [Column("fwc_are_you_interested_in_a_leadership_role", TypeName = "character varying")]
        [JsonProperty("fwc_are_you_interested_in_a_leadership_role")]
        public string FwcAreYouInterestedInALeadershipRole { get; set; }
        [Column("fwc_leadership_experience", TypeName = "character varying")]
        [JsonProperty("fwc_leadership_experience")]
        public string FwcLeadershipExperience { get; set; }
        [Column("work_experience", TypeName = "character varying")]
        [JsonProperty("work_experience")]
        public string WorkExperience { get; set; }
        [Column("ceremonies_yes_no")]
        [JsonProperty("ceremonies_yes_no")]
        public bool? CeremoniesYesNo { get; set; }
        [Column("cast_yes_no")]
        [JsonProperty("cast_yes_no")]
        public bool? CastYesNo { get; set; }
        [Column("cast_options", TypeName = "character varying")]
        [JsonProperty("cast_options")]
        public string CastOptions { get; set; }
        [Column("motivation_to_volunteer_at_fwc", TypeName = "character varying")]
        [JsonProperty("motivation_to_volunteer_at_fwc")]
        public string MotivationToVolunteerAtFwc { get; set; }
        [Column("leave_or_arrange", TypeName = "character varying")]
        [JsonProperty("leave_or_arrange")]
        public string LeaveOrArrange { get; set; }
        [Column("local__international_volunteer", TypeName = "character varying")]
        [JsonProperty("local__international_volunteer")]
        public string LocalInternationalVolunteer { get; set; }
        [Column("language_path_-_english__arabic", TypeName = "character varying")]
        [JsonProperty("language_path_-_english__arabic")]
        public string LanguagePathEnglishArabic { get; set; }
        [Column("fwc_availability_pre_tournament_stage_one", TypeName = "character varying")]
        [JsonProperty("fwc_availability_pre_tournament_stage_one")]
        public string FwcAvailabilityPreTournamentStageOne { get; set; }
        [Column("fwc_availability_pre_tournament_stage_two", TypeName = "character varying")]
        [JsonProperty("fwc_availability_pre_tournament_stage_two")]
        public string FwcAvailabilityPreTournamentStageTwo { get; set; }
        [Column("availability_during_tournament", TypeName = "character varying")]
        [JsonProperty("availability_during_tournament")]
        public string AvailabilityDuringTournament { get; set; }
        [Column("daily_availability_shift_morning", TypeName = "character varying")]
        [JsonProperty("daily_availability_shift_morning")]
        public string daily_availability_shift_morning { get; set; }
        [Column("daily_availability_shift_morning", TypeName = "character varying")]
        [JsonProperty("daily_availability_shift_morning")]
        public string DailyAvailabilityShiftAfternoon { get; set; }
        [Column("daily_availability_shift_night", TypeName = "character varying")]
        [JsonProperty("daily_availability_shift_night")]
        public string DailyAvailabilityShiftNight { get; set; }
        [Column("daily_availability_shift_overnight", TypeName = "character varying")]
        [JsonProperty("daily_availability_shift_overnight")]
        public string DailyAvailabilityShiftOvernight { get; set; }
        [Column("candidate_under_18", TypeName = "character varying")]
        [JsonProperty("candidate_under_18")]
        public string CandidateUnder18 { get; set; }
        [Column("special_groups_international", TypeName = "character varying")]
        [JsonProperty("special_groups_international")]
        public string SpecialGroupsInternational { get; set; }
        [Column("phone", TypeName = "character varying")]
        [JsonProperty("municipality_address")]
        public string MunicipalityAddress { get; set; }
    }
}
