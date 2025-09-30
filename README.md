

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
│   │   └── patern.md  # General purpose template to create pattern docs
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
As reflected within the overall [Directory Structure](#directory-structure), there are repeating folders for "patterns". These will all maintain there own structure as follows:
```bash
.\patterns\<pattern-name>
├── README.md              # Entry point for documentation for this pattern
├── docs/                  # Any additional documentation beyond README.md
└── src/                   # Different framework source code examples
    ├── <framework-1>/     # Framework 1 folder
    ├── semantic-kernel/   # Example folder for semantic kernel source code
    ├── langgraph/         # Example folder for langgraph source code
    └── <framework-n>/     # Additional frameworks
```

**Pattern Structure Further Explanation:**
- **pattern-name**
  This is a brief title for the pattern and should be 1 - 3 words (5 words max)
- **`README.md`**
  This is the entry point for documentation that is specific to the particular pattern being covered in the patterns folder. This README should follow the template defined within `/docs/templates/pattern.md`
- **`src/`**
  This folder will hold the exemplar implementations of the patterns. The folder will contain subfolders that are specific the framework used for the exemplar (e.g., Semantic Kernel, LangGraph, LangChain)