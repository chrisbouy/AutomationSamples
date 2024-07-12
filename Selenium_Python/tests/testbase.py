import json
import unittest
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.common.action_chains import ActionChains
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.keys import Keys
from webdriver_manager.chrome import ChromeDriverManager
from helper import helper
from pages.basepage import basepage
from pages.productspage import productspage
from pages.checkoutpage1 import checkoutpage1
from pages.checkoutpage2 import checkoutpage2
from pages.checkoutcompletepage import checkoutcompletepage
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.chrome.options import Options
from configsettings import configsettings

class testbase(unittest.TestCase):
    def setUp(self):
        # with open("../appsettings.test.json", "r") as f:
        #     self.config = json.load(f)

        options = Options()
        options.add_argument("--start-maximized")
        service = Service(ChromeDriverManager().install())
        self.driver = webdriver.Chrome(service=service, options=options)
        self.driver.implicitly_wait(10)  # Implicit wait

        self.config = configsettings._init_configuration()
        self.base_url = self.config["BaseUrl"]
        print(f"Base URL in setUp: {self.base_url}")

        if not self.base_url.startswith("http"):
            raise ValueError(f"Invalid URL format: {self.base_url}")

        self.driver.get(self.base_url)
    def tearDown(self):
        self.driver.quit()



if __name__ == "__main__":
    unittest.main()
