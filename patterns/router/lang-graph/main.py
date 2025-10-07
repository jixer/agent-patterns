import sys
import os
from enum import Enum
from typing import Annotated, TypedDict
from langgraph.graph import START, END, StateGraph
from langchain.output_parsers import EnumOutputParser
from langchain_core.prompts import PromptTemplate
from langchain_openai import AzureChatOpenAI
from langchain_core.messages import BaseMessage



# Dynamically construct the absolute path to the shared directory
root_dir = os.path.dirname(os.path.abspath(__file__))
sys.path.append(os.path.join(root_dir, '../../../shared/python-infra'))
from config import AppSettingsFactory

# Load settings
settings = AppSettingsFactory.load_from_dotenv()

# Options for routing and state
class RoutingOptions(Enum):
    SIMPLE = "simple"
    ADVANCED = "advanced"

class State(TypedDict):
    user_query: str
    result: Annotated[BaseMessage, "result"]


# Nodes
def query_router(state: State) -> str:
    """Route the query based on its complexity."""
    parser = EnumOutputParser(enum=RoutingOptions)

    # TODO - Utilize create_structured_output_runnable to avoid hard coding valid outputs
    prompt = PromptTemplate(
        template="Determine if the user query requires a simple or advanced model. An 'advanced' request might require \
        multiple steps like retrieving an order ID and looking up shipping information, whereas a 'simple' request can \
        handle more straightforward queries. return either 'simple' or 'advanced'. Do not explain your reasoning Your \
        only task is to determine where to route the user query. \
        user_query: {input}",
        input_variables=["input"]
    )

    llm = AzureChatOpenAI(
        azure_endpoint=settings.AZURE_OPENAI_ENDPOINT,
        api_key=settings.AZURE_OPENAI_KEY,
        deployment_name=settings.AZURE_OPENAI_DEPLOYMENT_NAME,
        api_version=settings.AZURE_OPENAI_API_VERSION,
        temperature=0
    )

    routing_chain = prompt | llm | parser
    result = routing_chain.invoke({"input": state["user_query"]})
    return result.value

def handle_advanced(state: State):
    state['result'] = "Advanced handling of prompt: " + state['user_query']
    return state

def handle_simple(state: State):
    state['result'] = "Simple handling of prompt: " + state['user_query']
    return state

def end(state: State):
    """End the conversation."""
    print(f"Conversation ended with result -> {state['result']}")


# Add nodes to the graph
workflow = StateGraph(State)
workflow.add_node("handle_advanced", handle_advanced)
workflow.add_node("handle_simple", handle_simple)
workflow.add_node("end", end)

workflow.add_conditional_edges(START,
                               query_router,
                               {
                                   RoutingOptions.SIMPLE.value: "handle_simple",
                                   RoutingOptions.ADVANCED.value: "handle_advanced"
                               }
                               )

workflow.add_edge("handle_simple", "end")
workflow.add_edge("handle_advanced", "end")
workflow.add_edge("end", END)


# Compile the graph
graph = workflow.compile()

# Invoke the graph
simple_prompt = "What is the capital of France?"
state = State(user_query=simple_prompt, result=None)
graph.invoke(state)

complex_prompt = ("Analyze this dataset to identify the top three customer segments, explain why "
                  "they're valuable, and recommend one specific marketing action for each segment "
                  "within a $10,000 budget.")
state2 = State(user_query=complex_prompt, result=None)
graph.invoke(state2)