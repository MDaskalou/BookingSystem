# 🏭 Factory Pattern – User Role Creation

## 📖 Why We Chose Factory Pattern
In our booking system, we need to **dynamically create different user roles** (Doctors, Nurses, Admins). The Factory Pattern **prevents code duplication** and makes role creation **scalable**.

## 📌 Use Case
### **Use Case: User Role Factory**
- **Actors:** Admin, System.
- **Preconditions:** The admin selects a user role.
- **Main Flow:**
  1. Admin selects a role (Doctor, Nurse, Admin).
  2. The system calls the **UserFactory** to create the appropriate user object.
  3. The system assigns the correct permissions to the user.
- **Alternate Flow:** If an invalid role is selected, the system displays an error message.

## 📝 User Story
*"As an admin, I want to create different user accounts (Doctors, Nurses, Admins) so that they receive the appropriate access permissions in the system."*

## 🖥️ Code Example (Factory Pattern)
```csharp
//Define an interface for Users
public interface IUser
{
    void AssignPermissions();
}

// Implement User Roles
public class Doctor : IUser
{
    public void AssignPermissions() => Console.WriteLine("Doctor has medical access.");
}

public class Nurse : IUser
{
    public void AssignPermissions() => Console.WriteLine("Nurse can monitor patients.");
}

// Implement the Factory Pattern
public class UserFactory
{
    public static IUser CreateUser(string role)
    {
        return role switch
        {
            "Doctor" => new Doctor(),
            "Nurse" => new Nurse(),
            _ => throw new ArgumentException("Invalid role"),
        };
    }
}

// Usage Example
class Program
{
    static void Main()
    {
        IUser doctor = UserFactory.CreateUser("Doctor");
        doctor.AssignPermissions();
    }
}
