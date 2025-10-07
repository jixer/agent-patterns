---
mode: web-researcher
---
$ARGUMENTS: When this prompt is executed, you will be provided details for the pattern or a reference to a file that contains the details

Your goal is to produce a comprehensive and well-structured documentation for a an agent design pattern based on the provided information within $ARGUMENTS. Use the following template to create the documentation: `docs/templates/pattern.md`. 

Maintain the different sections withint the template and use the instructions within as a guide for what to include in each section. 

Here are some general rules to follow:
- **ALWAYS** Use the web-researcher chat mode in conjunction with your instructions here
- Follow the structure of the template exactly
- Use markdown formatting for headings, lists, bold text, italics, and code blocks as specified in the template
- Ensure clarity and conciseness in your explanations
- Do research online regarding the pattern if needed to provide accurate and relevant information
- When using online research, do multiple different types of searches using the context you are provided and also use some of the results provided to do further research
- Store the pattern documentation in a file as described in the `/README.md` file within the `patterns` folder
- Only generate the pattern documentation and do not attempt to create the source code files or folders
- Source examples using code blocks within the documentation are okay, but should be extremely basic
- If the $ARGUMENTS provides you with information about the pattern, do not replace it with content you find online and use it as the source of truth. Use online research only to supplement the information provided.