from dotenv import load_dotenv
import os

class AppSettings:
    AZURE_OPENAI_ENDPOINT: str
    AZURE_OPENAI_DEPLOYMENT_NAME: str
    AZURE_OPENAI_KEY: str
    AZURE_OPENAI_API_VERSION: str

    def __init__(self, endpoint: str, deployment_name: str, key: str, api_version: str):
        self.AZURE_OPENAI_ENDPOINT = endpoint
        self.AZURE_OPENAI_DEPLOYMENT_NAME = deployment_name
        self.AZURE_OPENAI_KEY = key
        self.AZURE_OPENAI_API_VERSION = api_version

class AppSettingsFactory:
    @staticmethod
    def load_from_dotenv(path = "../../.env"):
        load_dotenv(path)

        return AppSettings(
            endpoint=os.getenv("AZURE_OPENAI_ENDPOINT"),
            deployment_name=os.getenv("AZURE_OPENAI_DEPLOYMENT_NAME"),
            key=os.getenv("AZURE_OPENAI_KEY"),
            api_version=os.getenv("AZURE_OPENAI_API_VERSION"),
        )