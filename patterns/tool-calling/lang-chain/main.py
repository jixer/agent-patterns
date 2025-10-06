import os
from typing import Annotated
from langchain_core.tools import tool
from langchain_openai import AzureChatOpenAI
from langchain_core.messages import HumanMessage, ToolMessage
from dotenv import load_dotenv
load_dotenv()


# Define custom tools using the @tool decorator
@tool
def add(
    x: Annotated[float, "The first number"],
    y: Annotated[float, "The second number"]
) -> float:
    """Add two numbers."""
    return x + y


# Get Azure OpenAI configuration from environment variables
azure_endpoint = os.getenv("AZURE_OPENAI_ENDPOINT")
api_key = os.getenv("AZURE_OPENAI_KEY")
deployment_name = os.getenv("AZURE_OPENAI_DEPLOYMENT_NAME", "gpt-4")
api_version = os.getenv("AZURE_OPENAI_API_VERSION", "2024-02-15-preview")

# Initialize the Azure OpenAI model
llm = AzureChatOpenAI(
    azure_endpoint=azure_endpoint,
    api_key=api_key,
    deployment_name=deployment_name,
    api_version=api_version,
    temperature=0
)

# Create a list of tools
tools = [add]

# Bind tools to the model
llm_with_tools = llm.bind_tools(tools)

query = "What is 1 + 2?"
response = llm_with_tools.invoke([HumanMessage(content=query)])

print("\nIn this example, the tool call is not invoked directory so you will "
      "either get the Tool call response back from the LLM or a message if "
      "it didn't find a relevant tool")
if response.tool_calls:
    print(f"\nTool call response:")
    for tool_call in response.tool_calls:
        print(f"  - Tool: {tool_call['name']}")
        print(f"    Arguments: {tool_call['args']}")
else:
    print(f"AI Response: {response.content}")