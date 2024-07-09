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

class testbase(unittest.TestCase):
    def setUp(self):
       #  self.driver = webdriver.Chrome(ChromeDriverManager().install())
       # driver.implicitly_wait(10)
       #  self.driver.maximize_window()

        options = Options()
        options.add_argument("--start-maximized")  # Example option
        service = Service(ChromeDriverManager().install())
        self.driver = webdriver.Chrome(service=service, options=options)
        self.driver.implicitly_wait(20)
    def tearDown(self):
        self.driver.quit()



if __name__ == "__main__":
    unittest.main()
