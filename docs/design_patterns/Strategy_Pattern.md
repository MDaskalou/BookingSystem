# 🔑 Strategy Pattern – Authentication System

## 📖 Why We Chose Strategy Pattern
Our system supports **multiple authentication methods** (BankID and Mobile Verification). Instead of hardcoding these options, the **Strategy Pattern** allows users to dynamically choose an authentication method.

## 📌 Use Case: Select Authentication Method
- **Actors:** Healthcare Personnel, System.
- **Preconditions:** The user is logging into the system.
- **Main Flow:**
  1. The system presents two authentication options: **BankID** or **Mobile Verification**.
  2. The user selects an authentication method.
  3. The system executes the chosen authentication strategy.
- **Alternate Flow:** If authentication fails, the system prompts the user to try another method.

## 📝 User Story
*"As a healthcare professional, I want to authenticate my login using either BankID or Mobile Verification so that I can securely access the system."*

## 🖥️ Code Example (Strategy Pattern)
```csharp
// Step 1: Define the Strategy Interface
public interface IAuthenticationStrategy
{
    void Authenticate();
}

// Step 2: Implement Concrete Strategies
public class BankIDAuthentication : IAuthenticationStrategy
{
    public void Authenticate() => Console.WriteLine("Authenticating via BankID...");
}

public class MobileVerification : IAuthenticationStrategy
{
    public void Authenticate() => Console.WriteLine("Authenticating via Mobile Verification...");
}

// Step 3: Context Class
public class AuthenticationContext
{
    private IAuthenticationStrategy _strategy;

    public AuthenticationContext(IAuthenticationStrategy strategy) => _strategy = strategy;

    public void AuthenticateUser() => _strategy.Authenticate();
}

// Step 4: Usage Example
class Program
{
    static void Main()
    {
        AuthenticationContext authContext = new AuthenticationContext(new BankIDAuthentication());
        authContext.AuthenticateUser(); // Output: Authenticating via BankID...

        authContext = new AuthenticationContext(new MobileVerification());
        authContext.AuthenticateUser(); // Output: Authenticating via Mobile Verification...
    }
}
