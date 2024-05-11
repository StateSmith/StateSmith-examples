// Example test file using jest. User-supplied, not generated

import {LightSm} from './LightSm.js';
import {jest} from '@jest/globals'; // We'll use jest in this example. see package.json for config details

// Mock the functions used by the state machine
globalThis.println = jest.fn();

test('adds 1 + 2 to equal 3', () => {
    const sm = new LightSm();
    sm.start();
//   expect(sum(1, 2)).toBe(3);
});

