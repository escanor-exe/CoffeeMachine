# Coffee Vending Machine — Product Requirements

## 1. Overview

Develop a console-based Coffee Vending Machine application that allows a customer to select and customize a coffee, confirm the order, and monitor its preparation.

The application is intended to simulate the software behavior of a single coffee machine. No physical hardware or external systems are required.

The solution should focus on correct behavior, clear separation of responsibilities, and reasonable extensibility.

No specific architecture, design pattern, C# language feature, or implementation technique is prescribed.

---

## 2. Product Objective

The system shall allow a customer to:

1. Create a coffee order.
2. Select the coffee type, size, and strength.
3. Select optional customizations.
4. Review and confirm the order.
5. Start coffee preparation.
6. Monitor preparation progress.
7. Cancel an active preparation.
8. Be notified when preparation is completed or cancelled.
9. Place another order once the machine becomes available.

---

## 3. Supported Coffee Types

The machine shall support the following coffee types:

- Espresso
- Americano
- Cappuccino
- Latte

Each coffee type shall have its own required preparation sequence.

A preparation sequence may contain stages such as:

- Grinding
- Heating
- Extraction
- Milk preparation
- Mixing
- Serving

Not every coffee type is required to use every stage.

---

## 4. Coffee Size

The customer shall select one of the following sizes:

- Small
- Medium
- Large

The selected size shall affect the preparation requirements and/or preparation duration.

---

## 5. Coffee Strength

The customer shall select one of the following strengths:

- Mild
- Normal
- Strong

The selected strength shall affect the preparation requirements and/or preparation duration.

---

## 6. Optional Customizations

The customer may select zero or more of the following:

- Sugar
- Extra Milk
- Extra Coffee Shot

The same customization shall not be selected more than once for the same order.

An order without any customization shall be valid.

---

## 7. Order Creation

The customer shall create an order by specifying:

- Coffee type
- Size
- Strength
- Optional customizations

An order shall not be considered ready for preparation until all required selections have been provided.

Before preparation begins, the system shall display an order summary containing:

- Selected coffee
- Selected size
- Selected strength
- Selected customizations
- Estimated preparation time

The customer shall be able to confirm or cancel the order.

---

## 8. Preparation

Once an order is confirmed, the machine shall prepare the coffee using the preparation sequence associated with the selected coffee type.

Preparation stages shall execute in the required order.

Each preparation stage shall require a defined amount of time.

The total estimated preparation time shall depend on the characteristics of the selected order and the stages required to prepare it.

The estimated preparation time shall be displayed before preparation starts.

---

## 9. Preparation Progress

While preparation is in progress, the system shall display meaningful progress information.

At minimum, the customer shall be able to determine:

- The coffee currently being prepared.
- The current preparation stage.
- Progress of the current stage.
- Overall preparation progress.
- Approximate remaining preparation time.

Progress information shall be updated while preparation is running.

---

## 10. Cancellation

The customer shall be able to cancel an active preparation.

When cancellation occurs:

1. The active preparation shall stop.
2. The order shall be considered cancelled.
3. The machine shall no longer report the order as successfully completed.
4. The machine shall become available for another order.
5. The customer shall be informed that preparation was cancelled.

A cancelled preparation shall not subsequently transition to a successful completion state.

---

## 11. Successful Completion

When all required preparation stages have completed successfully:

1. The order shall be considered completed.
2. The customer shall be informed that the coffee is ready.
3. The machine shall become available for another order.

---

## 12. Machine Availability

The machine shall prepare only one coffee at a time.

While a coffee is being prepared:

- A second preparation shall not be started.
- The active preparation shall remain the machine's current operation.

After the active preparation is either completed or cancelled, the machine shall become available for another order.

---

## 13. Machine States

The system shall distinguish between the machine's major operational states.

At minimum, the following situations shall be represented:

- Available for a new order
- Preparing an order
- Preparation completed
- Preparation cancelled

Operations that are invalid for the current state shall be rejected appropriately.

For example, a second preparation must not be started while another preparation is already active.

---

## 14. Invalid Input and Operations

The application shall handle invalid input and invalid operations without terminating unexpectedly.

Examples include:

- Selecting an unsupported coffee type.
- Selecting an invalid size.
- Selecting an invalid strength.
- Attempting to confirm an incomplete order.
- Attempting to start a preparation while the machine is busy.
- Attempting to cancel when no preparation is active.
- Selecting the same customization more than once.

