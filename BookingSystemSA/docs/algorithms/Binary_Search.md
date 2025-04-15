# 🔍 Binary Search – Find Available Appointment Slots

## 📖 Why We Chose Binary Search
In our booking system, we need to **quickly find available appointment slots** from a sorted list. **Binary Search** helps us efficiently locate an open time slot without checking every single entry.

## 📌 Use Case: Search for Free Appointment Slots
- **Actors:** Patient, System.
- **Preconditions:** The system has a **sorted list of appointment slots**.
- **Main Flow:**
  1. The patient searches for an available appointment time.
  2. The system performs a **binary search** on the sorted list of available slots.
  3. If a match is found, the system books the appointment.
- **Alternate Flow:** If no match is found, the system suggests the **closest available slot**.

## 📝 User Story
*"As a patient, I want to quickly find an available appointment so that I can book my visit without delays."*

## 🖥️ Code Example (Binary Search)
```csharp
public static int FindAvailableSlot(int[] slots, int target)
{
    int left = 0, right = slots.Length - 1;
    while (left <= right)
    {
        int mid = left + (right - left) / 2;
        if (slots[mid] == target)
            return mid; // Found
        else if (slots[mid] < target)
            left = mid + 1;
        else
            right = mid - 1;
    }
    return -1; // Not found
}

// Example usage:
int[] appointmentSlots = { 9, 10, 11, 14, 15 }; // Sorted slots
int index = FindAvailableSlot(appointmentSlots, 11);
Console.WriteLine(index); // Output: 2 (Slot found at index 2)
