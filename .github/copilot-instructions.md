# Copilot Instructions for RimWorld Mod Project

Welcome to the RimWorld Mod Project! This document provides detailed guidelines and suggestions to help you contribute effectively using GitHub Copilot. The project extends RimWorld's gameplay by introducing new features and patches.

## Mod Overview and Purpose

This mod aims to enhance the RimWorld experience by adding new mechanics, improving existing systems, and integrating community feedback. It focuses on improving game immersion and providing players with fresh challenges while maintaining the balance and fun of the original game.

## Key Features and Systems

- **New Gameplay Mechanics:** The mod introduces several new mechanics that add depth to colony management and interaction with the environment.
- **Quality of Life Improvements:** Various enhancements aimed at streamlining player actions and improving game feedback.
- **Integration with Steam Workshop:** Features for easier upload and update of mods directly to Steam Workshop.

## Coding Patterns and Conventions

- **Naming Conventions:** Use PascalCase for class names and camelCase for method and variable names.
- **Commenting:** Write clear and concise comments for all classes and methods to explain their purpose and usage.
- **File Structure:** Organize files logically, keeping related classes and methods grouped to enhance readability and maintainability.
- **Exception Handling:** Implement try-catch blocks where necessary to ensure robust error handling, especially in code interfacing with external systems or performing file I/O operations.

## XML Integration

- **XML-Based Definitions:** RimWorld heavily relies on XML for defining game elements. Ensure XML files are well-formed and validated against the game's XML schema.
- **Mod Extensions:** Utilize XML to extend and modify existing game definitions. Be cautious of overwriting base game files; prefer patching or extending them instead.
- **Localization:** Use XML for localizing in-game texts to support multiple languages. Be mindful of XML syntax to avoid parsing errors.

## Harmony Patching

- **Introduction to Harmony:** Harmony is used for patching methods in RimWorld. It enables overriding or extending the functionality of existing game methods.
- **Creating Patches:** Define patches using attributes like `HarmonyPatch` and implement prefix, postfix, or transpiler methods to modify game behavior.
- **Compatibility Considerations:** Ensure patches are compatible with other mods by checking for method existence and using conditional patches where applicable.

## Suggestions for Copilot

To make the most out of GitHub Copilot, consider the following suggestions:

1. **Define clear function signatures:** Having descriptive method names and parameters helps Copilot suggest relevant code snippets.
2. **Utilize existing patterns:** When writing new patches or features, refer to existing patterns in the codebase for consistency.
3. **Optimize suggestions for XML:** When working with XML files, start by writing a base snippet, allowing Copilot to assist in completing structure and attributes.
4. **Incorporate localization suggestions:** Ensure to use keys appropriately in the XML for text, prompting Copilot to extrapolate multiple language supports.
5. **Streamline repetitive code:** Let Copilot help with boilerplate code, especially when creating classes or methods that adhere to standard patterns.

By following these guidelines, you can leverage GitHub Copilot to efficiently contribute high-quality code to the RimWorld Mod project. Happy modding!
