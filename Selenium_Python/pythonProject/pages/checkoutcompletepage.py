from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from helper import helper
from pages.basepage import basepage
class checkoutcompletepage(basepage):
    def __init__(self, driver):
        super().__init__(driver)

    @property
    def lbl_message(self):
        return self.find_element_with_wait(By.CLASS_NAME, "complete-text", 20)

    def get_message(self):
        return self.lbl_message.text

    def find_element_with_wait(self, by, value, timeout):
        wait = WebDriverWait(self._driver, timeout)
        return wait.until(EC.visibility_of_element_located((by, value)))
