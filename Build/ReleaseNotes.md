# Summary
Update to Result

# New Features
- Limit Value and Errors visibility

# Bug Fixes
- None

# Comments
The Value and Errors properties were previously publicly visible and accessible.  This could lead to issues if accessing the properties directly.  The behavior was changed to force access through the Match method which will return one or the other as appropriate.