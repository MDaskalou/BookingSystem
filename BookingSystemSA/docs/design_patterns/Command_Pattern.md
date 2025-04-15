
---



# 📅 Command Pattern – Booking Management

## 📖 Why We Chose Command Pattern
In our booking system, we need a **flexible way to manage appointment requests**, allowing users to **book, update, or cancel** an appointment. The **Command Pattern** encapsulates these actions as commands that can be executed, undone, or stored.

## 📌 Use Case: Manage Booking Requests
- **Actors:** Patient, Doctor, System.
- **Preconditions:** The patient wants to book an appointment.
- **Main Flow:**
  1. The patient selects a date and submits a booking request.
  2. The **Command Pattern executes** the booking request.
  3. The system stores the command, allowing it to be undone if needed.
- **Alternate Flow:** If the patient cancels, the system **executes the undo command**.

## 📝 User Story
*"As a patient, I want to book, modify, or cancel my appointment so that I have control over my healthcare schedule."*

## 🖥️ Code Example (Command Pattern)
```csharp
// Step 1: Define the Command Interface
public interface ICommand
{
    void Execute();
    void Undo();
}

// Step 2: Implement Concrete Commands
public class BookAppointmentCommand : ICommand
{
    private string _appointment;
    public BookAppointmentCommand(string appointment) => _appointment = appointment;

    public void Execute() => Console.WriteLine($"Booking appointment: {_appointment}");
    public void Undo() => Console.WriteLine($"Canceling appointment: {_appointment}");
}

// Step 3: Implement the Invoker
public class AppointmentManager
{
    private ICommand _command;
    public void SetCommand(ICommand command) => _command = command;
    public void ConfirmBooking() => _command.Execute();
    public void CancelBooking() => _command.Undo();
}

// Step 4: Usage Example
class Program
{
    static void Main()
    {
        ICommand bookCommand = new BookAppointmentCommand("John Doe, 2024-06-15");
        AppointmentManager manager = new AppointmentManager();

        manager.SetCommand(bookCommand);
        manager.ConfirmBooking(); // Output: Booking appointment: John Doe, 2024-06-15
        manager.CancelBooking(); // Output: Canceling appointment: John Doe, 2024-06-15
    }
}
