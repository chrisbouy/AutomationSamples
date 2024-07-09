from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import NoSuchElementException

from helper import helper
from pages.basepage import basepage

class checkoutpage1(basepage):
    def __init__(self, driver):
        super().__init__(driver)
        self.input_elements = helper.find_elements_with_wait(self._driver, By.CLASS_NAME, "form_input", 20)
        self.but_continue = helper.find_element_with_wait_clickable(self._driver, By.XPATH, "//*[@data-test='continue']", 20)

    def enter_info(self, f, l, zip_code):
        input_values = [f, l, zip_code]
        for i in range(min(len(self.input_elements), len(input_values))):
            self.input_elements[i].send_keys(input_values[i])
        self.but_continue.click()

    def goto_checkout_page1(self):
        self.goto("checkout-step-one.html")
