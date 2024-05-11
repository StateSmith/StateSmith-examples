// Example test file using jest. User-supplied, not generated

import {LightSm} from './LightSm.js';
import {jest} from '@jest/globals'; // We'll use jest in this example. see package.json for config details

// Mock the functions used by the state machine
// We recommend mocking rather than importing your actual functions,
// to keep these tests purely about testing the state machine itself.
// Your implementations should also be tested, but in separate tests.
globalThis.println = jest.fn();

test('starts in the OFF state', () => {
    const sm = new LightSm();
    sm.start();
    expect(globalThis.println.mock.calls).toHaveLength(1);
    // expect(sm.StateId).toBe(LightSm.StateId.OFF);
});

