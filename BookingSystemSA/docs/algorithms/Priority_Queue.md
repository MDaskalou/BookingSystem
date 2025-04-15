
---

# 🚑 Priority Queue – Handle Urgent Appointments

## 📖 Why We Chose Priority Queue
Our booking system must **prioritize emergency patients over regular ones**. A **priority queue** ensures that **urgent cases (e.g., critical patients) are scheduled first**, while routine checkups are handled later.

## 📌 Use Case: Schedule Appointments Based on Priority
- **Actors:** System, Doctor, Patient.
- **Preconditions:** The system has an **appointment request queue**.
- **Main Flow:**
  1. Patients request appointments.
  2. The system **assigns a priority** (e.g., **1 = High, 2 = Medium, 3 = Low**).
  3. The **priority queue processes requests** based on urgency.
- **Alternate Flow:** If all slots are filled, non-urgent patients are moved to **next available day**.

## 📝 User Story
*"As a hospital administrator, I want emergency patients to be scheduled before regular appointments so that critical cases receive immediate attention."*

## 🖥️ Code Example (Priority Queue)
```csharp
using System;
using System.Collections.Generic;

public class Appointment
{
    public string PatientName { get; set; }
    public int Priority { get; set; } // 1 = High, 2 = Medium, 3 = Low

    public Appointment(string name, int priority)
    {
        PatientName = name;
        Priority = priority;
    }
}

public class AppointmentScheduler
{
    private PriorityQueue<Appointment, int> queue = new();

    public void AddAppointment(Appointment appt)
    {
        queue.Enqueue(appt, appt.Priority);
    }

    public Appointment GetNextAppointment()
    {
        return queue.Count > 0 ? queue.Dequeue() : null;
    }
}

// Example usage:
class Program
{
    static void Main()
    {
        var scheduler = new AppointmentScheduler();
        scheduler.AddAppointment(new Appointment("John Doe", 2)); // Medium Priority
        scheduler.AddAppointment(new Appointment("Emergency Patient", 1)); // High Priority
        scheduler.AddAppointment(new Appointment("Routine Checkup", 3)); // Low Priority

        Console.WriteLine(scheduler.GetNextAppointment().PatientName); // Output: Emergency Patient
    }
}
