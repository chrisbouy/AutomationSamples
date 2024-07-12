from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.action_chains import ActionChains
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.chrome.options import Options
#from selenium.webdriver.common.cookie import Cookie
from helper import helper
from pages.basepage import basepage

import time

class homepage(basepage):
    def __init__(self, driver):
        super().__init__(driver)

    @property
    def TB_name(self):
        return helper.find_element_with_wait_clickable(self._driver, By.NAME, "username", 10)

    @property
    def TB_pw(self):
        return helper.find_element_with_wait_clickable(self._driver, By.NAME, "password", 10)

    def goto_home_page(self):
        self.goto("")
        time.sleep(3)

    def login(self, name, pw):
        self.TB_name.send_keys(name)
        self.TB_pw.send_keys(pw)
        self.TB_pw.send_keys(Keys.RETURN)

    def bypass_login_with_cookie(self):
        cookie = {'name': 'session-username', 'value': 'standard_user'}
        self._driver.add_cookie(cookie)

