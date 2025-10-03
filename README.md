This repository is intended to be used as a toolkit. The source code and documentation herein are not intended to be used as a framework or end-to-end solution. The source code can be forked at an individual level and included in another project as an accelerator, but this repository intends to yield value by documenting different patterns and approaches with lightweight examples implemented using different agentic frameworks.

# Repository Overview
Within this repository, you will find the following guidance:
- **Patterns**
  These are the lowest level of guidance. They are framework-agnostic and represent agentic engineering patterns that can be used across any framework. These are the core building blocks that will be used together to build end-to-end agent solutions. For each pattern, you will find an overview of the pattern along with code samples as described in the [Pattern Directory Structure](#pattern-structure).

- **Recipes**
  Recipes are akin to a cooking recipe. They will leverage numerous different ingredients (patterns) and utensils (agentic frameworks) to create an entire dish (end-to-end agent example). With recipes, we will leverage specific platforms and frameworks (e.g., Semantic Kernel, Agent Framework, Copilot Studio). These recipes are there to provide larger exemplar implementations of patterns, but are not here to dictate the only optimal selection of frameworks and platforms that could be used.

# Patterns Table of Contents
The following patterns are captured currently in this repository:
- [**Tool Calling**](patterns/tool-calling/README.md): Tool Calling, also known as Function Calling, is a design pattern that enables agents to interact with external systems, APIs, or functions. This pattern allows agents to go beyond language-only reasoning by invoking external tools to perform specific tasks.

> [!WARNING]
> There are many more patterns than what are captured in this repository. Do not consider this an exhaustive list. We will capture as many as we can with the time we have.


# Directory Structure
Here is the structure for how patterns are captured within this repository:
```bash
.                      # Repository root
├── .github/
│   ├── chatmodes/     # Beneficial chat modes for VS Code
│   └── prompts/       # Reusable prompts for this repository
│
├── docs/              # Overall documentation (not specific patterns)
│   ├── templates/     # Documentation templates
│   │   └── pattern.md # General-purpose template to create pattern docs
│   └── ...
│
└── patterns/          # Root directory where all patterns are documented
│   ├── <pattern-1>/   # Each pattern is its own folder
│   ├── <pattern-2>/
│   └── <pattern-n>/   # Each pattern has its own structure (discussed below)
│
└── README.md          # Root README (you are here)
```

There are some repeating directory formats that will be covered in the subsections below:
- Pattern Folder

## Pattern Structure
As reflected within the overall [Directory Structure](#directory-structure), there are repeating folders for "patterns". These will all maintain their own structure as follows:
```bash
.\patterns\<pattern-name>
├── README.md              # Entry point for documentation for this pattern
├── docs/                  # Any additional documentation beyond README.md
└── examples/              # Different framework source code examples
    ├── <framework-1>/     # Framework 1 folder
    ├── semantic-kernel/   # Example folder for semantic kernel source code
    ├── langgraph/         # Example folder for langgraph source code
    └── <framework-n>/     # Additional frameworks
```

**Pattern Structure Further Explanation:**
- **pattern-name**
  This is a brief title for the pattern and should be 1–3 words (5 words max).
- **`README.md`**
  This is the entry point for documentation that is specific to the particular pattern being covered in the patterns folder. This README should follow the template defined within `/docs/templates/pattern.md`.
- **`examples/`**
  This folder will hold the exemplar implementations of the patterns. The folder will contain subfolders that are specific to the framework used for the exemplar (e.g., Semantic Kernel, LangGraph, LangChain).