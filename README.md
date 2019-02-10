# LanguageLookup
An easy, reflection based solution to the problem of using different strings for different languages.

## The Problem

Using a different string per language is complicated. You have a few options:

- Having a config file for languages
- Using a database
- Resource files
- Include file full of strings
- Glorified dictionary

Or, what this library proposes: an interface.

## The solution

Whilst not *entirely* ideal (yet), LanguageLookup provides a way to access a variety of strings in an extremely clean way.

```cs
public interface ILang
{
    [Value("Hello!", "english")]
    [Value("¡Hola!", "spanish")]
    [Value("こんにちは！", "japanese")]
    string Hello { get; }
}

ILoader<ILang> loader = new DefaultLoader<ILang>();
ILang english = loader.Load("english");

Console.WriteLine(english.Hello);
```
This library is extensible so you could easily add JSON support for a language without having to specify every language in the attributes.

## Examples

[Here](./src/LanguageLookup.Example/Examples)