The application shall provide an appropriate response and allow the customer to continue where applicable.

---

## 15. Configuration

The preparation characteristics of the machine shall be configurable.

Configuration shall include, at minimum:

- Preparation stages associated with each coffee type.
- Base duration of each preparation stage.
- Effect of coffee size on preparation time.
- Effect of coffee strength on preparation time.

Changing preparation configuration should not require changes to unrelated parts of the application.

For example, changing the duration of an extraction stage should not require changes to order creation or user-interface behavior.

---

## 16. Preparation Notifications

The system shall provide notifications for significant preparation events.

At minimum, the customer shall be informed when:

- Preparation starts.
- A preparation stage starts.
- A preparation stage completes.
- Preparation is cancelled.
- Preparation completes successfully.

The implementation mechanism for these notifications is not prescribed.

---

## 17. Console Application

The application shall provide a console-based user interface.

The customer shall be able to:

1. Start a new order.
2. Select the coffee type.
3. Select the size.
4. Select the strength.
5. Select optional customizations.
6. Review the order.
7. Confirm or cancel the order.
8. Monitor preparation.
9. Cancel an active preparation.
10. Start another order after the machine becomes available.
11. Exit the application.

The console shall clearly communicate the available actions and current machine status.

---

## 18. Non-Functional Requirements

### 18.1 Responsiveness

The application shall remain responsive while preparation is in progress.

The customer shall be able to request cancellation without waiting for the entire preparation process to complete.

### 18.2 Separation of Responsibilities

User-interface concerns shall be separated from the rules governing coffee preparation.

Changes to the console presentation should not require changes to the underlying preparation rules.

### 18.3 Extensibility

The design should allow the following changes without extensive modification to unrelated functionality:

- Adding a new coffee type.
- Adding a new size.
- Adding a new customization.
- Changing preparation durations.
- Changing the preparation sequence of an existing coffee.
- Changing size or strength effects on preparation time.

---

## 19. Out of Scope

The following are explicitly outside the scope of this project:

- Payment processing
- User accounts
- Authentication
- Database persistence
- Web APIs
- Graphical user interfaces
- Multiple machines
- Physical hardware integration
- Network communication
- Order delivery
- Customer history
- Inventory management
- External notifications
- Temperature simulation
- Physical or scientific simulation of coffee preparation

The application is intended to simulate the software behavior of a single coffee vending machine.

---

## 20. Example User Scenario

A customer starts the application and creates an order:

- Coffee: Cappuccino
- Size: Medium
- Strength: Strong
- Customization: Sugar

The system displays the order summary and estimated preparation time.

The customer confirms the order.

The machine begins preparation and reports the progress of each stage.

For example:

```text
Grinding       - Started
Grinding       - Completed

Heating        - Started
Heating        - Completed

Extraction     - Started
Extraction     - Completed

Milk           - Started
...
```

The customer may cancel the preparation while it is running.

If cancelled:

```text
Preparation cancelled.
Machine is now available.
```

If preparation completes successfully:

```text
Preparation completed.
Your Cappuccino is ready.
Machine is now available.
```

---

## 21. Acceptance Criteria

The implementation shall be considered functionally complete when:

- A valid coffee order can be created.
- Invalid selections are handled appropriately.
- An order can be reviewed before preparation.
- The customer can confirm or cancel an order.
- Each coffee type follows its configured preparation sequence.
- Preparation takes the configured duration.
- Preparation progress is visible while the machine is operating.
- An active preparation can be cancelled.
- A cancelled preparation does not report successful completion.
- A successfully completed preparation reports the coffee as ready.
- Only one preparation can be active at a time.
- The machine becomes available after completion or cancellation.
- Preparation configuration can be changed without modifying unrelated functionality.
- The application remains usable after invalid input or a cancelled preparation.
- The application can be exited cleanly.

---

## 22. Implementation Constraint

This is a C# practice project.

The requirements intentionally do **not** prescribe:

- Classes
- Interfaces
- Inheritance
- Composition
- Generics
- Delegates
- Events
- Tasks
- Threads
- Cancellation tokens
- Collections
- Design patterns
- Dependency injection
- Specific project structure

These are implementation decisions to be made by the developer based on the requirements above.
