
---

📌 **Create inside `docs/design_patterns/`**

```md
# 📡 Observer Pattern – Notification System

## 📖 Why We Chose Observer Pattern
Doctors, nurses, and patients **need to be notified** when an appointment is scheduled or updated.

## 📌 Use Case: Appointment Notifications
- **Actors:** System, Doctors, Nurses.
- **Preconditions:** An appointment is booked.
- **Main Flow:**
  1. The system books an appointment.
  2. Notifications are sent to relevant stakeholders.

## 📝 User Story
*"As a doctor, I want to receive notifications when a patient books an appointment so that I can be prepared for the consultation."*

## 🖥️ Code Example (Observer Pattern)
```csharp
public interface IObserver
{
    void Update(string message);
}

public class Doctor : IObserver
{
    public void Update(string message) => Console.WriteLine($"Doctor received: {message}");
}

public class BookingSystem
{
    private List<IObserver> observers = new();

    public void Attach(IObserver observer) => observers.Add(observer);
    public void Notify(string message) => observers.ForEach(o => o.Update(message));

    public void BookAppointment()
    {
        Console.WriteLine("Appointment booked.");
        Notify("New appointment scheduled.");
    }
}

class Program
{
    static void Main()
    {
        BookingSystem bookingSystem = new BookingSystem();
        Doctor doc = new Doctor();
        bookingSystem.Attach(doc);
        bookingSystem.BookAppointment();
