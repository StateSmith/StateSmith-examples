// Example test file using jest. User-supplied, not generated

import {LightSm} from './LightSm.js';
import {jest} from '@jest/globals'; // We'll use jest in this example. see package.json for config details

// Mock the functions used by the state machine
// We recommend mocking rather than importing your actual functions,
// to keep these tests purely about testing the state machine itself.
// Your implementations should also be tested, but in separate tests.
globalThis.println = jest.fn();
globalThis.light_blue = jest.fn();

beforeEach(() => {
    jest.clearAllMocks();
});

test('starts in the off state', () => {
    const sm = new LightSm();
    sm.start();
    expect(sm.stateId).toBe(LightSm.StateId.OFF);
});

test('println to be called once on startup', () => {
    const sm = new LightSm();
    sm.start();
    expect(globalThis.println.mock.calls).toHaveLength(1);
});

test('light is blue when turned on', () => {
    const sm = new LightSm();
    sm.start();
    sm.dispatchEvent(LightSm.EventId.INCREASE);
    expect(sm.stateId).toBe(LightSm.StateId.ON1);
    expect(globalThis.light_blue.mock.calls).toHaveLength(1);
});

test('light can be turned off', () => {
    const sm = new LightSm();
    sm.start();
    sm.dispatchEvent(LightSm.EventId.INCREASE);
    expect(sm.stateId).toBe(LightSm.StateId.ON1);

    sm.dispatchEvent(LightSm.EventId.DIM);
    expect(sm.stateId).toBe(LightSm.StateId.OFF);
});
