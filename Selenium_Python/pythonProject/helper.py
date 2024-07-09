from selenium.common import NoSuchElementException
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait, Select
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.action_chains import ActionChains
from selenium.webdriver.common.keys import Keys
import os

class helper:

    @staticmethod
    def highlight_element(driver, element):
        js = driver.execute_script
        js("arguments[0].style.border='3px solid red'", element)

    @staticmethod
    def capture(driver, screenshot_name):
        local_path = ""
        try:
            screenshot = driver.get_screenshot_as_file(f"Defect_Screenshots/{screenshot_name}.png")
            local_path = os.path.abspath(f"Defect_Screenshots/{screenshot_name}.png")
        except Exception as e:
            print(f"Error capturing screenshot: {e}")
        return local_path

    @staticmethod
    def element_is_present(element):
        try:
            return element.is_displayed()
        except NoSuchElementException:
            return False

    @staticmethod
    def find_element_with_wait_clickable(driver, by, value, timeout):
        try:
            wait = WebDriverWait(driver, timeout)
            return wait.until(EC.element_to_be_clickable((by, value)))
        except NoSuchElementException:
            return None

    @staticmethod
    def find_element_with_wait_viewable(driver, by, value, timeout):
        try:
            wait = WebDriverWait(driver, timeout)
            return wait.until(EC.visibility_of_element_located((by, value)))
        except NoSuchElementException:
            return None

    @staticmethod
    def find_element_with_wait_exists(driver, by, value, timeout):
        try:
            wait = WebDriverWait(driver, timeout)
            return wait.until(EC.presence_of_element_located((by, value)))
        except:
            return None

    @staticmethod
    def find_element(driver, by, value):
        try:
            return driver.find_element(by, value)
        except:
            return None

    @staticmethod
    def find_elements(driver, by, value):
        try:
            return driver.find_elements(by, value)
        except:
            return None

    @staticmethod
    def find_elements_with_wait(driver, by, value, timeout):
        try:
            wait = WebDriverWait(driver, timeout)
            return wait.until(EC.visibility_of_all_elements_located((by, value)))
        except:
            return None

    @staticmethod
    def get_element_value(element):
        if helper.element_is_present(element):
            return element.get_attribute("value")
        return ""

    @staticmethod
    def get_select_element_value(select_element):
        return helper.get_element_value(select_element.first_selected_option)
