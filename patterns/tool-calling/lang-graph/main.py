import os
from typing import Annotated
from langchain_core.tools import tool
from langchain_openai import AzureChatOpenAI
from langchain_core.messages import HumanMessage
from langgraph.prebuilt import create_react_agent
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

# Create the ReAct agent
agent = create_react_agent(llm, tools)

query = {
    "messages": ["What is 1 + 2?"]
}
response = agent.invoke(query)

print("Response:", response['messages'][-1].content)
print('Full chain:')
for i, message in enumerate(response['messages']):
    if message.type == 'ai' and message.tool_calls:
        print(f" {i+1}. {message.type}: {message.content} Tool call->{message.tool_calls[0]})")
    else:
        print(f" {i+1}. {message.type}: {message.content}")