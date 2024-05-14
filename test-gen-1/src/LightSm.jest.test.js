// Example test file using jest. User-supplied, not generated

import {LightSm} from './LightSm.js';
import {jest} from '@jest/globals'; // We'll use jest in this example. see package.json for config details

// Mock the functions used by the state machine
// We recommend mocking rather than importing your actual functions,
// to keep these tests purely about testing the state machine itself.
// Your implementations should also be tested, but in separate tests.
globalThis.println = jest.fn();
globalThis.light_blue = jest.fn();
globalThis.light_yellow = jest.fn();
globalThis.light_red = jest.fn();

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
    // Arrange
    const sm = new LightSm();
    sm.start();

    // Act
    sm.dispatchEvent(LightSm.EventId.INCREASE);

    // Assert
    expect(sm.stateId).toBe(LightSm.StateId.ON1);
    expect(globalThis.light_blue.mock.calls).toHaveLength(1);
});

test('light can be turned off', () => {
    // Arrange
    const sm = new LightSm();
    sm.start();
    sm.dispatchEvent(LightSm.EventId.INCREASE);
    expect(sm.stateId).toBe(LightSm.StateId.ON1);

    // Act
    sm.dispatchEvent(LightSm.EventId.DIM);

    // Assert
    expect(sm.stateId).toBe(LightSm.StateId.OFF);
});

test('count how many times INCREASE is pressed in ON2', ()=>{
    // Arrange
    const sm = new LightSm();
    sm.start();
    sm.dispatchEvent(LightSm.EventId.INCREASE);
    sm.dispatchEvent(LightSm.EventId.INCREASE);
    expect(sm.stateId).toBe(LightSm.StateId.ON2);
    expect(sm.vars.count).toBe(0);

    // Act
    sm.dispatchEvent(LightSm.EventId.INCREASE);

    // Assert
    expect(sm.stateId).toBe(LightSm.StateId.ON2);
    expect(sm.vars.count).toBe(1);
})

test('increase until red', ()=>{
    // Arrange
    const sm = new LightSm();
    sm.start();
    sm.dispatchEvent(LightSm.EventId.INCREASE);
    sm.dispatchEvent(LightSm.EventId.INCREASE);
    sm.dispatchEvent(LightSm.EventId.INCREASE);
    sm.dispatchEvent(LightSm.EventId.INCREASE);
    expect(sm.stateId).toBe(LightSm.StateId.ON2);
    expect(sm.vars.count).toBe(2);

    // Act
    sm.dispatchEvent(LightSm.EventId.INCREASE);

    // Assert
    expect(sm.stateId).toBe(LightSm.StateId.ON_HOT);
    expect(sm.vars.count).toBe(3);
})

/// Notes:
/// - in JS we have access to the internals of the state machine so we don't need
///   to implement a special test api. For other languages we'll need a test api.