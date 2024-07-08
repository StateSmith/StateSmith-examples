This example illustrates a new feature that reports the location of an XML error in a draw.io file.
See https://github.com/StateSmith/StateSmith/issues/353

The `LightSm.drawio` file purposely contains an mxCell at line 35 without an id attribute (we renamed it to `notAnIdAttribute="75"`).

```
Running script: `LightSm.csx`
Exception DrawIoException : failed getting attribute `id` from xml element `mxCell`.
Line contents: `                <mxCell notAnIdAttribute="75" value="OFF" style="edgeStyle=none;html=1;exitX=0.5;exi` (trimmed to 100 chars)
Location (usually relative to parent <mxGraphModel>): line 33, column 18.
HELP INFO: https://github.com/StateSmith/StateSmith/issues/354

Get exception detail with 'propagate exceptions' or 'dump errors to file' settings.
```