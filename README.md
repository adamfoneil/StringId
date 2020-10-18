[![Nuget](https://img.shields.io/nuget/v/AO.StringId)](https://www.nuget.org/packages/AO.StringId/)

Use this to create random string Ids of a specified length and combination of [character ranges](https://github.com/adamfoneil/StringId/blob/master/StringId/StringIdBuilder.cs#L10-L16). For example:

```csharp
var id = StringId.New(9, StringIdRanges.Lower | StringIdRanges.Numeric);
```
Might produce `5m65urzga`

You can also chain several random strings together like this:

```csharp
string result = new StringIdBuilder()
                  .Add(4, StringIdRanges.Upper)
                  .Add("-")
                  .Add(4, StringIdRanges.Upper)
                  .Add("-")
                  .Add(4, StringIdRanges.Upper)
                  .Build();
```
Might produce `KRHD-OZBB-CVUU`

The [unit test](https://github.com/adamfoneil/StringId/blob/master/Testing/StringIdTests.cs#L11) for Id uniqueness achieves 1 million unique 9-character Ids. You can of course create Ids of any length.

The original idea for this came from a [blog post by Scott Lilly](https://scottlilly.com/create-better-random-numbers-in-c/), but I adapted it for creating random strings.
