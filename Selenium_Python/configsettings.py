import os
import json

class configsettings:
    @staticmethod
    def _init_configuration():
        try:
            # Get the directory of the current script
            current_dir = os.path.dirname(os.path.abspath(__file__))
            # Construct the full path to the JSON file
            config_path = os.path.join(current_dir, "appsettings.test.json")

            # Open and read the JSON file
            with open(config_path, "r", encoding="utf-8-sig") as json_file:
                file_content = json_file.read()
                print("File content before JSON parsing:", file_content)  # Debug print
                config = json.loads(file_content)

            return config
        except FileNotFoundError as e:
            print(f"File not found: {e}")
            raise
        except json.JSONDecodeError as e:
            print(f"Error decoding JSON: {e}")
            raise

    @staticmethod
    def get_base_url():
        config = configsettings._init_configuration()
        return config.get("BaseUrl")

# Usage
try:
    base_url = configsettings.get_base_url()
    print("Base URL:", base_url)
except Exception as e:
    print(f"An error occurred: {e}")
