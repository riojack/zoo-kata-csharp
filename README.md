## Kata: Refactoring to the Single Responsibility Pattern

Zoo is an off-the-shelf ticketing software package.  It can register, remove, or edit guest tickets.

Zoo patrons can get tickets by calling the park, emailing the park, visiting Guest Services at 
the park's front office, or through the zoo's website.

The code is a mess, though, because it was written in a single weekend by two software developers.  
Can you clean it up?

#### Requirements

As a guest services agent<br />
I want to list existing tickets<br />
So that I can see whats guest will be visiting the zoo.

As a guest services agent<br />
I want to create a new ticket for a guest<br />
So that the guest is able to validate at the front gate.

As a guest services agent<br />
I want to be able to remove a ticket<br />
So that guests are able to cancel their attendance.

As a guest services agent<br />
I want to be able to edit details of a ticket<br />
So that the guest's information stays up-to-date.

Other considerations:
  - The Zoo will sometimes run campaigns through third-party vendors
  - Tickets can only be purchased with a credit or debit card.
  - After purchase, tickets can be mailed, emailed, and/or sent to the guest via the Zoo's mobile app.

### Rules
1. Behavior cannot change.
2. Tests cannot be modified except for renames and moves.
3. All tests must pass.

### Outcomes
1. Code should be clear of SRP violations.

### Technical Notes
Publish a self-contained binary on OSX Mojave: `dotnet publish Zoo -c release --self-contained --runtime osx.10.14-x64 --framework netcoreapp2.2`.

Other runtimes: [RID Catalog](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog).