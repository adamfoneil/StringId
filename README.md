[![Nuget](https://img.shields.io/nuget/v/AO.StringId)](https://www.nuget.org/packages/AO.StringId/)

Use this to create random string Ids of a specified length and combination of [character ranges](https://github.com/adamfoneil/StringId/blob/master/StringId/StringIdBuilder.cs#L10-L16). For example:

```csharp
var id = StringId.New(9, StringIdRanges.Upper | StringIdRanges.Numeric);
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
