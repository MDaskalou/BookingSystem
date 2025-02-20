# BookingSystem
Project Idea (Scenario)
Brief Description:
The project aims to develop a digital booking system for ECT treatments at Sahlgrenska Hospital. The system will facilitate the administration and scheduling of treatment appointments for staff and optimize patient flow management efficiently.

_________________________________________________________________________________________________________________________________________________________________________________________________________

Background & Purpose:
ECT treatment requires careful planning and coordination between different departments. Currently, bookings are handled manually (via phone calls and fax), which can lead to inefficiencies and risks of missed documentation. A digital system would improve workflows, reduce administrative burdens, and enhance patient safety.
_________________________________________________________________________________________________________________________________________________________________________________________________________

Vision & Goals
Vision/Problem Statement:
The goal is to create a secure and efficient booking system for the ECT department that reduces manual administration and provides better oversight of planned treatments.

_________________________________________________________________________________________________________________________________________________________________________________________________________

Clear Objectives:

Implement a digital booking platform for healthcare staff.
Enable seamless management of bookings, cancellations, and prioritizations.
Create a notification functionality to reduce missed treatment sessions.
Integrate a secure documentation module for patient data, ensuring GDPR compliance and patient confidentiality.
Improve coordination between different hospital departments.
Project success can be measured by:

Reduced number of manual errors in the booking process.
Increased user interaction and efficiency among healthcare personnel.
Fewer missed treatment sessions.
Improved time efficiency.
Stakeholder Mapping
________________________________________________________________________________________________________________________________________________________________________________________________________

Roles:

Nurses – Schedule appointments and manage documentation.
Specialist Doctors – Assess patient needs, approve, and book treatments.
Junior Doctors – Schedule appointments and manage documentation.
ECT Staff – Coordinate treatment sessions and prioritize bookings.
IT Department – Developers and system administrators responsible for implementation and maintenance.
Hospital Management – Project owners responsible for resource allocation and implementation decisions.
Patients – Indirect stakeholders who benefit from a more structured booking system.
________________________________________________________________________________________________________________________________________________________________________________________________________

Requirement Specification – Digital Booking System for ECT Treatments

1. Functional Requirements
Booking Management
  *The system must support booking and cancellation of ECT treatments.
  *The system must handle two treatment types: ECT and rTMS.
  *The system must be able to register and manage four types of ECT treatments:
    *Index Series (acute/inpatient treatment)
    *Maintenance ECT (outpatient treatment)
    *GREES-ECT (outpatient treatment)
    *Outpatient Index Series
   
Priority Management
  *The system must allow color-coded booking based on priority:
    *Green: Low priority
    *Yellow: Medium priority
    *Red: High priority
History and Traceability
  *The system must log all bookings and changes.
   
Search and Filtering Functions
  *The system must allow filtering and searching of bookings based on:
    *Patient
    *Department
    *Date

Notifications
  *The system must send notifications to healthcare personnel when a treatment time has been assigned.

Documentation Management
  *The system must ensure that the correct documentation is submitted before a booking can be completed.
  *The system must support document submission through the application while ensuring patient safety and confidentiality.
________________________________________________________________________________________________________________________________________________________________________________________________________

Non-Functional Requirements
  *The system must support up to 10 concurrent departments.
  *The system must be available on desktop (mobile adaptation planned for the future).
  *The system must have two-factor authentication via BankID.
  *The system must complete a booking within 1 second.
  *The system requires an internet connection to function.
________________________________________________________________________________________________________________________________________________________________________________________________________

Notifications and User Experience
  *The system must send reminders only to healthcare personnel.
  *Users must be able to view a summary of upcoming treatment sessions.
________________________________________________________________________________________________________________________________________________________________________________________________________
Documentation Management
  *Each department must submit the correct documentation before a booking can be completed.
  *A verification function must be in place for users to confirm that documents have been submitted.
  *If documentation is missing, the system must block the booking until it is resolved.
  *In the future, the system should support direct document submission through the application.
________________________________________________________________________________________________________________________________________________________________________________________________________

Requirement Prioritization (MoSCoW Method)
Must Have (Mandatory Requirements)
✅ Booking and cancellation functionality
✅ Notification system for healthcare personnel
✅ Logging of bookings and changes
✅ Access control system for different roles (nurse, junior doctor, specialist doctor, senior doctor)
✅ Requirement that correct documentation is submitted before a booking can be completed

Should Have (Important but Not Critical Requirements)
⚡ Integration with hospital systems
⚡ Dashboard with an overview of available time slots and treatments
⚡ Improved filter and search functions

Could Have (Future Improvements and Possible Additions)
💡 AI-based scheduling optimization
💡 Mobile adaptation
💡 Automated report generation and statistics
________________________________________________________________________________________________________________________________________________________________________________________________________

User Stories
As a nurse, I want to be able to book an ECT treatment so that the patient receives a treatment appointment.
As a junior doctor, I want to be able to cancel an ECT treatment so that the patient’s treatment can be rescheduled if necessary.
As a specialist doctor, I want to be able to prioritize patients based on medical urgency so that the most critical patients receive treatment first.
As ECT staff, I want to be able to receive bookings and schedule treatment times so that resources are used optimally.
As a senior doctor, I want to be able to approve changes and cancellations of treatments so that the treatment plan is followed correctly.
________________________________________________________________________________________________________________________________________________________________________________________________________
Use Cases
Name: Create a Booking for ECT Treatment
Actors: Nurse, junior doctor, specialist doctor

Preconditions:
✅ The user is logged in and has the appropriate permissions.
✅ The patient is approved for ECT treatment.

Main Flow:

The user navigates to the booking system.
Selects a patient and the type of ECT treatment.
The system checks available time slots and treatment capacity.
The user confirms the booking.
The system registers the booking and sends a notification to the ECT department.
Alternative Flows:
❗ If no available time slots exist, the user receives a message to choose another day.
❗ If the patient lacks approval from the senior doctor, a warning is displayed.
________________________________________________________________________________________________________________________________________________________________________________________________________
