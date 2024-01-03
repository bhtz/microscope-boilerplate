# Code language ADR (sample)
- 📅 Date: 2024/01/11
- 👷 Decision made by: Product Owner, Developers

## Context
Business objects have French names, but the execution team speaks English.
The question is which language to use in our codebase.

## Considered Options 💡

French
✅ Pro: We use the same words as the business team, making code reviews with them easier.
🚫 Con: The execution team does not understand it.

English
✅ Pro: The execution team will understand it.
🚫 Con: The business team will have to translate, and we might need to think a bit more while coding.

## Decision 🏆
We choose English because it is easier to translate from English to French when you know both languages than from French to English when you don't know French.

## Consequence
We decide to implement a translation dictionary with codes to aid the translation process.
The template used for this example can be found on GitHub, and examples of more complex decisions are also provided.