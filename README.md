## Kata: Refactoring to the Single Responsibility Pattern

Zoo is an off-the-shelf ticketing software package.  It can register, remove, or edit guest tickets.

Zoo patrons can get tickets by calling the park, emailing the park, visiting Guest Services at 
the park's front office, or through the zoo's website.

The code is a mess, though, because it was written in a single weekend by two software developers.  
Can you clean it up?

#### Requirements

As a guest services agent
I want to create a new ticket for a guest
So that the guest is able to validate at the front gate.

As a guest services agent
I want to be able to remove a ticket
So that guests are able to cancel their attendance.

As a guest services agent
I want to be able to edit details of a ticket
So that guests are able update blah blah blah

### Rules
1. Behavior cannot change.
2. Tests cannot be modified except for renames and moves.

### Outcomes
1. Code should be clear of SRP violations.
