﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.Xml.Serialization;
namespace EdFi.SampleDataGenerator.Models {
//
//	Association classes need to have an additional property of "id"
//
public partial class FeederSchoolAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class InterchangeStaffAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class StaffEducationOrgAssignmentAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class StaffEducationOrgEmploymentAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class StaffProgramAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class TeacherSchoolAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class TeacherSectionAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class StaffCohortAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class StudentCohortAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class StudentDisciplineIncidentAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class StudentEducationOrganizationAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class StudentSchoolAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class StudentSectionAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class EducationOrganizationInterventionPrescriptionAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class StudentInterventionAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class StudentParentAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
public partial class StudentProgramAssociation : IComplexObjectType { 
	[XmlAttribute]
	public string id { get; set; } 
}
//
//	Interchange Types
//
public interface IInterchange<T> { T[] Items { get; set; } }
public partial class InterchangeAssessmentMetadata : IInterchange<ComplexObjectType> {}
public partial class InterchangeDescriptors : IInterchange<DescriptorType> {}
public partial class InterchangeEducationOrganization : IInterchange<object> {}
public partial class InterchangeEducationOrgCalendar : IInterchange<ComplexObjectType> {}
public partial class InterchangeFinance : IInterchange<ComplexObjectType> {}
public partial class InterchangeMasterSchedule : IInterchange<ComplexObjectType> {}
public partial class InterchangePostSecondaryEvent : IInterchange<PostSecondaryEvent> {}
public partial class InterchangeStaffAssociation : IInterchange<object> {}
public partial class InterchangeStudentAssessment : IInterchange<object> {}
public partial class InterchangeStudentAttendance : IInterchange<AttendanceEvent> {}
public partial class InterchangeStudentCohort : IInterchange<object> {}
public partial class InterchangeStudentDiscipline : IInterchange<object> {}
public partial class InterchangeStudentEnrollment : IInterchange<object> {}
public partial class InterchangeStudentGrade : IInterchange<ComplexObjectType> {}
public partial class InterchangeStudentGradebook : IInterchange<ComplexObjectType> {}
public partial class InterchangeStudentIntervention : IInterchange<object> {}
public partial class InterchangeStudentParent : IInterchange<object> {}
public partial class InterchangeStudentProgram : IInterchange<object> {}
public partial class InterchangeStudentTranscript : IInterchange<ComplexObjectType> {}
}