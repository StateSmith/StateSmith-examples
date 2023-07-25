# Assumptions
Assumes that you have gone through basic StateSmith tutorials already.

# User Post Processing
Open this directory with `vscode`.

This example project shows how you can post process StateSmith generated code to:
1. add [code coverage markers](https://github.com/StateSmith/StateSmith-examples/commit/64fd25a084bf9372b2a261b3e7ef3a9dbf70f536#diff-34edd0a700bbcbb77e0d970203cb190976f823d7546ac42b2b973620315c22cdR90-R101). Issue [#105](https://github.com/StateSmith/StateSmith/issues/105).
2. [remove the generated state_id_to_string function](https://github.com/StateSmith/StateSmith-examples/commit/64fd25a084bf9372b2a261b3e7ef3a9dbf70f536#diff-34edd0a700bbcbb77e0d970203cb190976f823d7546ac42b2b973620315c22cdL80-L90) (assuming you don't want it). Issue [#105](https://github.com/StateSmith/StateSmith/issues/105).
    * NOTE! There is an easier solution with a new setting: https://github.com/StateSmith/StateSmith/issues/181
3. generate [your own custom event_id_to_string function](https://github.com/StateSmith/StateSmith-examples/commit/64fd25a084bf9372b2a261b3e7ef3a9dbf70f536#diff-34edd0a700bbcbb77e0d970203cb190976f823d7546ac42b2b973620315c22cdR203-R213). Issue [#109](https://github.com/StateSmith/StateSmith/issues/109).

Have any other common needs or suggestions?
