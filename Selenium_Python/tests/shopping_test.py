import unittest
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import TimeoutException
from selenium.webdriver.chrome.service import Service as ChromeService
from selenium import webdriver
from webdriver_manager.chrome import ChromeDriverManager
from selenium.webdriver.common.action_chains import ActionChains
from selenium.webdriver.common.keys import Keys

from helper import helper
from pages.basepage import basepage
from pages.productspage import productspage
from pages.homepage import homepage
from pages.checkoutpage1 import checkoutpage1
from pages.checkoutpage2 import checkoutpage2
from pages.checkoutcompletepage import checkoutcompletepage
from tests.testbase import testbase

class shoppingtests(testbase):
    def setUp(self):
        super().setUp()
        self._productspage = productspage(self.driver)
        self._homepage = homepage(self.driver)
        self._checkoutpage1 = checkoutpage1(self.driver)
        self._checkoutpage2 = checkoutpage2(self.driver)
        self._checkoutcompletepage = checkoutcompletepage(self.driver)

    def _products(self):
        return [
            ["Onesie", "Backpack"],
            ["Onesie"]
        ]

    def test_add_products(self):
        for products in self._products():
            with self.subTest(products=products):
                self._homepage.bypass_login_with_cookie()
                self._productspage.goto_products_page()
                self._productspage.add_all_to_cart(products)
                # assert all products added
                self.assertEqual(self._productspage.get_number_of_items_in_cart(), len(products))
                # remove products to reset state
                self._productspage.remove_all_from_cart(products)

    def test_enter_checkout_info_happy_path(self):
        self._homepage.bypass_login_with_cookie()
        self._productspage.inject_products_into_cart_with_javascript()
        
        # print("Current URL:", self.driver.current_url)
        # print("Page Source:", self.driver.page_source[:500])  # Print first 500 characters for brevity
        
        # # Verify that the correct page is loaded
        # self.assertIn("inventory", self.driver.current_url, "Not on inventory page")

        self._checkoutpage1.goto_checkout_page1()
        self._checkoutpage1.enter_info("Chris", "Bouy", "12345")
        # assert next page is loaded
        self.assertIn("checkout-step-two.html", self.driver.current_url)

    def test_confirm_checkout_info_happy_path(self):
        self._homepage.bypass_login_with_cookie()
        self._productspage.inject_products_into_cart_with_javascript()
        self._checkoutpage2.goto_checkout_page2()
        # todo: assert everything looks good
        self._checkoutpage2.confirm_order_info()
        # assert success message
        self.assertIn("Your order has been dispatched", self._checkoutcompletepage.get_message())

if __name__ == "__main__":
    unittest.main()
