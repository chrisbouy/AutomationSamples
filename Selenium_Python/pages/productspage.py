from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.remote.webdriver import WebDriver
from selenium.webdriver.common.action_chains import ActionChains
from selenium.webdriver.common.keys import Keys
from selenium.common.exceptions import NoSuchElementException, TimeoutException

from helper import helper
from pages.basepage import basepage
class productspage(basepage):
    def __init__(self, driver: WebDriver):
        super().__init__(driver)

    def wait_for_page_to_load(self):
        # Example wait for an element that indicates the page is loaded
        WebDriverWait(self._driver, 20).until(
            EC.presence_of_element_located((By.XPATH, "//*[@data-test='continue']"))
        )

    @property
    def div_products(self):
        self.wait_for_page_to_load()
        return helper.find_elements_with_wait(self._driver, By.CSS_SELECTOR, ".inventory_item", 20)

    @property
    def div_cart_icon(self):
        self.wait_for_page_to_load()
        return helper.find_element_with_wait_clickable(self._driver, By.CLASS_NAME, "shopping_cart_link", 20)

    @property
    def div_cart_product_counter(self):
        self.wait_for_page_to_load()
        return self.div_cart_icon.find_elements(By.CLASS_NAME, "shopping_cart_badge") if self.div_cart_icon else []

    def goto_products_page(self):
        self.wait_for_page_to_load()
        self.goto("inventory.html")

    def add_to_cart(self, product: str):
        for item in self.div_products:
            product_name = item.find_element(By.CLASS_NAME, "inventory_item_name").text
            if product_name.lower() == product.lower():
                add_to_cart_button = item.find_element(By.CSS_SELECTOR, ".btn_inventory")
                add_to_cart_button.click()

    def add_all_to_cart(self, products: list):
        cnt = 0
        for p in products:
            for item in self.div_products:
                product_name = item.find_element(By.CLASS_NAME, "inventory_item_name").text
                if product_name.lower() == p.lower():
                    add_to_cart_button = item.find_element(By.CSS_SELECTOR, ".btn_inventory")
                    add_to_cart_button.click()
                    cnt += 1
        return cnt

    def remove_all_from_cart(self, products: list):
        for p in products:
            for item in self.div_products:
                product_name = item.find_element(By.CLASS_NAME, "inventory_item_name").text
                if product_name.lower() == p.lower():
                    remove_from_cart_button = item.find_element(By.CSS_SELECTOR, ".btn_inventory")
                    remove_from_cart_button.click()

    def get_number_of_items_in_cart(self):
        return int(self.div_cart_product_counter[0].text) if self.div_cart_product_counter else 0

    def inject_products_into_cart_with_javascript(self):
        self._driver.execute_script("window.sessionStorage.setItem('session-username', 'standard-user')")
        self._driver.execute_script("window.localStorage.setItem('cart-contents', '[4]')")
        self._driver.refresh()
