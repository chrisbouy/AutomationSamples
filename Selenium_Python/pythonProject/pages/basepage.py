from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.by import By
from selenium.webdriver.common.action_chains import ActionChains
from selenium.webdriver.common.keys import Keys
from configsettings import configsettings
class basepage:
    def __init__(self, driver):
        self._driver = driver
        self._base_url = configsettings.get_base_url()

    @property
    def url(self):
        try:
            return self._driver.current_url.lower()
        except Exception as ex:
            return ''

    @property
    def page_title(self):
        try:
            return self._driver.title.lower()
        except Exception as ex:
            return ''

    def goto(self, url, use_base_url=True):
        navigate_url = f"{self._base_url}/{url}" if use_base_url else url
        self._driver.get(navigate_url)

    def wait_for_js(self):
        wait = WebDriverWait(self._driver, 20)
        wait.until(lambda d: d.execute_script("return document.readyState == 'complete' && jQuery.active == 0"))
