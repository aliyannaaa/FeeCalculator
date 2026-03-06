# FeeCalculator - Windows Forms Application

## Project Overview

This application is a simple Windows Forms (WinForms) desktop program developed in C#.
It calculates a service fee using the following formula:

Fee = ROUNDUP((Surcharge + 5) x 0.045) + 10

The user enters a surcharge amount, clicks the "Compute" button, and the computed fee is displayed on the form.

It calculates the AR amount using the following formula:

AR Amount = Ticket total + payment service fee

The user enters a total amount of the ticket and the servivce fee, clicks the "Compute" button, and the computed fee is displayed on the form.

---

# Application Architecture

The application follows a basic event-driven architecture common in Windows Forms applications.

### 1. Presentation Layer (User Interface)

The UI contains:
- TextBox - for surcharge input, ticket total, and payment service fee
- Button - to trigger computation and clear
- Label - to display the computed fee

The UI is responsible for:
- Accepting user input
- Displaying the result

---

## How to Run the Project

1. Open th solution in Visual Studio Community.
2. Build the project (Ctrl + Shift + B).
3. Click Start.
4. Click the drop down list where it ask "Choose what fee to be calculated?".
5. Enter the amount in the Surcharge tab or in the AR Amount tab.
6. Click the "Compute" button to view the fee.
7. Click "Clear" if you want to input another amount.
8. Press Ctrl + Shift + A for the admin button to appear.
9. Click the "Admin" button if you want to change the formula.
10. Once you change the formula, click "Ok" button and a popup will appear saying "Formula updated".
11. Enter the amount in the Surcharge tab or in the AR Amount tab.
12. Click the "Compute" button to view the updated formula and the fees.

---

## Design Decisions
1. Windows Forms was used because it is simple and suitable for beginner-level desktop applications.
2. Math.Ceiling() was used to implement the ROUNDUP requirement.
3. The application follows event-driven programming where the calculation happen when the button is clicked.
4. Implemented a hidden hotkey (Ctrl + Shift + A) to toggle admin features, keeping configuration tools away from standard users while maintaining a clean UI.
5. Controls are dynamically shown or hidden based on the selected fee type to reduce clutter and prevent input errors.
