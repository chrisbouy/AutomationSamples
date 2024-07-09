from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import NoSuchElementException

from helper import helper
from pages.basepage import basepage

class checkoutpage2(basepage):
    def __init__(self, driver):
        super().__init__(driver)
        self.but_finish = helper.find_element_with_wait_clickable(self._driver, By.XPATH, "//*[@data-test='finish']", 20)

    def confirm_order_info(self):
        self.but_finish.click()

    def goto_checkout_page2(self):
        self.goto("checkout-step-two.html")
