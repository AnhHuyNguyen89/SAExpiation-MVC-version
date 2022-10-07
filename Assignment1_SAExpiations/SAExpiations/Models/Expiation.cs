using System;
using System.Collections.Generic;

namespace SAExpiations.Models
{
    public partial class Expiation
    {
        public string NoticeStatusDesc { get; set; } = null!;
        public string? DriverLicenseState { get; set; }
        public string? RegistrationState { get; set; }
        public DateTime IncidentStartDate { get; set; }
        public TimeSpan TimeIncidentStart { get; set; }
        public DateTime IssueDate { get; set; }
        public string? ExpiationOffenceCode { get; set; }
        public int? OffencePenaltyAmt { get; set; }
        public int? OffenceLevyAmt { get; set; }
        public int? CorporateFeeAmount { get; set; }
        public string? OffenceStatusCode { get; set; }
        public int? PenaltyWrittenOnNoticeAmount { get; set; }
        public int VehicleSpeed { get; set; }
        public int ExpiationZoneSpeedLimit { get; set; }
        public decimal? BloodAlcoholContentExp { get; set; }
        public string? SpeedCameraCategory { get; set; }
        public int? PhotoRejectionCode { get; set; }
        public string? WithdrawnReasonDesc { get; set; }
        public string NoticeTypeDesc { get; set; } = null!;
        public int? EnforcementWarningNoticeFeeAmount { get; set; }
        public int? FixedCameraLocnCode { get; set; }
        public int? LocationCodeMobileSpeedCamera { get; set; }
        public string? LocalServiceAreaCode { get; set; }

        public virtual State? DriverLicenseStateNavigation { get; set; }
        public virtual ExpiationOffence? ExpiationOffenceCodeNavigation { get; set; }
        public virtual LocalServiceArea? LocalServiceAreaCodeNavigation { get; set; }
        public virtual OffenceStatus? OffenceStatusCodeNavigation { get; set; }
        public virtual PhotoRejectionReason? PhotoRejectionCodeNavigation { get; set; }
        public virtual State? RegistrationStateNavigation { get; set; }
    }
}
