# EXPERIMENTAL!

Stuff in here will change. This directory name will also likely change. Don't link to it just yet.

# Purpose
Demonstrates how StateSmith could generate some simple scaffolding to make testing simple.

This example demonstrates JS tests using npm and jest.

StateSmith would generate the following:
- LightSm.sample.jest.test.js
- package.sample.json (static)

Users would use the sample as guidance on how to write their own tests.
The easiest thing to do is copy the sample file to LightSm.jest.test.js
and start editing. I've done that and provided LightSm.jest.test.js,
which is how I imagine a user might go about testing this particular
state machine.

Setup instructions are in the test file, but tldr `npm test`.

In this example I added "jest" to the file names. It's a bit verbose and
maybe we don't want to do that, but I did it to suggest that we could
also provide samples using other frameworks. eg. we could allow the
user to select between jest and mocha generators. But this might be overkill.

# Other languages

JS is a handy language for writing tests, but there are all kinds of reasons
someone might want to use a different language. The idea would be to generate
tests for any languages that the user specifies. If and when StateSmith
generates JS by default https://github.com/StateSmith/StateSmith/issues/267, 
this would give the user the choice of implementing in there target language
or implementing in JS.

The JS state machine is easy to inspect, but that may not be the case for other
languages such as C++ that support access restrictions. StateSmith will need
to provide an inspection interface to allow developers to write tests based 
on internal state, eg. the equivalent of expect(sm.stateId).toBe(LightSm.StateId.OFF);